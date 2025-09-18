namespace Domain.Models;

public class WorkoutResponseModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Location { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public string Instructor { get; set; } = null!;
}
