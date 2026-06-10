using System;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Recruitment_Workflow_Jared
{
    partial class FormScreening
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
        private Label lblAppID;
        private Label lblResult;
        private Label lblRemarks;

        private TextBox txtAppID;
        private ComboBox cmbResult;
        private TextBox txtRemarks;
        private Button btnSubmitScreening;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblAppID = new Label();
            lblResult = new Label();
            lblRemarks = new Label();

            txtAppID = new TextBox();
            cmbResult = new ComboBox();
            txtRemarks = new TextBox();
            btnSubmitScreening = new Button();

            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Text = "Applicant Screening";

            lblAppID.AutoSize = true;
            lblAppID.Location = new Point(20, 50);
            lblAppID.Text = "Application ID:";

            txtAppID.Location = new Point(140, 47);
            txtAppID.Size = new Size(120, 27);

            lblResult.AutoSize = true;
            lblResult.Location = new Point(280, 50);
            lblResult.Text = "Result:";

            cmbResult.Location = new Point(340, 47);
            cmbResult.Size = new Size(150, 28);
            cmbResult.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbResult.Items.AddRange(new object[] { "Qualified", "Not Qualified" });

            lblRemarks.AutoSize = true;
            lblRemarks.Location = new Point(20, 90);
            lblRemarks.Text = "Remarks:";

            txtRemarks.Location = new Point(20, 110);
            txtRemarks.Size = new Size(500, 200);
            txtRemarks.Multiline = true;
            txtRemarks.ScrollBars = ScrollBars.Vertical;

            btnSubmitScreening.Location = new Point(340, 310);
            btnSubmitScreening.Size = new Size(180, 35);
            btnSubmitScreening.Text = "Submit Screening";
            btnSubmitScreening.UseVisualStyleBackColor = true;
            btnSubmitScreening.Click += btnSubmitScreening_Click;

            ClientSize = new Size(600, 380);
            Controls.Add(lblTitle);
            Controls.Add(lblAppID);
            Controls.Add(txtAppID);
            Controls.Add(lblResult);
            Controls.Add(cmbResult);
            Controls.Add(lblRemarks);
            Controls.Add(txtRemarks);
            Controls.Add(btnSubmitScreening);

            Name = "FormScreening";
            Text = "Screening";

            ResumeLayout(false);
            PerformLayout();
        }
    }
}