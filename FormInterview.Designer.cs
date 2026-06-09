using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HR_Recruitment_Workflow_Jared
{
    public class DatabaseConnection
    {
        private string connectionString = "Server=localhost;Database=hr_applicant_system;Uid=root;Pwd=admin;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Failed: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}