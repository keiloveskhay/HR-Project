using System;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class ManageProfileForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Label lblTitle;

        private Label lblEducation;
        private Label lblSchool;
        private Label lblYear;
        private Label lblSkills;
        private Label lblWork;

        private TextBox educationBox;
        private TextBox schoolBox;
        private TextBox yearBox;
        private TextBox skillsBox;
        private TextBox workBox;

        private Button saveBtn;
        private Button changePasswordBtn;
        private Button closeBtn;

        private void InitializeComponent()
        {
            lblTitle = new Label();

            lblEducation = new Label();
            lblSchool = new Label();
            lblYear = new Label();
            lblSkills = new Label();
            lblWork = new Label();

            educationBox = new TextBox();
            schoolBox = new TextBox();
            yearBox = new TextBox();
            skillsBox = new TextBox();
            workBox = new TextBox();

            saveBtn = new Button();
            changePasswordBtn = new Button();
            closeBtn = new Button();

            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Text = "Manage Profile";

            lblEducation.AutoSize = true;
            lblEducation.Location = new Point(20, 50);
            lblEducation.Text = "Highest Education";

            educationBox.Location = new Point(160, 47);
            educationBox.Size = new Size(450, 27);

            lblSchool.AutoSize = true;
            lblSchool.Location = new Point(20, 90);
            lblSchool.Text = "School";

            schoolBox.Location = new Point(160, 87);
            schoolBox.Size = new Size(450, 27);

            lblYear.AutoSize = true;
            lblYear.Location = new Point(20, 130);
            lblYear.Text = "Year Graduated";

            yearBox.Location = new Point(160, 127);
            yearBox.Size = new Size(450, 27);

            lblSkills.AutoSize = true;
            lblSkills.Location = new Point(20, 170);
            lblSkills.Text = "Skills";

            skillsBox.Location = new Point(160, 167);
            skillsBox.Size = new Size(450, 80);
            skillsBox.Multiline = true;
            skillsBox.ScrollBars = ScrollBars.Vertical;

            lblWork.AutoSize = true;
            lblWork.Location = new Point(20, 260);
            lblWork.Text = "Work Experience";

            workBox.Location = new Point(160, 257);
            workBox.Size = new Size(450, 100);
            workBox.Multiline = true;
            workBox.ScrollBars = ScrollBars.Vertical;

            saveBtn.Location = new Point(160, 380);
            saveBtn.Size = new Size(120, 35);
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;

            changePasswordBtn.Location = new Point(300, 380);
            changePasswordBtn.Size = new Size(150, 35);
            changePasswordBtn.Text = "Change Password";
            changePasswordBtn.UseVisualStyleBackColor = true;

            closeBtn.Location = new Point(470, 380);
            closeBtn.Size = new Size(120, 35);
            closeBtn.Text = "Close";
            closeBtn.UseVisualStyleBackColor = true;
            closeBtn.Click += (s, e) => Close();

            ClientSize = new Size(650, 450);

            Controls.Add(lblTitle);
            Controls.Add(lblEducation);
            Controls.Add(educationBox);
            Controls.Add(lblSchool);
            Controls.Add(schoolBox);
            Controls.Add(lblYear);
            Controls.Add(yearBox);
            Controls.Add(lblSkills);
            Controls.Add(skillsBox);
            Controls.Add(lblWork);
            Controls.Add(workBox);
            Controls.Add(saveBtn);
            Controls.Add(changePasswordBtn);
            Controls.Add(closeBtn);

            Name = "ManageProfileForm";
            Text = "Manage Profile";

            ResumeLayout(false);
            PerformLayout();
        }
    }
}