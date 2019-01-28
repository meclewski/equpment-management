using System.ComponentModel.DataAnnotations;

namespace Measuring_equipment.Models.ViewModels
{
    public class UserViewModel
    {
        public class CreateModel
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }
}
