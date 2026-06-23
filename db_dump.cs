using System;
using MySql.Data.MySqlClient;
using System.IO;

class Program
{
    static void Main()
    {
        string connStr = "server=localhost;database=hr_applicant_system;uid=root;pwd=09080706;";
        try
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM ApplicationStatuses";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    using (StreamWriter writer = new StreamWriter("db_dump.txt"))
                    {
                        while (reader.Read())
                        {
                            writer.WriteLine($"{reader[0]} | {reader[1]}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            File.WriteAllText("db_dump.txt", ex.Message);
        }
    }
}
