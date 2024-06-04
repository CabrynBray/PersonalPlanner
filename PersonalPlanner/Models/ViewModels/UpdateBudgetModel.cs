using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalPlanner.Models.ViewModels
{
    public class UpdateBudgetModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please provide an Amount")]
        public double Amount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a currency")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "Please select a start date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please select an end date")]
        public DateTime EndDate { get; set; }
    }
}
