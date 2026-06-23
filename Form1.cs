using HR_Project;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace HR_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadVacancies();
            LoadApplicants();
        }

        private void LoadVacancies()
        {
            try
            {
                using (MySqlConnection conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT VacancyID, JobTitle FROM JobVacancies WHERE Status = 'Open'";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Add "All Vacancies" option
                    DataRow row = dt.NewRow();
                    row["VacancyID"] = 0;
                    row["JobTitle"] = "All Vacancies";
                    dt.Rows.InsertAt(row, 0);

                    cmbVacancies.DisplayMember = "JobTitle";
                    cmbVacancies.ValueMember = "VacancyID";
                    cmbVacancies.DataSource = dt;
                    cmbVacancies.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading vacancies: " + ex.Message);
            }
        }

        private void cmbVacancies_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApplicants();
        }

        private void LoadApplicants()
        {
            try
            {
                using (MySqlConnection conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            a.ApplicationID,
                            CONCAT(ap.FirstName, ' ', ap.LastName) AS 'Applicant Name',
                            jv.JobTitle AS 'Position',
                            s.StatusName AS 'Status'
                        FROM Applications a
                        INNER JOIN Applicants ap ON a.ApplicantID = ap.ApplicantID
                        INNER JOIN JobVacancies jv ON a.VacancyID = jv.VacancyID
                        INNER JOIN ApplicationStatuses s ON a.StatusID = s.StatusID
                        WHERE (s.StatusName = 'Draft' OR s.StatusName = 'Submitted' OR s.StatusName = 'Under Review')";

                    if (cmbVacancies.SelectedValue != null && int.TryParse(cmbVacancies.SelectedValue.ToString(), out int vacId) && vacId > 0)
                    {
                        query += " AND a.VacancyID = @VacID";
                    }

                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        query += " AND CONCAT(ap.FirstName, ' ', ap.LastName) LIKE @SearchTerm";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    if (cmbVacancies.SelectedValue != null && int.TryParse(cmbVacancies.SelectedValue.ToString(), out int vacId2) && vacId2 > 0)
                    {
                        cmd.Parameters.AddWithValue("@VacID", vacId2);
                    }

                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", "%" + txtSearch.Text + "%");
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvApplicants.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                // Ignore errors during initial setup or binding
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadApplicants();
        }

        private void btnLockReview_Click(object sender, EventArgs e)
        {
            if (dgvApplicants.SelectedRows.Count > 0)
            {
                int appID = Convert.ToInt32(dgvApplicants.SelectedRows[0].Cells["ApplicationID"].Value);

                try
                {
                    using (MySqlConnection conn = DatabaseConfig.GetConnection())
                    {
                        conn.Open();

                        string updateQuery = @"
                            UPDATE Applications
                            SET StatusID = (SELECT StatusID FROM ApplicationStatuses WHERE StatusName = 'Under Review')
                            WHERE ApplicationID = @AppID";

                        MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                        cmd.Parameters.AddWithValue("@AppID", appID);
                        cmd.ExecuteNonQuery();

                        string historyQuery = @"
                            INSERT INTO ApplicationStatusHistory (ApplicationID, NewStatus, ChangedBy, Remarks)
                            VALUES (@AppID, 'Under Review', @ChangedBy, 'Application locked for review.')";

                        MySqlCommand histCmd = new MySqlCommand(historyQuery, conn);
                        histCmd.Parameters.AddWithValue("@AppID", appID);
                        histCmd.Parameters.AddWithValue("@ChangedBy", Session.UserId);
                        histCmd.ExecuteNonQuery();

                        MessageBox.Show("Application locked and is now Under Review.");
                        LoadApplicants();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error locking application: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please click the empty space on the far left of a row to select an applicant first.");
            }
        }

        private void dgvApplicants_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvApplicants.CurrentRow != null && dgvApplicants.CurrentRow.Cells["ApplicationID"].Value != DBNull.Value)
            {
                if (int.TryParse(dgvApplicants.CurrentRow.Cells["ApplicationID"].Value.ToString(), out int appId))
                {
                    LoadMissingDocuments(appId);
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

        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            if (dgvApplicants.CurrentRow != null && dgvApplicants.CurrentRow.Cells["ApplicationID"].Value != DBNull.Value)
            {
                if (int.TryParse(dgvApplicants.CurrentRow.Cells["ApplicationID"].Value.ToString(), out int appId))
                {
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
                            cmd.Parameters.AddWithValue("@AppID", appId);

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
            else
            {
                MessageBox.Show("Please select an applicant first.");
            }
        }
    }
}