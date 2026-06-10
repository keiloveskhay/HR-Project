using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class ManageProfileForm : Form
    {
        private readonly string _email;
        private int _applicantId;

        public ManageProfileForm(string email)
        {
            _email = email;

            InitializeComponent();

            LoadData();

            saveBtn.Click += SaveProfile;
            changePasswordBtn.Click += ChangePassword;
            closeBtn.Click += (s, e) => Close();
        }

        private void LoadData()
        {
            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT p.*
                        FROM ApplicantProfiles p
                        INNER JOIN Users u ON u.UserID = p.UserID
                        WHERE u.Email = @Email
                        LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", _email);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Profile not found.");
                                Close();
                                return;
                            }

                            _applicantId = Convert.ToInt32(reader["ApplicantID"]);

                            educationBox.Text = reader["HighestEducation"]?.ToString() ?? "";
                            schoolBox.Text = reader["SchoolName"]?.ToString() ?? "";
                            yearBox.Text = reader["YearGraduated"]?.ToString() ?? "";
                            skillsBox.Text = reader["Skills"]?.ToString() ?? "";
                            workBox.Text = reader["WorkExperience"]?.ToString() ?? "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load profile:\n" + ex.Message);
                Close();
            }
        }

        private void SaveProfile(object sender, EventArgs e)
        {
            if (_applicantId <= 0)
            {
                MessageBox.Show("Invalid profile session.");
                return;
            }

            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        UPDATE ApplicantProfiles
                        SET
                            HighestEducation=@Edu,
                            SchoolName=@School,
                            YearGraduated=@Year,
                            Skills=@Skills,
                            WorkExperience=@Work
                        WHERE ApplicantID=@Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", _applicantId);
                        cmd.Parameters.AddWithValue("@Edu", educationBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@School", schoolBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@Year", yearBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@Skills", skillsBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@Work", workBox.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Profile updated successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save profile:\n" + ex.Message);
            }
        }

        private void ChangePassword(object sender, EventArgs e)
        {
            using (var f = new ChangePasswordForm(_applicantId))
            {
                f.ShowDialog();
            }
        }
    }
}