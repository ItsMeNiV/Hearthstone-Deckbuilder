using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearthstone_Deckbuilder.Database.NSDatabaseConnector;
using Hearthstone_Deckbuilder.NSDatatypes;
using Npgsql;

namespace Hearthstone_Deckbuilder.Database.NSCardDatabase.Controller
{
    class CardDatabaseController
    {

        DatabaseConnectionHandler _dc;

        public CardDatabaseController()
        {
            _dc = new DatabaseConnectionHandler();
        }

        public Card getCardById(string cardId)
        {
            Card returnCard = new Card(cardId);
            return returnCard;
        }

    }
}
