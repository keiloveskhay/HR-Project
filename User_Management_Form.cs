using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class UserManagementForm : Form
    {
        private int selectedUserID = 0;

        public UserManagementForm()
        {
            InitializeComponent();
        }

        private void LoadUsers()
        {
            try
            {
                using (MySqlConnection conn =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            UserID,
                            FullName,
                            Email,
                            RoleID,
                            IsActive,
                            CreatedAt
                        FROM Users";

                    using (MySqlDataAdapter da = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvUsers.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

                selectedUserID = Convert.ToInt32(row.Cells["UserID"].Value);

                txtFullName.Text = row.Cells["FullName"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                cmbUserType.Text = row.Cells["RoleID"].Value.ToString();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO Users
                        (FullName, Email, Password, RoleID)
                        VALUES
                        (@FullName, @Email, @Password, @RoleID)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@RoleID", Convert.ToInt32(cmbUserType.Text));

                        cmd.ExecuteNonQuery();
                    }
                }

                LoadUsers();

                txtFullName.Clear();
                txtEmail.Clear();
                txtPassword.Clear();
                cmbUserType.SelectedIndex = -1;

                MessageBox.Show("User added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (selectedUserID == 0)
            {
                MessageBox.Show("Please select a user.");
                return;
            }

            try
            {
                using (MySqlConnection conn =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        UPDATE Users
                        SET
                            FullName = @FullName,
                            Email = @Email,
                            Password = @Password,
                            RoleID = @RoleID
                        WHERE UserID = @UserID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@RoleID", Convert.ToInt32(cmbUserType.Text));
                        cmd.Parameters.AddWithValue("@UserID", selectedUserID);

                        cmd.ExecuteNonQuery();
                    }
                }

                LoadUsers();

                MessageBox.Show("User updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void btnDeactivateUser_Click(object sender, EventArgs e)
        {
            if (selectedUserID == 0)
            {
                MessageBox.Show("Please select a user.");
                return;
            }

            try
            {
                using (MySqlConnection conn =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        UPDATE Users
                        SET IsActive = 0
                        WHERE UserID = @UserID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", selectedUserID);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadUsers();

                MessageBox.Show("User deactivated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }
    }
}