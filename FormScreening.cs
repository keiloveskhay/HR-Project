using HR_Project;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace HR_Recruitment_Workflow_Jared
{
    public partial class FormScreening : Form
    {
        public FormScreening()
        {
            InitializeComponent();
        }

        private void btnSubmitScreening_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAppID.Text) || cmbResult.SelectedItem == null)
            {
                MessageBox.Show("Please enter Application ID and select result.");
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
            int screenBy = 2;

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }
    }
}