using System;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Recruitment_Workflow_Jared
{
    partial class FormApplicantReview
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

        private TextBox txtAppID;
        private Button btnLock;
        private Button btnViewProfile;
        private Label lblTitle;
        private Label lblAppId;

        private void InitializeComponent()
        {
            txtAppID = new TextBox();
            btnLock = new Button();
            btnViewProfile = new Button();
            lblTitle = new Label();
            lblAppId = new Label();

            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(290, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(220, 28);
            lblTitle.Text = "Applicant Review";

            lblAppId.AutoSize = true;
            lblAppId.Location = new Point(240, 120);
            lblAppId.Name = "lblAppId";
            lblAppId.Size = new Size(100, 20);
            lblAppId.Text = "Application ID:";

            txtAppID.Location = new Point(360, 117);
            txtAppID.Name = "txtAppID";
            txtAppID.Size = new Size(180, 27);

            btnLock.Location = new Point(260, 170);
            btnLock.Name = "btnLock";
            btnLock.Size = new Size(280, 35);
            btnLock.Text = "Lock Application (Under Review)";
            btnLock.UseVisualStyleBackColor = true;
            btnLock.Click += btnLock_Click;

            btnViewProfile.Location = new Point(260, 220);
            btnViewProfile.Name = "btnViewProfile";
            btnViewProfile.Size = new Size(280, 35);
            btnViewProfile.Text = "View Applicant Profile";
            btnViewProfile.UseVisualStyleBackColor = true;
            btnViewProfile.Click += btnViewProfile_Click;

            ClientSize = new Size(800, 450);
            Controls.Add(lblTitle);
            Controls.Add(lblAppId);
            Controls.Add(txtAppID);
            Controls.Add(btnLock);
            Controls.Add(btnViewProfile);
            Name = "FormApplicantReview";
            Text = "Applicant Review";

            ResumeLayout(false);
            PerformLayout();
        }
    }
}