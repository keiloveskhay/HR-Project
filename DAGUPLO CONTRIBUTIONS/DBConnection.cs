using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRApplicationSystem
{
    internal class DBConnection
    {
        public static MySqlConnection GetConnection()
        {
            string conn = "server=localhost;database=hr_applicant_system;uid=root;pwd=erimysql123!;";
            return new MySqlConnection(conn);
        }
    }
}
