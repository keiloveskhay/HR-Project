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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            login_form login = new login_form();
            login.Show();
            this.Close();
        }
    }
}