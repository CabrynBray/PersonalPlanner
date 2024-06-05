using System.ComponentModel.DataAnnotations;

namespace PersonalPlanner;

public class TodoTask
{
    public int? Id { get; set; }

    [Required]
    public string? Todo { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; }
    public string user_name { get; set; }

}