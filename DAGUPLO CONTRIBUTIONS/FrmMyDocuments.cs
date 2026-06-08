using MySql.Data.MySqlClient;
using System.Data;

namespace HRApplicationSystem
{
    public partial class FrmMyDocuments : Form
    {
        public FrmMyDocuments()
        {
            InitializeComponent();
        }

        private void LoadDocuments()
        {
            try
            {
                MySqlConnection conn = DBConnection.GetConnection();

                string query = @"
        SELECT
            rt.RequirementName,
            ad.FileName,
            ad.Status,
            ad.SubmittedAt
        FROM ApplicantDocuments ad
        INNER JOIN RequirementTypes rt
            ON ad.RequirementTypeID = rt.RequirementTypeID
        WHERE ad.ApplicationID = 1";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvDocuments.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmMyDocuments_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = DBConnection.GetConnection();

            try
            {
                conn.Open();

                string query = @"SELECT RequirementTypeID, RequirementName
                         FROM RequirementTypes
                         WHERE IsActive = 1";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                cmbRequirements.DisplayMember = "RequirementName";
                cmbRequirements.ValueMember = "RequirementTypeID";
                cmbRequirements.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            LoadDocuments();
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select Document";

            openFileDialog.Filter =
                "PDF Files (*.pdf)|*.pdf|" +
                "Word Documents (*.doc;*.docx)|*.doc;*.docx|" +
                "All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (cmbRequirements.SelectedIndex == -1 || string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("Please select a requirement and a file first.");
                return;
            }

            MySqlConnection conn = DBConnection.GetConnection();

            try
            {
                conn.Open();

                string query = @"INSERT INTO ApplicantDocuments
                        (ApplicationID, RequirementTypeID, FileName, FilePath, Status, Remarks, SubmittedAt)
                        VALUES
                        (@ApplicationID, @RequirementTypeID, @FileName, @FilePath, @Status, @Remarks, @SubmittedAt)";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ApplicationID", 1);
                // TEMP: hardcoded for now (we will fix later when login system exists)

                cmd.Parameters.AddWithValue("@RequirementTypeID", cmbRequirements.SelectedValue);
                cmd.Parameters.AddWithValue("@FileName", System.IO.Path.GetFileName(txtFilePath.Text));
                cmd.Parameters.AddWithValue("@FilePath", txtFilePath.Text);
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.Parameters.AddWithValue("@Remarks", "");
                cmd.Parameters.AddWithValue("@SubmittedAt", DateTime.Now);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Document uploaded successfully!");
                LoadDocuments();

                txtFilePath.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
