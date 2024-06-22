using CourseApiCodeChallenge.Data;
using CourseApiCodeChallenge.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseApiCodeChallenge.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class LessonController : ControllerBase
{
    private readonly AppDbContext _context;

    public LessonController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetLesson(Guid id)
    {
        var lesson = await _context.Lessons
            .Include(l => l.Section)
            .ThenInclude(s => s.Course)
            .ThenInclude(c => c.Sections)
            .ThenInclude(s => s.Lessons)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (lesson == null)
        {
            return NotFound();
        }

        var sectionsModel = lesson.Section.Course.Sections.Select(s => new SectionResponseModel
        {
            Id = s.Id,
            Name = s.Name,
            Order = s.Order,
            Lessons = s.Lessons.Select(l => new LessonResponseModel
            {
                Id = l.Id,
                Name = l.Name,
                Order = l.Order,
                IsCompleted = _context.WatchLogs.Any(wl => wl.LessonId == l.Id && wl.PercentageWatched == 100)
            }).ToList()
        }).ToList();

        var courseModel = new CourseResponseModel
        {
            Name = lesson.Section.Course.Name,
            Sections = sectionsModel
        };

        var response = new MainResponseModel
        {
            Id = lesson.Id,
            Name = lesson.Name,
            VideoUrl = lesson.VideoUrl,
            Course = courseModel
        };

        return Ok(response);
    }
}