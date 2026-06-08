using System;
using System.Linq;
using System.Windows.Forms;

namespace IDK2
{
    public class HiringDecisionForm : Form
    {
        private ComboBox vacancyBox;
        private ListBox decisionsList;
        private TextBox candName;
        private TextBox candEmail;
        private ComboBox decisionBox;
        private TextBox notesBox;
        private Button addBtn;
        private Button refreshBtn;

        public HiringDecisionForm()
        {
            Text = "Hiring Decisions";
            Width = 900;
            Height = 520;
            InitializeComponents();
            LoadVacancies();
        }

        private void InitializeComponents()
        {
            var lVac = new Label { Text = "Vacancy:", Left = 10, Top = 10, Width = 80 };
            vacancyBox = new ComboBox { Left = 100, Top = 10, Width = 600, DropDownStyle = ComboBoxStyle.DropDownList };
            vacancyBox.SelectedIndexChanged += (s, e) => LoadDecisions();
            decisionsList = new ListBox { Left = 10, Top = 40, Width = 860, Height = 200 };

            var lName = new Label { Text = "Candidate name:", Left = 10, Top = 250, Width = 100 };
            candName = new TextBox { Left = 120, Top = 250, Width = 300 };
            var lEmail = new Label { Text = "Candidate email:", Left = 440, Top = 250, Width = 100 };
            candEmail = new TextBox { Left = 560, Top = 250, Width = 310 };
            var lDecision = new Label { Text = "Decision:", Left = 10, Top = 290, Width = 100 };
            decisionBox = new ComboBox { Left = 120, Top = 290, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            decisionBox.Items.AddRange(new object[] { "Hired", "Rejected", "Waitlist" });
            var lNotes = new Label { Text = "Notes:", Left = 10, Top = 330, Width = 100 };
            notesBox = new TextBox { Left = 120, Top = 330, Width = 750, Height = 100, Multiline = true, ScrollBars = ScrollBars.Vertical };
            addBtn = new Button { Text = "Add Decision", Left = 120, Top = 440, Width = 140 };
            refreshBtn = new Button { Text = "Refresh", Left = 280, Top = 440, Width = 120 };
            addBtn.Click += (s, e) => AddDecision();
            refreshBtn.Click += (s, e) => LoadDecisions();

            Controls.AddRange(new Control[] { lVac, vacancyBox, decisionsList, lName, candName, lEmail, candEmail, lDecision, decisionBox, lNotes, notesBox, addBtn, refreshBtn });
        }

        private void LoadVacancies()
        {
            vacancyBox.Items.Clear();
            foreach (var v in AdminService.GetVacancies()) vacancyBox.Items.Add(v);
            if (vacancyBox.Items.Count > 0) vacancyBox.SelectedIndex = 0;
        }

        private void LoadDecisions()
        {
            decisionsList.Items.Clear();
            var v = vacancyBox.SelectedItem as Vacancy;
            if (v == null) return;
            var decs = AdminService.GetDecisionsForVacancy(v.Id);
            foreach (var d in decs) decisionsList.Items.Add($"{d.DecisionDate}: {d.CandidateName} ({d.Decision}) - {d.Notes}");
        }

        private void AddDecision()
        {
            var v = vacancyBox.SelectedItem as Vacancy;
            if (v == null) { MessageBox.Show("Select a vacancy."); return; }
            if (string.IsNullOrWhiteSpace(candName.Text)) { MessageBox.Show("Candidate name required."); return; }
            var d = new HiringDecision { VacancyId = v.Id, CandidateName = candName.Text.Trim(), CandidateEmail = candEmail.Text.Trim(), Decision = decisionBox.SelectedItem?.ToString() ?? "", Notes = notesBox.Text.Trim(), DecisionDate = DateTime.UtcNow.ToString("o") };
            AdminService.AddHiringDecision(d);
            LoadDecisions();
            candName.Text = ""; candEmail.Text = ""; notesBox.Text = "";
        }
    }
}
