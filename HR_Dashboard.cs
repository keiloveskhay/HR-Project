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
    }
}
