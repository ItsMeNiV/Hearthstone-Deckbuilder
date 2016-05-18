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

        public void openDeckCreatorWindow()
        {
            DeckCreatorWindow deckCreatorWindow = new DeckCreatorWindow();
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

        public void applyFilters(string className, string typeName, string rarityName, int manaCost, int attackValue, int healthValue, int durabilityValue)
        {
            List<Card> newList = new List<Card>();
            foreach(Card card in currentCardList)
            {
                bool remove = false;
                if(!className.Equals("") && !className.Equals(card.CardClass))
                {
                    remove = true;
                }
                else if (!typeName.Equals("") && !typeName.Equals(card.CardClass))
                {
                    remove = true;
                }
                else if (!rarityName.Equals("") && !rarityName.Equals(card.CardClass))
                {
                    remove = true;
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

    }
}
