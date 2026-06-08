using System;
using System.Linq;
using System.Windows.Forms;

namespace IDK2
{
    public partial class MaintenanceForm : Form
    {
        private ListBox deptList;
        private TextBox deptBox;
        private Button addDeptBtn;

        private ListBox roleList;
        private TextBox roleBox;
        private Button addRoleBtn;

        private ListBox typeList;
        private TextBox typeBox;
        private Button addTypeBtn;

        public MaintenanceForm()
        {
            Text = "Maintenance";
            Width = 900;
            Height = 520;
            InitializeComponents();
            LoadAll();
        }

        private void InitializeComponents()
        {
            var lDept = new Label { Text = "Departments", Left = 10, Top = 10, Width = 200 };
            deptList = new ListBox { Left = 10, Top = 30, Width = 260, Height = 300 };
            deptBox = new TextBox { Left = 10, Top = 340, Width = 180 };
            addDeptBtn = new Button { Text = "Add Dept", Left = 200, Top = 338, Width = 70 };
            addDeptBtn.Click += (s, e) => { if (!string.IsNullOrWhiteSpace(deptBox.Text)) { AdminService.CreateDepartment(deptBox.Text.Trim()); deptBox.Text = ""; LoadAll(); } };

            var lRole = new Label { Text = "Roles", Left = 300, Top = 10, Width = 200 };
            roleList = new ListBox { Left = 300, Top = 30, Width = 260, Height = 300 };
            roleBox = new TextBox { Left = 300, Top = 340, Width = 180 };
            addRoleBtn = new Button { Text = "Add Role", Left = 490, Top = 338, Width = 70 };
            addRoleBtn.Click += (s, e) => { var dep = deptList.SelectedItem as Department; if (dep == null) { MessageBox.Show("Select a department first."); return; } if (!string.IsNullOrWhiteSpace(roleBox.Text)) { AdminService.CreateRole(dep.Id, roleBox.Text.Trim()); roleBox.Text = ""; LoadAll(); } };

            var lType = new Label { Text = "Employment Types", Left = 590, Top = 10, Width = 200 };
            typeList = new ListBox { Left = 590, Top = 30, Width = 260, Height = 300 };
            typeBox = new TextBox { Left = 590, Top = 340, Width = 180 };
            addTypeBtn = new Button { Text = "Add Type", Left = 770, Top = 338, Width = 70 };
            addTypeBtn.Click += (s, e) => { if (!string.IsNullOrWhiteSpace(typeBox.Text)) { AdminService.CreateEmploymentType(typeBox.Text.Trim()); typeBox.Text = ""; LoadAll(); } };

            Controls.AddRange(new Control[] { lDept, deptList, deptBox, addDeptBtn, lRole, roleList, roleBox, addRoleBtn, lType, typeList, typeBox, addTypeBtn });
            deptList.SelectedIndexChanged += (s, e) => { LoadRolesForSelectedDepartment(); };
        }

        private void LoadAll()
        {
            deptList.Items.Clear();
            foreach (var d in AdminService.GetDepartments()) deptList.Items.Add(d);
            typeList.Items.Clear();
            foreach (var t in AdminService.GetEmploymentTypes()) typeList.Items.Add(t);
            LoadRolesForSelectedDepartment();
        }

        private void LoadRolesForSelectedDepartment()
        {
            roleList.Items.Clear();
            var dep = deptList.SelectedItem as Department;
            if (dep == null) return;
            foreach (var r in AdminService.GetRoles(dep.Id)) roleList.Items.Add(r);
        }
    }
}
