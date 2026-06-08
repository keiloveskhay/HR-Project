using System;
using System.Windows.Forms;

namespace IDK2
{
    public class ReportsForm : Form
    {
        private Label summaryLabel;
        private Button refreshBtn;
        private Button exportBtn;

        public ReportsForm()
        {
            Text = "Reports";
            Width = 600;
            Height = 300;
            InitializeComponents();
            LoadSummary();
        }

        private void InitializeComponents()
        {
            summaryLabel = new Label { Left = 10, Top = 10, Width = 560, Height = 200 };
            refreshBtn = new Button { Text = "Refresh", Left = 10, Top = 220, Width = 120 };
            exportBtn = new Button { Text = "Export Report", Left = 150, Top = 220, Width = 120 };
            refreshBtn.Click += (s, e) => LoadSummary();
            exportBtn.Click += (s, e) => Export();
            Controls.AddRange(new Control[] { summaryLabel, refreshBtn, exportBtn });
        }

        private void LoadSummary()
        {
            var (total, open, closed, decisions, hires) = AdminService.GenerateSummary();
            summaryLabel.Text = $"Vacancies: {total}\nOpen: {open}\nClosed: {closed}\nDecisions: {decisions}\nHires: {hires}";
        }

        private void Export()
        {
            var (total, open, closed, decisions, hires) = AdminService.GenerateSummary();
            var txt = $"Report generated: {DateTime.UtcNow:o}\nVacancies: {total}\nOpen: {open}\nClosed: {closed}\nDecisions: {decisions}\nHires: {hires}";
            var dlg = new SaveFileDialog { Filter = "Text|*.txt", FileName = "report.txt" };
            if (dlg.ShowDialog() == DialogResult.OK) System.IO.File.WriteAllText(dlg.FileName, txt);
            MessageBox.Show("Report exported.");
        }
    }
}
