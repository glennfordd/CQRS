using Microsoft.AspNetCore.Mvc.Rendering;
using Stage1.Domain;
using System;
using System.Collections.Generic;

namespace Stage1.UI.Models
{
    public class AddBookingViewModel
    {
        public int GuestId { get; set; }
        public IEnumerable<SelectListItem> Restaurants { get; set; }
        public int SelectedRestaurantId { get; set; }

        public DateTime BookingDateTime { get; set; }
    }
}
