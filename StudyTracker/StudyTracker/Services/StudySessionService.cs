using StudyTracker.Models;

namespace StudyTracker.Services;

public static class StudySessionService
{
    public static void Validate(StudySession session)
    {
        if (string.IsNullOrWhiteSpace(session.Subject))
            throw new ArgumentException("Das Lernfach darf nicht leer sein.");

        if (session.EndTime <= session.StartTime)
            throw new ArgumentException("Die Endzeit muss nach der Startzeit liegen.");
    }

    public static Dictionary<string, TimeSpan> CalculateTotalsBySubject(IEnumerable<StudySession> sessions)
    {
        return sessions
            .GroupBy(session => session.Subject, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                group => group.Key,
                group => TimeSpan.FromMinutes(group.Sum(session => session.Duration.TotalMinutes)),
                StringComparer.OrdinalIgnoreCase);
    }
}
