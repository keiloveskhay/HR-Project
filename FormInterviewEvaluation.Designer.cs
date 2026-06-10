using System;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class FormInterviewEvaluation
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
            this.txtAppID = new System.Windows.Forms.TextBox();
            this.txtScore = new System.Windows.Forms.TextBox();
            this.txtFeedback = new System.Windows.Forms.TextBox();
            this.btnSubmitEval = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // txtAppID
            this.txtAppID.Location = new System.Drawing.Point(331, 145);
            this.txtAppID.Name = "txtAppID";
            this.txtAppID.Size = new System.Drawing.Size(125, 27);
            this.txtAppID.TabIndex = 0;

            // txtScore
            this.txtScore.Location = new System.Drawing.Point(331, 178);
            this.txtScore.Name = "txtScore";
            this.txtScore.Size = new System.Drawing.Size(125, 27);
            this.txtScore.TabIndex = 1;

            // txtFeedback
            this.txtFeedback.Location = new System.Drawing.Point(331, 211);
            this.txtFeedback.Multiline = true;
            this.txtFeedback.Name = "txtFeedback";
            this.txtFeedback.Size = new System.Drawing.Size(125, 34);
            this.txtFeedback.TabIndex = 2;

            // btnSubmitEval
            this.btnSubmitEval.Location = new System.Drawing.Point(279, 251);
            this.btnSubmitEval.Name = "btnSubmitEval";
            this.btnSubmitEval.Size = new System.Drawing.Size(232, 29);
            this.btnSubmitEval.TabIndex = 3;
            this.btnSubmitEval.Text = "Submit Interview Evaluation";
            this.btnSubmitEval.UseVisualStyleBackColor = true;
            this.btnSubmitEval.Click += new System.EventHandler(this.btnSubmitEval_Click);

            // FormInterviewEvaluation
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSubmitEval);
            this.Controls.Add(this.txtFeedback);
            this.Controls.Add(this.txtScore);
            this.Controls.Add(this.txtAppID);
            this.Name = "FormInterviewEvaluation";
            this.Text = "FormInterviewEvaluation";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtAppID;
        private System.Windows.Forms.TextBox txtScore;
        private System.Windows.Forms.TextBox txtFeedback;
        private System.Windows.Forms.Button btnSubmitEval;
    }
}