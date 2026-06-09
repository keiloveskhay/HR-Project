namespace HR_Recruitment_Workflow_Jared
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1460, 505);
            Controls.Add(btnLockReview);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(dgvApplicants);
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
    }
}
