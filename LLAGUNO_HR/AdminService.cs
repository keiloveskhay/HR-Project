using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace IDK2
{
    public static class AdminService
    {
        public static void AddAudit(string actor, string action, string details)
        {
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO AuditTrail (Timestamp, Actor, Action, Details) VALUES ($ts, $actor, $action, $details)";
            cmd.Parameters.AddWithValue("$ts", DateTime.UtcNow.ToString("o"));
            cmd.Parameters.AddWithValue("$actor", actor ?? string.Empty);
            cmd.Parameters.AddWithValue("$action", action ?? string.Empty);
            cmd.Parameters.AddWithValue("$details", details ?? string.Empty);
            cmd.ExecuteNonQuery();
        }

        public static bool CreateDepartment(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT OR IGNORE INTO Departments (Id, Name) VALUES ($id, $name)";
            cmd.Parameters.AddWithValue("$id", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("$name", name.Trim());
            var r = cmd.ExecuteNonQuery();
            AddAudit("system", "CreateDepartment", name);
            return r > 0;
        }

        public static List<Department> GetDepartments()
        {
            var list = new List<Department>();
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM Departments ORDER BY Name";
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(new Department { Id = r.GetString(0), Name = r.IsDBNull(1) ? string.Empty : r.GetString(1) });
            return list;
        }

        public static bool CreateRole(string departmentId, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Roles (Id, DepartmentId, Name) VALUES ($id, $did, $name)";
            cmd.Parameters.AddWithValue("$id", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("$did", departmentId ?? string.Empty);
            cmd.Parameters.AddWithValue("$name", name.Trim());
            var r = cmd.ExecuteNonQuery();
            AddAudit("system", "CreateRole", name);
            return r > 0;
        }

        public static List<Role> GetRoles(string departmentId = null)
        {
            var list = new List<Role>();
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            if (string.IsNullOrWhiteSpace(departmentId)) cmd.CommandText = "SELECT Id, DepartmentId, Name FROM Roles ORDER BY Name";
            else { cmd.CommandText = "SELECT Id, DepartmentId, Name FROM Roles WHERE DepartmentId=$did ORDER BY Name"; cmd.Parameters.AddWithValue("$did", departmentId); }
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(new Role { Id = r.GetString(0), DepartmentId = r.GetString(1), Name = r.IsDBNull(2) ? string.Empty : r.GetString(2) });
            return list;
        }

        public static bool CreateEmploymentType(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT OR IGNORE INTO EmploymentTypes (Id, Name) VALUES ($id, $name)";
            cmd.Parameters.AddWithValue("$id", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("$name", name.Trim());
            var r = cmd.ExecuteNonQuery();
            AddAudit("system", "CreateEmploymentType", name);
            return r > 0;
        }

        public static List<EmploymentType> GetEmploymentTypes()
        {
            var list = new List<EmploymentType>();
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM EmploymentTypes ORDER BY Name";
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(new EmploymentType { Id = r.GetString(0), Name = r.IsDBNull(1) ? string.Empty : r.GetString(1) });
            return list;
        }

        public static bool CreateVacancy(Vacancy v)
        {
            if (v == null) return false;
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Vacancies (Id, Title, DepartmentId, RoleId, EmploymentType, Description, OpenDate, Status) VALUES ($id, $title, $did, $rid, $et, $desc, $od, $st)";
            cmd.Parameters.AddWithValue("$id", v.Id);
            cmd.Parameters.AddWithValue("$title", v.Title ?? string.Empty);
            cmd.Parameters.AddWithValue("$did", v.DepartmentId ?? string.Empty);
            cmd.Parameters.AddWithValue("$rid", v.RoleId ?? string.Empty);
            cmd.Parameters.AddWithValue("$et", v.EmploymentType ?? string.Empty);
            cmd.Parameters.AddWithValue("$desc", v.Description ?? string.Empty);
            cmd.Parameters.AddWithValue("$od", DateTime.UtcNow.ToString("o"));
            cmd.Parameters.AddWithValue("$st", "Open");
            var r = cmd.ExecuteNonQuery();
            AddAudit("system", "CreateVacancy", v.Title);
            return r > 0;
        }

        public static List<Vacancy> GetVacancies()
        {
            var list = new List<Vacancy>();
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT v.Id, v.Title, v.DepartmentId, d.Name, v.RoleId, r.Name, v.EmploymentType, v.Description, v.OpenDate, v.CloseDate, v.Status
                                FROM Vacancies v
                                LEFT JOIN Departments d ON d.Id=v.DepartmentId
                                LEFT JOIN Roles r ON r.Id=v.RoleId
                                ORDER BY v.OpenDate DESC";
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Vacancy
                {
                    Id = r.IsDBNull(0) ? string.Empty : r.GetString(0),
                    Title = r.IsDBNull(1) ? string.Empty : r.GetString(1),
                    DepartmentId = r.IsDBNull(2) ? string.Empty : r.GetString(2),
                    DepartmentName = r.IsDBNull(3) ? string.Empty : r.GetString(3),
                    RoleId = r.IsDBNull(4) ? string.Empty : r.GetString(4),
                    RoleName = r.IsDBNull(5) ? string.Empty : r.GetString(5),
                    EmploymentType = r.IsDBNull(6) ? string.Empty : r.GetString(6),
                    Description = r.IsDBNull(7) ? string.Empty : r.GetString(7),
                    OpenDate = r.IsDBNull(8) ? string.Empty : r.GetString(8),
                    CloseDate = r.IsDBNull(9) ? string.Empty : r.GetString(9),
                    Status = r.IsDBNull(10) ? string.Empty : r.GetString(10)
                });
            }
            return list;
        }

        public static bool UpdateVacancy(Vacancy v)
        {
            if (v == null) return false;
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Vacancies SET Title=$title, DepartmentId=$did, RoleId=$rid, EmploymentType=$et, Description=$desc WHERE Id=$id";
            cmd.Parameters.AddWithValue("$title", v.Title ?? string.Empty);
            cmd.Parameters.AddWithValue("$did", v.DepartmentId ?? string.Empty);
            cmd.Parameters.AddWithValue("$rid", v.RoleId ?? string.Empty);
            cmd.Parameters.AddWithValue("$et", v.EmploymentType ?? string.Empty);
            cmd.Parameters.AddWithValue("$desc", v.Description ?? string.Empty);
            cmd.Parameters.AddWithValue("$id", v.Id);
            var r = cmd.ExecuteNonQuery();
            AddAudit("system", "UpdateVacancy", v.Title);
            return r > 0;
        }

        public static bool CloseVacancy(string id)
        {
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Vacancies SET CloseDate=$cd, Status='Closed' WHERE Id=$id";
            cmd.Parameters.AddWithValue("$cd", DateTime.UtcNow.ToString("o"));
            cmd.Parameters.AddWithValue("$id", id ?? string.Empty);
            var r = cmd.ExecuteNonQuery();
            AddAudit("system", "CloseVacancy", id ?? string.Empty);
            return r > 0;
        }

        public static bool AddHiringDecision(HiringDecision d)
        {
            if (d == null) return false;
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO HiringDecisions (Id, VacancyId, CandidateName, CandidateEmail, Decision, DecisionDate, Notes) VALUES ($id, $vid, $name, $email, $dec, $dd, $notes)";
            cmd.Parameters.AddWithValue("$id", d.Id);
            cmd.Parameters.AddWithValue("$vid", d.VacancyId ?? string.Empty);
            cmd.Parameters.AddWithValue("$name", d.CandidateName ?? string.Empty);
            cmd.Parameters.AddWithValue("$email", d.CandidateEmail ?? string.Empty);
            cmd.Parameters.AddWithValue("$dec", d.Decision ?? string.Empty);
            cmd.Parameters.AddWithValue("$dd", DateTime.UtcNow.ToString("o"));
            cmd.Parameters.AddWithValue("$notes", d.Notes ?? string.Empty);
            var r = cmd.ExecuteNonQuery();
            AddAudit("system", "AddHiringDecision", d.CandidateName ?? string.Empty);
            return r > 0;
        }

        public static List<HiringDecision> GetDecisionsForVacancy(string vacancyId)
        {
            var list = new List<HiringDecision>();
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, VacancyId, CandidateName, CandidateEmail, Decision, DecisionDate, Notes FROM HiringDecisions WHERE VacancyId=$vid ORDER BY DecisionDate DESC";
            cmd.Parameters.AddWithValue("$vid", vacancyId ?? string.Empty);
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(new HiringDecision { Id = r.GetString(0), VacancyId = r.GetString(1), CandidateName = r.IsDBNull(2) ? string.Empty : r.GetString(2), CandidateEmail = r.IsDBNull(3) ? string.Empty : r.GetString(3), Decision = r.IsDBNull(4) ? string.Empty : r.GetString(4), DecisionDate = r.IsDBNull(5) ? string.Empty : r.GetString(5), Notes = r.IsDBNull(6) ? string.Empty : r.GetString(6) });
            return list;
        }

        public static (int totalVacancies, int openVacancies, int closedVacancies, int totalDecisions, int hires)
            GenerateSummary()
        {
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(1) FROM Vacancies";
            var total = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.CommandText = "SELECT COUNT(1) FROM Vacancies WHERE Status='Open'";
            var open = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.CommandText = "SELECT COUNT(1) FROM Vacancies WHERE Status='Closed'";
            var closed = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.CommandText = "SELECT COUNT(1) FROM HiringDecisions";
            var dec = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.CommandText = "SELECT COUNT(1) FROM HiringDecisions WHERE Decision='Hired'";
            var hires = Convert.ToInt32(cmd.ExecuteScalar());
            return (total, open, closed, dec, hires);
        }

        // Smoke test helper: create sample data, add a vacancy and decision, write a report
        public static void SmokeTest()
        {
            AddAudit("smoketest", "start", "Starting smoke test");
            try
            {
                // create lookups
                CreateDepartment("HR");
                var hr = GetDepartments().FirstOrDefault(d => d.Name == "HR") ?? GetDepartments().FirstOrDefault();
                if (hr == null) CreateDepartment("HR");
                hr = GetDepartments().FirstOrDefault(d => d.Name == "HR");
                CreateRole(hr.Id, "Developer");
                CreateEmploymentType("Full-time");
                var ft = GetEmploymentTypes().FirstOrDefault(t => t.Name == "Full-time");

                // create vacancy
                var v = new Vacancy { Title = "Software Developer", DepartmentId = hr?.Id ?? string.Empty, DepartmentName = hr?.Name ?? string.Empty, RoleId = GetRoles(hr?.Id).FirstOrDefault()?.Id ?? string.Empty, RoleName = GetRoles(hr?.Id).FirstOrDefault()?.Name ?? string.Empty, EmploymentType = ft?.Name ?? "Full-time", Description = "Smoke test vacancy" };
                CreateVacancy(v);

                // add hiring decision
                var created = GetVacancies().FirstOrDefault(x => x.Title == "Software Developer");
                if (created != null)
                {
                    var d = new HiringDecision { VacancyId = created.Id, CandidateName = "Alice Test", CandidateEmail = "alice@example.com", Decision = "Hired", Notes = "Automated smoke test" };
                    AddHiringDecision(d);
                }

                var summary = GenerateSummary();
                var outText = $"Smoke test summary - {DateTime.UtcNow:o}\nVacancies: {summary.totalVacancies}\nOpen: {summary.openVacancies}\nClosed: {summary.closedVacancies}\nDecisions: {summary.totalDecisions}\nHires: {summary.hires}\n";
                Console.WriteLine(outText);
                var outPath = Path.Combine(AppContext.BaseDirectory, "smoke_report.txt");
                File.WriteAllText(outPath, outText);
                AddAudit("smoketest", "end", "Smoke test completed");
                Console.WriteLine($"Smoke report written to: {outPath}");
            }
            catch (Exception ex)
            {
                AddAudit("smoketest", "error", ex.ToString());
                Console.WriteLine("Smoke test failed: " + ex.Message);
            }
        }
    }
}
