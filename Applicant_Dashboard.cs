using HR_Project;
using System;
using System.Windows.Forms;

namespace HR_Project
{
    public partial class Applicant_Dashboard : Form
    {
        public Applicant_Dashboard()
        {
            InitializeComponent();
        }

        private void Applicant_Dashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Welcome, " + Session.Username;
        }

        private void btnJobVacancies_Click(object sender, EventArgs e)
        {
            FormJobVacancies form = new FormJobVacancies();
            form.Show();
        }

        private void btnMyProfile_Click(object sender, EventArgs e)
        {
            // Opens the profile form using the email stored in session
            ManageProfileForm form = new ManageProfileForm(Session.Username);
            form.Show();
        }

        // Renamed button2_Click to btnMyApplications_Click
        private void btnMyApplications_Click(object sender, EventArgs e)
        {
            // Ensure you have stored ApplicantId in your Session class during login
            if (Session.ApplicantId > 0)
            {
                frmMyApplication form = new frmMyApplication(Session.ApplicantId);
                form.Show();
            }
            else
            {
                MessageBox.Show("Error: Applicant ID not found. Please log in again.");
            }
        }

        private void btnMyInterviews_Click(object sender, EventArgs e)
        {
            if (Session.ApplicantId > 0)
            {
                frmMyInterviews form = new frmMyInterviews(Session.ApplicantId);
                form.Show();
            }
            else
            {
                MessageBox.Show("Error: Applicant ID not found. Please log in again.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Clear session
            Session.UserId = 0;
            Session.ApplicantId = 0;
            Session.Username = "";
            Session.FullName = "";
            Session.Role = "";

            login_form login = new login_form();
            login.Show();
            this.Close();
        }
    }
}