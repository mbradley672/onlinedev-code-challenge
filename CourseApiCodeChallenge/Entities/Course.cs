using System.ComponentModel.DataAnnotations;

namespace CourseApiCodeChallenge.Entities;

public class Course
{
    public Guid Id { get; set; }
    [MaxLength(250)] public string Name { get; set; } = string.Empty;
    public ICollection<Section> Sections { get; set; } = new List<Section>();
    public ICollection<WatchLog> WatchLogs { get; set; } = new List<WatchLog>();
}