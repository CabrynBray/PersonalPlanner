using System.ComponentModel.DataAnnotations;

namespace PersonalPlanner.Models.ViewModels
{
    public class EditGoalsModels
    {
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime? GoalDate { get; set; }

        public string? Description { get; set; }

        [Range(0, 100000001)]
        public decimal Amount { get; set; }

    }
}

