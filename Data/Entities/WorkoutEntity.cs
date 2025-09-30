using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class WorkoutEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Location { get; set; } = null!;

    [Column(TypeName = "datetime2")]
    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public string Instructor { get; set; } = null!;

    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

}
