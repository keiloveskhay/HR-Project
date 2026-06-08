namespace HRApplicationSystem
{
    partial class FrmApplicationStatus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvStatusHistory = new DataGridView();
            Label = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvStatusHistory).BeginInit();
            SuspendLayout();
            // 
            // dgvStatusHistory
            // 
            dgvStatusHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStatusHistory.Location = new Point(12, 65);
            dgvStatusHistory.Name = "dgvStatusHistory";
            dgvStatusHistory.RowHeadersWidth = 51;
            dgvStatusHistory.Size = new Size(784, 188);
            dgvStatusHistory.TabIndex = 0;
            // 
            // Label
            // 
            Label.AutoSize = true;
            Label.Location = new Point(12, 23);
            Label.Name = "Label";
            Label.Size = new Size(181, 20);
            Label.TabIndex = 1;
            Label.Text = "Application Status History";
            // 
            // FrmApplicationStatus
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(808, 450);
            Controls.Add(Label);
            Controls.Add(dgvStatusHistory);
            Name = "FrmApplicationStatus";
            Text = "FrmApplicationStatus";
            Load += FrmApplicationStatus_Load;
            ((System.ComponentModel.ISupportInitialize)dgvStatusHistory).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvStatusHistory;
        private Label Label;
    }
}