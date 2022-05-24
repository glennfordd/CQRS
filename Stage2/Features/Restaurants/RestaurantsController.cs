using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Stage2.Features.Restaurants
{
    public class RestaurantsController : Controller
    {
        private readonly IMediator _mediator;
        public RestaurantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var request = new Index.Query();
            var response = await _mediator.Send(request);

            return View(response);
        }

        public IActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRestaurant(Add.Command model)
        {
            await _mediator.Send(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
