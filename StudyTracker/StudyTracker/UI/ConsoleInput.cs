using System.Globalization;

namespace StudyTracker.UI;

public static class ConsoleInput
{
    private const string DateFormat = "dd.MM.yyyy HH:mm";

    public static string ReadRequiredText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var value = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(value))
                return value;

            Console.WriteLine("Bitte gib einen Wert ein.");
        }
    }

    public static string ReadOptionalText(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public static DateTime ReadDateTime(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} ({DateFormat}): ");
            var input = Console.ReadLine();
            if (DateTime.TryParseExact(input, DateFormat, CultureInfo.GetCultureInfo("de-AT"), DateTimeStyles.None, out var value))
                return value;

            Console.WriteLine("Ungültiges Format. Beispiel: 02.09.2026 18:30");
        }
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out var value))
                return value;

            Console.WriteLine("Bitte gib eine ganze Zahl ein.");
        }
    }
}
