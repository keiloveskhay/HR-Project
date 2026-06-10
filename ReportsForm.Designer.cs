using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class ReportsForm
    {
        private IContainer components = null;

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
            this.summaryLabel = new System.Windows.Forms.Label();
            this.refreshBtn = new System.Windows.Forms.Button();
            this.exportBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // summaryLabel
            // 
            this.summaryLabel.Location = new System.Drawing.Point(12, 9);
            this.summaryLabel.Name = "summaryLabel";
            this.summaryLabel.Size = new System.Drawing.Size(560, 180);
            this.summaryLabel.TabIndex = 0;
            this.summaryLabel.Text = "Loading report...";
            this.summaryLabel.AutoSize = false;

            // 
            // refreshBtn
            // 
            this.refreshBtn.Location = new System.Drawing.Point(12, 200);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(120, 30);
            this.refreshBtn.TabIndex = 1;
            this.refreshBtn.Text = "Refresh";
            this.refreshBtn.UseVisualStyleBackColor = true;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);

            // 
            // exportBtn
            // 
            this.exportBtn.Location = new System.Drawing.Point(150, 200);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(120, 30);
            this.exportBtn.TabIndex = 2;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);

            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 260);
            this.Controls.Add(this.summaryLabel);
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.exportBtn);
            this.Name = "ReportsForm";
            this.Text = "Reports Dashboard";
            this.ResumeLayout(false);
        }

        #endregion

        private Label summaryLabel;
        private Button refreshBtn;
        private Button exportBtn;
    }
}