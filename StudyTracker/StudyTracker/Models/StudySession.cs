namespace StudyTracker.Models;

public class StudySession
{
    public int Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Notes { get; set; } = string.Empty;

    public TimeSpan Duration => EndTime - StartTime;
}
