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

        public Card(string cardId, string cardName, int manaCost, string cardText, int attack, int health, string cardType, string rarity, string cardClass, int durability)
        {
            CardId = cardId;
            CardName = cardName;
            ManaCost = manaCost;
            CardText = cardText;
            Attack = attack;
            Health = health;
            CardType = cardType;
            Rarity = rarity;
            CardClass = cardClass;
            Durability = durability;
        }

        public string CardId { get; set; }

        public string CardName { get; set; }

        public int ManaCost { get; set; }

        public string CardText { get; set; }

        public int Attack { get; set; }

        public int Health { get; set; }

        public string CardType { get; set; }

        public string Rarity { get; set; }

        public string CardClass { get; set; }

        public int Durability { get; set; }

        public string ImgLink { get; set; }

        public string CardToDeckId { get; set; }
    }
}
