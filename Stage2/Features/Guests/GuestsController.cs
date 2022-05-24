using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Stage2.Features.Guests
{
    public class GuestsController : Controller
    {
        private readonly IMediator _mediator;

        public GuestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var request = new Index.Query();
            var response = await _mediator.Send(request);

            return View(response);
        }

        public async Task<IActionResult> Details(int id)
        {
            var request = new Details.Query() { Id = id };
            var response = await _mediator.Send(request);

            return View(response);
        }

        public IActionResult AddGuest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGuest(AddGuest.Command model)
        {
            await _mediator.Send(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddBooking(int guestId)
        {
            var request = new AddBooking.Query() { Id = guestId };
            var response = await _mediator.Send(request);

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBooking(AddBooking.Command model)
        {
            await _mediator.Send(model);

            return RedirectToAction(nameof(Details), new { Id = model.GuestId });
        }   

        public IActionResult Share(int bookingId, int hostId)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Share(Share.Command model)
        {
            await _mediator.Send(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
