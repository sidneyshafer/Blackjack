namespace BlackJack.Models
{
    public interface IGame
    {
        Player Player { get; set; }
        Dealer Dealer { get; set; }

        Game.Result Deal();
        Game.Result Hit();
        Game.Result Stand();
    }
}