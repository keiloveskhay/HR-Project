namespace HR_Project
{
    partial class Applicant_Dashboard
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
            this.btnMyProfile = new System.Windows.Forms.Button();
            this.btnMyApplications = new System.Windows.Forms.Button();
            this.btnJobVacancies = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMyInterviews = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMyProfile
            // 
            this.btnMyProfile.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnMyProfile.ForeColor = System.Drawing.Color.White;
            this.btnMyProfile.Location = new System.Drawing.Point(65, 80);
            this.btnMyProfile.Name = "btnMyProfile";
            this.btnMyProfile.Size = new System.Drawing.Size(116, 39);
            this.btnMyProfile.TabIndex = 1;
            this.btnMyProfile.Text = "My Profile";
            this.btnMyProfile.UseVisualStyleBackColor = false;
            this.btnMyProfile.Click += new System.EventHandler(this.btnMyProfile_Click);
            // 
            // btnMyApplications
            // 
            this.btnMyApplications.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnMyApplications.ForeColor = System.Drawing.Color.White;
            this.btnMyApplications.Location = new System.Drawing.Point(209, 80);
            this.btnMyApplications.Name = "btnMyApplications";
            this.btnMyApplications.Size = new System.Drawing.Size(116, 39);
            this.btnMyApplications.TabIndex = 2;
            this.btnMyApplications.Text = "My Application/s";
            this.btnMyApplications.UseVisualStyleBackColor = false;
            this.btnMyApplications.Click += new System.EventHandler(this.btnMyApplications_Click);
            // 
            // btnJobVacancies
            // 
            this.btnJobVacancies.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnJobVacancies.ForeColor = System.Drawing.Color.White;
            this.btnJobVacancies.Location = new System.Drawing.Point(360, 80);
            this.btnJobVacancies.Name = "btnJobVacancies";
            this.btnJobVacancies.Size = new System.Drawing.Size(116, 39);
            this.btnJobVacancies.TabIndex = 3;
            this.btnJobVacancies.Text = "Job Vacancies ";
            this.btnJobVacancies.UseVisualStyleBackColor = false;
            this.btnJobVacancies.Click += new System.EventHandler(this.btnJobVacancies_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.DarkBlue;
            this.button4.Location = new System.Drawing.Point(217, 183);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 25);
            this.button4.TabIndex = 4;
            this.button4.Text = "Logout";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.BackColor = System.Drawing.Color.White;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblWelcome.Location = new System.Drawing.Point(322, 54);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(167, 20);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome Applicant!";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.btnMyProfile);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.btnMyApplications);
            this.panel1.Controls.Add(this.btnJobVacancies);
            this.panel1.Controls.Add(this.btnMyInterviews);
            this.panel1.Location = new System.Drawing.Point(139, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(532, 237);
            this.panel1.TabIndex = 5;
            // 
            // 
            // btnMyInterviews
            // 
            this.btnMyInterviews.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnMyInterviews.ForeColor = System.Drawing.Color.White;
            this.btnMyInterviews.Location = new System.Drawing.Point(209, 130);
            this.btnMyInterviews.Name = "btnMyInterviews";
            this.btnMyInterviews.Size = new System.Drawing.Size(116, 39);
            this.btnMyInterviews.TabIndex = 6;
            this.btnMyInterviews.Text = "My Interviews";
            this.btnMyInterviews.UseVisualStyleBackColor = false;
            this.btnMyInterviews.Click += new System.EventHandler(this.btnMyInterviews_Click);
            // 
            // Applicant_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.panel1);
            this.Name = "Applicant_Dashboard";
            this.Text = "Applicant_Dashboard";
            this.Load += new System.EventHandler(this.Applicant_Dashboard_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnMyProfile;
        private System.Windows.Forms.Button btnMyApplications;
        private System.Windows.Forms.Button btnJobVacancies;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnMyInterviews;
    }
}