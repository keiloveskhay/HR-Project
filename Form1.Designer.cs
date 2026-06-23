using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvApplicants = new DataGridView();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnLockReview = new Button();
            cmbVacancies = new ComboBox();
            lblVacancies = new Label();
            lstMissingDocs = new ListBox();
            lblMissingDocs = new Label();
            btnViewProfile = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvApplicants).BeginInit();
            SuspendLayout();
            // 
            // dgvApplicants
            // 
            dgvApplicants.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvApplicants.Location = new Point(39, 64);
            dgvApplicants.Name = "dgvApplicants";
            dgvApplicants.RowHeadersWidth = 51;
            dgvApplicants.Size = new Size(847, 309);
            dgvApplicants.TabIndex = 0;
            dgvApplicants.SelectionChanged += dgvApplicants_SelectionChanged;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(39, 31);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 27);
            txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(245, 31);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click; // FIXED: Attached click event to code-behind handler
            // 
            // btnLockReview
            // 
            btnLockReview.Location = new Point(39, 379);
            btnLockReview.Name = "btnLockReview";
            btnLockReview.Size = new Size(196, 29);
            btnLockReview.TabIndex = 3;
            btnLockReview.Text = "Lock & Review Application";
            btnLockReview.UseVisualStyleBackColor = true;
            btnLockReview.Click += btnLockReview_Click;
            // 
            // lblVacancies
            // 
            lblVacancies.AutoSize = true;
            lblVacancies.Location = new Point(350, 35);
            lblVacancies.Name = "lblVacancies";
            lblVacancies.Size = new Size(100, 20);
            lblVacancies.TabIndex = 4;
            lblVacancies.Text = "Filter Vacancy:";
            // 
            // cmbVacancies
            // 
            cmbVacancies.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVacancies.Location = new Point(460, 31);
            cmbVacancies.Name = "cmbVacancies";
            cmbVacancies.Size = new Size(250, 28);
            cmbVacancies.TabIndex = 5;
            cmbVacancies.SelectedIndexChanged += cmbVacancies_SelectedIndexChanged;
            // 
            // lblMissingDocs
            // 
            lblMissingDocs.AutoSize = true;
            lblMissingDocs.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMissingDocs.Location = new Point(910, 35);
            lblMissingDocs.Name = "lblMissingDocs";
            lblMissingDocs.Size = new Size(160, 23);
            lblMissingDocs.TabIndex = 6;
            lblMissingDocs.Text = "Missing Documents:";
            // 
            // lstMissingDocs
            // 
            lstMissingDocs.FormattingEnabled = true;
            lstMissingDocs.ItemHeight = 20;
            lstMissingDocs.Location = new Point(910, 64);
            lstMissingDocs.Name = "lstMissingDocs";
            lstMissingDocs.Size = new Size(300, 304);
            lstMissingDocs.TabIndex = 7;
            // 
            // btnViewProfile
            // 
            btnViewProfile.Location = new Point(910, 379);
            btnViewProfile.Name = "btnViewProfile";
            btnViewProfile.Size = new Size(196, 29);
            btnViewProfile.TabIndex = 8;
            btnViewProfile.Text = "View Applicant Profile";
            btnViewProfile.UseVisualStyleBackColor = true;
            btnViewProfile.Click += btnViewProfile_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1460, 505);
            Controls.Add(btnLockReview);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(dgvApplicants);
            Controls.Add(lblVacancies);
            Controls.Add(cmbVacancies);
            Controls.Add(lblMissingDocs);
            Controls.Add(lstMissingDocs);
            Controls.Add(btnViewProfile);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvApplicants).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvApplicants;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnLockReview;
        private ComboBox cmbVacancies;
        private Label lblVacancies;
        private ListBox lstMissingDocs;
        private Label lblMissingDocs;
        private Button btnViewProfile;
    }
}