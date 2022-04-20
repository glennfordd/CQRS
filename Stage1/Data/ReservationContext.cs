using Microsoft.EntityFrameworkCore;
using Stage1.Domain;

namespace Stage1.Data
{
    public class ReservationContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        public ReservationContext(DbContextOptions options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
