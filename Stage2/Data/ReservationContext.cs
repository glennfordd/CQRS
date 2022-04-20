using Microsoft.EntityFrameworkCore;
using Stage2.Domain;

namespace Stage2.Data
{
    public class ReservationContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
