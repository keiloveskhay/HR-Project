using HR_Project;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace HR_Recruitment_Workflow_Jared
{
    public partial class FormInterview : Form
    {
        string connectionString = DatabaseConfig.ConnectionString;

        public FormInterview()
        {
            InitializeComponent();
        }

        private void FormInterview_Load(object sender, EventArgs e)
        {
            LoadApplications();
        }

        private void LoadApplications()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
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
                        WHERE s.StatusID = 4";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvInterviews.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading applications: " + ex.Message);
            }
        }

        private void dgvInterviews_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInterviews.CurrentRow != null && dgvInterviews.CurrentRow.Cells["ApplicationID"].Value != DBNull.Value)
            {
                txtAppID.Text = dgvInterviews.CurrentRow.Cells["ApplicationID"].Value.ToString();
            }
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppID.Text) ||
                string.IsNullOrWhiteSpace(txtInterviewer.Text))
            {
                MessageBox.Show("Please enter Application ID and Interviewer name.");
                return;
            }

            if (!int.TryParse(txtAppID.Text, out int appID))
            {
                MessageBox.Show("Invalid Application ID.");
                return;
            }

            DateTime interviewDate = dtpInterviewDate.Value;
            string interviewer = txtInterviewer.Text.Trim();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string insertInterview = @"
                        INSERT INTO InterviewSchedules 
                        (ApplicationID, InterviewDate, InterviewTime, Interviewer, Status)
                        VALUES (@AppID, @Date, @Time, @Interviewer, 'Scheduled')";

                    using (MySqlCommand cmdInterview = new MySqlCommand(insertInterview, conn))
                    {
                        cmdInterview.Parameters.AddWithValue("@AppID", appID);
                        cmdInterview.Parameters.AddWithValue("@Date", interviewDate.Date);
                        cmdInterview.Parameters.AddWithValue("@Time", interviewDate.TimeOfDay);
                        cmdInterview.Parameters.AddWithValue("@Interviewer", interviewer);
                        cmdInterview.ExecuteNonQuery();
                    }

                    string updateApp = @"
                        UPDATE Applications 
                        SET StatusID = 5,
                            Status = 'For Interview'
                        WHERE ApplicationID = @AppID";

                    using (MySqlCommand cmdUpdate = new MySqlCommand(updateApp, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@AppID", appID);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    string historyQuery = @"
                        INSERT INTO ApplicationStatusHistory 
                        (ApplicationID, NewStatus, ChangedBy, Remarks)
                        VALUES 
                        (@AppID, 'For Interview', 'HR Staff',
                         CONCAT('Scheduled interview with: ', @Interviewer))";

                    using (MySqlCommand histCmd = new MySqlCommand(historyQuery, conn))
                    {
                        histCmd.Parameters.AddWithValue("@AppID", appID);
                        histCmd.Parameters.AddWithValue("@Interviewer", interviewer);
                        histCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Interview scheduled successfully!");

                    txtAppID.Clear();
                    txtInterviewer.Clear();
                    dtpInterviewDate.Value = DateTime.Now;

                    LoadApplications(); // refresh list
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }
    }
}