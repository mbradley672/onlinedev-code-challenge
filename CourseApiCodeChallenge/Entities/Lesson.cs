using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseApiCodeChallenge.Entities;

public class Lesson
{
    public Guid Id { get; set; }
    [MaxLength(250)] public string Name { get; set; } = string.Empty;
    [MaxLength(355)] public string VideoUrl { get; set; } = string.Empty;
    public int Order { get; set; }
    public Guid SectionId { get; set; }
    [JsonIgnore] public Section Section { get; set; } = default!;
    public ICollection<WatchLog> WatchLogs { get; set; } = new List<WatchLog>();
}