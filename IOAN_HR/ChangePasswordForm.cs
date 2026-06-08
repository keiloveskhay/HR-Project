using System;
using System.Windows.Forms;

namespace IDk
{
    public partial class ChangePasswordForm : Form
    {
        private readonly string _email;
        private TextBox oldBox;
        private TextBox newBox;
        private TextBox confirmBox;
        private Button okBtn;
        private Button cancelBtn;

        public ChangePasswordForm(string email)
        {
            _email = email;
            Text = "Change Password";
            Width = 480;
            Height = 220;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            var lOld = new Label { Text = "Old password:", Left = 10, Top = 10, Width = 120 };
            oldBox = new TextBox { Left = 140, Top = 10, Width = 300, UseSystemPasswordChar = true };
            var lNew = new Label { Text = "New password:", Left = 10, Top = 50, Width = 120 };
            newBox = new TextBox { Left = 140, Top = 50, Width = 300, UseSystemPasswordChar = true };
            var lConfirm = new Label { Text = "Confirm new:", Left = 10, Top = 90, Width = 120 };
            confirmBox = new TextBox { Left = 140, Top = 90, Width = 300, UseSystemPasswordChar = true };
            okBtn = new Button { Text = "Change", Left = 140, Top = 130, Width = 100 };
            cancelBtn = new Button { Text = "Cancel", Left = 260, Top = 130, Width = 100 };
            okBtn.Click += OkClicked;
            cancelBtn.Click += (s, e) => DialogResult = DialogResult.Cancel;
            Controls.AddRange(new Control[] { lOld, oldBox, lNew, newBox, lConfirm, confirmBox, okBtn, cancelBtn });
        }

        private void OkClicked(object sender, EventArgs e)
        {
            var oldp = oldBox.Text;
            var newp = newBox.Text;
            var c = confirmBox.Text;
            if (newp != c) { MessageBox.Show("New password confirmation does not match."); return; }
            if (!AccountService.ChangePassword(_email, oldp, newp)) { MessageBox.Show("Password change failed (wrong old password).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }
    }
}
