using System;
using System.Windows.Forms;

namespace IDK2
{
    public class AddVacancyForm : Form
    {
        private TextBox titleBox;
        private ComboBox deptBox;
        private ComboBox roleBox;
        private ComboBox typeBox;
        private TextBox descBox;
        private Button okBtn;
        private Button cancelBtn;
        public Vacancy Vacancy { get; private set; }

        public AddVacancyForm(Vacancy existing = null)
        {
            Text = existing == null ? "Add Vacancy" : "Edit Vacancy";
            Width = 700;
            Height = 420;
            InitializeComponents();
            LoadLookups();
            if (existing != null) LoadExisting(existing);
        }

        private void InitializeComponents()
        {
            var lTitle = new Label { Text = "Title:", Left = 10, Top = 10, Width = 100 };
            titleBox = new TextBox { Left = 120, Top = 10, Width = 540 };
            var lDept = new Label { Text = "Department:", Left = 10, Top = 40, Width = 100 };
            deptBox = new ComboBox { Left = 120, Top = 40, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            var lRole = new Label { Text = "Role:", Left = 10, Top = 70, Width = 100 };
            roleBox = new ComboBox { Left = 120, Top = 70, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            var lType = new Label { Text = "Employment type:", Left = 10, Top = 100, Width = 100 };
            typeBox = new ComboBox { Left = 120, Top = 100, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            var lDesc = new Label { Text = "Description:", Left = 10, Top = 130, Width = 100 };
            descBox = new TextBox { Left = 120, Top = 130, Width = 540, Height = 160, Multiline = true, ScrollBars = ScrollBars.Vertical };
            okBtn = new Button { Text = "OK", Left = 120, Top = 310, Width = 120 };
            cancelBtn = new Button { Text = "Cancel", Left = 260, Top = 310, Width = 120 };
            okBtn.Click += OkClicked;
            cancelBtn.Click += (s, e) => DialogResult = DialogResult.Cancel;
            Controls.AddRange(new Control[] { lTitle, titleBox, lDept, deptBox, lRole, roleBox, lType, typeBox, lDesc, descBox, okBtn, cancelBtn });
            deptBox.SelectedIndexChanged += (s, e) => FillRolesForSelectedDepartment();
        }

        private void LoadLookups()
        {
            deptBox.Items.Clear();
            foreach (var d in AdminService.GetDepartments()) deptBox.Items.Add(d);
            typeBox.Items.Clear();
            foreach (var t in AdminService.GetEmploymentTypes()) typeBox.Items.Add(t);
        }

        private void FillRolesForSelectedDepartment()
        {
            roleBox.Items.Clear();
            var dep = deptBox.SelectedItem as Department;
            if (dep == null) return;
            foreach (var r in AdminService.GetRoles(dep.Id)) roleBox.Items.Add(r);
        }

        private void LoadExisting(Vacancy v)
        {
            Vacancy = v;
            titleBox.Text = v.Title;
            descBox.Text = v.Description;
            // select department
            for (int i = 0; i < deptBox.Items.Count; i++) if (((Department)deptBox.Items[i]).Id == v.DepartmentId) { deptBox.SelectedIndex = i; break; }
            FillRolesForSelectedDepartment();
            for (int i = 0; i < roleBox.Items.Count; i++) if (((Role)roleBox.Items[i]).Id == v.RoleId) { roleBox.SelectedIndex = i; break; }
            for (int i = 0; i < typeBox.Items.Count; i++) if (((EmploymentType)typeBox.Items[i]).Name == v.EmploymentType) { typeBox.SelectedIndex = i; break; }
        }

        private void OkClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleBox.Text)) { MessageBox.Show("Title required."); return; }
            var v = Vacancy ?? new Vacancy();
            v.Title = titleBox.Text.Trim();
            v.Description = descBox.Text.Trim();
            var dep = deptBox.SelectedItem as Department;
            var role = roleBox.SelectedItem as Role;
            var et = typeBox.SelectedItem as EmploymentType;
            v.DepartmentId = dep?.Id ?? string.Empty;
            v.DepartmentName = dep?.Name ?? string.Empty;
            v.RoleId = role?.Id ?? string.Empty;
            v.RoleName = role?.Name ?? string.Empty;
            v.EmploymentType = et?.Name ?? string.Empty;
            Vacancy = v;
            DialogResult = DialogResult.OK;
        }
    }
}
