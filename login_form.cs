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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = txtUsername.Text;
            string password = txtPassword.Text;

            using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                try
                {
                    conn.Open();
                    
                    // 1. Check if it's an employee (HR/Admin)
                    string hrQuery = "SELECT UserID, RoleID, Email FROM Users WHERE Email = @Email AND Password = @Password";
                    using (MySqlCommand hrCmd = new MySqlCommand(hrQuery, conn))
                    {
                        hrCmd.Parameters.AddWithValue("@Email", usernameOrEmail);
                        hrCmd.Parameters.AddWithValue("@Password", password);
                        
                        using (var reader = hrCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = Convert.ToInt32(reader["UserID"]);
                                int roleId = Convert.ToInt32(reader["RoleID"]);
                                string email = reader["Email"].ToString();
                                
                                Session.UserId = userId;
                                Session.Username = email;
                                
                                if (roleId == 1) Session.Role = "HR Staff";
                                else if (roleId == 2) Session.Role = "HR Manager";
                                else if (roleId == 3) Session.Role = "Admin";
                                else Session.Role = "Unknown";
                                
                                reader.Close();
                                
                                HR_Dashboard hrDash = new HR_Dashboard();
                                hrDash.Show();
                                this.Hide();
                                return; // Exit method
                            }
                        }
                    }

                    // 2. Check if it's an applicant
                    string authQuery = "SELECT AccountID FROM ApplicantAccounts WHERE Email = @Email AND Password = @Password";
                    using (MySqlCommand authCmd = new MySqlCommand(authQuery, conn))
                    {
                        authCmd.Parameters.AddWithValue("@Email", usernameOrEmail);
                        authCmd.Parameters.AddWithValue("@Password", password);
                        object accountIdResult = authCmd.ExecuteScalar();

                        if (accountIdResult != null)
                        {
                            int accId = Convert.ToInt32(accountIdResult);
                            Session.UserId = accId;
                            Session.Username = usernameOrEmail;
                            Session.Role = "Applicant";

                            string idQuery = "SELECT ApplicantID FROM Applicants WHERE AccountID = @AccID";
                            using (MySqlCommand idCmd = new MySqlCommand(idQuery, conn))
                            {
                                idCmd.Parameters.AddWithValue("@AccID", accId);
                                object applicantIdResult = idCmd.ExecuteScalar();
                                if (applicantIdResult != null)
                                {
                                    Session.ApplicantId = Convert.ToInt32(applicantIdResult);
                                }
                            }
                            Applicant_Dashboard dash = new Applicant_Dashboard();
                            dash.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username/email or password.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Opens the MainForm (the one you provided earlier)
            MainForm registerForm = new MainForm();
            registerForm.Show();
        }
    }
}