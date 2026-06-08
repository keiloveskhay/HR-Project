using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IDK2
{
    partial class HiringDecisionForm
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
            // HiringDecisionForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "HiringDecisionForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
