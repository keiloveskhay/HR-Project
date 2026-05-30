using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class Form1 : Form
    {
        string connStr = "server=localhost;database=HR_ProjectDB;uid=root;pwd=;";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter a password.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    string query = "SELECT * FROM Users WHERE Username=@username AND Password=@password";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Applicant_Dashboard dashboard =
                            new Applicant_Dashboard();

                        dashboard.Show();

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }
    }
}
