using System;

namespace Stage1.Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDateTime { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }
    }
}
