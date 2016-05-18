namespace Hearthstone_Deckbuilder.NSDatatypes
{
    public class Card
    {
        public Card(string cardId, string cardToDeckId)
        {
            CardId = cardId;
            CardToDeckId = cardToDeckId;
        }

        public Card(string cardId)
        {
            CardId = cardId;
        }

        public string CardId { get; set; }

        public string CardName { get; set; }

        public int ManaCost { get; set; }

        public string CardText { get; set; }

        public string CardToDeckId { get; set; }
    }
}
