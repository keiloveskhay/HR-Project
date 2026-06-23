using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
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

            btnWithdraw = new Button();
            btnViewDocuments = new Button();
            lblTitle = new Label();
            lblMissingDocs = new Label();
            lstMissingDocs = new ListBox();
            lblRecentUpdates = new Label();
            dgvRecentUpdates = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvApplications).BeginInit();
            SuspendLayout();

            // dgvApplications
            dgvApplications.Location = new Point(92, 158);
            dgvApplications.Name = "dgvApplications";
            dgvApplications.Size = new Size(538, 150);
            dgvApplications.TabIndex = 0;
            dgvApplications.SelectionChanged += new System.EventHandler(this.dgvApplications_SelectionChanged);
            
            // lblMissingDocs
            lblMissingDocs.Location = new Point(92, 320);
            lblMissingDocs.Name = "lblMissingDocs";
            lblMissingDocs.Size = new Size(260, 20);
            lblMissingDocs.Text = "Missing Documents:";
            lblMissingDocs.ForeColor = SystemColors.ButtonHighlight;
            lblMissingDocs.BackColor = SystemColors.ActiveCaptionText;

            // lstMissingDocs
            lstMissingDocs.Location = new Point(92, 345);
            lstMissingDocs.Name = "lstMissingDocs";
            lstMissingDocs.Size = new Size(260, 119);
            lstMissingDocs.TabIndex = 6;

            // lblRecentUpdates
            lblRecentUpdates.Location = new Point(370, 320);
            lblRecentUpdates.Name = "lblRecentUpdates";
            lblRecentUpdates.Size = new Size(260, 20);
            lblRecentUpdates.Text = "Recent Updates:";
            lblRecentUpdates.ForeColor = SystemColors.ButtonHighlight;
            lblRecentUpdates.BackColor = SystemColors.ActiveCaptionText;

            // dgvRecentUpdates
            dgvRecentUpdates.Location = new Point(370, 345);
            dgvRecentUpdates.Name = "dgvRecentUpdates";
            dgvRecentUpdates.Size = new Size(260, 119);
            dgvRecentUpdates.TabIndex = 7;
            dgvRecentUpdates.ReadOnly = true;
            dgvRecentUpdates.AllowUserToAddRows = false;
            ((System.ComponentModel.ISupportInitialize)dgvRecentUpdates).BeginInit();


            // btnViewStatus
            btnViewStatus.BackColor = SystemColors.ActiveCaptionText;
            btnViewStatus.ForeColor = SystemColors.ButtonFace;
            btnViewStatus.Location = new Point(708, 158);
            btnViewStatus.Name = "btnViewStatus";
            btnViewStatus.Size = new Size(150, 40);
            btnViewStatus.TabIndex = 1;
            btnViewStatus.Text = "View Status";
            btnViewStatus.UseVisualStyleBackColor = false;
            btnViewStatus.Click += new System.EventHandler(this.btnViewStatus_Click);


            // btnWithdraw
            btnWithdraw.BackColor = SystemColors.ActiveCaptionText;
            btnWithdraw.ForeColor = SystemColors.ButtonHighlight;
            btnWithdraw.Location = new Point(708, 258);
            btnWithdraw.Name = "btnWithdraw";
            btnWithdraw.Size = new Size(150, 40);
            btnWithdraw.TabIndex = 3;
            btnWithdraw.Text = "Withdraw";
            btnWithdraw.UseVisualStyleBackColor = false;
            btnWithdraw.Click += new System.EventHandler(this.btnWithdraw_Click);

            // btnViewDocuments
            btnViewDocuments.BackColor = SystemColors.ActiveCaptionText;
            btnViewDocuments.ForeColor = SystemColors.ButtonHighlight;
            btnViewDocuments.Location = new Point(708, 408);
            btnViewDocuments.Name = "btnViewDocuments";
            btnViewDocuments.Size = new Size(150, 40);
            btnViewDocuments.TabIndex = 4;
            btnViewDocuments.Text = "View Documents";
            btnViewDocuments.UseVisualStyleBackColor = false;
            btnViewDocuments.Click += new System.EventHandler(this.btnViewDocuments_Click);

            // lblTitle
            lblTitle.BackColor = SystemColors.ActiveCaptionText;
            lblTitle.Font = new Font("Arial", 20F, FontStyle.Bold);
            lblTitle.ForeColor = SystemColors.ButtonHighlight;
            lblTitle.Location = new Point(92, 60);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(766, 50);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "MY APPLICATIONS";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // frmMyApplication
            ClientSize = new Size(986, 543);
            Controls.Add(dgvApplications);
            Controls.Add(btnViewStatus);
            Controls.Add(btnWithdraw);
            Controls.Add(btnViewDocuments);
            Controls.Add(lblTitle);
            Controls.Add(lblMissingDocs);
            Controls.Add(lstMissingDocs);
            Controls.Add(lblRecentUpdates);
            Controls.Add(dgvRecentUpdates);
            Name = "frmMyApplication";
            Text = "My Applications";
            Load += new System.EventHandler(this.frmMyApplication_Load);

            ((System.ComponentModel.ISupportInitialize)dgvApplications).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRecentUpdates).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvApplications;
        private System.Windows.Forms.Button btnViewStatus;
        private System.Windows.Forms.Button btnWithdraw;
        private System.Windows.Forms.Button btnViewDocuments;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMissingDocs;
        private System.Windows.Forms.ListBox lstMissingDocs;
        private System.Windows.Forms.Label lblRecentUpdates;
        private System.Windows.Forms.DataGridView dgvRecentUpdates;
    }
}