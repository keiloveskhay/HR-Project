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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnUserManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserManagement.ForeColor = System.Drawing.Color.White;
            this.btnUserManagement.Location = new System.Drawing.Point(93, 86);
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.Size = new System.Drawing.Size(164, 36);
            this.btnUserManagement.TabIndex = 0;
            this.btnUserManagement.Text = "User Management";
            this.btnUserManagement.UseVisualStyleBackColor = false;
            this.btnUserManagement.Click += new System.EventHandler(this.btnUserManagement_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(657, 397);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(94, 28);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnApplicantList
            // 
            this.btnApplicantList.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnApplicantList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplicantList.ForeColor = System.Drawing.Color.White;
            this.btnApplicantList.Location = new System.Drawing.Point(333, 38);
            this.btnApplicantList.Name = "btnApplicantList";
            this.btnApplicantList.Size = new System.Drawing.Size(164, 39);
            this.btnApplicantList.TabIndex = 2;
            this.btnApplicantList.Text = "Applicant List";
            this.btnApplicantList.UseVisualStyleBackColor = false;
            this.btnApplicantList.Click += new System.EventHandler(this.btnApplicantList_Click);
            // 
            // btnAddVacancy
            // 
            this.btnAddVacancy.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnAddVacancy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddVacancy.ForeColor = System.Drawing.Color.White;
            this.btnAddVacancy.Location = new System.Drawing.Point(95, 39);
            this.btnAddVacancy.Name = "btnAddVacancy";
            this.btnAddVacancy.Size = new System.Drawing.Size(162, 38);
            this.btnAddVacancy.TabIndex = 6;
            this.btnAddVacancy.Text = "Manage Vacancies";
            this.btnAddVacancy.UseVisualStyleBackColor = true;
            this.btnAddVacancy.Click += new System.EventHandler(this.btnAddVacancy_Click);
            // 
            // btnHiringDecision
            // 
            this.btnHiringDecision.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnHiringDecision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHiringDecision.ForeColor = System.Drawing.Color.White;
            this.btnHiringDecision.Location = new System.Drawing.Point(93, 131);
            this.btnHiringDecision.Name = "btnHiringDecision";
            this.btnHiringDecision.Size = new System.Drawing.Size(164, 36);
            this.btnHiringDecision.TabIndex = 4;
            this.btnHiringDecision.Text = "Hiring Decision";
            this.btnHiringDecision.UseVisualStyleBackColor = false;
            this.btnHiringDecision.Click += new System.EventHandler(this.btnHiringDecision_Click);
            // 
            // btnReports
            // 
            this.btnReports.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReports.ForeColor = System.Drawing.Color.White;
            this.btnReports.Location = new System.Drawing.Point(93, 177);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(164, 35);
            this.btnReports.TabIndex = 5;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnMaintenance
            // 
            this.btnMaintenance.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnMaintenance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaintenance.ForeColor = System.Drawing.Color.White;
            this.btnMaintenance.Location = new System.Drawing.Point(95, 220);
            this.btnMaintenance.Name = "btnMaintenance";
            this.btnMaintenance.Size = new System.Drawing.Size(162, 37);
            this.btnMaintenance.TabIndex = 6;
            this.btnMaintenance.Text = "Maintenance";
            this.btnMaintenance.UseVisualStyleBackColor = false;
            this.btnMaintenance.Click += new System.EventHandler(this.btnMaintenance_Click);
            // 
            // btnInterviewSchedule
            // 
            this.btnInterviewSchedule.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnInterviewSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInterviewSchedule.ForeColor = System.Drawing.Color.White;
            this.btnInterviewSchedule.Location = new System.Drawing.Point(335, 176);
            this.btnInterviewSchedule.Name = "btnInterviewSchedule";
            this.btnInterviewSchedule.Size = new System.Drawing.Size(162, 35);
            this.btnInterviewSchedule.TabIndex = 7;
            this.btnInterviewSchedule.Text = "Interview Schedule";
            this.btnInterviewSchedule.UseVisualStyleBackColor = false;
            this.btnInterviewSchedule.Click += new System.EventHandler(this.btnInterviewSchedule_Click);
            // 
            // btnInterviewEvaluation
            // 
            this.btnInterviewEvaluation.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnInterviewEvaluation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInterviewEvaluation.ForeColor = System.Drawing.Color.White;
            this.btnInterviewEvaluation.Location = new System.Drawing.Point(335, 219);
            this.btnInterviewEvaluation.Name = "btnInterviewEvaluation";
            this.btnInterviewEvaluation.Size = new System.Drawing.Size(162, 37);
            this.btnInterviewEvaluation.TabIndex = 8;
            this.btnInterviewEvaluation.Text = "Interview Evaluation";
            this.btnInterviewEvaluation.UseVisualStyleBackColor = false;
            this.btnInterviewEvaluation.Click += new System.EventHandler(this.btnInterviewEvaluation_Click);
            // 
            // btnScreening
            // 
            this.btnScreening.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnScreening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScreening.ForeColor = System.Drawing.Color.White;
            this.btnScreening.Location = new System.Drawing.Point(335, 130);
            this.btnScreening.Name = "btnScreening";
            this.btnScreening.Size = new System.Drawing.Size(162, 36);
            this.btnScreening.TabIndex = 9;
            this.btnScreening.Text = "Screening";
            this.btnScreening.UseVisualStyleBackColor = false;
            this.btnScreening.Click += new System.EventHandler(this.btnScreening_Click);
            // 
            // btnApplicantReview
            // 
            this.btnApplicantReview.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnApplicantReview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplicantReview.ForeColor = System.Drawing.Color.White;
            this.btnApplicantReview.Location = new System.Drawing.Point(335, 85);
            this.btnApplicantReview.Name = "btnApplicantReview";
            this.btnApplicantReview.Size = new System.Drawing.Size(162, 39);
            this.btnApplicantReview.TabIndex = 10;
            this.btnApplicantReview.Text = "Applicant Review";
            this.btnApplicantReview.UseVisualStyleBackColor = false;
            this.btnApplicantReview.Click += new System.EventHandler(this.btnApplicantReview_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SlateGray;
            this.panel1.Controls.Add(this.btnApplicantReview);
            this.panel1.Controls.Add(this.btnUserManagement);
            this.panel1.Controls.Add(this.btnScreening);
            this.panel1.Controls.Add(this.btnApplicantList);
            this.panel1.Controls.Add(this.btnInterviewEvaluation);
            this.panel1.Controls.Add(this.btnAddVacancy);
            this.panel1.Controls.Add(this.btnInterviewSchedule);
            this.panel1.Controls.Add(this.btnHiringDecision);
            this.panel1.Controls.Add(this.btnMaintenance);
            this.panel1.Controls.Add(this.btnReports);
            this.panel1.Location = new System.Drawing.Point(97, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(606, 298);
            this.panel1.TabIndex = 11;
            // 
            // HR_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.panel1);
            this.Name = "HR_Dashboard";
            this.Text = "HR_Dashboard";
            this.panel1.ResumeLayout(false);
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
        private Panel panel1;
    }
}