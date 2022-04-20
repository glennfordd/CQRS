using Stage1.Data;
using Stage1.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stage1.Application
{
    public class GuestsService
    {
        private readonly GuestsRepository _guestsRepository;

        public GuestsService(GuestsRepository guestsRepository)
        {
            _guestsRepository = guestsRepository;
        }

        public IEnumerable<Guest> GetAll()
        {
            return _guestsRepository.GetGuests();
        }

        public Guest GetGuestById(int id)
        {
            return _guestsRepository.GetGuestById(id);
        }

        public async Task AddGuest(string name, string email)
        {
            await _guestsRepository.AddGuest(name, email);
        }

        public async Task AddBooking(int guestId, int restaurantId, DateTime bookingDateTime)
        {
            await _guestsRepository.AddBooking(guestId, restaurantId, bookingDateTime);
        }

        public IEnumerable<Booking> GetGuestBookings(int guestId)
        {
            return _guestsRepository.GetGuestBookings(guestId);
        }

        public async Task ShareBooking(int bookingId, int hostId, string email, string name)
        {
            await _guestsRepository.ShareBooking(bookingId, hostId, email, name);
        }
    }
}
