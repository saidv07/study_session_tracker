using StudyTracker.Data;
using StudyTracker.Models;
using StudyTracker.Services;
using StudyTracker.UI;

var repository = new StudySessionRepository("studytracker.db");
repository.InitializeDatabase();

var running = true;
while (running)
{
    ShowMenu();
    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1": AddSession(repository); break;
            case "2": ShowSessions(repository.GetAll()); break;
            case "3": UpdateSession(repository); break;
            case "4": DeleteSession(repository); break;
            case "5": ShowStatistics(repository.GetAll()); break;
            case "0": running = false; break;
            default: Console.WriteLine("Bitte wähle einen vorhandenen Menüpunkt."); break;
        }
    }
    catch (Exception exception)
    {
        Console.WriteLine($"Fehler: {exception.Message}");
    }

    if (running)
    {
        Console.WriteLine("\nDrücke Enter, um fortzufahren.");
        Console.ReadLine();
        Console.Clear();
    }
}

static void ShowMenu()
{
    Console.WriteLine("=== STUDY SESSION TRACKER ===");
    Console.WriteLine("1 - Lernzeit hinzufügen");
    Console.WriteLine("2 - Alle Lernzeiten anzeigen");
    Console.WriteLine("3 - Lernzeit bearbeiten");
    Console.WriteLine("4 - Lernzeit löschen");
    Console.WriteLine("5 - Auswertung nach Lernfach");
    Console.WriteLine("0 - Programm beenden");
    Console.Write("Auswahl: ");
}

static void AddSession(StudySessionRepository repository)
{
    var session = ReadSessionFromUser();
    StudySessionService.Validate(session);
    repository.Add(session);
    Console.WriteLine("Lernzeit wurde gespeichert.");
}

static void ShowSessions(List<StudySession> sessions)
{
    if (sessions.Count == 0)
    {
        Console.WriteLine("Es wurden noch keine Lernzeiten gespeichert.");
        return;
    }

    Console.WriteLine("\nID | Fach | Start | Ende | Dauer | Notiz");
    Console.WriteLine(new string('-', 90));
    foreach (var session in sessions)
    {
        Console.WriteLine($"{session.Id} | {session.Subject} | {session.StartTime:dd.MM.yyyy HH:mm} | " +
                          $"{session.EndTime:dd.MM.yyyy HH:mm} | {FormatDuration(session.Duration)} | {session.Notes}");
    }
}

static void UpdateSession(StudySessionRepository repository)
{
    ShowSessions(repository.GetAll());
    var id = ConsoleInput.ReadInt("ID des Eintrags: ");
    var session = ReadSessionFromUser();
    session.Id = id;
    StudySessionService.Validate(session);

    Console.WriteLine(repository.Update(session)
        ? "Lernzeit wurde aktualisiert."
        : "Es wurde kein Eintrag mit dieser ID gefunden.");
}

static void DeleteSession(StudySessionRepository repository)
{
    ShowSessions(repository.GetAll());
    var id = ConsoleInput.ReadInt("ID des zu löschenden Eintrags: ");
    Console.WriteLine(repository.Delete(id)
        ? "Eintrag wurde gelöscht."
        : "Es wurde kein Eintrag mit dieser ID gefunden.");
}

static void ShowStatistics(List<StudySession> sessions)
{
    var totals = StudySessionService.CalculateTotalsBySubject(sessions);
    if (totals.Count == 0)
    {
        Console.WriteLine("Für die Auswertung sind noch keine Daten vorhanden.");
        return;
    }

    Console.WriteLine("\n=== LERNZEIT PRO FACH ===");
    foreach (var total in totals.OrderByDescending(item => item.Value))
        Console.WriteLine($"{total.Key}: {FormatDuration(total.Value)}");
}

static StudySession ReadSessionFromUser()
{
    return new StudySession
    {
        Subject = ConsoleInput.ReadRequiredText("Lernfach: "),
        StartTime = ConsoleInput.ReadDateTime("Startzeit"),
        EndTime = ConsoleInput.ReadDateTime("Endzeit"),
        Notes = ConsoleInput.ReadOptionalText("Notiz (optional): ")
    };
}

static string FormatDuration(TimeSpan duration)
{
    var totalHours = (int)duration.TotalHours;
    return $"{totalHours} h {duration.Minutes} min";
}
