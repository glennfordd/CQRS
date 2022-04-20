using Microsoft.AspNetCore.Mvc;

namespace Stage2.Features.Guests
{
    public class GuestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
