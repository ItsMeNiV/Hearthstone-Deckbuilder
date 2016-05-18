using Hearthstone_Deckbuilder.Database.NSDeckDatabase.Controller;
using Hearthstone_Deckbuilder.NSDatatypes;
using Hearthstone_Deckbuilder.NSGlobalVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearthstone_Deckbuilder.UserInterface.NSDeckCreatorWindow.Controller
{
    class DeckCreatorWindowController
    {

        DeckDatabaseController deckDatabaseController;

        public DeckCreatorWindowController()
        {
            deckDatabaseController = new DeckDatabaseController();
        }

    }
}
