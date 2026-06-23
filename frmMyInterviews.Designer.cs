using System;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class frmMyInterviews
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
        private DataGridView dgvInterviews;
        private Button btnClose;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            dgvInterviews = new DataGridView();
            btnClose = new Button();

            ((System.ComponentModel.ISupportInitialize)(dgvInterviews)).BeginInit();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Text = "My Interview Schedules";

            dgvInterviews.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInterviews.Location = new Point(20, 50);
            dgvInterviews.Name = "dgvInterviews";
            dgvInterviews.RowHeadersWidth = 51;
            dgvInterviews.Size = new Size(740, 300);
            dgvInterviews.TabIndex = 0;
            dgvInterviews.ReadOnly = true;

            btnClose.Location = new Point(640, 370);
            btnClose.Size = new Size(120, 35);
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += (s, e) => Close();

            ClientSize = new Size(800, 450);
            Controls.Add(lblTitle);
            Controls.Add(dgvInterviews);
            Controls.Add(btnClose);
            Name = "frmMyInterviews";
            Text = "My Interviews";
            Load += frmMyInterviews_Load;

            ((System.ComponentModel.ISupportInitialize)(dgvInterviews)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
