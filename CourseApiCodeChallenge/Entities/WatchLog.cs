using System.ComponentModel.DataAnnotations;

namespace CourseApiCodeChallenge.Entities;

public class WatchLog
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = default!;
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; } = default!;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    [Range(1,100)]
    public int PercentageWatched { get; set; }
}