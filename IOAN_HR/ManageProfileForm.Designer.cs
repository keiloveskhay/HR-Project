using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IDk
{
    partial class ManageProfileForm
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
            // ManageProfileForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ManageProfileForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
