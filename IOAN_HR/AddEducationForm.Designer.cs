using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IDk
{
    partial class AddEducationForm
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
            // AddEducationForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "AddEducationForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
