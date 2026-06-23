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
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    // Lowercase table names to match database
                    string query = @"
                        SELECT a.ApplicationID, j.JobTitle, s.StatusName, a.CreatedAt
                        FROM applications a
                        INNER JOIN jobvacancies j ON a.VacancyID = j.VacancyID
                        INNER JOIN applicationstatuses s ON a.StatusID = s.StatusID
                        WHERE a.ApplicantID = @ApplicantID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ApplicantID", applicantId);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvApplications.DataSource = dt;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading applications: " + ex.Message); }
        }

        private void btnViewStatus_Click(object sender, EventArgs e)
        {
            var value = dgvApplications.CurrentRow?.Cells["StatusName"]?.Value;
            if (value != null) MessageBox.Show("Current Status: " + value);
            else MessageBox.Show("Please select an application first.");
        }

        private void dgvApplications_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvApplications.CurrentRow != null)
            {
                var value = dgvApplications.CurrentRow.Cells["ApplicationID"]?.Value;
                if (value != null && int.TryParse(value.ToString(), out int appId))
                {
                    LoadMissingDocuments(appId);
                    LoadRecentUpdates(appId);
                }
            }
        }

        private void LoadMissingDocuments(int applicationId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT rt.RequirementName
                        FROM RequirementTypes rt
                        LEFT JOIN ApplicantDocuments ad
                            ON rt.RequirementTypeID = ad.RequirementTypeID
                            AND ad.ApplicationID = @ApplicationID
                        WHERE rt.IsActive = 1 AND ad.DocumentID IS NULL";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ApplicationID", applicationId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            lstMissingDocs.Items.Clear();
                            while (reader.Read())
                            {
                                lstMissingDocs.Items.Add(reader["RequirementName"].ToString());
                            }
                            if (lstMissingDocs.Items.Count == 0)
                            {
                                lstMissingDocs.Items.Add("All requirements submitted.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading missing documents: " + ex.Message); }
        }

        private void LoadRecentUpdates(int applicationId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT OldStatus, NewStatus, Remarks, ChangedAt
                        FROM ApplicationStatusHistory
                        WHERE ApplicationID = @ApplicationID
                        ORDER BY ChangedAt DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ApplicationID", applicationId);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvRecentUpdates.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading recent updates: " + ex.Message); }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            var value = dgvApplications.CurrentRow?.Cells["ApplicationID"]?.Value;
            if (value != null)
            {
                if (MessageBox.Show("Are you sure you want to withdraw this application?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                    {
                        conn.Open();
                        // StatusID 10 is 'Withdrawn'
                        string query = "UPDATE applications SET StatusID = 10 WHERE ApplicationID = @ID";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", value);
                            cmd.ExecuteNonQuery();
                        }
                        
                        string logQuery = "INSERT INTO ApplicationStatusHistory (ApplicationID, NewStatus, ChangedBy, Remarks) VALUES (@AppID, 'Withdrawn', 'Applicant', 'Application withdrawn by applicant')";
                        using (var logCmd = new MySqlCommand(logQuery, conn))
                        {
                            logCmd.Parameters.AddWithValue("@AppID", value);
                            logCmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Application withdrawn successfully.");
                    LoadMyApplications();
                }
            }
            else { MessageBox.Show("Select an application first."); }
        }

        private void btnViewDocuments_Click(object sender, EventArgs e)
        {
            var value = dgvApplications.CurrentRow?.Cells["ApplicationID"]?.Value;
            if (value != null && int.TryParse(value.ToString(), out int appId))
            {
                FrmMyDocuments form = new FrmMyDocuments(appId);
                form.ShowDialog();
                LoadMissingDocuments(appId);
            }
            else
            {
                MessageBox.Show("Please select an application first.");
            }
        }
    }
}