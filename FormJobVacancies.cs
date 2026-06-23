using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class FormJobVacancies : Form
    {
        private int currentApplicantID;

        public FormJobVacancies()
        {
            InitializeComponent();
            currentApplicantID = Session.ApplicantId;
        }

        private void FormJobVacancies_Load(object sender, EventArgs e)
        {
            LoadData(string.Empty);
        }

        private void LoadData(string keyword)
        {
            using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();

                string query = @"SELECT VacancyID, JobTitle, EmploymentType, Description, Qualifications, Slots, Status
                                 FROM JobVacancies
                                 WHERE Status = 'Open' AND JobTitle LIKE @keyword";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvJobVacancies.DataSource = dt;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (currentApplicantID <= 0)
            {
                MessageBox.Show("Please login as an applicant first before applying.");
                return;
            }

            if (dgvJobVacancies.CurrentRow != null &&
                dgvJobVacancies.CurrentRow.Cells["VacancyID"].Value != null)
            {
                int vacancyID = Convert.ToInt32(dgvJobVacancies.CurrentRow.Cells["VacancyID"].Value);

                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string checkQuery = @"SELECT a.ApplicationID, a.StatusID, j.Status as VacancyStatus 
                                          FROM Applications a 
                                          INNER JOIN JobVacancies j ON a.VacancyID = j.VacancyID
                                          WHERE a.ApplicantID = @ApplicantID 
                                          AND a.VacancyID = @VacancyID";

                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@ApplicantID", currentApplicantID);
                    checkCmd.Parameters.AddWithValue("@VacancyID", vacancyID);

                    // Check if the vacancy itself was closed right before applying
                    string checkStatusQuery = "SELECT Status FROM JobVacancies WHERE VacancyID = @VacancyID";
                    using (MySqlCommand statusCmd = new MySqlCommand(checkStatusQuery, conn))
                    {
                        statusCmd.Parameters.AddWithValue("@VacancyID", vacancyID);
                        string currentStatus = statusCmd.ExecuteScalar()?.ToString();
                        if (currentStatus != "Open")
                        {
                            MessageBox.Show("This job vacancy is no longer accepting applications.");
                            return;
                        }
                    }

                    using (var reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int statusId = reader["StatusID"] != DBNull.Value ? Convert.ToInt32(reader["StatusID"]) : 0;
                            int appId = Convert.ToInt32(reader["ApplicationID"]);
                            
                            if (statusId == 10)
                            {
                                reader.Close();
                                string updateQuery = "UPDATE Applications SET StatusID = 1, Status = 'Draft', SubmittedAt = NULL WHERE ApplicationID = @AppID";
                                using (var updCmd = new MySqlCommand(updateQuery, conn))
                                {
                                    updCmd.Parameters.AddWithValue("@AppID", appId);
                                    updCmd.ExecuteNonQuery();
                                }
                                
                                string logQuery = "INSERT INTO ApplicationStatusHistory (ApplicationID, OldStatus, NewStatus, ChangedBy, Remarks) VALUES (@AppID, 'Withdrawn', 'Draft', 'Applicant', 'Re-applied for position')";
                                using (var logCmd = new MySqlCommand(logQuery, conn))
                                {
                                    logCmd.Parameters.AddWithValue("@AppID", appId);
                                    logCmd.ExecuteNonQuery();
                                }
                                
                                MessageBox.Show("Your withdrawn application has been reopened and saved as DRAFT successfully!");
                            }
                            else
                            {
                                MessageBox.Show("You have already applied for this position.");
                            }
                        }
                        else
                        {
                            reader.Close();
                            string insertQuery = @"INSERT INTO Applications (ApplicantID, VacancyID, Status, StatusID)
                                                   VALUES (@ApplicantID, @VacancyID, 'Draft', 1)";

                            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                            insertCmd.Parameters.AddWithValue("@ApplicantID", currentApplicantID);
                            insertCmd.Parameters.AddWithValue("@VacancyID", vacancyID);

                            insertCmd.ExecuteNonQuery();
                            long newAppId = insertCmd.LastInsertedId;

                            string logQuery = "INSERT INTO ApplicationStatusHistory (ApplicationID, NewStatus, ChangedBy, Remarks) VALUES (@AppID, 'Draft', 'Applicant', 'Application started')";
                            using (var logCmd = new MySqlCommand(logQuery, conn))
                            {
                                logCmd.Parameters.AddWithValue("@AppID", newAppId);
                                logCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Application saved as DRAFT successfully!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a job vacancy from the list first.");
            }
        }
    }
}