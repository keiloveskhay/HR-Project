using System;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Recruitment_Workflow_Jared
{
    partial class FormInterview
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

        private DataGridView dgvInterviews;
        private Label lblAppID;
        private TextBox txtAppID;
        private Label lblDate;
        private DateTimePicker dtpInterviewDate;
        private Label lblInterviewer;
        private TextBox txtInterviewer;
        private Button btnSchedule;

        private void InitializeComponent()
        {
            dgvInterviews = new DataGridView();
            lblAppID = new Label();
            txtAppID = new TextBox();
            lblDate = new Label();
            dtpInterviewDate = new DateTimePicker();
            lblInterviewer = new Label();
            txtInterviewer = new TextBox();
            btnSchedule = new Button();

            ((System.ComponentModel.ISupportInitialize)(dgvInterviews)).BeginInit();
            SuspendLayout();

            dgvInterviews.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInterviews.Location = new Point(20, 20);
            dgvInterviews.Name = "dgvInterviews";
            dgvInterviews.RowHeadersWidth = 51;
            dgvInterviews.Size = new Size(740, 200);
            dgvInterviews.TabIndex = 0;
            dgvInterviews.SelectionChanged += dgvInterviews_SelectionChanged;

            lblAppID.AutoSize = true;
            lblAppID.Location = new Point(20, 243);
            lblAppID.Text = "Application ID:";

            txtAppID.Location = new Point(140, 240);
            txtAppID.Name = "txtAppID";
            txtAppID.Size = new Size(200, 27);
            txtAppID.ReadOnly = true;

            lblDate.AutoSize = true;
            lblDate.Location = new Point(20, 283);
            lblDate.Text = "Interview Date:";

            dtpInterviewDate.Location = new Point(140, 280);
            dtpInterviewDate.Name = "dtpInterviewDate";
            dtpInterviewDate.Size = new Size(250, 27);

            lblInterviewer.AutoSize = true;
            lblInterviewer.Location = new Point(20, 323);
            lblInterviewer.Text = "Interviewer:";

            txtInterviewer.Location = new Point(140, 320);
            txtInterviewer.Name = "txtInterviewer";
            txtInterviewer.Size = new Size(300, 27);

            btnSchedule.Location = new Point(140, 370);
            btnSchedule.Name = "btnSchedule";
            btnSchedule.Size = new Size(180, 35);
            btnSchedule.Text = "Schedule Interview";
            btnSchedule.UseVisualStyleBackColor = true;
            btnSchedule.Click += btnSchedule_Click;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvInterviews);
            Controls.Add(lblAppID);
            Controls.Add(txtAppID);
            Controls.Add(lblDate);
            Controls.Add(dtpInterviewDate);
            Controls.Add(lblInterviewer);
            Controls.Add(txtInterviewer);
            Controls.Add(btnSchedule);
            Name = "FormInterview";
            Text = "Interview Scheduling";
            Load += FormInterview_Load;

            ((System.ComponentModel.ISupportInitialize)(dgvInterviews)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}