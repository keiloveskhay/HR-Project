using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class login_form : Form
    {
        public login_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // VALIDATION
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter your email.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter your password.");
                return;
            }

            try
            {
                using (MySqlConnection conn =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    // FIXED COLUMN NAME ONLY
                    string query = @"
                        SELECT UserID, FullName, Email, UserType
                        FROM Users
                        WHERE Email = @Email AND PasswordHash = @Password
                        LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // STORE SESSION DATA
                                Session.UserId = Convert.ToInt32(reader["UserID"]);
                                Session.Username = reader["Email"].ToString();
                                Session.FullName = reader["FullName"].ToString();

                                Session.Role = reader["UserType"] != DBNull.Value
                                    ? reader["UserType"].ToString()
                                    : "User";

                                // OPEN DASHBOARD (UPDATED ROUTING ONLY)
                                this.Hide();

                                if (Session.Role == "Admin")
                                {
                                    HR_Dashboard dashboard = new HR_Dashboard();
                                    dashboard.Show();
                                }
                                else if (Session.Role == "HR")
                                {
                                    HR_Dashboard dashboard = new HR_Dashboard();
                                    dashboard.Show();
                                }
                                else if (Session.Role == "Applicant")
                                {
                                    Applicant_Dashboard dashboard = new Applicant_Dashboard();
                                    dashboard.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Unknown user role.");
                                    this.Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid email or password.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            MainForm registerForm = new MainForm();
            registerForm.Show();
        }
    }
}