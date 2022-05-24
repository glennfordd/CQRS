using System.Collections.Generic;

namespace Stage2.Domain
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
