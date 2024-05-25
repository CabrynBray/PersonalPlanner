using System.ComponentModel.DataAnnotations;

namespace PersonalPlanner.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
