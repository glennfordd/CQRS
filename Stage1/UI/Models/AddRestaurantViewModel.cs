using System.ComponentModel.DataAnnotations;

namespace Stage1.UI.Models
{
    public class AddRestaurantViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
