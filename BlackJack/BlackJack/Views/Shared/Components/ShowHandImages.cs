using BlackJack.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlackJack.Views.Shared.Components
{
    public class ShowHandImages : ViewComponent
    {
        public IViewComponentResult Invoke(Hand hand)
        {
            return View("~/Views/Shared/_ShowHandImagesPartial.cshtml", hand);
        }
    }
}
