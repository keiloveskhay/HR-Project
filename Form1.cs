using HR_Project;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace HR_Recruitment_Workflow_Jared
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadApplicants();
        }

        private void LoadApplicants()
        {
            try
            {
                using (MySqlConnection conn =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            a.ApplicationID,
                            u.FullName AS 'Applicant Name',
                            jv.JobTitle AS 'Position',
                            s.StatusName AS 'Status'
                        FROM Applications a
                        INNER JOIN ApplicantProfiles ap
                            ON a.ApplicantID = ap.ApplicantID
                        INNER JOIN Users u
                            ON ap.UserID = u.UserID
                        INNER JOIN JobVacancies jv
                            ON a.VacancyID = jv.VacancyID
                        INNER JOIN ApplicationStatuses s
                            ON a.StatusID = s.StatusID
                        WHERE s.StatusName = 'Submitted'
                           OR s.StatusName = 'Under Review'";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvApplicants.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading applicants: " + ex.Message);
            }
        }

        private void btnLockReview_Click(object sender, EventArgs e)
        {
            if (dgvApplicants.SelectedRows.Count > 0)
            {
                int appID = Convert.ToInt32(dgvApplicants.SelectedRows[0].Cells["ApplicationID"].Value);

                try
                {
                    using (MySqlConnection conn =
                           new MySqlConnection(DatabaseConfig.ConnectionString))
                    {
                        conn.Open();

                        string updateQuery = @"
                            UPDATE Applications
                            SET StatusID =
                            (
                                SELECT StatusID
                                FROM ApplicationStatuses
                                WHERE StatusName = 'Under Review'
                            )
                            WHERE ApplicationID = @AppID";

                        MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                        cmd.Parameters.AddWithValue("@AppID", appID);
                        cmd.ExecuteNonQuery();

                        string historyQuery = @"
                            INSERT INTO ApplicationStatusHistory
                            (ApplicationID, NewStatus, ChangedBy, Remarks)
                            VALUES
                            (@AppID, 'Under Review', @ChangedBy, 'Application locked for review.')";

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            a.ApplicationID,
                            u.FullName AS 'Applicant Name',
                            jv.JobTitle AS 'Position',
                            s.StatusName AS 'Status'
                        FROM Applications a
                        INNER JOIN ApplicantProfiles ap
                            ON a.ApplicantID = ap.ApplicantID
                        INNER JOIN Users u
                            ON ap.UserID = u.UserID
                        INNER JOIN JobVacancies jv
                            ON a.VacancyID = jv.VacancyID
                        INNER JOIN ApplicationStatuses s
                            ON a.StatusID = s.StatusID
                        WHERE
                            (s.StatusName = 'Submitted'
                             OR s.StatusName = 'Under Review')
                            AND u.FullName LIKE @SearchTerm";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SearchTerm", "%" + txtSearch.Text + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvApplicants.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search Error: " + ex.Message);
            }
        }
    }
}