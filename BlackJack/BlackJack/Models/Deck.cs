namespace BlackJack.Models
{
    public class Deck
    {
        private string[] ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        private string[] suits = { "C", "D", "H", "S" };

        public List<Card> Cards { get; set; } = null!;

        public void Shuffle()
        {
            Cards = new List<Card>();
            foreach (string r in ranks) {
                foreach (string s in suits) {
                    Cards.Add(new Card { Rank = r, Suit = s });
                }
            }
        }

        public Card Deal()
        {
            // get a card at random
            var random = new Random();
            int index = random.Next(Cards.Count);

            // retrieve card and then remove it from the deck
            var card = Cards[index];
            Cards.Remove(card);

            return card;
        }
    }
}
