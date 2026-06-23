using HR_Project;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace HR_Recruitment_Workflow_Jared
{
    public partial class FormScreening : Form
    {
        public FormScreening()
        {
            InitializeComponent();
        }

        private void FormScreening_Load(object sender, EventArgs e)
        {
            LoadApplications();
        }

        private void LoadApplications()
        {
            try
            {
                using (MySqlConnection conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            a.ApplicationID,
                            CONCAT(ap.FirstName, ' ', ap.LastName) AS 'Applicant Name',
                            jv.JobTitle AS 'Position',
                            a.SubmittedAt AS 'Date Applied'
                        FROM Applications a
                        INNER JOIN Applicants ap ON a.ApplicantID = ap.ApplicantID
                        INNER JOIN JobVacancies jv ON a.VacancyID = jv.VacancyID
                        INNER JOIN ApplicationStatuses s ON a.StatusID = s.StatusID
                        WHERE s.StatusName = 'Under Review'";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvScreening.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading applications: " + ex.Message);
            }
        }

        private void dgvScreening_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvScreening.CurrentRow != null && dgvScreening.CurrentRow.Cells["ApplicationID"].Value != DBNull.Value)
            {
                txtAppID.Text = dgvScreening.CurrentRow.Cells["ApplicationID"].Value.ToString();
            }
        }

        private void btnSubmitScreening_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppID.Text) || cmbResult.SelectedItem == null)
            {
                MessageBox.Show("Please select an application and a result.");
                return;
            }

            if (!int.TryParse(txtAppID.Text, out int appID))
            {
                MessageBox.Show("Invalid Application ID.");
                return;
            }

            string result = cmbResult.SelectedItem.ToString();
            string remarks = txtRemarks.Text.Trim();

            int newStatusId = (result == "Qualified") ? 4 : 9;
            int screenBy = Session.UserId > 0 ? Session.UserId : 2;

            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string insertScreening = @"
                        INSERT INTO ScreeningResults
                        (ApplicationID, ScreenedBy, Result, Remarks, ScreenedAt)
                        VALUES
                        (@AppID, @ScreenedBy, @Result, @Remarks, NOW())";

                    MySqlCommand cmdScreening = new MySqlCommand(insertScreening, conn);
                    cmdScreening.Parameters.AddWithValue("@AppID", appID);
                    cmdScreening.Parameters.AddWithValue("@ScreenedBy", screenBy);
                    cmdScreening.Parameters.AddWithValue("@Result", result);
                    cmdScreening.Parameters.AddWithValue("@Remarks", remarks);
                    cmdScreening.ExecuteNonQuery();

                    string updateApp = @"
                        UPDATE Applications
                        SET StatusID = @StatusID
                        WHERE ApplicationID = @AppID";

                    MySqlCommand cmdUpdate = new MySqlCommand(updateApp, conn);
                    cmdUpdate.Parameters.AddWithValue("@StatusID", newStatusId);
                    cmdUpdate.Parameters.AddWithValue("@AppID", appID);
                    cmdUpdate.ExecuteNonQuery();

                    MessageBox.Show("Screening completed successfully.");

                    txtAppID.Clear();
                    cmbResult.SelectedIndex = -1;
                    txtRemarks.Clear();
                    
                    LoadApplications(); // Refresh list
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }
    }
}