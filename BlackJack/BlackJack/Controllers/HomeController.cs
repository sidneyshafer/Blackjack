using Microsoft.AspNetCore.Mvc;
using BlackJack.Models;

namespace BlackJack.Controllers
{
    public class HomeController : Controller
    {
        private IGame game { get; set; }
        public HomeController(IGame g) => game = g;

        public IActionResult Index() => View(game);

        [HttpPost("Deal")]
        public IActionResult Deal()
        {
            var result = game.Deal();

            if (result == Game.Result.Shuffling)
            {
                TempData["message"] = "Shuffling. Press Deal to continue.";
                TempData["background"] = "info";
            }
            else if (result == Game.Result.PlayerBlackJack)
            {
                TempData["message"] = "Blackjack! You win!";
                TempData["background"] = "success";
            }
            else if (result == Game.Result.DealerBlackJack)
            {
                TempData["message"] = "Dang! Dealer got a Blackjack! You lose.";
                TempData["background"] = "danger";
            }
            else if (result == Game.Result.DoubleBlackJack)
            {
                TempData["message"] = "Push";
                TempData["background"] = "info";
            }

            return RedirectToAction("Index");

        }

        [HttpPost("Hit")]
        public IActionResult Hit()
        {
            var result = game.Hit();

            if (result == Game.Result.Shuffling)
            {
                TempData["message"] = "Shuffling. Press Hit to continue.";
                TempData["background"] = "info";
            }
            else if (result == Game.Result.PlayerBust)
            {
                TempData["message"] = "BUST! You lose.";
                TempData["background"] = "danger";
            }

            return RedirectToAction("Index");
        }

        [HttpPost("Stand")]
        public IActionResult Stand()
        {
            var result = game.Stand();

            if (result == Game.Result.Shuffling)
            {
                TempData["message"] = "Shuffling. Press Hit to continue.";
                TempData["background"] = "info";
            }
            else if (result == Game.Result.Continue)
            {
                TempData["message"] = "Dealer needs another card. Hit Stand to continue.";
                TempData["background"] = "info";
            }
            else if (result == Game.Result.DealerBust)
            {
                TempData["message"] = "Dealer BUST! You win!";
                TempData["background"] = "success";
            }
            else if (result == Game.Result.DealerWin)
            {
                TempData["message"] = "You lose.";
                TempData["background"] = "danger";
            }
            else if (result == Game.Result.PlayerWin)
            {
                TempData["message"] = "You win!";
                TempData["background"] = "success";
            }
            else if (result == Game.Result.Push)
            {
                TempData["message"] = "PUSH";
                TempData["background"] = "info";
            }

            return RedirectToAction("Index");

        }
    }
}
