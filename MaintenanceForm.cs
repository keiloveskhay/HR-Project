using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HR_Project
{
    public partial class MaintenanceForm : Form
    {
        public MaintenanceForm()
        {
            InitializeComponent();
            LoadDepartments();
            LoadEmploymentTypes();
        }

        private void LoadDepartments()
        {
            deptList.Items.Clear();

            using (MySqlConnection conn =
                new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT DepartmentID, DepartmentName FROM Departments WHERE IsActive = 1", conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    deptList.Items.Add(new DepartmentItem
                    {
                        Id = reader.GetInt32("DepartmentID"),
                        Name = reader.GetString("DepartmentName")
                    });
                }
            }
        }

        private void addDeptBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(deptBox.Text))
            {
                MessageBox.Show("Enter department name.");
                return;
            }

            using (MySqlConnection conn =
                new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO Departments (DepartmentName) VALUES (@name)", conn);

                cmd.Parameters.AddWithValue("@name", deptBox.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            deptBox.Clear();
            LoadDepartments();
        }

        private void LoadEmploymentTypes()
        {
            typeList.Items.Clear();
            typeList.Items.Add("Full-time");
            typeList.Items.Add("Part-time");
            typeList.Items.Add("Contractual");
            typeList.Items.Add("Internship");
        }

        private void addTypeBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(typeBox.Text))
            {
                MessageBox.Show("Enter type name.");
                return;
            }

            typeList.Items.Add(typeBox.Text.Trim());
            typeBox.Clear();
        }

        private void addRoleBtn_Click(object sender, EventArgs e)
        {
            if (deptList.SelectedItem == null)
            {
                MessageBox.Show("Select a department first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(roleBox.Text))
            {
                MessageBox.Show("Enter role name.");
                return;
            }

            roleList.Items.Add(roleBox.Text.Trim());
            roleBox.Clear();
        }

        private void deptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            roleList.Items.Clear();
        }
    }

    public class DepartmentItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}