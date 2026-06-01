using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public static class DatabaseConfig
    {
        public static string ConnectionString =
            "server=localhost;database=hr_applicant_system;uid=root;pwd=Ckvolleyball050924!;";
    }
}