using CourseApiCodeChallenge.Data;
using CourseApiCodeChallenge.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseApiCodeChallenge.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WatchLogController : ControllerBase
{
    private readonly AppDbContext _context;

    public WatchLogController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("{lessonId}")]
    public async Task<IActionResult> CreateWatchLog(Guid lessonId, [FromQuery] int percentageWatched)
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        var userId = userIdClaim.Value;

        var lesson = await _context.Lessons
            .Include(l => l.Section)
            .ThenInclude(s => s.Course)
            .FirstOrDefaultAsync(l => l.Id == lessonId);

        if (lesson == null || lesson.Section == null || lesson.Section.Course == null)
        {
            return NotFound("Lesson, Section, or Course not found.");
        }

        var watchLog = new WatchLog
        {
            Id = Guid.NewGuid(),
            CourseId = lesson.Section.CourseId,
            LessonId = lessonId,
            UserId = Guid.Parse(userId),
            PercentageWatched = percentageWatched
        };

        _context.WatchLogs.Add(watchLog);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateWatchLog), new { id = watchLog.Id }, watchLog);
    }
}
