using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IDk
{
    partial class MainForm
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
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
