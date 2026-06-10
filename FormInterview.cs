using HR_Project;
using MySql.Data.MySqlClient;
using System;
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
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string insertInterview = @"
                        INSERT INTO InterviewSchedules 
                        (ApplicationID, InterviewDate, Interviewer, Status)
                        VALUES (@AppID, @Date, @Interviewer, 'Scheduled')";

                    using (MySqlCommand cmdInterview = new MySqlCommand(insertInterview, conn))
                    {
                        cmdInterview.Parameters.AddWithValue("@AppID", appID);
                        cmdInterview.Parameters.AddWithValue("@Date", interviewDate);
                        cmdInterview.Parameters.AddWithValue("@Interviewer", interviewer);
                        cmdInterview.ExecuteNonQuery();
                    }

                    string updateApp = @"
                        UPDATE Applications 
                        SET Status = 'Interview Scheduled' 
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
                        (@AppID, 'Interview Scheduled', 'HR Staff',
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }
    }
}