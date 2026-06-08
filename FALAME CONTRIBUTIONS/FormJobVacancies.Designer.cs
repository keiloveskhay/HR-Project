namespace HRApplicantSystem
{
    partial class FormJobVacancies
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtSearch = new TextBox();
            tnt = new Button();
            dgvJobVacancies = new DataGridView();
            lblTitle = new Label();
            btnApply = new Button();
            btnMyApplications = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvJobVacancies).BeginInit();
            SuspendLayout();
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(111, 129);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(734, 23);
            txtSearch.TabIndex = 1;
            // 
            // tnt
            // 
            tnt.BackColor = SystemColors.ActiveCaptionText;
            tnt.ForeColor = Color.White;
            tnt.Location = new Point(867, 120);
            tnt.Name = "tnt";
            tnt.Size = new Size(154, 38);
            tnt.TabIndex = 2;
            tnt.Text = "Search";
            tnt.UseVisualStyleBackColor = false;
            tnt.Click += btnSearch_Click;
            // 
            // dgvJobVacancies
            // 
            dgvJobVacancies.BackgroundColor = SystemColors.ButtonFace;
            dgvJobVacancies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvJobVacancies.Location = new Point(111, 174);
            dgvJobVacancies.Name = "dgvJobVacancies";
            dgvJobVacancies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJobVacancies.Size = new Size(734, 298);
            dgvJobVacancies.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = SystemColors.ActiveCaptionText;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = SystemColors.ControlLightLight;
            lblTitle.Location = new Point(219, 45);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(734, 50);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "JOB VACANCIES";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnApply
            // 
            btnApply.BackColor = SystemColors.ActiveCaptionText;
            btnApply.ForeColor = Color.White;
            btnApply.Location = new Point(867, 261);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(154, 36);
            btnApply.TabIndex = 5;
            btnApply.Text = "APPLY";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnApply_Click;
            // 
            // btnMyApplications
            // 
            btnMyApplications.BackColor = SystemColors.ActiveCaptionText;
            btnMyApplications.ForeColor = SystemColors.ButtonHighlight;
            btnMyApplications.Location = new Point(867, 358);
            btnMyApplications.Name = "btnMyApplications";
            btnMyApplications.Size = new Size(154, 35);
            btnMyApplications.TabIndex = 6;
            btnMyApplications.Text = "My Applications";
            btnMyApplications.UseVisualStyleBackColor = false;
            btnMyApplications.Click += btnMyApplications_Click;
            // 
            // FormJobVacancies
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonShadow;
            ClientSize = new Size(1138, 547);
            Controls.Add(btnMyApplications);
            Controls.Add(btnApply);
            Controls.Add(lblTitle);
            Controls.Add(tnt);
            Controls.Add(txtSearch);
            Controls.Add(dgvJobVacancies);
            Name = "FormJobVacancies";
            Load += FormJobVacancies_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvJobVacancies).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button tnt;
        private System.Windows.Forms.DataGridView dgvJobVacancies;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnApply;
        private Button btnMyApplications;
    }
}