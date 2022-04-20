using System.Collections.Generic;

namespace Stage2.Domain
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
