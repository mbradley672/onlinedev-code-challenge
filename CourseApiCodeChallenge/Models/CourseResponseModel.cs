namespace CourseApiCodeChallenge.Models;

public class CourseResponseModel
{
    public string Name { get; set; } = string.Empty;
    public List<SectionResponseModel> Sections { get; set; } = default!;
}