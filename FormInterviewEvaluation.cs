using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class FormInterviewEvaluation : Form
    {
        public FormInterviewEvaluation()
        {
            InitializeComponent();
        }

        private void FormInterviewEvaluation_Load(object sender, EventArgs e)
        {
            LoadScheduledInterviews();
        }

        private void LoadScheduledInterviews()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            s.ScheduleID,
                            a.ApplicationID,
                            CONCAT(ap.FirstName, ' ', ap.LastName) AS 'Applicant Name',
                            jv.JobTitle AS 'Position',
                            s.InterviewDate AS 'Date',
                            s.Interviewer
                        FROM InterviewSchedules s
                        INNER JOIN Applications a ON s.ApplicationID = a.ApplicationID
                        INNER JOIN Applicants ap ON a.ApplicantID = ap.ApplicantID
                        INNER JOIN JobVacancies jv ON a.VacancyID = jv.VacancyID
                        ORDER BY s.InterviewDate DESC";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvScheduled.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading interviews: " + ex.Message);
            }
        }

        private void dgvScheduled_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvScheduled.CurrentRow != null && dgvScheduled.CurrentRow.Cells["ScheduleID"].Value != DBNull.Value)
            {
                int scheduleId = Convert.ToInt32(dgvScheduled.CurrentRow.Cells["ScheduleID"].Value);
                txtAppID.Text = scheduleId.ToString();

                CheckExistingEvaluation(scheduleId);
            }
        }

        private void CheckExistingEvaluation(int scheduleId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = "SELECT Score, Remarks FROM InterviewEvaluations WHERE ScheduleID = @ScheduleID LIMIT 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ScheduleID", scheduleId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtScore.Text = reader["Score"].ToString();
                                txtFeedback.Text = reader["Remarks"].ToString();
                                
                                txtScore.ReadOnly = true;
                                txtFeedback.ReadOnly = true;
                                btnSubmitEval.Enabled = false;
                                btnSubmitEval.Text = "Already Evaluated";
                            }
                            else
                            {
                                txtScore.Clear();
                                txtFeedback.Clear();
                                
                                txtScore.ReadOnly = false;
                                txtFeedback.ReadOnly = false;
                                btnSubmitEval.Enabled = true;
                                btnSubmitEval.Text = "Submit Interview Evaluation";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking evaluation: " + ex.Message);
            }
        }

        private void btnSubmitEval_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppID.Text) || string.IsNullOrWhiteSpace(txtScore.Text))
            {
                MessageBox.Show("Please select a schedule and enter a numerical score.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int scheduleID = Convert.ToInt32(txtAppID.Text);
            
            if (!decimal.TryParse(txtScore.Text, out decimal score))
            {
                MessageBox.Show("Invalid score. Please enter a number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string remarks = txtFeedback.Text;
            string result = score >= 75 ? "Pass" : "Fail";

            try
            {
                using (MySqlConnection conn =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string insertEval = @"
                        INSERT INTO InterviewEvaluations
                        (ScheduleID, EvaluatedBy, Score, Remarks, Result)
                        VALUES
                        (@ScheduleID, @EvaluatedBy, @Score, @Remarks, @Result)";

                    MySqlCommand cmdEval = new MySqlCommand(insertEval, conn);
                    cmdEval.Parameters.AddWithValue("@ScheduleID", scheduleID);
                    cmdEval.Parameters.AddWithValue("@EvaluatedBy", Session.UserId > 0 ? Session.UserId : 1);
                    cmdEval.Parameters.AddWithValue("@Score", score);
                    cmdEval.Parameters.AddWithValue("@Remarks", remarks);
                    cmdEval.Parameters.AddWithValue("@Result", result);
                    cmdEval.ExecuteNonQuery();

                    string updateSchedule = "UPDATE InterviewSchedules SET Status = 'Completed' WHERE ScheduleID = @ScheduleID";
                    MySqlCommand cmdSched = new MySqlCommand(updateSchedule, conn);
                    cmdSched.Parameters.AddWithValue("@ScheduleID", scheduleID);
                    cmdSched.ExecuteNonQuery();

                    string updateApp = @"
                        UPDATE Applications a
                        INNER JOIN InterviewSchedules s ON a.ApplicationID = s.ApplicationID
                        SET a.StatusID = 6, a.Status = 'For Assessment'
                        WHERE s.ScheduleID = @ScheduleID";
                    MySqlCommand cmdApp = new MySqlCommand(updateApp, conn);
                    cmdApp.Parameters.AddWithValue("@ScheduleID", scheduleID);
                    cmdApp.ExecuteNonQuery();

                    string historyQuery = @"
                        INSERT INTO ApplicationStatusHistory (ApplicationID, NewStatus, ChangedBy, Remarks)
                        SELECT ApplicationID, 'For Assessment', 'HR Staff', CONCAT('Interview Evaluated: ', @Result)
                        FROM InterviewSchedules WHERE ScheduleID = @ScheduleID";
                    MySqlCommand histCmd = new MySqlCommand(historyQuery, conn);
                    histCmd.Parameters.AddWithValue("@ScheduleID", scheduleID);
                    histCmd.Parameters.AddWithValue("@Result", result);
                    histCmd.ExecuteNonQuery();

                    MessageBox.Show("Evaluation submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh logic
                    CheckExistingEvaluation(scheduleID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}