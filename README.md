# Study Session Tracker

Eine C#-Konsolenanwendung zur Erfassung und Auswertung von Lernzeiten.

## Funktionen

- Lernzeiten hinzufügen, anzeigen, bearbeiten und löschen
- automatische Berechnung der Lerndauer
- Auswertung der gesamten Lernzeit pro Fach
- Speicherung in einer SQLite-Datenbank
- Überprüfung ungültiger Eingaben
- automatisierte Tests mit xUnit

## Technologien

- C#
- .NET 8
- SQLite
- xUnit

## Starten

```powershell
dotnet restore
dotnet run --project StudyTracker/StudyTracker.csproj
