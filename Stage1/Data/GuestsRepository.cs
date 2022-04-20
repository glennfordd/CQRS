using Microsoft.EntityFrameworkCore;
using Stage1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stage1.Data
{
    // Problem #1: Get methods tends to bloat depending on requirements (specification pattern solves this)
    // Problem #2: Sometimes logic creeps in repository (pushing logic to domain solves this)
    // Problem #3: Query projections. You have to decide whether to push down the ViewModels on the Data layer or create a DTO which basically a copy of your ViewModel. Then application layer will handle the mapping between those 2
    // If this two problems are solvable outside of repo, then what does benefit the repo gives you? Abstraction.
    public class GuestsRepository
    {
        private readonly ReservationContext _reservationContext;

        public GuestsRepository(ReservationContext reservationContext)
        {
            _reservationContext = reservationContext;
        }

        public IEnumerable<Guest> GetGuests()
        {
            return _reservationContext.Guests.ToList();
        }

        public Guest GetGuestById(int guestId)
        {
            return _reservationContext.Guests.Find(guestId);
        }

        public Guest GetGuestByEmail(string email)
        {
            return _reservationContext.Guests.Where(p => p.Email == email).FirstOrDefault();
        }

        public async Task AddGuest(string name, string email)
        {
            var guest = new Guest() { Name = name, Email = email };
            _reservationContext.Guests.Add(guest);
            
            await _reservationContext.SaveChangesAsync();
        }

        public async Task AddBooking(int guestId, int restaurantId, DateTime bookingDateTime)
        {
            var guest = GetGuestById(guestId);
            var booking = new Booking()
            {
                RestaurantId = restaurantId, 
                BookingDateTime = bookingDateTime
            };

            guest.Bookings.Add(booking);

            await _reservationContext.SaveChangesAsync();
        }

        public IEnumerable<Booking> GetGuestBookings(int guestId)
        {
            return _reservationContext.Guests
                .Include(p => p.Bookings)
                .ThenInclude(p => p.Restaurant)
                .Where(p => p.Id == guestId)
                .SelectMany(p => p.Bookings)
                .ToList();
        }

        public async Task ShareBooking(int bookingId, int hostId, string email, string name)
        {
            var booking = _reservationContext.Guests
                .Where(p => p.Id == hostId)
                .SelectMany(p => p.Bookings)
                .Where(p => p.Id == bookingId)
                .FirstOrDefault();

            var invitedGuest =  GetGuestByEmail(email);
            if(invitedGuest == null)
            {
                invitedGuest = new Guest()
                {
                    Name = name,
                    Email = email,
                };

                invitedGuest.Bookings.Add(booking);
                _reservationContext.Guests.Add(invitedGuest);
            }
            else
            {
                invitedGuest.Bookings.Add(booking);
            }

            await _reservationContext.SaveChangesAsync();
        }
    }
}
