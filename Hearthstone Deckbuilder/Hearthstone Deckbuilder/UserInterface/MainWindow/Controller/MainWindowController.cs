using Hearthstone_Deckbuilder.Database.NSCardDatabase.Controller;
using Hearthstone_Deckbuilder.Database.NSDeckDatabase.Controller;
using Hearthstone_Deckbuilder.NSDatatypes;
using Hearthstone_Deckbuilder.NSGlobalVariables;
using Hearthstone_Deckbuilder.UserInterface.NSDeckCreatorWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void applyFilters()
        {

        }

        public void resetFilters()
        {
            currentCardList = cardDatabaseController.GetAllCards();
        }

        

    }
}
