namespace CourseApiCodeChallenge.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<WatchLog> WatchLogs { get; set; }
}