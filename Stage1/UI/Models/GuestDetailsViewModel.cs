using System;
using System.Collections.Generic;

namespace Stage1.UI.Models
{
    public class GuestDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }

        public class Booking
        {
            public int Id { get; set; }
            public string Restaurant { get; set; }
            public DateTime BookingDateTime { get; set; }
        }
    }
}
