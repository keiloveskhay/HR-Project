using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class HR_Dashboard
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
            this.btnUserManagement = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnApplicantList = new System.Windows.Forms.Button();
            this.btnAddVacancy = new System.Windows.Forms.Button();
            this.btnHiringDecision = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnMaintenance = new System.Windows.Forms.Button();
            this.btnInterviewSchedule = new System.Windows.Forms.Button();
            this.btnInterviewEvaluation = new System.Windows.Forms.Button();
            this.btnScreening = new System.Windows.Forms.Button();
            this.btnApplicantReview = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.Location = new System.Drawing.Point(41, 98);
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.Size = new System.Drawing.Size(127, 23);
            this.btnUserManagement.TabIndex = 0;
            this.btnUserManagement.Text = "User Management";
            this.btnUserManagement.UseVisualStyleBackColor = true;
            this.btnUserManagement.Click += new System.EventHandler(this.btnUserManagement_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(472, 364);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnApplicantList
            // 
            this.btnApplicantList.Location = new System.Drawing.Point(204, 98);
            this.btnApplicantList.Name = "btnApplicantList";
            this.btnApplicantList.Size = new System.Drawing.Size(127, 23);
            this.btnApplicantList.TabIndex = 2;
            this.btnApplicantList.Text = "Applicant List";
            this.btnApplicantList.UseVisualStyleBackColor = true;
            this.btnApplicantList.Click += new System.EventHandler(this.btnApplicantList_Click);
            // 
            // btnAddVacancy
            // 
            this.btnAddVacancy.Location = new System.Drawing.Point(41, 151);
            this.btnAddVacancy.Name = "btnAddVacancy";
            this.btnAddVacancy.Size = new System.Drawing.Size(125, 23);
            this.btnAddVacancy.TabIndex = 3;
            this.btnAddVacancy.Text = "Add Job Vacancies";
            this.btnAddVacancy.UseVisualStyleBackColor = true;
            this.btnAddVacancy.Click += new System.EventHandler(this.btnAddVacancy_Click);
            // 
            // btnHiringDecision
            // 
            this.btnHiringDecision.Location = new System.Drawing.Point(41, 204);
            this.btnHiringDecision.Name = "btnHiringDecision";
            this.btnHiringDecision.Size = new System.Drawing.Size(127, 23);
            this.btnHiringDecision.TabIndex = 4;
            this.btnHiringDecision.Text = "Hiring Decision";
            this.btnHiringDecision.UseVisualStyleBackColor = true;
            this.btnHiringDecision.Click += new System.EventHandler(this.btnHiringDecision_Click);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(41, 255);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(125, 23);
            this.btnReports.TabIndex = 5;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnMaintenance
            // 
            this.btnMaintenance.Location = new System.Drawing.Point(41, 303);
            this.btnMaintenance.Name = "btnMaintenance";
            this.btnMaintenance.Size = new System.Drawing.Size(125, 23);
            this.btnMaintenance.TabIndex = 6;
            this.btnMaintenance.Text = "Maintenance";
            this.btnMaintenance.UseVisualStyleBackColor = true;
            this.btnMaintenance.Click += new System.EventHandler(this.btnMaintenance_Click);
            // 
            // btnInterviewSchedule
            // 
            this.btnInterviewSchedule.Location = new System.Drawing.Point(206, 255);
            this.btnInterviewSchedule.Name = "btnInterviewSchedule";
            this.btnInterviewSchedule.Size = new System.Drawing.Size(125, 23);
            this.btnInterviewSchedule.TabIndex = 7;
            this.btnInterviewSchedule.Text = "Interview Schedule";
            this.btnInterviewSchedule.UseVisualStyleBackColor = true;
            this.btnInterviewSchedule.Click += new System.EventHandler(this.btnInterviewSchedule_Click);
            // 
            // btnInterviewEvaluation
            // 
            this.btnInterviewEvaluation.Location = new System.Drawing.Point(206, 303);
            this.btnInterviewEvaluation.Name = "btnInterviewEvaluation";
            this.btnInterviewEvaluation.Size = new System.Drawing.Size(125, 23);
            this.btnInterviewEvaluation.TabIndex = 8;
            this.btnInterviewEvaluation.Text = "Interview Evaluation";
            this.btnInterviewEvaluation.UseVisualStyleBackColor = true;
            this.btnInterviewEvaluation.Click += new System.EventHandler(this.btnInterviewEvaluation_Click);
            // 
            // btnScreening
            // 
            this.btnScreening.Location = new System.Drawing.Point(206, 204);
            this.btnScreening.Name = "btnScreening";
            this.btnScreening.Size = new System.Drawing.Size(125, 23);
            this.btnScreening.TabIndex = 9;
            this.btnScreening.Text = "Screening";
            this.btnScreening.UseVisualStyleBackColor = true;
            this.btnScreening.Click += new System.EventHandler(this.btnScreening_Click);
            // 
            // btnApplicantReview
            // 
            this.btnApplicantReview.Location = new System.Drawing.Point(206, 151);
            this.btnApplicantReview.Name = "btnApplicantReview";
            this.btnApplicantReview.Size = new System.Drawing.Size(125, 23);
            this.btnApplicantReview.TabIndex = 10;
            this.btnApplicantReview.Text = "Applicant Review";
            this.btnApplicantReview.UseVisualStyleBackColor = true;
            this.btnApplicantReview.Click += new System.EventHandler(this.btnApplicantReview_Click);
            // 
            // HR_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnApplicantReview);
            this.Controls.Add(this.btnScreening);
            this.Controls.Add(this.btnInterviewEvaluation);
            this.Controls.Add(this.btnInterviewSchedule);
            this.Controls.Add(this.btnMaintenance);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnHiringDecision);
            this.Controls.Add(this.btnAddVacancy);
            this.Controls.Add(this.btnApplicantList);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnUserManagement);
            this.Name = "HR_Dashboard";
            this.Text = "HR_Dashboard";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUserManagement;
        private System.Windows.Forms.Button btnLogout;
        private Button btnApplicantList;
        private Button btnAddVacancy;
        private Button btnHiringDecision;
        private Button btnReports;
        private Button btnMaintenance;
        private Button btnInterviewSchedule;
        private Button btnInterviewEvaluation;
        private Button btnScreening;
        private Button btnApplicantReview;
    }
}