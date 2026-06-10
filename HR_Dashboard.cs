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
            AddVacancyForm form = new AddVacancyForm();
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
