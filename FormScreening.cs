namespace HR_Recruitment_Workflow_Jared
{
    partial class FormApplicantReview
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
            btnLock = new Button();
            txtDetail = new TextBox();
            SuspendLayout();
            // 
            // txtAppID
            // 
            txtAppID.Location = new Point(322, 135);
            txtAppID.Name = "txtAppID";
            txtAppID.Size = new Size(125, 27);
            txtAppID.TabIndex = 0;
            // 
            // btnLock
            // 
            btnLock.Location = new Point(247, 168);
            btnLock.Name = "btnLock";
            btnLock.Size = new Size(281, 29);
            btnLock.TabIndex = 1;
            btnLock.Text = "Lock Application (Under Review)";
            btnLock.UseVisualStyleBackColor = true;
            btnLock.Click += btnLock_Click;
            // 
            // txtDetail
            // 
            txtDetail.Location = new Point(322, 203);
            txtDetail.Multiline = true;
            txtDetail.Name = "txtDetail";
            txtDetail.Size = new Size(125, 34);
            txtDetail.TabIndex = 2;
            // 
            // FormApplicantReview
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtDetail);
            Controls.Add(btnLock);
            Controls.Add(txtAppID);
            Name = "FormApplicantReview";
            Text = "FormApplicantReview";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtAppID;
        private Button btnLock;
        private TextBox txtDetail;
    }
}