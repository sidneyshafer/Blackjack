using BlackJack.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlackJack.Views.Shared.Components
{
    public class CurrentWinnings : ViewComponent
    {
        private IGame game { get; set; }
        public CurrentWinnings(IGame g) => game = g;

        public IViewComponentResult Invoke() => View("~/Views/Shared/_CurrentWinningsPartial.cshtml", game);
    }
}
