using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class frmMyApplication : Form
    {
        private int applicantId;

        public frmMyApplication(int applicantId)
        {
            InitializeComponent();
            this.applicantId = applicantId;
        }

        private void frmMyApplication_Load(object sender, EventArgs e)
        {
            LoadMyApplications();
        }

        private void LoadMyApplications()
        {
            using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        a.ApplicationID,
                        j.JobTitle,
                        s.StatusName,
                        a.CreatedAt
                    FROM Applications a
                    INNER JOIN JobVacancies j ON a.VacancyID = j.VacancyID
                    INNER JOIN ApplicationStatuses s ON a.StatusID = s.StatusID
                    WHERE a.ApplicantID = @ApplicantID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ApplicantID", applicantId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvApplications.DataSource = dt;
            }
        }

        private void btnViewStatus_Click(object sender, EventArgs e)
        {
            var value = dgvApplications.CurrentRow?.Cells["StatusName"]?.Value;

            if (value != null)
                MessageBox.Show("Current Status: " + value);
            else
                MessageBox.Show("Please select an application first.");
        }

        private void btnEditApplication_Click(object sender, EventArgs e)
        {
            var value = dgvApplications.CurrentRow?.Cells["ApplicationID"]?.Value;

            if (value != null)
            {
                MessageBox.Show("Edit Application ID: " + value);
            }
            else
            {
                MessageBox.Show("Select an application first.");
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            var value = dgvApplications.CurrentRow?.Cells["ApplicationID"]?.Value;

            if (value != null)
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = "DELETE FROM Applications WHERE ApplicationID = @ID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(value));
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Application withdrawn successfully.");
                LoadMyApplications();
            }
            else
            {
                MessageBox.Show("Select an application first.");
            }
        }

        private void btnViewDocuments_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your current database does NOT include DocumentPath in Applications table.");
        }
    }
}