using System.ComponentModel.DataAnnotations;

namespace PersonalPlanner;

public class Budget
{
    public int? Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double? Amount { get; set; }

    [Required]
    public string? Currency { get; set; }

    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }
    public string user_name { get; set; }
}
