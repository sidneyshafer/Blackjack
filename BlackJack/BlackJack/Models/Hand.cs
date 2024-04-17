using System.Text.Json.Serialization;

namespace BlackJack.Models
{
    public class Hand 
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public bool HideHoleCard { get; set; }

        public void Add(Card card) => Cards.Add(card);

        // don't store read-only properties in session
        [JsonIgnore]
        public int Total {
            get {
                int total = Cards.Sum(c => c.Value);
                int numberOfAces = Cards.Where(c => c.IsAce).Count();

                // check aces - count as 1 instead of 11 as needed
                for (int i = 0; i < numberOfAces; i++) {
                    if (total > 21) {
                        total -= 10;
                    }
                }

                return total;
            }
        }

        [JsonIgnore]
        public int Count => Cards.Count;

        [JsonIgnore]
        public bool HasCards => Cards.Count > 0;

        [JsonIgnore]
        public bool HasAce => Cards.FirstOrDefault(c => c.IsAce) != null;

        [JsonIgnore]
        public bool HasBlackJack => Cards.Count == 2 && Total == 21;

        [JsonIgnore]
        public bool HasSoftSeventeen => Cards.Count == 2 && Total == 17 && HasAce;

        [JsonIgnore]
        public bool IsBusted => Total > 21;
    }
}
