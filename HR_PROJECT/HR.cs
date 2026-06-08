using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Windows.Forms;

namespace IDk
{
    // Minimal SQLite-based implementation for ApplicantAccounts and Applicants
    public static class Db
    {
        private static string DbPath => Path.Combine(AppContext.BaseDirectory, "applicants.db");

        public static void EnsureDatabase()
        {
            var cs = new SqliteConnectionStringBuilder { DataSource = DbPath }.ToString();
            using var conn = new SqliteConnection(cs);
            conn.Open();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Applicants (
                Id TEXT PRIMARY KEY,
                FirstName TEXT,
                LastName TEXT,
                Email TEXT UNIQUE,
                Phone TEXT,
                Address TEXT
            );
            CREATE TABLE IF NOT EXISTS ApplicantAccounts (
                Id TEXT PRIMARY KEY,
                Email TEXT UNIQUE,
                PasswordHash TEXT,
                ApplicantId TEXT,
                CreatedAt TEXT
            );
            CREATE TABLE IF NOT EXISTS Education (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ApplicantId TEXT,
                Institution TEXT,
                Degree TEXT,
                FieldOfStudy TEXT,
                Year INTEGER
            );
            CREATE TABLE IF NOT EXISTS Skills (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ApplicantId TEXT,
                Skill TEXT
            );
            CREATE TABLE IF NOT EXISTS WorkExperiences (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ApplicantId TEXT,
                Company TEXT,
                Title TEXT,
                Description TEXT,
                StartYear INTEGER,
                EndYear INTEGER
            );
            ";
            cmd.ExecuteNonQuery();
        }

        public static SqliteConnection GetConnection()
        {
            var cs = new SqliteConnectionStringBuilder { DataSource = DbPath }.ToString();
            var conn = new SqliteConnection(cs);
            conn.Open();
            return conn;
        }
    }

