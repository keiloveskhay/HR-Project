using MySql.Data.MySqlClient;
using System;
using System.Drawing; // Required for Label/TextBox locations
using System.Windows.Forms;

namespace HR_Project
{
    public partial class ChangePasswordForm : Form
    {
        private int userId;

        // Define these at the class level so the whole file can "see" them
        private TextBox oldBox = new TextBox();
        private TextBox newBox = new TextBox();
        private TextBox confirmBox = new TextBox();

        public ChangePasswordForm(int userId)
        {
            this.userId = userId;
            this.Text = "Change Password";
            this.Width = 480;
            this.Height = 220;

            // Call the setup method
            SetupFormLayout();
        }

        private void SetupFormLayout()
        {
            Label lOld = new Label { Text = "Old Password:", Left = 10, Top = 10, Width = 120 };
            oldBox = new TextBox { Left = 140, Top = 10, Width = 300, UseSystemPasswordChar = true };

            Label lNew = new Label { Text = "New Password:", Left = 10, Top = 50, Width = 120 };
            newBox = new TextBox { Left = 140, Top = 50, Width = 300, UseSystemPasswordChar = true };

            Label lConfirm = new Label { Text = "Confirm Password:", Left = 10, Top = 90, Width = 120 };
            confirmBox = new TextBox { Left = 140, Top = 90, Width = 300, UseSystemPasswordChar = true };

            Button okBtn = new Button { Text = "Change Password", Left = 140, Top = 130, Width = 140 };
            Button cancelBtn = new Button { Text = "Cancel", Left = 300, Top = 130, Width = 100 };

            okBtn.Click += OkClicked;
            cancelBtn.Click += (s, e) => this.Close();

            this.Controls.Add(lOld);
            this.Controls.Add(oldBox);
            this.Controls.Add(lNew);
            this.Controls.Add(newBox);
            this.Controls.Add(lConfirm);
            this.Controls.Add(confirmBox);
            this.Controls.Add(okBtn);
            this.Controls.Add(cancelBtn);
        }

        private void OkClicked(object sender, EventArgs e)
        {
            if (newBox.Text != confirmBox.Text)
            {
                MessageBox.Show("New password confirmation does not match.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string verifyQuery = @"
                        SELECT COUNT(*) FROM applicantaccounts aa
                        INNER JOIN applicants a ON aa.AccountID = a.AccountID
                        WHERE a.ApplicantID = @ApplicantID AND aa.Password = @OldPassword";

                    using (MySqlCommand cmd = new MySqlCommand(verifyQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ApplicantID", userId);
                        cmd.Parameters.AddWithValue("@OldPassword", oldBox.Text.Trim());
                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        {
                            MessageBox.Show("Incorrect old password.");
                            return;
                        }
                    }

                    string updateQuery = @"
                        UPDATE applicantaccounts aa
                        INNER JOIN applicants a ON aa.AccountID = a.AccountID
                        SET aa.Password = @NewPassword
                        WHERE a.ApplicantID = @ApplicantID";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@NewPassword", newBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@ApplicantID", userId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Password updated successfully.");
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }
    }
}