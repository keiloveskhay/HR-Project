using System;
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

        private void btnSubmitEval_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppID.Text) || string.IsNullOrWhiteSpace(txtScore.Text))
            {
                MessageBox.Show("Please enter the Schedule ID and the numerical score.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int scheduleID = Convert.ToInt32(txtAppID.Text);
            decimal score = Convert.ToDecimal(txtScore.Text);
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
                    cmdEval.Parameters.AddWithValue("@EvaluatedBy", Session.UserId);
                    cmdEval.Parameters.AddWithValue("@Score", score);
                    cmdEval.Parameters.AddWithValue("@Remarks", remarks);
                    cmdEval.Parameters.AddWithValue("@Result", result);
                    cmdEval.ExecuteNonQuery();

                    MessageBox.Show("Evaluation submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtAppID.Clear();
                    txtScore.Clear();
                    txtFeedback.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}