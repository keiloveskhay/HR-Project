using System;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class FormInterviewEvaluation
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

        private DataGridView dgvScheduled;
        private Label lblScheduleID;
        private TextBox txtAppID;
        private Label lblScore;
        private TextBox txtScore;
        private Label lblFeedback;
        private TextBox txtFeedback;
        private Button btnSubmitEval;

        private void InitializeComponent()
        {
            dgvScheduled = new DataGridView();
            lblScheduleID = new Label();
            txtAppID = new TextBox();
            lblScore = new Label();
            txtScore = new TextBox();
            lblFeedback = new Label();
            txtFeedback = new TextBox();
            btnSubmitEval = new Button();

            ((System.ComponentModel.ISupportInitialize)(dgvScheduled)).BeginInit();
            SuspendLayout();

            dgvScheduled.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvScheduled.Location = new Point(20, 20);
            dgvScheduled.Name = "dgvScheduled";
            dgvScheduled.RowHeadersWidth = 51;
            dgvScheduled.Size = new Size(740, 250);
            dgvScheduled.TabIndex = 0;
            dgvScheduled.SelectionChanged += dgvScheduled_SelectionChanged;

            lblScheduleID.AutoSize = true;
            lblScheduleID.Location = new Point(20, 293);
            lblScheduleID.Text = "Schedule ID:";

            txtAppID.Location = new Point(140, 290);
            txtAppID.Name = "txtAppID";
            txtAppID.Size = new Size(125, 27);
            txtAppID.TabIndex = 1;
            txtAppID.ReadOnly = true;

            lblScore.AutoSize = true;
            lblScore.Location = new Point(20, 333);
            lblScore.Text = "Score (1-100):";

            txtScore.Location = new Point(140, 330);
            txtScore.Name = "txtScore";
            txtScore.Size = new Size(125, 27);
            txtScore.TabIndex = 2;

            lblFeedback.AutoSize = true;
            lblFeedback.Location = new Point(20, 373);
            lblFeedback.Text = "Feedback:";

            txtFeedback.Location = new Point(140, 370);
            txtFeedback.Multiline = true;
            txtFeedback.Name = "txtFeedback";
            txtFeedback.Size = new Size(620, 100);
            txtFeedback.ScrollBars = ScrollBars.Vertical;
            txtFeedback.TabIndex = 3;

            btnSubmitEval.Location = new Point(140, 490);
            btnSubmitEval.Name = "btnSubmitEval";
            btnSubmitEval.Size = new Size(232, 35);
            btnSubmitEval.TabIndex = 4;
            btnSubmitEval.Text = "Submit Interview Evaluation";
            btnSubmitEval.UseVisualStyleBackColor = true;
            btnSubmitEval.Click += new System.EventHandler(btnSubmitEval_Click);

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 560);
            Controls.Add(dgvScheduled);
            Controls.Add(lblScheduleID);
            Controls.Add(txtAppID);
            Controls.Add(lblScore);
            Controls.Add(txtScore);
            Controls.Add(lblFeedback);
            Controls.Add(txtFeedback);
            Controls.Add(btnSubmitEval);
            Name = "FormInterviewEvaluation";
            Text = "Interview Evaluation";
            Load += FormInterviewEvaluation_Load;

            ((System.ComponentModel.ISupportInitialize)(dgvScheduled)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}