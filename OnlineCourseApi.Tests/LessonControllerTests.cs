using CourseApiCodeChallenge.Controllers;
using CourseApiCodeChallenge.Data;
using CourseApiCodeChallenge.Entities;
using CourseApiCodeChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourseApi.Tests;

public class LessonControllerTests
{
    private readonly AppDbContext _dbContext;
    private readonly LessonController _controller;

    public LessonControllerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new AppDbContext(options);
        _controller = new LessonController(_dbContext);

        SeedTestData();
    }

    private void SeedTestData()
    {
        var courseId = Guid.Parse("81a130d2-502f-4cf1-a376-63edeb000e9f");
        var section1Id = Guid.Parse("a83a1a6d-1f52-4c21-80b0-196ef3d8d11d");
        var lesson1Id = Guid.Parse("35d9f982-07ed-4b61-9dd0-1de3b7d1a829");

        var course = new Course
        {
            Id = courseId,
            Name = "Sample Course"
        };

        var section = new Section
        {
            Id = section1Id,
            Name = "Section 1",
            Order = 1,
            CourseId = courseId,
            Course = course
        };

        var lesson = new Lesson
        {
            Id = lesson1Id,
            Name = "Lesson 1",
            VideoUrl = "https://example.com/lesson1.mp4",
            Order = 1,
            SectionId = section1Id,
            Section = section
        };

        _dbContext.Courses.Add(course);
        _dbContext.Sections.Add(section);
        _dbContext.Lessons.Add(lesson);
        _dbContext.SaveChanges();
    }

    [Fact]
    public async Task GetLesson_ReturnsLessonDetails()
    {
        // Arrange
        var lessonId = Guid.Parse("35d9f982-07ed-4b61-9dd0-1de3b7d1a829");

        // Act
        var result = await _controller.GetLesson(lessonId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var lessonResponse = Assert.IsType<MainResponseModel>(okResult.Value);

        Assert.NotNull(lessonResponse);
        Assert.Equal("35d9f982-07ed-4b61-9dd0-1de3b7d1a829", lessonResponse.Id.ToString());
        Assert.Equal("Lesson 1", lessonResponse.Name);
        Assert.Equal("https://example.com/lesson1.mp4", lessonResponse.VideoUrl);
    }

    [Fact]
    public async Task GetLesson_ReturnsNotFound()
    {
        // Arrange
        var lessonId = Guid.NewGuid();

        // Act
        var result = await _controller.GetLesson(lessonId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}