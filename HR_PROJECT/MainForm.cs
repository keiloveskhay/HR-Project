using System;
using System.Windows.Forms;

namespace IDk
{
    public class MainForm : Form
    {
        private TabControl tabControl;
        private TabPage tabRegister;
        private TabPage tabLogin;

        private TextBox regEmail;
        private TextBox regPassword;
        private TextBox regConfirm;
        private Button regButton;

        private TextBox loginEmail;
        private TextBox loginPassword;
        private Button loginButton;
        private Label profileLabel;
        private TextBox profileName;
        private TextBox profileEmail;
        private TextBox profilePhone;
        private TextBox profileAddress;
        private Button refreshProfileButton;
        private Button editProfileButton;
        private Button manageDetailsButton;

        private string _currentEmail;

        public MainForm()
        {
            Text = "ApplicantApp - UI";
            Width = 640;
            Height = 480;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            tabControl = new TabControl { Dock = DockStyle.Fill };

            tabRegister = new TabPage("Register");
            tabLogin = new TabPage("Login");

            regEmail = new TextBox { Left = 20, Top = 20, Width = 420 };
            regPassword = new TextBox { Left = 20, Top = 60, Width = 420, UseSystemPasswordChar = true };
            regConfirm = new TextBox { Left = 20, Top = 100, Width = 420, UseSystemPasswordChar = true };
            regButton = new Button { Text = "Create Account", Left = 20, Top = 140, Width = 120 };
            regButton.Click += (s, e) => RegisterClicked();
            var regEmailLabel = new Label { Text = "Email:", Left = 20, Top = 0, Width = 100 };
            var regPassLabel = new Label { Text = "Password:", Left = 20, Top = 40, Width = 100 };
            var regConfirmLabel = new Label { Text = "Confirm:", Left = 20, Top = 80, Width = 100 };

            tabRegister.Controls.AddRange(new Control[] { regEmailLabel, regEmail, regPassLabel, regPassword, regConfirmLabel, regConfirm, regButton });

            loginEmail = new TextBox { Left = 20, Top = 20, Width = 420 };
            loginPassword = new TextBox { Left = 20, Top = 60, Width = 420, UseSystemPasswordChar = true };
            loginButton = new Button { Text = "Login", Left = 20, Top = 100, Width = 100 };
            loginButton.Click += (s, e) => LoginClicked();

            profileLabel = new Label { Text = "Profile", Left = 20, Top = 140, Width = 100 };
            profileName = new TextBox { Left = 20, Top = 170, Width = 420 };
            profileEmail = new TextBox { Left = 20, Top = 200, Width = 420, ReadOnly = true };
            profilePhone = new TextBox { Left = 20, Top = 230, Width = 420 };
            profileAddress = new TextBox { Left = 20, Top = 260, Width = 420 };
            refreshProfileButton = new Button { Text = "Refresh", Left = 460, Top = 170, Width = 120 };
            editProfileButton = new Button { Text = "Save", Left = 460, Top = 200, Width = 120 };

            refreshProfileButton.Click += (s, e) => RefreshProfile();
            editProfileButton.Click += (s, e) => SaveProfile();
            manageDetailsButton = new Button { Text = "Manage...", Left = 460, Top = 240, Width = 120 };
            manageDetailsButton.Click += (s, e) => ManageDetailsClicked();

            var loginEmailLabel = new Label { Text = "Email:", Left = 20, Top = 0, Width = 100 };
            var loginPassLabel = new Label { Text = "Password:", Left = 20, Top = 40, Width = 100 };

            tabLogin.Controls.AddRange(new Control[] { loginEmailLabel, loginEmail, loginPassLabel, loginPassword, loginButton, profileLabel, profileName, profileEmail, profilePhone, profileAddress, refreshProfileButton, editProfileButton, manageDetailsButton });

            tabControl.TabPages.Add(tabRegister);
            tabControl.TabPages.Add(tabLogin);

            Controls.Add(tabControl);
        }

        private void RegisterClicked()
        {
            var email = regEmail.Text.Trim();
            var pwd = regPassword.Text;
            var pwd2 = regConfirm.Text;
            if (pwd != pwd2) { MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            var (success, message) = AccountService.CreateAccount(email, pwd);
            MessageBox.Show(message, success ? "Success" : "Error", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (success) { regEmail.Text = ""; regPassword.Text = ""; regConfirm.Text = ""; }
        }

        private void LoginClicked()
        {
            var email = loginEmail.Text.Trim();
            var pwd = loginPassword.Text;
            if (!AccountService.VerifyCredentials(email, pwd)) { MessageBox.Show("Invalid credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            _currentEmail = email;
            LoadProfile(email);
        }

        private void LoadProfile(string email)
        {
            var a = ProfileService.GetByEmail(email);
            if (a == null) { MessageBox.Show("Profile not found."); return; }
            profileName.Text = $"{a.FirstName} {a.LastName}".Trim();
            profileEmail.Text = a.Email;
            profilePhone.Text = a.Phone;
            profileAddress.Text = a.Address;
        }

        private void RefreshProfile()
        {
            if (string.IsNullOrWhiteSpace(_currentEmail)) return;
            LoadProfile(_currentEmail);
        }

        private void SaveProfile()
        {
            if (string.IsNullOrWhiteSpace(_currentEmail)) { MessageBox.Show("No user logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            var a = ProfileService.GetByEmail(_currentEmail);
            if (a == null) { MessageBox.Show("Profile not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            var name = profileName.Text.Trim();
            var fn = name;
            var ln = string.Empty;
            var idx = name.IndexOf(' ');
            if (idx > 0) { fn = name.Substring(0, idx); ln = name.Substring(idx + 1); }
            ProfileService.UpdatePersonalInfo(a.Id, fn, ln, profilePhone.Text.Trim(), profileAddress.Text.Trim());
            MessageBox.Show("Profile saved.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadProfile(_currentEmail);
        }

        private void ManageDetailsClicked()
        {
            if (string.IsNullOrWhiteSpace(_currentEmail)) { MessageBox.Show("No user logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            using var f = new ManageProfileForm(_currentEmail);
            f.ShowDialog();
            LoadProfile(_currentEmail);
        }
    }
}
