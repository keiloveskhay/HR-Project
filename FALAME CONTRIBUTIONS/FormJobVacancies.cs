using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HRApplicantSystem
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
            string query = "SELECT VacancyID, JobTitle, EmploymentType, Description, Qualifications, Slots, Status " +
                           "FROM JobVacancies WHERE Status = 'Open' AND JobTitle LIKE @keyword";

            DataTable dt = DBHelper.ExecuteQuery(query, new MySqlParameter("@keyword", "%" + keyword + "%"));
            dgvJobVacancies.DataSource = dt;
        }

        // IMPORTANT: The name MUST be exactly 'btnSearch_Click'
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (dgvJobVacancies.CurrentRow != null && dgvJobVacancies.CurrentRow.Cells["VacancyID"].Value != null)
            {
                int vacancyID = Convert.ToInt32(dgvJobVacancies.CurrentRow.Cells["VacancyID"].Value);

                // 1. Check if the application already exists in the database
                string checkQuery = "SELECT COUNT(*) FROM Applications WHERE ApplicantID = @ApplicantID AND VacancyID = @VacancyID";
                DataTable result = DBHelper.ExecuteQuery(checkQuery,
                    new MySqlParameter("@ApplicantID", currentApplicantID),
                    new MySqlParameter("@VacancyID", vacancyID));

                int count = Convert.ToInt32(result.Rows[0][0]);

                if (count > 0)
                {
                    // If count > 0, the application already exists. Show a message and STOP.
                    MessageBox.Show("You have already applied for this position.");
                }
                else
                {
                    // 2. Only if count is 0, proceed to insert
                    string insertQuery = "INSERT INTO Applications (ApplicantID, VacancyID, Status) VALUES (@ApplicantID, @VacancyID, 'Draft')";
                    DBHelper.ExecuteNonQuery(insertQuery,
                        new MySqlParameter("@ApplicantID", currentApplicantID),
                        new MySqlParameter("@VacancyID", vacancyID));

                    MessageBox.Show("Application saved as DRAFT successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select a job vacancy from the list first.");
            }
        }

        private void btnMyApplications_Click(object sender, EventArgs e)
        {
            frmMyApplication myApps = new frmMyApplication();
            myApps.Show();
        }

        private void FormJobVacancies_Load_1(object sender, EventArgs e)
        {

        }
    }
}