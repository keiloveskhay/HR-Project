using System;
using System.Data;
using MySql.Data.MySqlClient;

public class DBHelper
{
    // Ensure this connection string matches your local MySQL server setup
    private static string connectionString = "Server=localhost;Port=3306;Database=hr_applicant_system;Uid=root;Pwd=Jimin00#;";

    public static DataTable ExecuteQuery(string query, params MySqlParameter[] parameters)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }

    public static void ExecuteNonQuery(string query, params MySqlParameter[] parameters)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
            }
        }
    }
}