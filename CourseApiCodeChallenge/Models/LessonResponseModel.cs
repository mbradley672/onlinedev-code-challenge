namespace CourseApiCodeChallenge.Models;

public class LessonResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsCompleted { get; set; }
}