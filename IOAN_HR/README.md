# Applicant Registration & Profile — Demo

This is a minimal console demo implementing the requested features and persisting data to a local SQLite database.

Features implemented:
- Account creation
- Duplicate email validation
- Change password
- Personal information
- Education
- Skills
- Work experience
- Tables: ApplicantAccounts, Applicants (plus Education, Skills, WorkExperiences)

Run instructions:

Prerequisites:
- Install the .NET 8.0 SDK or later. Verify with `dotnet --info`.

Development (recommended):

1. Open a terminal and change to the project folder:

```powershell
cd IDk
```

2. Restore, build and run the app:

```powershell
dotnet restore
dotnet build
dotnet run --project ApplicantApp.csproj
```

You can also run the app from the workspace root:

```powershell
dotnet run --project IDk/ApplicantApp.csproj
```

Notes:
- This project creates `applicants.db` in the working directory at runtime.
- The UI mode requires Windows desktop; run with `-- ui` to open the WinForms UI on Windows.
