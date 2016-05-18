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
            DeckCreatorWindow deckCreatorWindow = new DeckCreatorWindow();
            deckCreatorWindow.
            deckCreatorWindow.Show();
            window.Close();
        }

    }
}
