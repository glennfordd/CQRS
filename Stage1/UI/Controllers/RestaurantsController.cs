using Microsoft.AspNetCore.Mvc;
using Stage1.Application;
using Stage1.Data;
using Stage1.UI.Models;
using System.Threading.Tasks;

namespace Stage1.UI.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly RestaurantsService _restaurantsService;
        public RestaurantsController(RestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;
        }

        public IActionResult Index()
        {
            var restaurants = _restaurantsService.GetAll();
            
            return View(restaurants);
        }

        public IActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRestaurant(AddRestaurantViewModel model)
        {
            await _restaurantsService.AddRestaurant(model.Name);

            return RedirectToAction(nameof(Index));
        }
    }
}
