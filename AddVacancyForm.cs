using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class AddVacancyForm : Form
    {
        public AddVacancyForm()
        {
            InitializeComponent();
        }

        private void AddVacancyForm_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            LoadEmploymentTypes();
        }

        private void LoadDepartments()
        {
            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query =
                        "SELECT DepartmentID, DepartmentName FROM Departments WHERE IsActive = 1";

                    MySqlDataAdapter adapter =
                        new MySqlDataAdapter(query, conn);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cmbDepartment.DataSource = dt;
                    cmbDepartment.DisplayMember = "DepartmentName";
                    cmbDepartment.ValueMember = "DepartmentID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadEmploymentTypes()
        {
            cmbEmploymentType.Items.Clear();

            cmbEmploymentType.Items.Add("Full-time");
            cmbEmploymentType.Items.Add("Part-time");
            cmbEmploymentType.Items.Add("Contractual");
            cmbEmploymentType.Items.Add("Internship");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO JobVacancies
                        (
                            JobTitle,
                            DepartmentID,
                            EmploymentType,
                            Description,
                            Qualifications,
                            Slots,
                            PostedBy
                        )
                        VALUES
                        (
                            @JobTitle,
                            @DepartmentID,
                            @EmploymentType,
                            @Description,
                            @Qualifications,
                            @Slots,
                            @PostedBy
                        )";

                    MySqlCommand cmd =
                        new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                    cmd.Parameters.AddWithValue("@DepartmentID", cmbDepartment.SelectedValue);
                    cmd.Parameters.AddWithValue("@EmploymentType", cmbEmploymentType.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Qualifications", txtQualifications.Text);
                    cmd.Parameters.AddWithValue("@Slots", Convert.ToInt32(numSlots.Value));

                    // Replace later with logged-in admin UserID
                    cmd.Parameters.AddWithValue("@PostedBy", 1);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Vacancy added successfully.");

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}