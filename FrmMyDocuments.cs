using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace HR_Project
{
    public partial class FrmMyDocuments : Form
    {
        private int applicationId;

        public FrmMyDocuments(int applicationId)
        {
            InitializeComponent();
            this.applicationId = applicationId;
        }

        private void FrmMyDocuments_Load(object sender, EventArgs e)
        {
            LoadRequirements();
            LoadMissingDocuments();
            LoadDocuments();
        }

        private void LoadRequirements()
        {
            try
            {
                using (MySqlConnection conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT RequirementTypeID, RequirementName
                                     FROM RequirementTypes
                                     WHERE IsActive = 1";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cmbRequirements.DisplayMember = "RequirementName";
                    cmbRequirements.ValueMember = "RequirementTypeID";
                    cmbRequirements.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadMissingDocuments()
        {
            try
            {
                using (MySqlConnection conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            rt.RequirementName,
                            CASE
                                WHEN ad.DocumentID IS NULL THEN 'Missing'
                                ELSE 'Uploaded'
                            END AS Status
                        FROM RequirementTypes rt
                        LEFT JOIN ApplicantDocuments ad
                            ON rt.RequirementTypeID = ad.RequirementTypeID
                            AND ad.ApplicationID = @ApplicationID
                        WHERE rt.IsActive = 1";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ApplicationID", applicationId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvDocuments.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadDocuments()
        {
            try
            {
                using (MySqlConnection conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            rt.RequirementName,
                            ad.FileName,
                            ad.Status,
                            ad.Remarks,
                            ad.SubmittedAt
                        FROM ApplicantDocuments ad
                        INNER JOIN RequirementTypes rt
                            ON ad.RequirementTypeID = rt.RequirementTypeID
                        WHERE ad.ApplicationID = @ApplicationID";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ApplicationID", applicationId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvDocuments.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select Document",
                Filter = "PDF Files (*.pdf)|*.pdf|Word Documents (*.doc;*.docx)|*.doc;*.docx|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (cmbRequirements.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                MessageBox.Show("Please select a requirement and a file first.");
                return;
            }

            try
            {
                using (MySqlConnection conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO ApplicantDocuments
                        (ApplicationID, RequirementTypeID, FileName, FilePath, Status, Remarks, SubmittedAt)
                        VALUES
                        (@ApplicationID, @RequirementTypeID, @FileName, @FilePath, @Status, @Remarks, @SubmittedAt)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@ApplicationID", applicationId);
                    cmd.Parameters.AddWithValue("@RequirementTypeID", cmbRequirements.SelectedValue);
                    cmd.Parameters.AddWithValue("@FileName", System.IO.Path.GetFileName(txtFilePath.Text));
                    cmd.Parameters.AddWithValue("@FilePath", txtFilePath.Text);
                    cmd.Parameters.AddWithValue("@Status", "Pending");
                    cmd.Parameters.AddWithValue("@Remarks", "");
                    cmd.Parameters.AddWithValue("@SubmittedAt", DateTime.Now);

                    cmd.ExecuteNonQuery();

                    string docName = System.IO.Path.GetFileName(txtFilePath.Text);
                    string logQuery = "INSERT INTO ApplicationStatusHistory (ApplicationID, NewStatus, ChangedBy, Remarks) VALUES (@AppID, 'Update', 'Applicant', @RemarksLog)";
                    using (var logCmd = new MySqlCommand(logQuery, conn))
                    {
                        logCmd.Parameters.AddWithValue("@AppID", applicationId);
                        logCmd.Parameters.AddWithValue("@RemarksLog", "Uploaded document: " + docName);
                        logCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Document uploaded successfully!");

                    LoadMissingDocuments();
                    LoadDocuments();

                    txtFilePath.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenStatus_Click(object sender, EventArgs e)
        {
            FrmApplicationStatus form = new FrmApplicationStatus(applicationId);
            form.Show();
        }
    }
}