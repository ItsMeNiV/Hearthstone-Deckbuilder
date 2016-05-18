using System.Collections.Generic;

namespace Hearthstone_Deckbuilder.NSDatatypes
{
    public class Deck
    {
        public Deck()
        {
            CardList = new List<Card>();
            DeckUser = new User();
        }

        public Deck(string deckId, string deckName, User deckUser, string deckClass)
        {
            CardList = new List<Card>();
            DeckId = deckId;
            DeckName = deckName;
            DeckUser = deckUser;
            DeckClass = deckClass;
        }

        public List<Card> CardList { get; set; }

        public string DeckId { get; set; }

        public string DeckName { get; set; }

        public User DeckUser { get; set; }

        public string DeckClass { get; set; }
    }
}
