using Xunit;
using StudyTracker.Models;
using StudyTracker.Services;

namespace StudyTracker.Tests;

public class StudySessionServiceTests
{
    [Fact]
    public void Validate_ThrowsException_WhenEndIsBeforeStart()
    {
        var session = new StudySession
        {
            Subject = "C#",
            StartTime = new DateTime(2026, 9, 2, 18, 0, 0),
            EndTime = new DateTime(2026, 9, 2, 17, 0, 0)
        };

        Assert.Throws<ArgumentException>(() => StudySessionService.Validate(session));
    }

    [Fact]
    public void CalculateTotalsBySubject_AddsDurationsForSameSubject()
    {
        var sessions = new List<StudySession>
        {
            CreateSession("C#", 60),
            CreateSession("C#", 30),
            CreateSession("Mathematik", 45)
        };

        var totals = StudySessionService.CalculateTotalsBySubject(sessions);

        Assert.Equal(90, totals["C#"].TotalMinutes);
        Assert.Equal(45, totals["Mathematik"].TotalMinutes);
    }

    private static StudySession CreateSession(string subject, int minutes)
    {
        var start = new DateTime(2026, 9, 2, 10, 0, 0);
        return new StudySession
        {
            Subject = subject,
            StartTime = start,
            EndTime = start.AddMinutes(minutes)
        };
    }
}
