using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace HR_Project
{
    public partial class frmMyInterviews : Form
    {
        private int _applicantId;

        public frmMyInterviews(int applicantId)
        {
            _applicantId = applicantId;
            InitializeComponent();
        }

        private void frmMyInterviews_Load(object sender, EventArgs e)
        {
            LoadInterviews();
        }

        private void LoadInterviews()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            jv.JobTitle AS 'Position',
                            s.InterviewDate AS 'Date & Time',
                            s.Interviewer AS 'Interviewer',
                            s.Status AS 'Interview Status'
                        FROM InterviewSchedules s
                        INNER JOIN Applications a ON s.ApplicationID = a.ApplicationID
                        INNER JOIN JobVacancies jv ON a.VacancyID = jv.VacancyID
                        WHERE a.ApplicantID = @AppId
                        ORDER BY s.InterviewDate DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AppId", _applicantId);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvInterviews.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading interviews: " + ex.Message);
            }
        }
    }
}
