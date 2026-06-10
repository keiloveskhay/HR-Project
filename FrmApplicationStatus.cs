using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace HR_Project
{
    public partial class FrmApplicationStatus : Form
    {
        private int applicationId;

        public FrmApplicationStatus(int applicationId)
        {
            InitializeComponent();
            this.applicationId = applicationId;
        }

        private void FrmApplicationStatus_Load(object sender, EventArgs e)
        {
            LoadStatusHistory();
        }

        private void LoadStatusHistory()
        {
            try
            {
                using (MySqlConnection conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT
                            HistoryID,
                            ApplicationID,
                            OldStatus,
                            NewStatus,
                            Remarks,
                            ChangedBy,
                            ChangedAt
                        FROM ApplicationStatusHistory
                        WHERE ApplicationID = @ApplicationID
                        ORDER BY ChangedAt DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ApplicationID", applicationId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvStatusHistory.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}