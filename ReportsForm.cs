using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
            LoadSummary();
        }

        private void LoadSummary()
        {
            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    // Total vacancies
                    int totalVacancies = GetCount(conn, "SELECT COUNT(*) FROM JobVacancies");

                    // Open vacancies
                    int openVacancies = GetCount(conn,
                        "SELECT COUNT(*) FROM JobVacancies WHERE Status = 'Open'");

                    // Closed vacancies
                    int closedVacancies = GetCount(conn,
                        "SELECT COUNT(*) FROM JobVacancies WHERE Status = 'Closed'");

                    // Total applications
                    int totalApplications = GetCount(conn, "SELECT COUNT(*) FROM Applications");

                    // Total hiring decisions
                    int totalDecisions = GetCount(conn, "SELECT COUNT(*) FROM HiringDecisions");

                    // Total hires
                    int totalHires = GetCount(conn,
                        "SELECT COUNT(*) FROM HiringDecisions WHERE Decision = 'Accepted'");

                    summaryLabel.Text =
                        $"📊 HR SYSTEM REPORT\n\n" +
                        $"Vacancies: {totalVacancies}\n" +
                        $"Open Vacancies: {openVacancies}\n" +
                        $"Closed Vacancies: {closedVacancies}\n\n" +
                        $"Applications: {totalApplications}\n" +
                        $"Decisions Made: {totalDecisions}\n" +
                        $"Hires: {totalHires}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetCount(MySqlConnection conn, string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, conn);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private void Export()
        {
            try
            {
                string reportText = summaryLabel.Text + "\n\nGenerated: " + DateTime.Now;

                SaveFileDialog dlg = new SaveFileDialog
                {
                    Filter = "Text File|*.txt",
                    FileName = "HR_Report.txt"
                };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(dlg.FileName, reportText, Encoding.UTF8);
                    MessageBox.Show("Report exported successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            LoadSummary();
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            Export();
        }
    }
}