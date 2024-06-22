﻿namespace CourseApiCodeChallenge.Models;

public class SectionResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<LessonResponseModel> Lessons { get; set; } = default!;
}