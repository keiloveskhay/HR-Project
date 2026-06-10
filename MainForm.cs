using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class MainForm : Form
    {
        private TabControl tabControl;
        private TabPage tabRegister;
        private TabPage tabProfile;

        // REGISTER
        private TextBox regEmail;
        private TextBox regPassword;
        private TextBox regConfirm;
        private Button regButton;

        // PROFILE (NO LOGIN DEPENDENCY)
        private Label profileLabel;
        private TextBox profileName;
        private TextBox profileEmail;
        private TextBox profilePhone;
        private TextBox profileAddress;
        private Button refreshProfileButton;
        private Button editProfileButton;

        private int currentUserId = 0;
        private int currentApplicantId = 0;

        public MainForm()
        {
            Text = "Applicant Registration & Profile";
            Width = 640;
            Height = 480;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            tabControl = new TabControl { Dock = DockStyle.Fill };

            tabRegister = new TabPage("Register");
            tabProfile = new TabPage("Profile");

            // ================= REGISTER =================
            regEmail = new TextBox { Left = 20, Top = 20, Width = 420 };
            regPassword = new TextBox { Left = 20, Top = 60, Width = 420, UseSystemPasswordChar = true };
            regConfirm = new TextBox { Left = 20, Top = 100, Width = 420, UseSystemPasswordChar = true };
            regButton = new Button { Text = "Create Account", Left = 20, Top = 140, Width = 120 };

            regButton.Click += (s, e) => RegisterClicked();

            var regEmailLabel = new Label { Text = "Email:", Left = 20, Top = 0 };
            var regPassLabel = new Label { Text = "Password:", Left = 20, Top = 40 };
            var regConfirmLabel = new Label { Text = "Confirm:", Left = 20, Top = 80 };

            tabRegister.Controls.AddRange(new Control[]
            {
                regEmailLabel, regEmail,
                regPassLabel, regPassword,
                regConfirmLabel, regConfirm,
                regButton
            });

            // ================= PROFILE =================
            profileLabel = new Label { Text = "Profile", Left = 20, Top = 20 };

            profileName = new TextBox { Left = 20, Top = 50, Width = 420 };
            profileEmail = new TextBox { Left = 20, Top = 80, Width = 420, ReadOnly = true };
            profilePhone = new TextBox { Left = 20, Top = 110, Width = 420 };
            profileAddress = new TextBox { Left = 20, Top = 140, Width = 420 };

            refreshProfileButton = new Button { Text = "Refresh", Left = 460, Top = 50 };
            editProfileButton = new Button { Text = "Save", Left = 460, Top = 80 };

            refreshProfileButton.Click += (s, e) => LoadProfile();
            editProfileButton.Click += (s, e) => SaveProfile();

            tabProfile.Controls.AddRange(new Control[]
            {
                profileLabel,
                profileName,
                profileEmail,
                profilePhone,
                profileAddress,
                refreshProfileButton,
                editProfileButton
            });

            tabControl.TabPages.Add(tabRegister);
            tabControl.TabPages.Add(tabProfile);

            Controls.Add(tabControl);
        }

        // ================= REGISTER =================
        private void RegisterClicked()
        {
            string email = regEmail.Text.Trim();
            string pass = regPassword.Text;
            string confirm = regConfirm.Text;

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (pass != confirm)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();

                // CHECK DUPLICATE EMAIL
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Email=@Email";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Email", email);

                int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (exists > 0)
                {
                    MessageBox.Show("Email already exists.");
                    return;
                }

                // INSERT USER
                string insertUser = @"
                    INSERT INTO Users (FullName, Email, PasswordHash, UserType)
                    VALUES (@FullName, @Email, @Password, 'Applicant')";

                MySqlCommand cmd = new MySqlCommand(insertUser, conn);
                cmd.Parameters.AddWithValue("@FullName", email);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", pass);

                cmd.ExecuteNonQuery();

                long userId = cmd.LastInsertedId;

                // CREATE PROFILE
                string insertProfile = @"
                    INSERT INTO ApplicantProfiles (UserID)
                    VALUES (@UserID)";

                MySqlCommand cmd2 = new MySqlCommand(insertProfile, conn);
                cmd2.Parameters.AddWithValue("@UserID", userId);

                cmd2.ExecuteNonQuery();
            }

            MessageBox.Show("Account created successfully!");

            regEmail.Clear();
            regPassword.Clear();
            regConfirm.Clear();
        }

        // ================= PROFILE =================
        private void LoadProfile()
        {
            if (currentUserId == 0)
            {
                MessageBox.Show("No user loaded.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();

                string query = @"
                    SELECT u.Email, p.ContactNumber, p.Address
                    FROM Users u
                    JOIN ApplicantProfiles p ON u.UserID = p.UserID
                    WHERE u.UserID = @UserID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", currentUserId);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    profileEmail.Text = reader["Email"].ToString();
                    profilePhone.Text = reader["ContactNumber"].ToString();
                    profileAddress.Text = reader["Address"].ToString();
                }
            }
        }

        private void SaveProfile()
        {
            if (currentUserId == 0) return;

            using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();

                string query = @"
                    UPDATE ApplicantProfiles
                    SET ContactNumber=@Phone,
                        Address=@Address
                    WHERE UserID=@UserID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Phone", profilePhone.Text);
                cmd.Parameters.AddWithValue("@Address", profileAddress.Text);
                cmd.Parameters.AddWithValue("@UserID", currentUserId);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Profile updated.");
        }
    }
}