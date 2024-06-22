using System.Security.Claims;
using CourseApiCodeChallenge.Controllers;
using CourseApiCodeChallenge.Data;
using CourseApiCodeChallenge.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourseApi.Tests;

public class WatchLogControllerTests
{
    private readonly AppDbContext _dbContext;
    private readonly WatchLogController _controller;

    public WatchLogControllerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new AppDbContext(options);
        _controller = new WatchLogController(_dbContext);

        SeedTestData(_dbContext);
        SetUserIdClaim();
    }

    private void SeedTestData(AppDbContext context)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Test User",
            ImageUrl = "https://example.com/user.jpg"
        };

        var course = new Course
        {
            Id = Guid.NewGuid(),
            Name = "Sample Course"
        };

        var section = new Section
        {
            Id = Guid.NewGuid(),
            Name = "Section 1",
            Order = 1,
            CourseId = course.Id,
            Course = course
        };

        var lesson = new Lesson
        {
            Id = Guid.NewGuid(),
            Name = "Lesson 1",
            VideoUrl = "https://example.com/lesson1.mp4",
            Order = 1,
            SectionId = section.Id,
            Section = section
        };

        context.Users.Add(user);
        context.Courses.Add(course);
        context.Sections.Add(section);
        context.Lessons.Add(lesson);
        context.SaveChanges();
    }

    private void SetUserIdClaim()
    {
        var userId = _dbContext.Users.First().Id;
        var claims = new List<Claim>
        {
            new Claim("id", userId.ToString())
        };

        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            }
        };
    }

    [Fact]
    public async Task CreateWatchLog_ReturnsCreated()
    {
        // Arrange
        var lessonId = _dbContext.Lessons.First().Id;

        // Act
        var result = await _controller.CreateWatchLog(lessonId, 75);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var watchLog = Assert.IsType<WatchLog>(createdResult.Value);

        Assert.Equal(lessonId, watchLog.LessonId);
        Assert.Equal(75, watchLog.PercentageWatched);
    }

    [Fact]
    public async Task CreateWatchLog_ReturnsNotFound()
    {
        // Arrange
        var lessonId = Guid.NewGuid();

        // Act
        var result = await _controller.CreateWatchLog(lessonId, 75);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}