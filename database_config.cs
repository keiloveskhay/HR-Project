using MySql.Data.MySqlClient;

namespace HR_Project
{
    public static class DatabaseConfig
    {
        public static string ConnectionString =
            "server=localhost;database=hr_applicant_system;uid=root;pwd=09080706;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}