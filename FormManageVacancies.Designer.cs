using System;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class FormManageVacancies
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

        private DataGridView dgvVacancies;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblQualifications;
        private TextBox txtQualifications;
        private Label lblRequiredDocs;
        private TextBox txtRequiredDocs;
        private Button btnSave;
        private Button btnCloseVacancy;
        private Button btnReopenVacancy;
        private Button btnAdd;
        private Label lblStatus;

        private void InitializeComponent()
        {
            dgvVacancies = new DataGridView();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblQualifications = new Label();
            txtQualifications = new TextBox();
            lblRequiredDocs = new Label();
            txtRequiredDocs = new TextBox();
            btnSave = new Button();
            btnCloseVacancy = new Button();
            btnReopenVacancy = new Button();
            btnAdd = new Button();
            lblStatus = new Label();

            ((System.ComponentModel.ISupportInitialize)(dgvVacancies)).BeginInit();
            SuspendLayout();

            dgvVacancies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVacancies.Location = new Point(20, 20);
            dgvVacancies.Name = "dgvVacancies";
            dgvVacancies.RowHeadersWidth = 51;
            dgvVacancies.Size = new Size(840, 250);
            dgvVacancies.TabIndex = 0;
            dgvVacancies.SelectionChanged += dgvVacancies_SelectionChanged;
            dgvVacancies.ReadOnly = true;

            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(20, 290);
            lblDescription.Text = "Job Description:";

            txtDescription.Location = new Point(160, 290);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(700, 60);
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.TabIndex = 1;

            lblQualifications.AutoSize = true;
            lblQualifications.Location = new Point(20, 360);
            lblQualifications.Text = "Qualifications:";

            txtQualifications.Location = new Point(160, 360);
            txtQualifications.Multiline = true;
            txtQualifications.Name = "txtQualifications";
            txtQualifications.Size = new Size(700, 60);
            txtQualifications.ScrollBars = ScrollBars.Vertical;
            txtQualifications.TabIndex = 2;

            lblRequiredDocs.AutoSize = true;
            lblRequiredDocs.Location = new Point(20, 430);
            lblRequiredDocs.Text = "Required Documents:";

            txtRequiredDocs.Location = new Point(160, 430);
            txtRequiredDocs.Multiline = true;
            txtRequiredDocs.Name = "txtRequiredDocs";
            txtRequiredDocs.Size = new Size(700, 60);
            txtRequiredDocs.ScrollBars = ScrollBars.Vertical;
            txtRequiredDocs.TabIndex = 3;

            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.Location = new Point(20, 510);
            lblStatus.Text = "Status: -";

            btnSave.Location = new Point(160, 510);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 35);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save Changes";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;

            btnCloseVacancy.Location = new Point(330, 510);
            btnCloseVacancy.Name = "btnCloseVacancy";
            btnCloseVacancy.Size = new Size(150, 35);
            btnCloseVacancy.TabIndex = 5;
            btnCloseVacancy.Text = "Close Vacancy";
            btnCloseVacancy.BackColor = Color.LightCoral;
            btnCloseVacancy.UseVisualStyleBackColor = false;
            btnCloseVacancy.Click += btnCloseVacancy_Click;

            btnReopenVacancy.Location = new Point(500, 510);
            btnReopenVacancy.Name = "btnReopenVacancy";
            btnReopenVacancy.Size = new Size(150, 35);
            btnReopenVacancy.TabIndex = 6;
            btnReopenVacancy.Text = "Reopen Vacancy";
            btnReopenVacancy.BackColor = Color.LightGreen;
            btnReopenVacancy.UseVisualStyleBackColor = false;
            btnReopenVacancy.Click += btnReopenVacancy_Click;

            btnAdd.Location = new Point(670, 510);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(150, 35);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Add New Vacancy";
            btnAdd.BackColor = Color.LightBlue;
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(880, 580);
            Controls.Add(dgvVacancies);
            Controls.Add(lblDescription);
            Controls.Add(txtDescription);
            Controls.Add(lblQualifications);
            Controls.Add(txtQualifications);
            Controls.Add(lblRequiredDocs);
            Controls.Add(txtRequiredDocs);
            Controls.Add(lblStatus);
            Controls.Add(btnSave);
            Controls.Add(btnCloseVacancy);
            Controls.Add(btnReopenVacancy);
            Controls.Add(btnAdd);
            Name = "FormManageVacancies";
            Text = "Manage Job Vacancies";
            Load += FormManageVacancies_Load;

            ((System.ComponentModel.ISupportInitialize)(dgvVacancies)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
