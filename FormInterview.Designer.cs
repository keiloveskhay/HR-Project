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

        private TextBox txtAppID;
        private DateTimePicker dtpInterviewDate;
        private TextBox txtInterviewer;
        private Button btnSchedule;

        private void InitializeComponent()
        {
            txtAppID = new TextBox();
            dtpInterviewDate = new DateTimePicker();
            txtInterviewer = new TextBox();
            btnSchedule = new Button();

            SuspendLayout();

            // txtAppID
            txtAppID.Location = new Point(50, 50);
            txtAppID.Name = "txtAppID";
            txtAppID.Size = new Size(200, 27);

            // dtpInterviewDate
            dtpInterviewDate.Location = new Point(50, 90);
            dtpInterviewDate.Name = "dtpInterviewDate";
            dtpInterviewDate.Size = new Size(250, 27);

            // txtInterviewer
            txtInterviewer.Location = new Point(50, 130);
            txtInterviewer.Name = "txtInterviewer";
            txtInterviewer.Size = new Size(300, 27);

            // btnSchedule
            btnSchedule.Location = new Point(50, 170);
            btnSchedule.Name = "btnSchedule";
            btnSchedule.Size = new Size(180, 35);
            btnSchedule.Text = "Schedule Interview";
            btnSchedule.UseVisualStyleBackColor = true;
            btnSchedule.Click += btnSchedule_Click;

            // FormInterview
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 250);
            Controls.Add(txtAppID);
            Controls.Add(dtpInterviewDate);
            Controls.Add(txtInterviewer);
            Controls.Add(btnSchedule);
            Name = "FormInterview";
            Text = "Interview Scheduling";

            ResumeLayout(false);
            PerformLayout();
        }
    }
}