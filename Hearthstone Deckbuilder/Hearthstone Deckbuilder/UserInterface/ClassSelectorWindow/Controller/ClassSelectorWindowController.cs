using Hearthstone_Deckbuilder.NSDatatypes;
using Hearthstone_Deckbuilder.NSGlobalVariables;
using Hearthstone_Deckbuilder.UserInterface.NSDeckCreatorWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone_Deckbuilder.UserInterface.ClassSelectorWindow.Controller
{
    class ClassSelectorWindowController
    {

        public ClassSelectorWindowController()
        {

        }

        public void openDeckCreatorAndCloseClassSelector(ClassSelectorWindow window, string selectedClass)
        {
            Deck deck = new Deck();
            deck.DeckClass = selectedClass;
            deck.DeckName = "New Deck";
            deck.DeckUser = GlobalVariables.LoggedInUser;
            DeckCreatorWindow deckCreatorWindow = new DeckCreatorWindow(deck);
            deckCreatorWindow.Show();
            window.Close();
        }

    }
}
