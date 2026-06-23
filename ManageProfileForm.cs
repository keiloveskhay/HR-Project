using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

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
        }

        private void LoadData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT a.* FROM applicants a
                        INNER JOIN applicantaccounts aa ON a.AccountID = aa.AccountID
                        WHERE aa.Email = @Email LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", _email);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _applicantId = Convert.ToInt32(reader["ApplicantID"]);
                                
                                string fName = reader["FirstName"]?.ToString() ?? "";
                                string lName = reader["LastName"]?.ToString() ?? "";
                                fullNameBox.Text = string.IsNullOrWhiteSpace(fName + lName) ? "" : $"{fName} {lName}".Trim();

                                educationBox.Text = reader["HighestEducation"]?.ToString();
                                schoolBox.Text = reader["SchoolName"]?.ToString();
                                yearBox.Text = reader["YearGraduated"]?.ToString();
                                skillsBox.Text = reader["Skills"]?.ToString();
                                workBox.Text = reader["WorkExperience"]?.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading: " + ex.Message); }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string fullName = fullNameBox.Text.Trim();
                string firstName = fullName;
                string lastName = "";

                int spaceIdx = fullName.IndexOf(' ');
                if (spaceIdx > 0)
                {
                    firstName = fullName.Substring(0, spaceIdx).Trim();
                    lastName = fullName.Substring(spaceIdx + 1).Trim();
                }

                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE applicants SET 
                        FirstName = @First, LastName = @Last,
                        HighestEducation = @Edu, SchoolName = @School, 
                        YearGraduated = @Year, Skills = @Skills, WorkExperience = @Work
                        WHERE ApplicantID = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", _applicantId);
                        cmd.Parameters.AddWithValue("@First", firstName);
                        cmd.Parameters.AddWithValue("@Last", lastName);
                        cmd.Parameters.AddWithValue("@Edu", educationBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@School", schoolBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@Year", int.TryParse(yearBox.Text, out int y) ? y : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Skills", skillsBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@Work", workBox.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Profile updated!");
                }
            }
            catch (Exception ex) { MessageBox.Show("Error saving: " + ex.Message); }
        }

        private void changePasswordBtn_Click(object sender, EventArgs e)
        {
            using (var f = new ChangePasswordForm(_applicantId)) { f.ShowDialog(); }
        }
    }
}