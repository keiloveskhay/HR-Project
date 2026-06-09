namespace HR_Recruitment_Workflow_Jared
{
    partial class FormInterviewEvaluation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtAppID = new TextBox();
            txtScore = new TextBox();
            txtFeedback = new TextBox();
            btnSubmitEval = new Button();
            SuspendLayout();
            // 
            // txtAppID
            // 
            txtAppID.Location = new Point(331, 145);
            txtAppID.Name = "txtAppID";
            txtAppID.Size = new Size(125, 27);
            txtAppID.TabIndex = 0;
            // 
            // txtScore
            // 
            txtScore.Location = new Point(331, 178);
            txtScore.Name = "txtScore";
            txtScore.Size = new Size(125, 27);
            txtScore.TabIndex = 1;
            // 
            // txtFeedback
            // 
            txtFeedback.Location = new Point(331, 211);
            txtFeedback.Multiline = true;
            txtFeedback.Name = "txtFeedback";
            txtFeedback.Size = new Size(125, 34);
            txtFeedback.TabIndex = 2;
            // 
            // btnSubmitEval
            // 
            btnSubmitEval.Location = new Point(279, 251);
            btnSubmitEval.Name = "btnSubmitEval";
            btnSubmitEval.Size = new Size(232, 29);
            btnSubmitEval.TabIndex = 3;
            btnSubmitEval.Text = "Submit Interview Evaluation";
            btnSubmitEval.UseVisualStyleBackColor = true;
            btnSubmitEval.Click += btnSubmitEval_Click;
            // 
            // FormInterviewEvaluation
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSubmitEval);
            Controls.Add(txtFeedback);
            Controls.Add(txtScore);
            Controls.Add(txtAppID);
            Name = "FormInterviewEvaluation";
            Text = "FormInterviewEvaluation";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtAppID;
        private TextBox txtScore;
        private TextBox txtFeedback;
        private Button btnSubmitEval;
    }
}