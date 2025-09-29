using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class WorkoutCreationRequest
{
    public string Title { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public string Instructor { get; set; } = null!;
    public string? Description { get; set; }
}
