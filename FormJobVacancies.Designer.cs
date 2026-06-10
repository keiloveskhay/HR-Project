namespace HR_Project
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvJobVacancies = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnMyApplications = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvJobVacancies)).BeginInit();
            this.SuspendLayout();

            // txtSearch
            this.txtSearch.Location = new System.Drawing.Point(111, 129);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(734, 23);

            // btnSearch
            this.btnSearch.Location = new System.Drawing.Point(867, 120);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(154, 38);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // dgvJobVacancies
            this.dgvJobVacancies.Location = new System.Drawing.Point(111, 174);
            this.dgvJobVacancies.Name = "dgvJobVacancies";
            this.dgvJobVacancies.Size = new System.Drawing.Size(734, 298);
            this.dgvJobVacancies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // lblTitle
            this.lblTitle.Location = new System.Drawing.Point(219, 45);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(734, 50);
            this.lblTitle.Text = "JOB VACANCIES";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);

            // btnApply
            this.btnApply.Location = new System.Drawing.Point(867, 261);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(154, 36);
            this.btnApply.Text = "APPLY";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);

            // btnMyApplications
            this.btnMyApplications.Location = new System.Drawing.Point(867, 358);
            this.btnMyApplications.Name = "btnMyApplications";
            this.btnMyApplications.Size = new System.Drawing.Size(154, 35);
            this.btnMyApplications.Text = "My Applications";
            this.btnMyApplications.Click += new System.EventHandler(this.btnMyApplications_Click);

            // FormJobVacancies
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 547);
            this.Controls.Add(this.btnMyApplications);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvJobVacancies);
            this.Name = "FormJobVacancies";
            this.Text = "Job Vacancies";
            this.Load += new System.EventHandler(this.FormJobVacancies_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvJobVacancies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvJobVacancies;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnMyApplications;
    }
}