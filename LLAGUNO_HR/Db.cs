using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace IDK2
{
    public static class Db
    {
        private static string DbPath => Path.Combine(AppContext.BaseDirectory, "admin.db");

        public static void EnsureDatabase()
        {
            var cs = new SqliteConnectionStringBuilder { DataSource = DbPath }.ToString();
            using var conn = new SqliteConnection(cs);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Departments (
                Id TEXT PRIMARY KEY,
                Name TEXT UNIQUE
            );
            CREATE TABLE IF NOT EXISTS Roles (
                Id TEXT PRIMARY KEY,
                DepartmentId TEXT,
                Name TEXT
            );
            CREATE TABLE IF NOT EXISTS EmploymentTypes (
                Id TEXT PRIMARY KEY,
                Name TEXT UNIQUE
            );
            CREATE TABLE IF NOT EXISTS Vacancies (
                Id TEXT PRIMARY KEY,
                Title TEXT,
                DepartmentId TEXT,
                RoleId TEXT,
                EmploymentType TEXT,
                Description TEXT,
                OpenDate TEXT,
                CloseDate TEXT,
                Status TEXT
            );
            CREATE TABLE IF NOT EXISTS HiringDecisions (
                Id TEXT PRIMARY KEY,
                VacancyId TEXT,
                CandidateName TEXT,
                CandidateEmail TEXT,
                Decision TEXT,
                DecisionDate TEXT,
                Notes TEXT
            );
            CREATE TABLE IF NOT EXISTS AuditTrail (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Timestamp TEXT,
                Actor TEXT,
                Action TEXT,
                Details TEXT
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
}
