using System.ComponentModel.DataAnnotations;

namespace Stage1.UI.Models
{
    public class AddGuestViewModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }
    }
}
