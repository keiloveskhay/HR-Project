using System;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class FrmApplicationStatus
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
            this.dgvStatusHistory = new DataGridView();
            this.lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatusHistory)).BeginInit();
            this.SuspendLayout();

            // dgvStatusHistory
            this.dgvStatusHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatusHistory.Location = new Point(12, 65);
            this.dgvStatusHistory.Name = "dgvStatusHistory";
            this.dgvStatusHistory.RowHeadersWidth = 51;
            this.dgvStatusHistory.Size = new Size(784, 188);
            this.dgvStatusHistory.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(12, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(181, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Application Status History";

            // FrmApplicationStatus
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(808, 450);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvStatusHistory);
            this.Name = "FrmApplicationStatus";
            this.Text = "Application Status";
            this.Load += FrmApplicationStatus_Load;

            ((System.ComponentModel.ISupportInitialize)(this.dgvStatusHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DataGridView dgvStatusHistory;
        private Label lblTitle;
    }
}