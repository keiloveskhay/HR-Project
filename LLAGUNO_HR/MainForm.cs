using System;
using System.Windows.Forms;

namespace IDK2
{
    public partial class MainForm : Form
    {
        private Button vacanciesBtn;
        private Button decisionsBtn;
        private Button reportsBtn;
        private Button maintenanceBtn;
        private Button exitBtn;

        public MainForm()
        {
            Text = "Admin - Hiring Decisions & Reports";
            Width = 520;
            Height = 320;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            vacanciesBtn = new Button { Text = "Job Vacancy Management", Left = 20, Top = 20, Width = 200, Height = 40 };
            vacanciesBtn.Click += (s, e) => { using var f = new VacancyForm(); f.ShowDialog(); };
            decisionsBtn = new Button { Text = "Hiring Decisions", Left = 20, Top = 80, Width = 200, Height = 40 };
            decisionsBtn.Click += (s, e) => { using var f = new HiringDecisionForm(); f.ShowDialog(); };
            reportsBtn = new Button { Text = "Reports", Left = 20, Top = 140, Width = 200, Height = 40 };
            reportsBtn.Click += (s, e) => { using var f = new ReportsForm(); f.ShowDialog(); };
            maintenanceBtn = new Button { Text = "Maintenance Module", Left = 20, Top = 200, Width = 200, Height = 40 };
            maintenanceBtn.Click += (s, e) => { using var f = new MaintenanceForm(); f.ShowDialog(); };
            exitBtn = new Button { Text = "Exit", Left = 240, Top = 200, Width = 200, Height = 40 };
            exitBtn.Click += (s, e) => Close();
            Controls.AddRange(new Control[] { vacanciesBtn, decisionsBtn, reportsBtn, maintenanceBtn, exitBtn });
        }
    }
}
