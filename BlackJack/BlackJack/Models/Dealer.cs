using System.Text.Json.Serialization;

namespace BlackJack.Models
{
    public class Dealer : Player
    {
        // don't store read-only properties in session
        [JsonIgnore]
        public bool MustHit => Hand.Total < 17 || Hand.HasSoftSeventeen;

        [JsonIgnore]
        public bool MustShowCards => Hand.HasCards && !Hand.HideHoleCard;

        public void ShowCards() => Hand.HideHoleCard = false;
    }
}
