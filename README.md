# Study Session Tracker

Eine C#-Konsolenanwendung zur Erfassung und Auswertung von Lernzeiten.

## Funktionen

- lernzeiten hinzufügen, anzeigen, bearbeiten und löschen
- automatische berechnung der lerndauer
- auswertung der gesmten lernzeit pro fach
- speicherung in einer SQLite-Datenbank
- überprüfung ungültiger Eingaben
- automatisierte tests mit xUnit

## Technologien

- C#
- .NET 8
- SQLite
- xUnit

## Starten

```powershell
dotnet restore
dotnet run --project StudyTracker/StudyTracker.csproj
