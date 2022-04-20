using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stage1.Application;
using Stage1.UI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Stage1.UI.Controllers
{
    public class GuestsController : Controller
    {
        private readonly GuestsService _guestsService;
        private readonly RestaurantsService _restaurantsService;

        public GuestsController(GuestsService guestsService, RestaurantsService restaurantsService)
        {
            _guestsService = guestsService;
            _restaurantsService = restaurantsService;
        }

        public IActionResult Index()
        {
            var guests = _guestsService.GetAll();

            return View(guests);
        }

        public IActionResult AddGuest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGuest(AddGuestViewModel model)
        {
            await _guestsService.AddGuest(model.Name, model.Email);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddBooking(int guestId)
        {
            var model = new AddBookingViewModel()
            {
                GuestId = guestId,
                Restaurants = _restaurantsService.GetAll().Select(p => new SelectListItem()
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }),
                BookingDateTime = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBooking(AddBookingViewModel model)
        {
            await _guestsService.AddBooking(model.GuestId, model.SelectedRestaurantId, model.BookingDateTime);

            return RedirectToAction(nameof(Details), new { Id = model.GuestId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var guest = _guestsService.GetGuestById(id);
            var bookings = _guestsService.GetGuestBookings(guest.Id);

            var model = new GuestDetailsViewModel()
            {
                Id = guest.Id,
                Name = guest.Name,
                Email = guest.Email,
                Bookings = bookings.Select(p => new GuestDetailsViewModel.Booking()
                {
                    Id = p.Id,
                    BookingDateTime = p.BookingDateTime,
                    Restaurant = p.Restaurant.Name,
                })
            };

            return View(model);
        }

        public IActionResult Share(int bookingId, int hostId)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Share(ShareBookingViewModel model)
        {
            await _guestsService.ShareBooking(model.BookingId, model.HostId, model.Email, model.Name);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
