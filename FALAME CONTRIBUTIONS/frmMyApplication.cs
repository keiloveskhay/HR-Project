using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Diagnostics;

namespace HRApplicantSystem
{
    public partial class frmMyApplication : Form
    {
        private int currentApplicantID = 1;

        public frmMyApplication()
        {
            InitializeComponent();
        }

        private void frmMyApplication_Load(object sender, EventArgs e)
        {
            LoadMyApplications();
        }

        private void LoadMyApplications()
        {
            string query = @"SELECT a.ApplicationID, j.JobTitle, a.Status, a.DateApplied, a.DocumentPath 
                             FROM Applications a 
                             JOIN JobVacancies j ON a.VacancyID = j.VacancyID 
                             WHERE a.ApplicantID = @ApplicantID";

            try
            {
                DataTable dt = DBHelper.ExecuteQuery(query, new MySqlParameter("@ApplicantID", currentApplicantID));

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvApplications.DataSource = dt;
                }
                else
                {
                    dgvApplications.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void btnViewStatus_Click(object sender, EventArgs e)
        {
            // Use the null-conditional operator '?.' to avoid CS8602/CS8600
            var cellValue = dgvApplications.CurrentRow?.Cells["Status"]?.Value;

            if (cellValue != null)
            {
                MessageBox.Show("Current Application Status: " + cellValue.ToString(), "Application Status");
            }
            else
            {
                MessageBox.Show("Please select an application first.");
            }
        }

        private void btnEditApplication_Click(object sender, EventArgs e)
        {
            var cellValue = dgvApplications.CurrentRow?.Cells["ApplicationID"]?.Value;

            if (cellValue != null)
            {
                MessageBox.Show("Redirecting to edit logic for Application ID: " + cellValue.ToString());
            }
            else
            {
                MessageBox.Show("Please select an application to edit.");
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            var cellValue = dgvApplications.CurrentRow?.Cells["ApplicationID"]?.Value;

            if (cellValue != null)
            {
                int appId = Convert.ToInt32(cellValue);
                string deleteQuery = "DELETE FROM Applications WHERE ApplicationID = @ID";
                DBHelper.ExecuteNonQuery(deleteQuery, new MySqlParameter("@ID", appId));

                MessageBox.Show("Application withdrawn successfully.");
                LoadMyApplications();
            }
            else
            {
                MessageBox.Show("Please select an application to withdraw.");
            }
        }

        private void btnViewDocuments_Click(object sender, EventArgs e)
        {
            // Safely get the value from the DataGridView
            var cellValue = dgvApplications.CurrentRow?.Cells["DocumentPath"]?.Value;

            // Convert to string and handle nulls
            string filePath = cellValue?.ToString() ?? string.Empty;

            // Check if the path exists on the disk
            if (!string.IsNullOrWhiteSpace(filePath) && System.IO.File.Exists(filePath))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("File not found at: " + filePath);
            }
        }

        private void dgvApplications_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}