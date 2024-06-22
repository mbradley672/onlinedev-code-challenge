namespace CourseApiCodeChallenge.Models;

public class MainResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public CourseResponseModel Course { get; set; } = default!;
}