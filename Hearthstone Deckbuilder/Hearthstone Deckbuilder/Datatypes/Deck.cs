using System.Collections.Generic;
namespace Hearthstone_Deckbuilder.NSDatatypes
{
    public class Deck
    {

        public Deck()
        {
            cardList = new List<Card>();
            deckUser = new User();
        }

        public Deck(string deckId, string deckName, User deckUser)
        {
            this.deckId = deckId;
            this.deckName = deckName;
            this.deckUser = deckUser;
            cardList = new List<Card>();
            deckUser = new User();
        }

        public List<Card> cardList { get; set; }
        public string deckName { get; set; }
        public User deckUser { get; set; }
        public string deckId { get; set; }

    }
}
