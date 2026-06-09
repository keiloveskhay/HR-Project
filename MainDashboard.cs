using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Recruitment_Workflow_Jared
{
    public partial class FormInterviewEvaluation : Form
    {
        
        DatabaseConnection db = new DatabaseConnection();

        public FormInterviewEvaluation()
        {
            InitializeComponent();
        }

        private void btnSubmitEval_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(txtAppID.Text) || string.IsNullOrWhiteSpace(txtScore.Text))
            {
                MessageBox.Show("Please enter the Application ID and the numerical score.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int appID = Convert.ToInt32(txtAppID.Text);
            int score = Convert.ToInt32(txtScore.Text);
            string feedback = txtFeedback.Text;

            try
            {
                using (MySqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    
                    string insertEval = "INSERT INTO InterviewEvaluations (ApplicationID, Score, Feedback) VALUES (@AppID, @Score, @Feedback)";
                    MySqlCommand cmdEval = new MySqlCommand(insertEval, conn);
                    cmdEval.Parameters.AddWithValue("@AppID", appID);
                    cmdEval.Parameters.AddWithValue("@Score", score);
                    cmdEval.Parameters.AddWithValue("@Feedback", feedback);
                    cmdEval.ExecuteNonQuery();

                    string historyQuery = "INSERT INTO ApplicationStatusHistory (ApplicationID, NewStatus, ChangedBy, Remarks) VALUES (@AppID, 'Interview Evaluated', 'HR Staff', @Remarks)";
                    MySqlCommand histCmd = new MySqlCommand(historyQuery, conn);
                    histCmd.Parameters.AddWithValue("@AppID", appID);
                    histCmd.Parameters.AddWithValue("@Remarks", "Score: " + score.ToString() + " - " + feedback);
                    histCmd.ExecuteNonQuery();

                    MessageBox.Show("Evaluation submitted successfully! HR workflow complete.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
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