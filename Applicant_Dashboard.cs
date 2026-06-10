using HR_Project;
using MySql.Data.MySqlClient;
using System;
using System.Data;
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

   

        private void button4_Click(object sender, EventArgs e)
        {
            Session.UserId = 0;
            Session.Username = "";
            Session.FullName = "";
            Session.Role = "";

            login_form login = new login_form();
            login.Show();
            this.Close();
        }

        private void btnJobVacancies_Click(object sender, EventArgs e)
        {
            FormJobVacancies form = new FormJobVacancies();
            form.Show();
        }

        private void btnMyProfile_Click(object sender, EventArgs e)
        {
            ManageProfileForm form = new ManageProfileForm(Session.Username);
            form.Show();
        }
    }
}








