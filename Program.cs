using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Recruitment_Workflow_Jared
{
    public partial class FormScreening : Form
    {
        
        DatabaseConnection db = new DatabaseConnection();

        public FormScreening()
        {
            InitializeComponent();
        }

        private void btnSubmitScreening_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtAppID.Text) || cmbResult.SelectedItem == null)
            {
                MessageBox.Show("Please enter an Application ID and select a Result.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            int appID = Convert.ToInt32(txtAppID.Text);
            string result = cmbResult.SelectedItem.ToString();
            string remarks = txtRemarks.Text;

            
            string newStatus = (result == "Qualified") ? "Shortlisted" : "Rejected";

            try
            {
                using (MySqlConnection conn = db.GetConnection())
                {
                    conn.Open();

                    
                    string insertScreening = "INSERT INTO ScreeningResults (ApplicationID, Result, Remarks) VALUES (@AppID, @Result, @Remarks)";
                    MySqlCommand cmdScreening = new MySqlCommand(insertScreening, conn);
                    cmdScreening.Parameters.AddWithValue("@AppID", appID);
                    cmdScreening.Parameters.AddWithValue("@Result", result);
                    cmdScreening.Parameters.AddWithValue("@Remarks", remarks);
                    cmdScreening.ExecuteNonQuery();

                    
                    string updateApp = "UPDATE Applications SET Status = @NewStatus WHERE ApplicationID = @AppID";
                    MySqlCommand cmdUpdate = new MySqlCommand(updateApp, conn);
                    cmdUpdate.Parameters.AddWithValue("@NewStatus", newStatus);
                    cmdUpdate.Parameters.AddWithValue("@AppID", appID);
                    cmdUpdate.ExecuteNonQuery();

                    
                    string historyQuery = "INSERT INTO ApplicationStatusHistory (ApplicationID, NewStatus, ChangedBy, Remarks) VALUES (@AppID, @NewStatus, 'HR Staff', @Remarks)";
                    MySqlCommand histCmd = new MySqlCommand(historyQuery, conn);
                    histCmd.Parameters.AddWithValue("@AppID", appID);
                    histCmd.Parameters.AddWithValue("@NewStatus", newStatus);
                    histCmd.Parameters.AddWithValue("@Remarks", remarks);
                    histCmd.ExecuteNonQuery();

                    
                    MessageBox.Show($"Screening submitted perfectly! Applicant is now {newStatus}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtAppID.Clear();
                    cmbResult.SelectedIndex = -1;
                    txtRemarks.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}