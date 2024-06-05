using System.ComponentModel.DataAnnotations;

namespace PersonalPlanner;

public class Goal
{
    public int? Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [DataType(DataType.Date)]
    public DateTime? GoalDate { get; set; }

    public string? Description { get; set; }

    [Range(0, 100000001)]
    public decimal Amount { get; set; }
    public string user_name { get; set; }
}