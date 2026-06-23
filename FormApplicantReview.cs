using HR_Project;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace HR_Recruitment_Workflow_Jared
{
    public partial class FormApplicantReview : Form
    {
        public FormApplicantReview()
        {
            InitializeComponent();
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppID.Text))
            {
                MessageBox.Show("Enter Application ID first.");
                return;
            }

            if (!int.TryParse(txtAppID.Text, out int appID))
            {
                MessageBox.Show("Invalid Application ID.");
                return;
            }

            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    int oldStatusId = GetCurrentStatus(conn, appID);
                    if (oldStatusId == -1)
                    {
                        MessageBox.Show("Application not found.");
                        return;
                    }

                    int underReviewStatusId = 3;

                    string updateApp = @"
                        UPDATE Applications 
                        SET StatusID = @StatusID 
                        WHERE ApplicationID = @AppID";

                    MySqlCommand cmdUpdate = new MySqlCommand(updateApp, conn);
                    cmdUpdate.Parameters.AddWithValue("@StatusID", underReviewStatusId);
                    cmdUpdate.Parameters.AddWithValue("@AppID", appID);

                    int rows = cmdUpdate.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        string insertHistory = @"
                            INSERT INTO ApplicationStatusHistory 
                            (ApplicationID, OldStatus, NewStatus, ChangedBy, Remarks)
                            VALUES
                            (@AppID,
                             @OldStatus,
                             @NewStatus,
                             @ChangedBy,
                             @Remarks)";

                        MySqlCommand histCmd = new MySqlCommand(insertHistory, conn);
                        histCmd.Parameters.AddWithValue("@AppID", appID);
                        histCmd.Parameters.AddWithValue("@OldStatus", oldStatusId.ToString());
                        histCmd.Parameters.AddWithValue("@NewStatus", underReviewStatusId.ToString());
                        histCmd.Parameters.AddWithValue("@ChangedBy", 2);
                        histCmd.Parameters.AddWithValue("@Remarks", "Locked for HR review");

                        histCmd.ExecuteNonQuery();

                        MessageBox.Show("Application locked for review.");
                    }

                    txtAppID.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private int GetCurrentStatus(MySqlConnection conn, int appID)
        {
            string query = "SELECT StatusID FROM Applications WHERE ApplicationID = @id";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", appID);

            object result = cmd.ExecuteScalar();
            return result == null ? -1 : Convert.ToInt32(result);
        }

        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppID.Text))
            {
                MessageBox.Show("Enter Application ID first.");
                return;
            }

            if (!int.TryParse(txtAppID.Text, out int appID))
            {
                MessageBox.Show("Invalid Application ID.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT aa.Email 
                        FROM Applications a
                        INNER JOIN Applicants ap ON a.ApplicantID = ap.ApplicantID
                        INNER JOIN ApplicantAccounts aa ON ap.AccountID = aa.AccountID
                        WHERE a.ApplicationID = @AppID";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AppID", appID);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string email = result.ToString();
                        ManageProfileForm profileForm = new ManageProfileForm(email);
                        profileForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Applicant or Profile not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}