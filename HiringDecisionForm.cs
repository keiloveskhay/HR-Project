using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HR_Project
{
    public partial class HiringDecisionForm : Form
    {
        public HiringDecisionForm()
        {
            InitializeComponent();
            LoadVacancies();
        }

        private void LoadVacancies()
        {
            try
            {
                using (MySqlConnection conn =
                    new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT VacancyID, JobTitle
                        FROM JobVacancies
                        WHERE Status = 'Open'";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    vacancyBox.DataSource = dt;
                    vacancyBox.DisplayMember = "JobTitle";
                    vacancyBox.ValueMember = "VacancyID";

                    LoadDecisions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadDecisions()
        {
            try
            {
                if (vacancyBox.SelectedValue == null) return;

                using (MySqlConnection conn =
                    new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT Decision, Remarks, DecidedAt
                        FROM HiringDecisions
                        WHERE ApplicationID IN
                        (
                            SELECT ApplicationID
                            FROM Applications
                            WHERE VacancyID = @VacancyID
                        )";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@VacancyID", vacancyBox.SelectedValue);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    decisionsList.Items.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        decisionsList.Items.Add(
                            $"{row["DecidedAt"]} - {row["Decision"]}: {row["Remarks"]}"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddDecision(object sender, EventArgs e)
        {
            try
            {
                if (vacancyBox.SelectedValue == null)
                {
                    MessageBox.Show("Select a vacancy.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(candName.Text))
                {
                    MessageBox.Show("Candidate name required.");
                    return;
                }

                if (decisionBox.SelectedItem == null)
                {
                    MessageBox.Show("Select a decision.");
                    return;
                }

                using (MySqlConnection conn =
                    new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string findApplicant = @"
                        SELECT ap.ApplicantID
                        FROM ApplicantProfiles ap
                        INNER JOIN Users u ON ap.UserID = u.UserID
                        WHERE u.FullName = @Name
                        LIMIT 1";

                    MySqlCommand cmd1 = new MySqlCommand(findApplicant, conn);
                    cmd1.Parameters.AddWithValue("@Name", candName.Text.Trim());

                    object result = cmd1.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("Applicant not found.");
                        return;
                    }

                    int applicantId = Convert.ToInt32(result);

                    string findApp = @"
                        SELECT ApplicationID
                        FROM Applications
                        WHERE ApplicantID = @A AND VacancyID = @V
                        LIMIT 1";

                    MySqlCommand cmd2 = new MySqlCommand(findApp, conn);
                    cmd2.Parameters.AddWithValue("@A", applicantId);
                    cmd2.Parameters.AddWithValue("@V", vacancyBox.SelectedValue);

                    object appIdObj = cmd2.ExecuteScalar();

                    if (appIdObj == null)
                    {
                        MessageBox.Show("No application found.");
                        return;
                    }

                    int appId = Convert.ToInt32(appIdObj);

                    string insert = @"
                        INSERT INTO HiringDecisions
                        (ApplicationID, Decision, Remarks, DecidedBy)
                        VALUES
                        (@AppID, @Decision, @Remarks, @By)";

                    MySqlCommand cmd3 = new MySqlCommand(insert, conn);
                    cmd3.Parameters.AddWithValue("@AppID", appId);
                    cmd3.Parameters.AddWithValue("@Decision", decisionBox.Text);
                    cmd3.Parameters.AddWithValue("@Remarks", notesBox.Text);
                    cmd3.Parameters.AddWithValue("@By", 1);

                    cmd3.ExecuteNonQuery();

                    MessageBox.Show("Decision saved.");

                    LoadDecisions();

                    candName.Clear();
                    candEmail.Clear();
                    notesBox.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}