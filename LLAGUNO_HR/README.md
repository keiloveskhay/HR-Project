Admin WinForms App (IDK2)

This is a minimal Windows Forms admin app implementing:
- Job Vacancy Management
- Hiring Decisions
- Reports
- Maintenance (Departments, Roles, Employment Types)
- Audit trail

Build and run:

Prerequisites: .NET 8.0 SDK or later.

From the `IDK2` folder:

```powershell
cd IDK2
dotnet restore
dotnet build
dotnet run --project AdminApp.csproj
```

Or run from the workspace root:

```powershell
dotnet run --project IDK2/AdminApp.csproj
```
