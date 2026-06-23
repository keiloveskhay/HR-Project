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

                if (string.IsNullOrWhiteSpace(appIdBox.Text))
                {
                    MessageBox.Show("Application ID required.");
                    return;
                }

                if (decisionBox.SelectedItem == null)
                {
                    MessageBox.Show("Select a decision.");
                    return;
                }

                if (!int.TryParse(appIdBox.Text, out int appId))
                {
                    MessageBox.Show("Invalid Application ID.");
                    return;
                }

                using (MySqlConnection conn =
                    new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    conn.Open();

                    string insert = @"
                        INSERT INTO HiringDecisions
                        (ApplicationID, Decision, Remarks, DecidedBy)
                        VALUES
                        (@AppID, @Decision, @Remarks, @By)";

                    MySqlCommand cmd3 = new MySqlCommand(insert, conn);
                    cmd3.Parameters.AddWithValue("@AppID", appId);
                    cmd3.Parameters.AddWithValue("@Decision", decisionBox.Text);
                    cmd3.Parameters.AddWithValue("@Remarks", notesBox.Text);
                    cmd3.Parameters.AddWithValue("@By", Session.UserId > 0 ? Session.UserId : 1);

                    cmd3.ExecuteNonQuery();

                    if (decisionBox.Text == "Accepted")
                    {
                        // Get vacancy ID
                        string getVac = "SELECT VacancyID FROM Applications WHERE ApplicationID = @AppID";
                        MySqlCommand cmdVac = new MySqlCommand(getVac, conn);
                        cmdVac.Parameters.AddWithValue("@AppID", appId);
                        object vacIdObj = cmdVac.ExecuteScalar();

                        if (vacIdObj != null)
                        {
                            int vacId = Convert.ToInt32(vacIdObj);
                            
                            // Decrement slots
                            string decSlots = "UPDATE JobVacancies SET Slots = Slots - 1 WHERE VacancyID = @VacID AND Slots > 0";
                            MySqlCommand cmdDec = new MySqlCommand(decSlots, conn);
                            cmdDec.Parameters.AddWithValue("@VacID", vacId);
                            cmdDec.ExecuteNonQuery();

                            // Check slots and close
                            string checkSlots = "SELECT Slots FROM JobVacancies WHERE VacancyID = @VacID";
                            MySqlCommand cmdCheck = new MySqlCommand(checkSlots, conn);
                            cmdCheck.Parameters.AddWithValue("@VacID", vacId);
                            int remaining = Convert.ToInt32(cmdCheck.ExecuteScalar());

                            if (remaining <= 0)
                            {
                                string closeVac = "UPDATE JobVacancies SET Status = 'Closed', ClosedAt = NOW() WHERE VacancyID = @VacID";
                                MySqlCommand cmdClose = new MySqlCommand(closeVac, conn);
                                cmdClose.Parameters.AddWithValue("@VacID", vacId);
                                cmdClose.ExecuteNonQuery();
                            }
                            
                            // Update application status
                            string updApp = "UPDATE Applications SET StatusID = (SELECT StatusID FROM ApplicationStatuses WHERE StatusName = 'Accepted') WHERE ApplicationID = @AppID";
                            MySqlCommand cmdUpdApp = new MySqlCommand(updApp, conn);
                            cmdUpdApp.Parameters.AddWithValue("@AppID", appId);
                            cmdUpdApp.ExecuteNonQuery();
                        }
                    }
                    else if (decisionBox.Text == "Rejected")
                    {
                        string updApp = "UPDATE Applications SET StatusID = (SELECT StatusID FROM ApplicationStatuses WHERE StatusName = 'Rejected') WHERE ApplicationID = @AppID";
                        MySqlCommand cmdUpdApp = new MySqlCommand(updApp, conn);
                        cmdUpdApp.Parameters.AddWithValue("@AppID", appId);
                        cmdUpdApp.ExecuteNonQuery();
                    }

                    MessageBox.Show("Decision saved.");

                    LoadDecisions();

                    appIdBox.Clear();
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