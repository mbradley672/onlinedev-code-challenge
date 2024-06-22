using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseApiCodeChallenge.Entities;

public class Section
{
    public Guid Id { get; set; }
    [MaxLength(250)] public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public Guid CourseId { get; set; }
    [JsonIgnore] public Course Course { get; set; } = default!;
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}