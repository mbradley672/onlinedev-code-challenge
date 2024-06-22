using CourseApiCodeChallenge.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseApiCodeChallenge.Data;

public class AppDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<WatchLog> WatchLogs { get; set; }
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // We need to create non-clustered indexes as when the data get larger
        // there will be performance degradation on insert when the Index is a GUID
        modelBuilder.Entity<Section>()
            .HasIndex(x=>x.Id)
            .IsClustered(false);
        
        modelBuilder.Entity<Section>()
            .HasOne(s => s.Course)
            .WithMany(c => c.Sections)
            .HasForeignKey(s => s.CourseId);

        modelBuilder.Entity<Lesson>()
            .HasIndex(x=>x.Id)
            .IsClustered(false);
        
        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Section)
            .WithMany(s => s.Lessons)
            .HasForeignKey(l => l.SectionId);

        modelBuilder.Entity<WatchLog>()
            .HasIndex(x => x.Id)
            .IsClustered(false);
        
        modelBuilder.Entity<WatchLog>()
            .HasOne(wl => wl.Course)
            .WithMany(c => c.WatchLogs)
            .HasForeignKey(wl => wl.CourseId);

        modelBuilder.Entity<WatchLog>()
            .HasIndex(x => x.Id)
            .IsClustered(false);
        
        modelBuilder.Entity<WatchLog>()
            .HasOne(wl => wl.Lesson)
            .WithMany(l => l.WatchLogs)
            .HasForeignKey(wl => wl.LessonId);

        modelBuilder.Entity<WatchLog>()
            .HasIndex(x => x.Id)
            .IsClustered(false);
        
        modelBuilder.Entity<WatchLog>()
            .HasOne(wl => wl.User)
            .WithMany(u => u.WatchLogs)
            .HasForeignKey(wl => wl.UserId);
    }

    public void Seed()
    {
        var testUserId = Guid.Parse("623ac611-2aa2-406b-902f-3b7c2b11161e");
        var courseId = Guid.Parse("81a130d2-502f-4cf1-a376-63edeb000e9f");
        var section1Id = Guid.Parse("a83a1a6d-1f52-4c21-80b0-196ef3d8d11d");
        var section2Id = Guid.Parse("b72a2b1a-3153-4c6e-9442-03db8c4de1d2");
        var lesson1Id = Guid.Parse("35d9f982-07ed-4b61-9dd0-1de3b7d1a829");
        var lesson2Id = Guid.Parse("51efb2b5-2e83-4477-91e2-345c9e9353ab");
        var lesson3Id = Guid.Parse("74af5e8f-5f27-4c4f-8d6c-71a9f8b6d876");
        var lesson4Id = Guid.Parse("8992a1e2-7c3c-4689-b1ab-2f8b6e61d276");

        var user = new User
        {
            Id = testUserId,
            FullName = "Test User",
            ImageUrl = "https://example.com/user.jpg"
        };

        var course = new Course
        {
            Id = courseId,
            Name = "Sample Course"
        };

        var sections = new List<Section>
        {
            new() { Id = section1Id, Name = "Section 1", Order = 1, CourseId = courseId },
            new() { Id = section2Id, Name = "Section 2", Order = 2, CourseId = courseId }
        };

        var lessons = new List<Lesson>
        {
            new() { Id = lesson1Id, Name = "Lesson 1", VideoUrl = "https://example.com/lesson1.mp4", Order = 1, SectionId = section1Id },
            new() { Id = lesson2Id, Name = "Lesson 2", VideoUrl = "https://example.com/lesson2.mp4", Order = 2, SectionId = section1Id },
            new() { Id = lesson3Id, Name = "Lesson 3", VideoUrl = "https://example.com/lesson3.mp4", Order = 1, SectionId = section2Id },
            new() { Id = lesson4Id, Name = "Lesson 4", VideoUrl = "https://example.com/lesson4.mp4", Order = 2, SectionId = section2Id }
        };

        var watchLogs = new List<WatchLog>
        {
            new() { Id = Guid.NewGuid(), CourseId = courseId, LessonId = lesson1Id, UserId = testUserId, PercentageWatched = 100 },
            new() { Id = Guid.NewGuid(), CourseId = courseId, LessonId = lesson2Id, UserId = testUserId, PercentageWatched = 50 }
        };

        if (!Users.Any())
        {
            Users.Add(user);
        }

        if (!Courses.Any())
        {
            Courses.Add(course);
        }

        if (!Sections.Any())
        {
            Sections.AddRange(sections);
        }

        if (!Lessons.Any())
        {
            Lessons.AddRange(lessons);
        }

        if (!WatchLogs.Any())
        {
            WatchLogs.AddRange(watchLogs);
        }

        SaveChanges();
    }
}