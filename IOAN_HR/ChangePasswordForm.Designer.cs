using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IDk
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
            components = new System.ComponentModel.Container();
            this.SuspendLayout();
            // 
            // ChangePasswordForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ChangePasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
