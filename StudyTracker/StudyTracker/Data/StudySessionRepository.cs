using Microsoft.Data.Sqlite;
using StudyTracker.Models;

namespace StudyTracker.Data;

public class StudySessionRepository
{
    private readonly string _connectionString;

    public StudySessionRepository(string databasePath)
    {
        _connectionString = $"Data Source={databasePath}";
    }

    public void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS StudySessions (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Subject TEXT NOT NULL,
                StartTime TEXT NOT NULL,
                EndTime TEXT NOT NULL,
                Notes TEXT NOT NULL
            );
            """;
        command.ExecuteNonQuery();
    }

    public void Add(StudySession session)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO StudySessions (Subject, StartTime, EndTime, Notes)
            VALUES ($subject, $startTime, $endTime, $notes);
            """;
        AddParameters(command, session);
        command.ExecuteNonQuery();
    }

    public List<StudySession> GetAll()
    {
        var sessions = new List<StudySession>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Subject, StartTime, EndTime, Notes FROM StudySessions ORDER BY StartTime DESC;";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            sessions.Add(new StudySession
            {
                Id = reader.GetInt32(0),
                Subject = reader.GetString(1),
                StartTime = DateTime.Parse(reader.GetString(2)),
                EndTime = DateTime.Parse(reader.GetString(3)),
                Notes = reader.GetString(4)
            });
        }

        return sessions;
    }

    public bool Update(StudySession session)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
            UPDATE StudySessions
            SET Subject = $subject, StartTime = $startTime, EndTime = $endTime, Notes = $notes
            WHERE Id = $id;
            """;
        AddParameters(command, session);
        command.Parameters.AddWithValue("$id", session.Id);
        return command.ExecuteNonQuery() == 1;
    }

    public bool Delete(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM StudySessions WHERE Id = $id;";
        command.Parameters.AddWithValue("$id", id);
        return command.ExecuteNonQuery() == 1;
    }

    private static void AddParameters(SqliteCommand command, StudySession session)
    {
        command.Parameters.AddWithValue("$subject", session.Subject);
        command.Parameters.AddWithValue("$startTime", session.StartTime.ToString("O"));
        command.Parameters.AddWithValue("$endTime", session.EndTime.ToString("O"));
        command.Parameters.AddWithValue("$notes", session.Notes);
    }
}
