using System.ComponentModel.DataAnnotations;

namespace PersonalPlanner.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Name")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Password")]
        public string Password { get; set; }
    }
}