    // Models
    public class Applicant
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<EducationEntry> Education { get; } = new();
        public List<string> Skills { get; } = new();
        public List<WorkExperience> WorkExperiences { get; } = new();
    }

    public class ApplicantAccount
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string ApplicantId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class EducationEntry
    {
        public string Institution { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string FieldOfStudy { get; set; } = string.Empty;
        public int Year { get; set; }
        public override string ToString() => $"{Degree} in {FieldOfStudy}, {Institution} ({Year})";
    }

    public class WorkExperience
    {
        public string Company { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int StartYear { get; set; }
        public int? EndYear { get; set; }
        public override string ToString() => $"{Title} at {Company} ({StartYear}-{(EndYear.HasValue ? EndYear.Value.ToString() : "Present")})";
    }

    // AccountService using SQLite
    public static class AccountService
    {
        public static bool EmailExists(string email)
        {
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(1) FROM ApplicantAccounts WHERE lower(Email)=lower($email)";
            cmd.Parameters.AddWithValue("$email", email ?? string.Empty);
            var v = cmd.ExecuteScalar();
            return Convert.ToInt32(v) > 0;
        }

        public static (bool success, string message) CreateAccount(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return (false, "Email and password are required.");

            if (EmailExists(email))
                return (false, "Duplicate email: an account with that email already exists.");

            var applicant = new Applicant { Email = email };
            using (var conn = Db.GetConnection())
            {
                using var tx = conn.BeginTransaction();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Applicants (Id, FirstName, LastName, Email, Phone, Address) VALUES ($id, $fn, $ln, $email, $phone, $addr)";
                cmd.Parameters.AddWithValue("$id", applicant.Id);
                cmd.Parameters.AddWithValue("$fn", applicant.FirstName);
                cmd.Parameters.AddWithValue("$ln", applicant.LastName);
                cmd.Parameters.AddWithValue("$email", applicant.Email);
                cmd.Parameters.AddWithValue("$phone", applicant.Phone);
                cmd.Parameters.AddWithValue("$addr", applicant.Address);
                cmd.ExecuteNonQuery();

                using var cmd2 = conn.CreateCommand();
                cmd2.CommandText = "INSERT INTO ApplicantAccounts (Id, Email, PasswordHash, ApplicantId, CreatedAt) VALUES ($id, $email, $ph, $aid, $ca)";
                cmd2.Parameters.AddWithValue("$id", Guid.NewGuid().ToString());
                cmd2.Parameters.AddWithValue("$email", email);
                cmd2.Parameters.AddWithValue("$ph", Hash(password));
                cmd2.Parameters.AddWithValue("$aid", applicant.Id);
                cmd2.Parameters.AddWithValue("$ca", DateTime.UtcNow.ToString("o"));
                cmd2.ExecuteNonQuery();

                tx.Commit();
            }

            return (true, "Account created successfully.");
        }

        public static bool VerifyCredentials(string email, string password)
        {
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT PasswordHash FROM ApplicantAccounts WHERE lower(Email)=lower($email)";
            cmd.Parameters.AddWithValue("$email", email ?? string.Empty);
            var r = cmd.ExecuteScalar();
            if (r == null) return false;
            return r.ToString() == Hash(password);
        }

        public static bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, PasswordHash FROM ApplicantAccounts WHERE lower(Email)=lower($email)";
            cmd.Parameters.AddWithValue("$email", email ?? string.Empty);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return false;
            var id = reader.GetString(0);
            var ph = reader.GetString(1);
            if (ph != Hash(oldPassword)) return false;
            reader.Close();
            using var upd = conn.CreateCommand();
            upd.CommandText = "UPDATE ApplicantAccounts SET PasswordHash=$ph WHERE Id=$id";
            upd.Parameters.AddWithValue("$ph", Hash(newPassword));
            upd.Parameters.AddWithValue("$id", id);
            upd.ExecuteNonQuery();
            return true;
        }

        private static string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input ?? string.Empty);
            var hashed = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hashed);
        }
    }

    // ProfileService using SQLite
    public static class ProfileService
    {
        public static Applicant GetByEmail(string email)
        {
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, FirstName, LastName, Email, Phone, Address FROM Applicants WHERE lower(Email)=lower($email)";
            cmd.Parameters.AddWithValue("$email", email ?? string.Empty);
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;
            var a = new Applicant
            {
                Id = r.GetString(0),
                FirstName = r.IsDBNull(1) ? string.Empty : r.GetString(1),
                LastName = r.IsDBNull(2) ? string.Empty : r.GetString(2),
                Email = r.IsDBNull(3) ? string.Empty : r.GetString(3),
                Phone = r.IsDBNull(4) ? string.Empty : r.GetString(4),
                Address = r.IsDBNull(5) ? string.Empty : r.GetString(5)
            };
            r.Close();

            // load education
            using var cmdE = conn.CreateCommand();
            cmdE.CommandText = "SELECT Institution, Degree, FieldOfStudy, Year FROM Education WHERE ApplicantId=$aid";
            cmdE.Parameters.AddWithValue("$aid", a.Id);
            using var re = cmdE.ExecuteReader();
            while (re.Read())
            {
                a.Education.Add(new EducationEntry { Institution = re.GetString(0), Degree = re.GetString(1), FieldOfStudy = re.GetString(2), Year = re.GetInt32(3) });
            }
            re.Close();

            using var cmdS = conn.CreateCommand();
            cmdS.CommandText = "SELECT Skill FROM Skills WHERE ApplicantId=$aid";
            cmdS.Parameters.AddWithValue("$aid", a.Id);
            using var rs = cmdS.ExecuteReader();
            while (rs.Read()) a.Skills.Add(rs.GetString(0));
            rs.Close();

            using var cmdW = conn.CreateCommand();
            cmdW.CommandText = "SELECT Company, Title, Description, StartYear, EndYear FROM WorkExperiences WHERE ApplicantId=$aid";
            cmdW.Parameters.AddWithValue("$aid", a.Id);
            using var rw = cmdW.ExecuteReader();
            while (rw.Read())
            {
                var ey = rw.IsDBNull(4) ? null : (int?)rw.GetInt32(4);
                a.WorkExperiences.Add(new WorkExperience { Company = rw.GetString(0), Title = rw.GetString(1), Description = rw.GetString(2), StartYear = rw.GetInt32(3), EndYear = ey });
            }
            rw.Close();

            return a;
        }

        public static void UpdatePersonalInfo(string applicantId, string firstName, string lastName, string phone, string address)
        {
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Applicants SET FirstName=$fn, LastName=$ln, Phone=$phone, Address=$addr WHERE Id=$id";
            cmd.Parameters.AddWithValue("$fn", firstName ?? string.Empty);
            cmd.Parameters.AddWithValue("$ln", lastName ?? string.Empty);
            cmd.Parameters.AddWithValue("$phone", phone ?? string.Empty);
            cmd.Parameters.AddWithValue("$addr", address ?? string.Empty);
            cmd.Parameters.AddWithValue("$id", applicantId);
            cmd.ExecuteNonQuery();
        }

        public static void AddEducation(string applicantId, EducationEntry e)
        {
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Education (ApplicantId, Institution, Degree, FieldOfStudy, Year) VALUES ($aid, $inst, $deg, $fos, $yr)";
            cmd.Parameters.AddWithValue("$aid", applicantId);
            cmd.Parameters.AddWithValue("$inst", e.Institution ?? string.Empty);
            cmd.Parameters.AddWithValue("$deg", e.Degree ?? string.Empty);
            cmd.Parameters.AddWithValue("$fos", e.FieldOfStudy ?? string.Empty);
            cmd.Parameters.AddWithValue("$yr", e.Year);
            cmd.ExecuteNonQuery();
        }

        public static void AddSkill(string applicantId, string skill)
        {
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Skills (ApplicantId, Skill) VALUES ($aid, $skill)";
            cmd.Parameters.AddWithValue("$aid", applicantId);
            cmd.Parameters.AddWithValue("$skill", skill ?? string.Empty);
            cmd.ExecuteNonQuery();
        }

        public static void AddWorkExperience(string applicantId, WorkExperience w)
        {
            using var conn = Db.GetConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO WorkExperiences (ApplicantId, Company, Title, Description, StartYear, EndYear) VALUES ($aid, $co, $ti, $de, $sy, $ey)";
            cmd.Parameters.AddWithValue("$aid", applicantId);
            cmd.Parameters.AddWithValue("$co", w.Company ?? string.Empty);
            cmd.Parameters.AddWithValue("$ti", w.Title ?? string.Empty);
            cmd.Parameters.AddWithValue("$de", w.Description ?? string.Empty);
            cmd.Parameters.AddWithValue("$sy", w.StartYear);
            if (w.EndYear.HasValue) cmd.Parameters.AddWithValue("$ey", w.EndYear.Value); else cmd.Parameters.AddWithValue("$ey", DBNull.Value);
            cmd.ExecuteNonQuery();
        }
    }

    // Program & CLI
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Db.EnsureDatabase();
            if (args != null && args.Length > 0 && args[0].Equals("ui", StringComparison.OrdinalIgnoreCase))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                return;
            }
            Console.WriteLine("— Applicant Registration & Profile (SQLite-backed demo)");
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Forms");
                Console.WriteLine("1) Applicant Registration");
                Console.WriteLine("2) My Profile");
                Console.WriteLine("3) Exit");
                Console.Write("Select: ");
                var key = Console.ReadLine();
                if (key == "1") HandleRegistration();
                else if (key == "2") HandleLogin();
                else if (key == "3") break;
                else Console.WriteLine("Invalid option.");
            }
        }

        private static void HandleRegistration()
        {
            Console.WriteLine("--- Applicant Registration ---");
            Console.Write("Email: ");
            var email = Console.ReadLine()?.Trim();
            Console.Write("Password: ");
            var pwd = ReadPassword();
            Console.Write("Confirm password: ");
            var pwd2 = ReadPassword();
            if (pwd != pwd2) { Console.WriteLine("Passwords do not match."); return; }
            var (success, message) = AccountService.CreateAccount(email, pwd);
            Console.WriteLine(message);
        }

        private static void HandleLogin()
        {
            Console.WriteLine("--- Login to My Profile ---");
            Console.Write("Email: ");
            var email = Console.ReadLine()?.Trim();
            Console.Write("Password: ");
            var pwd = ReadPassword();
            if (!AccountService.VerifyCredentials(email, pwd)) { Console.WriteLine("Invalid credentials."); return; }
            var applicant = ProfileService.GetByEmail(email);
            if (applicant == null) { Console.WriteLine("Profile not found."); return; }
            ProfileMenu(applicant);
        }

        private static void ProfileMenu(Applicant applicant)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"My Profile - {applicant.Email}");
                Console.WriteLine("1) View personal information");
                Console.WriteLine("2) Edit personal information");
                Console.WriteLine("3) Add education");
                Console.WriteLine("4) Add skill");
                Console.WriteLine("5) Add work experience");
                Console.WriteLine("6) Change password");
                Console.WriteLine("7) Logout");
                Console.Write("Select: ");
                var sel = Console.ReadLine();
                if (sel == "1") DisplayProfile(applicant);
                else if (sel == "2") EditPersonalInfo(applicant);
                else if (sel == "3") AddEducation(applicant);
                else if (sel == "4") AddSkill(applicant);
                else if (sel == "5") AddWorkExperience(applicant);
                else if (sel == "6") ChangePassword(applicant.Email);
                else if (sel == "7") break;
                else Console.WriteLine("Invalid option.");
            }
        }

        private static void DisplayProfile(Applicant a)
        {
            var refreshed = ProfileService.GetByEmail(a.Email);
            if (refreshed != null) a = refreshed;
            Console.WriteLine("--- Personal Information ---");
            Console.WriteLine($"Name: {a.FirstName} {a.LastName}");
            Console.WriteLine($"Email: {a.Email}");
            Console.WriteLine($"Phone: {a.Phone}");
            Console.WriteLine($"Address: {a.Address}");
            Console.WriteLine();
            Console.WriteLine("Education:");
            if (a.Education.Count == 0) Console.WriteLine(" (none)"); else a.Education.ForEach(e => Console.WriteLine($" - {e}"));
            Console.WriteLine("Skills:");
            if (a.Skills.Count == 0) Console.WriteLine(" (none)"); else a.Skills.ForEach(s => Console.WriteLine($" - {s}"));
            Console.WriteLine("Work experience:");
            if (a.WorkExperiences.Count == 0) Console.WriteLine(" (none)"); else a.WorkExperiences.ForEach(w => Console.WriteLine($" - {w}"));
        }

        private static void EditPersonalInfo(Applicant a)
        {
            Console.Write("First name: ");
            var fn = Console.ReadLine()?.Trim();
            Console.Write("Last name: ");
            var ln = Console.ReadLine()?.Trim();
            Console.Write("Phone: ");
            var phone = Console.ReadLine()?.Trim();
            Console.Write("Address: ");
            var addr = Console.ReadLine()?.Trim();
            ProfileService.UpdatePersonalInfo(a.Id, fn, ln, phone, addr);
            Console.WriteLine("Personal information updated.");
        }

        private static void AddEducation(Applicant a)
        {
            Console.Write("Institution: ");
            var inst = Console.ReadLine()?.Trim();
            Console.Write("Degree: ");
            var degree = Console.ReadLine()?.Trim();
            Console.Write("Field of study: ");
            var field = Console.ReadLine()?.Trim();
            Console.Write("Year: ");
            var yearStr = Console.ReadLine()?.Trim();
            int.TryParse(yearStr, out var year);
            ProfileService.AddEducation(a.Id, new EducationEntry { Institution = inst, Degree = degree, FieldOfStudy = field, Year = year });
            Console.WriteLine("Education added.");
        }

        private static void AddSkill(Applicant a)
        {
            Console.Write("Skill: ");
            var skill = Console.ReadLine()?.Trim();
            ProfileService.AddSkill(a.Id, skill);
            Console.WriteLine("Skill added.");
        }

        private static void AddWorkExperience(Applicant a)
        {
            Console.Write("Company: ");
            var co = Console.ReadLine()?.Trim();
            Console.Write("Title: ");
            var title = Console.ReadLine()?.Trim();
            Console.Write("Description: ");
            var desc = Console.ReadLine()?.Trim();
            Console.Write("Start year: ");
            var syStr = Console.ReadLine()?.Trim(); int.TryParse(syStr, out var sy);
            Console.Write("End year (blank if present): ");
            var eyStr = Console.ReadLine()?.Trim(); int.TryParse(eyStr, out var ey); int? eyN = string.IsNullOrWhiteSpace(eyStr) ? null : ey;
            ProfileService.AddWorkExperience(a.Id, new WorkExperience { Company = co, Title = title, Description = desc, StartYear = sy, EndYear = eyN });
            Console.WriteLine("Work experience added.");
        }

        private static void ChangePassword(string email)
        {
            Console.Write("Old password: ");
            var oldp = ReadPassword();
            Console.Write("New password: ");
            var newp = ReadPassword();
            Console.Write("Confirm new password: ");
            var c = ReadPassword();
            if (newp != c) { Console.WriteLine("New password confirmation does not match."); return; }
            if (!AccountService.ChangePassword(email, oldp, newp)) Console.WriteLine("Password change failed (wrong old password).");
            else Console.WriteLine("Password changed successfully.");
        }

        private static string ReadPassword()
        {
            var sb = new StringBuilder();
            while (true)
            {
                var k = Console.ReadKey(true);
                if (k.Key == ConsoleKey.Enter) break;
                if (k.Key == ConsoleKey.Backspace && sb.Length > 0) { sb.Length--; Console.Write("\b \b"); continue; }
                if (char.IsControl(k.KeyChar)) continue;
                sb.Append(k.KeyChar);
                Console.Write("*");
            }
            Console.WriteLine();
            return sb.ToString();
        }
    }
}
