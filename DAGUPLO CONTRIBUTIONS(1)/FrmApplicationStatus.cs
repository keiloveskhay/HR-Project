using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HRApplicationSystem
{
    public partial class FrmApplicationStatus : Form
    {
        private void LoadStatusTimeline()
        {
            try
            {
                MySqlConnection conn = DBConnection.GetConnection();

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
        WHERE ApplicationID = 1
        ORDER BY ChangedAt DESC";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvStatusHistory.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public FrmApplicationStatus()
        {
            InitializeComponent();
        }
        private void LoadStatusHistory()
        {
            try
            {
                MySqlConnection conn = DBConnection.GetConnection();

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
        WHERE ApplicationID = 1
        ORDER BY ChangedAt DESC";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvStatusHistory.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmApplicationStatus_Load(object sender, EventArgs e)
        {
            LoadStatusHistory();
            LoadStatusTimeline();
        }
    }
}
