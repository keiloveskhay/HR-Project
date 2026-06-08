using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IDK2
{
    public class VacancyForm : Form
    {
        private DataGridView grid;
        private Button addBtn;
        private Button editBtn;
        private Button closeBtn;
        private Button refreshBtn;
        private List<Vacancy> vacancies = new();

        public VacancyForm()
        {
            Text = "Vacancies";
            Width = 900;
            Height = 520;
            InitializeComponents();
            LoadVacancies();
        }

        private void InitializeComponents()
        {
            grid = new DataGridView { Left = 10, Top = 10, Width = 860, Height = 380, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AllowUserToAddRows = false };
            grid.Columns.Add("Title", "Title");
            grid.Columns.Add("Department", "Department");
            grid.Columns.Add("Role", "Role");
            grid.Columns.Add("Type", "Employment Type");
            grid.Columns.Add("OpenDate", "Open Date");
            grid.Columns.Add("CloseDate", "Close Date");
            grid.Columns.Add("Status", "Status");

            addBtn = new Button { Text = "Add", Left = 10, Top = 400, Width = 120 };
            editBtn = new Button { Text = "Edit", Left = 140, Top = 400, Width = 120 };
            closeBtn = new Button { Text = "Close Vacancy", Left = 270, Top = 400, Width = 120 };
            refreshBtn = new Button { Text = "Refresh", Left = 400, Top = 400, Width = 120 };

            addBtn.Click += (s, e) => AddVacancy();
            editBtn.Click += (s, e) => EditVacancy();
            closeBtn.Click += (s, e) => CloseVacancy();
            refreshBtn.Click += (s, e) => LoadVacancies();

            Controls.AddRange(new Control[] { grid, addBtn, editBtn, closeBtn, refreshBtn });
        }

        private void LoadVacancies()
        {
            vacancies = AdminService.GetVacancies();
            grid.Rows.Clear();
            foreach (var v in vacancies)
            {
                grid.Rows.Add(v.Title, v.DepartmentName, v.RoleName, v.EmploymentType, v.OpenDate, v.CloseDate, v.Status);
            }
        }

        private Vacancy SelectedVacancy()
        {
            if (grid.SelectedRows.Count == 0) return null;
            var idx = grid.SelectedRows[0].Index;
            if (idx < 0 || idx >= vacancies.Count) return null;
            return vacancies[idx];
        }

        private void AddVacancy()
        {
            using var f = new AddVacancyForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                AdminService.CreateVacancy(f.Vacancy);
                LoadVacancies();
            }
        }

        private void EditVacancy()
        {
            var sel = SelectedVacancy();
            if (sel == null) { MessageBox.Show("Select a vacancy to edit."); return; }
            using var f = new AddVacancyForm(sel);
            if (f.ShowDialog() == DialogResult.OK)
            {
                AdminService.UpdateVacancy(f.Vacancy);
                LoadVacancies();
            }
        }

        private void CloseVacancy()
        {
            var sel = SelectedVacancy();
            if (sel == null) { MessageBox.Show("Select a vacancy to close."); return; }
            if (MessageBox.Show($"Close vacancy '{sel.Title}'?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            AdminService.CloseVacancy(sel.Id);
            LoadVacancies();
        }
    }
}
