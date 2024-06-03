using System.ComponentModel.DataAnnotations;

namespace PersonalPlanner.Models.ViewModels
{
    public class UpdateTodoModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Task Name")]
        public string Task { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a date")]
        public DateTime Date { get; set; }
    }
}