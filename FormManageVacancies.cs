using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class FormManageVacancies : Form
    {
        public FormManageVacancies()
        {
            InitializeComponent();
        }

        private void FormManageVacancies_Load(object sender, EventArgs e)
        {
            LoadVacancies();
        }

        private void LoadVacancies()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT VacancyID, JobTitle, EmploymentType, Slots, Status, Description, Qualifications, RequiredDocuments
                        FROM JobVacancies
                        ORDER BY Status DESC, PostedAt DESC";
                    
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvVacancies.DataSource = dt;

                    if (dgvVacancies.Columns.Contains("Description")) dgvVacancies.Columns["Description"].Visible = false;
                    if (dgvVacancies.Columns.Contains("Qualifications")) dgvVacancies.Columns["Qualifications"].Visible = false;
                    if (dgvVacancies.Columns.Contains("RequiredDocuments")) dgvVacancies.Columns["RequiredDocuments"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading vacancies: " + ex.Message);
            }
        }

        private void dgvVacancies_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVacancies.CurrentRow != null && dgvVacancies.CurrentRow.Cells["VacancyID"].Value != DBNull.Value)
            {
                txtDescription.Text = dgvVacancies.CurrentRow.Cells["Description"].Value?.ToString();
                txtQualifications.Text = dgvVacancies.CurrentRow.Cells["Qualifications"].Value?.ToString();
                txtRequiredDocs.Text = dgvVacancies.CurrentRow.Cells["RequiredDocuments"].Value?.ToString();
                
                string status = dgvVacancies.CurrentRow.Cells["Status"].Value?.ToString();
                lblStatus.Text = "Status: " + status;

                if (status == "Closed")
                {
                    btnCloseVacancy.Enabled = false;
                    btnReopenVacancy.Enabled = true;
                }
                else
                {
                    btnCloseVacancy.Enabled = true;
                    btnReopenVacancy.Enabled = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvVacancies.CurrentRow == null || dgvVacancies.CurrentRow.Cells["VacancyID"].Value == DBNull.Value) return;

            int vacancyId = Convert.ToInt32(dgvVacancies.CurrentRow.Cells["VacancyID"].Value);
            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE JobVacancies 
                        SET Description = @Desc, Qualifications = @Qual, RequiredDocuments = @Docs
                        WHERE VacancyID = @ID";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", vacancyId);
                        cmd.Parameters.AddWithValue("@Desc", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@Qual", txtQualifications.Text);
                        cmd.Parameters.AddWithValue("@Docs", txtRequiredDocs.Text);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Vacancy updated successfully!");
                    int selectedRowIndex = dgvVacancies.CurrentRow.Index;
                    LoadVacancies();
                    if (selectedRowIndex < dgvVacancies.Rows.Count)
                        dgvVacancies.Rows[selectedRowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating vacancy: " + ex.Message);
            }
        }

        private void btnCloseVacancy_Click(object sender, EventArgs e)
        {
            UpdateStatus("Closed");
        }

        private void btnReopenVacancy_Click(object sender, EventArgs e)
        {
            UpdateStatus("Open");
        }

        private void UpdateStatus(string newStatus)
        {
            if (dgvVacancies.CurrentRow == null || dgvVacancies.CurrentRow.Cells["VacancyID"].Value == DBNull.Value) return;

            int vacancyId = Convert.ToInt32(dgvVacancies.CurrentRow.Cells["VacancyID"].Value);
            
            if (MessageBox.Show($"Are you sure you want to {newStatus} this vacancy?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                    {
                        conn.Open();
                        string query = newStatus == "Closed" 
                            ? "UPDATE JobVacancies SET Status = 'Closed', ClosedAt = NOW() WHERE VacancyID = @ID"
                            : "UPDATE JobVacancies SET Status = 'Open', ClosedAt = NULL WHERE VacancyID = @ID";
                            
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", vacancyId);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show($"Vacancy marked as {newStatus}!");
                        LoadVacancies();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating status: " + ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddVacancyForm addForm = new AddVacancyForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadVacancies();
            }
        }
    }
}
