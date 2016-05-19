using Hearthstone_Deckbuilder.Database.NSCardDatabase.Controller;
using Hearthstone_Deckbuilder.Database.NSDeckDatabase.Controller;
using Hearthstone_Deckbuilder.NSDatatypes;
using Hearthstone_Deckbuilder.NSGlobalVariables;
using Hearthstone_Deckbuilder.UserInterface.NSDeckCreatorWindow;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hearthstone_Deckbuilder.UserInterface.NSMainWindow.Controller
{
    class MainWindowController
    {

        private DeckDatabaseController deckDatabaseController;
        private CardDatabaseController cardDatabaseController;
        private List<Card> currentCardList;

        public MainWindowController()
        {
            deckDatabaseController = new DeckDatabaseController();
            cardDatabaseController = new CardDatabaseController();
            currentCardList = cardDatabaseController.GetAllCards();
        }

        public void openDeckCreatorWindow(Deck deck)
        {
            DeckCreatorWindow deckCreatorWindow = new DeckCreatorWindow(deck);
            deckCreatorWindow.Show();
        }

        public void openSettingsWindow()
        {

        }

        public List<Deck> getDecksOfLoggedInUser()
        {
            return deckDatabaseController.GetAllDecksByUser(GlobalVariables.LoggedInUser);
        }

        public List<Card> getCurrentCardList()
        {
            return currentCardList;
        }

        public void applyFilters(string className, string typeName, string rarityName, string manaCost, string attackValue, string healthValue, string durabilityValue)
        {
            int manaCostInt = 0;
            int attackValueInt = 0;
            int healthValueInt = 0;
            int durabilityValueInt = 0;
            if (!manaCost.Equals("None"))
            {
                if (manaCost.Equals("8+"))
                {
                    manaCostInt = 100;
                }
                else
                {
                    manaCostInt = Convert.ToInt32(manaCost);
                }
            }
            if (!attackValue.Equals("None"))
            {
                if (attackValue.Equals("8+"))
                {
                    attackValueInt = 100;
                }
                else
                {
                    attackValueInt = Convert.ToInt32(attackValue);
                }
            }
            if (!healthValue.Equals("None"))
            {
                if (healthValue.Equals("8+"))
                {
                    healthValueInt = 100;
                }
                else
                {
                    healthValueInt = Convert.ToInt32(healthValue);
                }
            }
            if (!durabilityValue.Equals("None"))
            {
                if (durabilityValue.Equals("5+"))
                {
                    durabilityValueInt = 100;
                }
                else
                {
                    durabilityValueInt = Convert.ToInt32(durabilityValue);
                }
            }
            List<Card> newList = new List<Card>();
            foreach(Card card in currentCardList)
            {
                bool remove = false;
                if (!className.Equals("None") && !className.ToLower().Equals(card.CardClass))
                {
                    remove = true;
                }
                if (!typeName.Equals("None") && !typeName.ToLower().Equals(card.CardType))
                {
                    remove = true;
                }
                if (!rarityName.Equals("None") && !rarityName.ToLower().Equals(card.Rarity))
                {
                    remove = true;
                }
                if (!manaCost.Equals("None") && manaCostInt != card.ManaCost)
                {
                    if (!(card.ManaCost >= 8))
                    {
                        remove = true;
                    }
                }
                if (!attackValue.Equals("None") && attackValueInt != card.Attack)
                {
                    if (!(card.Attack >= 8))
                    {
                        remove = true;
                    }
                }
                if (!healthValue.Equals("None") && healthValueInt != card.Health)
                {
                    if (!(card.Health >= 8))
                    {
                        remove = true;
                    }
                }
                if (!durabilityValue.Equals("None") && durabilityValueInt != card.Durability)
                {
                    if (!(card.Durability >= 5))
                    {
                        remove = true;
                    }
                }
                if (!remove)
                {
                    newList.Add(card);
                }
            }
            currentCardList = newList;
        }

        public void resetFilters()
        {
            currentCardList = cardDatabaseController.GetAllCards();
        }

        public void searchCards(string searchText)
        {
            List<Card> newList = new List<Card>();
            foreach (Card card in currentCardList)
            {
                if (card.CardName.ToLower().Contains(searchText))
                {
                    newList.Add(card);
                }
            }
            currentCardList = newList;
        }

        public Card getCardByName(string cardName)
        {
            return cardDatabaseController.GetCardByName(cardName);
        }

        public Card getCardById(string cardId)
        {
            return cardDatabaseController.getCardById(cardId);
        }

    }
}
