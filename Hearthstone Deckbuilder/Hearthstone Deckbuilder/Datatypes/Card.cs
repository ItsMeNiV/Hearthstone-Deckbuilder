namespace Hearthstone_Deckbuilder.NSDatatypes
{
    public class Card
    {

        public Card(string cardId, string cardToDeckId)
        {
            this.cardId = cardId;
            this.cardToDeckId = cardToDeckId;
        }

        public Card(string cardId)
        {
            this.cardId = cardId;
        }

        public string cardId { get; set; }
        public string cardName { get; set; }
        public int manaCost { get; set; }
        public string cardText { get; set; }
        public string cardToDeckId { get; set; }

    }
}
