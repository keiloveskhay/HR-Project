namespace HRApplicantSystem
{
    partial class frmMyApplication
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvApplications = new DataGridView();
            btnViewStatus = new Button();
            btnEditApplication = new Button();
            btnWithdraw = new Button();
            btnViewDocuments = new Button();
            lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvApplications).BeginInit();
            SuspendLayout();
            // 
            // dgvApplications
            // 
            dgvApplications.Location = new Point(92, 158);
            dgvApplications.Name = "dgvApplications";
            dgvApplications.Size = new Size(538, 306);
            dgvApplications.TabIndex = 0;
            dgvApplications.CellContentClick += dgvApplications_CellContentClick;
            // 
            // btnViewStatus
            // 
            btnViewStatus.BackColor = SystemColors.ActiveCaptionText;
            btnViewStatus.ForeColor = SystemColors.ButtonFace;
            btnViewStatus.Location = new Point(708, 158);
            btnViewStatus.Name = "btnViewStatus";
            btnViewStatus.Size = new Size(150, 40);
            btnViewStatus.TabIndex = 1;
            btnViewStatus.Text = "View Status";
            btnViewStatus.UseVisualStyleBackColor = false;
            btnViewStatus.Click += btnViewStatus_Click;
            // 
            // btnEditApplication
            // 
            btnEditApplication.BackColor = SystemColors.ActiveCaptionText;
            btnEditApplication.ForeColor = SystemColors.ButtonHighlight;
            btnEditApplication.Location = new Point(708, 208);
            btnEditApplication.Name = "btnEditApplication";
            btnEditApplication.Size = new Size(150, 40);
            btnEditApplication.TabIndex = 2;
            btnEditApplication.Text = "Edit Application";
            btnEditApplication.UseVisualStyleBackColor = false;
            btnEditApplication.Click += btnEditApplication_Click;
            // 
            // btnWithdraw
            // 
            btnWithdraw.BackColor = SystemColors.ActiveCaptionText;
            btnWithdraw.ForeColor = SystemColors.ButtonHighlight;
            btnWithdraw.Location = new Point(708, 258);
            btnWithdraw.Name = "btnWithdraw";
            btnWithdraw.Size = new Size(150, 40);
            btnWithdraw.TabIndex = 3;
            btnWithdraw.Text = "Withdraw";
            btnWithdraw.UseVisualStyleBackColor = false;
            btnWithdraw.Click += btnWithdraw_Click;
            // 
            // btnViewDocuments
            // 
            btnViewDocuments.BackColor = SystemColors.ActiveCaptionText;
            btnViewDocuments.ForeColor = SystemColors.ButtonHighlight;
            btnViewDocuments.Location = new Point(708, 408);
            btnViewDocuments.Name = "btnViewDocuments";
            btnViewDocuments.Size = new Size(150, 40);
            btnViewDocuments.TabIndex = 4;
            btnViewDocuments.Text = "View Documents";
            btnViewDocuments.UseVisualStyleBackColor = false;
            btnViewDocuments.Click += btnViewDocuments_Click;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = SystemColors.ActiveCaptionText;
            lblTitle.Font = new Font("Arial", 20F, FontStyle.Bold);
            lblTitle.ForeColor = SystemColors.ButtonHighlight;
            lblTitle.Location = new Point(92, 60);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(766, 50);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "MY APPLICATIONS";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmMyApplication
            // 
            ClientSize = new Size(986, 543);
            Controls.Add(dgvApplications);
            Controls.Add(btnViewStatus);
            Controls.Add(btnEditApplication);
            Controls.Add(btnWithdraw);
            Controls.Add(btnViewDocuments);
            Controls.Add(lblTitle);
            Name = "frmMyApplication";
            Load += frmMyApplication_Load;
            ((System.ComponentModel.ISupportInitialize)dgvApplications).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvApplications;
        private System.Windows.Forms.Button btnViewStatus;
        private System.Windows.Forms.Button btnEditApplication;
        private System.Windows.Forms.Button btnWithdraw;
        private System.Windows.Forms.Button btnViewDocuments;
        private System.Windows.Forms.Label lblTitle;
    }
}