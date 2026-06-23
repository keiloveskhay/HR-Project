using HR_Recruitment_Workflow_Jared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HR_Project
{
    public partial class HR_Dashboard : Form
    {
        public HR_Dashboard()
        {
            InitializeComponent();
            this.Load += HR_Dashboard_Load;
        }

        private void HR_Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();
                    string query = "ALTER TABLE JobVacancies ADD COLUMN RequiredDocuments TEXT;";
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { /* Ignore duplicate column error */ }

            // Default: hide everything except logout
            btnUserManagement.Visible = false;
            btnApplicantList.Visible = false;
            btnAddVacancy.Visible = false;
            btnHiringDecision.Visible = false;
            btnReports.Visible = false;
            btnMaintenance.Visible = false;
            btnApplicantReview.Visible = false;
            btnScreening.Visible = false;
            btnInterviewSchedule.Visible = false;
            btnInterviewEvaluation.Visible = false;

            if (Session.Role == "Admin")
            {
                this.Text = "Admin Dashboard";
                btnUserManagement.Visible = true;
                btnMaintenance.Visible = true;
                btnReports.Visible = true;
            }
            else if (Session.Role == "HR Manager")
            {
                this.Text = "HR Manager Dashboard";
                btnAddVacancy.Visible = true;
                btnHiringDecision.Visible = true;
                btnReports.Visible = true;
                btnApplicantList.Visible = true;
                btnApplicantReview.Visible = true;
                btnScreening.Visible = true;
                btnInterviewSchedule.Visible = true;
                btnInterviewEvaluation.Visible = true;
            }
            else if (Session.Role == "HR Staff")
            {
                this.Text = "HR Staff Dashboard";
                btnApplicantList.Visible = true;
                btnApplicantReview.Visible = true;
                btnScreening.Visible = true;
                btnInterviewSchedule.Visible = true;
                btnInterviewEvaluation.Visible = true;
            }
            else
            {
                this.Text = "Dashboard";
            }
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            UserManagementForm form = new UserManagementForm();
            form.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Session.UserId = 0;
            Session.Username = "";
            Session.FullName = "";
            Session.Role = "";

            login_form login = new login_form();
            login.Show();
            this.Close();
        }

        private void btnApplicantList_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void btnAddVacancy_Click(object sender, EventArgs e)
        {
            FormManageVacancies form = new FormManageVacancies();
            form.ShowDialog();
        }

        private void btnHiringDecision_Click(object sender, EventArgs e)
        {
            HiringDecisionForm form = new HiringDecisionForm();
            form.Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportsForm form = new ReportsForm();
            form.ShowDialog(); // or Show()
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            new MaintenanceForm().ShowDialog();
        }

        private void btnApplicantReview_Click(object sender, EventArgs e)
        {
            new FormApplicantReview().Show();
        }

        private void btnScreening_Click(object sender, EventArgs e)
        {
            new FormScreening().Show();
        }

        private void btnInterviewSchedule_Click(object sender, EventArgs e)
        {
            new FormInterview().Show();
        }

        private void btnInterviewEvaluation_Click(object sender, EventArgs e)
        {
            new FormInterviewEvaluation().Show();
        }
    }
}
