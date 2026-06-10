using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class FormJobVacancies : Form
    {
        private int currentApplicantID = 1;

        public FormJobVacancies()
        {
            InitializeComponent();
        }

        private void FormJobVacancies_Load(object sender, EventArgs e)
        {
            LoadData(string.Empty);
        }

        private void LoadData(string keyword)
        {
            using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();

                string query = @"SELECT VacancyID, JobTitle, EmploymentType, Description, Qualifications, Slots, Status
                                 FROM JobVacancies
                                 WHERE Status = 'Open' AND JobTitle LIKE @keyword";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvJobVacancies.DataSource = dt;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (dgvJobVacancies.CurrentRow != null &&
                dgvJobVacancies.CurrentRow.Cells["VacancyID"].Value != null)
            {
                int vacancyID = Convert.ToInt32(dgvJobVacancies.CurrentRow.Cells["VacancyID"].Value);

                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string checkQuery = @"SELECT COUNT(*) 
                                          FROM Applications 
                                          WHERE ApplicantID = @ApplicantID 
                                          AND VacancyID = @VacancyID";

                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@ApplicantID", currentApplicantID);
                    checkCmd.Parameters.AddWithValue("@VacancyID", vacancyID);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("You have already applied for this position.");
                    }
                    else
                    {
                        string insertQuery = @"INSERT INTO Applications (ApplicantID, VacancyID, Status)
                                               VALUES (@ApplicantID, @VacancyID, 'Draft')";

                        MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@ApplicantID", currentApplicantID);
                        insertCmd.Parameters.AddWithValue("@VacancyID", vacancyID);

                        insertCmd.ExecuteNonQuery();

                        MessageBox.Show("Application saved as DRAFT successfully!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a job vacancy from the list first.");
            }
        }

        private void btnMyApplications_Click(object sender, EventArgs e)
        {
            // TEMP SAFE FIX (prevents crash if form missing)
            MessageBox.Show("My Applications clicked");
        }
    }
}