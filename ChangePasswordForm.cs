using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class ChangePasswordForm : Form
    {
        private int userId;

        private TextBox oldBox;
        private TextBox newBox;
        private TextBox confirmBox;
        private Button okBtn;
        private Button cancelBtn;

        public ChangePasswordForm(int userId)
        {
            this.userId = userId;

            Text = "Change Password";
            Width = 480;
            Height = 220;

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Label lOld = new Label
            {
                Text = "Old Password:",
                Left = 10,
                Top = 10,
                Width = 120
            };

            oldBox = new TextBox
            {
                Left = 140,
                Top = 10,
                Width = 300,
                UseSystemPasswordChar = true
            };

            Label lNew = new Label
            {
                Text = "New Password:",
                Left = 10,
                Top = 50,
                Width = 120
            };

            newBox = new TextBox
            {
                Left = 140,
                Top = 50,
                Width = 300,
                UseSystemPasswordChar = true
            };

            Label lConfirm = new Label
            {
                Text = "Confirm Password:",
                Left = 10,
                Top = 90,
                Width = 120
            };

            confirmBox = new TextBox
            {
                Left = 140,
                Top = 90,
                Width = 300,
                UseSystemPasswordChar = true
            };

            okBtn = new Button
            {
                Text = "Change Password",
                Left = 140,
                Top = 130,
                Width = 140
            };

            cancelBtn = new Button
            {
                Text = "Cancel",
                Left = 300,
                Top = 130,
                Width = 100
            };

            okBtn.Click += OkClicked;
            cancelBtn.Click += (s, e) => Close();

            Controls.Add(lOld);
            Controls.Add(oldBox);

            Controls.Add(lNew);
            Controls.Add(newBox);

            Controls.Add(lConfirm);
            Controls.Add(confirmBox);

            Controls.Add(okBtn);
            Controls.Add(cancelBtn);
        }

        private void OkClicked(object sender, EventArgs e)
        {
            string oldPassword = oldBox.Text.Trim();
            string newPassword = newBox.Text.Trim();
            string confirmPassword = confirmBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(oldPassword) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password confirmation does not match.");
                return;
            }

            try
            {
                using (MySqlConnection conn =
                       new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string verifyQuery = @"
                        SELECT COUNT(*)
                        FROM Users
                        WHERE UserID = @UserID
                        AND PasswordHash = @OldPassword";

                    MySqlCommand verifyCmd =
                        new MySqlCommand(verifyQuery, conn);

                    verifyCmd.Parameters.AddWithValue("@UserID", userId);
                    verifyCmd.Parameters.AddWithValue("@OldPassword", oldPassword);

                    int count = Convert.ToInt32(verifyCmd.ExecuteScalar());

                    if (count == 0)
                    {
                        MessageBox.Show("Old password is incorrect.");
                        return;
                    }

                    string updateQuery = @"
                        UPDATE Users
                        SET PasswordHash = @NewPassword
                        WHERE UserID = @UserID";

                    MySqlCommand updateCmd =
                        new MySqlCommand(updateQuery, conn);

                    updateCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    updateCmd.Parameters.AddWithValue("@UserID", userId);

                    updateCmd.ExecuteNonQuery();

                    MessageBox.Show("Password changed successfully.");

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}