using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IDK2
{
    partial class AddVacancyForm
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
            // AddVacancyForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "AddVacancyForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
