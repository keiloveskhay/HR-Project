using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class FrmMyDocuments
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
            this.lblRequirement = new Label();
            this.cmbRequirements = new ComboBox();
            this.txtFilePath = new TextBox();
            this.btnBrowse = new Button();
            this.btnUpload = new Button();
            this.dgvDocuments = new DataGridView();
            this.btnOpenStatus = new Button();
            ((System.ComponentModel.ISupportInitialize)this.dgvDocuments).BeginInit();
            this.SuspendLayout();

            // lblRequirement
            this.lblRequirement.AutoSize = true;
            this.lblRequirement.Location = new Point(61, 32);
            this.lblRequirement.Name = "lblRequirement";
            this.lblRequirement.Size = new Size(132, 20);
            this.lblRequirement.TabIndex = 0;
            this.lblRequirement.Text = "Requirement Type:";

            // cmbRequirements
            this.cmbRequirements.FormattingEnabled = true;
            this.cmbRequirements.Location = new Point(61, 65);
            this.cmbRequirements.Name = "cmbRequirements";
            this.cmbRequirements.Size = new Size(383, 28);
            this.cmbRequirements.TabIndex = 1;

            // txtFilePath
            this.txtFilePath.Location = new Point(61, 114);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new Size(283, 27);
            this.txtFilePath.TabIndex = 2;

            // btnBrowse
            this.btnBrowse.Location = new Point(350, 114);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new Size(94, 29);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += btnBrowse_Click;

            // btnUpload
            this.btnUpload.Location = new Point(350, 149);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new Size(94, 29);
            this.btnUpload.TabIndex = 4;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += btnUpload_Click;

            // dgvDocuments
            this.dgvDocuments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocuments.Location = new Point(61, 265);
            this.dgvDocuments.Name = "dgvDocuments";
            this.dgvDocuments.RowHeadersWidth = 51;
            this.dgvDocuments.Size = new Size(595, 187);
            this.dgvDocuments.TabIndex = 5;

            // btnOpenStatus
            this.btnOpenStatus.Location = new Point(450, 149);
            this.btnOpenStatus.Name = "btnOpenStatus";
            this.btnOpenStatus.Size = new Size(206, 29);
            this.btnOpenStatus.TabIndex = 6;
            this.btnOpenStatus.Text = "Application Status";
            this.btnOpenStatus.UseVisualStyleBackColor = true;
            this.btnOpenStatus.Click += btnOpenStatus_Click;

            // FrmMyDocuments
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(930, 588);
            this.Controls.Add(this.btnOpenStatus);
            this.Controls.Add(this.dgvDocuments);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.cmbRequirements);
            this.Controls.Add(this.lblRequirement);
            this.Name = "FrmMyDocuments";
            this.Text = "My Documents";
            this.Load += FrmMyDocuments_Load;

            ((System.ComponentModel.ISupportInitialize)this.dgvDocuments).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Label lblRequirement;
        private ComboBox cmbRequirements;
        private TextBox txtFilePath;
        private Button btnBrowse;
        private Button btnUpload;
        private DataGridView dgvDocuments;
        private Button btnOpenStatus;
    }
}