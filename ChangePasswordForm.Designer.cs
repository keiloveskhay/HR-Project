using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HR_Project
{
    partial class ChangePasswordForm
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

        private void InitializeComponent()
        {
            this.components = new Container();
            this.SuspendLayout();

            // 
            // ChangePasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 220);
            this.Name = "ChangePasswordForm";
            this.Text = "Change Password";

            this.ResumeLayout(false);
        }
    }
}