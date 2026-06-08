namespace HRApplicationSystem
{
    partial class FrmMyDocuments
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Label = new Label();
            cmbRequirements = new ComboBox();
            txtFilePath = new TextBox();
            btnBrowse = new Button();
            btnUpload = new Button();
            dgvDocuments = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvDocuments).BeginInit();
            SuspendLayout();
            // 
            // Label
            // 
            Label.AutoSize = true;
            Label.Location = new Point(61, 32);
            Label.Name = "Label";
            Label.Size = new Size(132, 20);
            Label.TabIndex = 0;
            Label.Text = "Requirement Type:";
            // 
            // cmbRequirements
            // 
            cmbRequirements.FormattingEnabled = true;
            cmbRequirements.Location = new Point(61, 65);
            cmbRequirements.Name = "cmbRequirements";
            cmbRequirements.Size = new Size(383, 28);
            cmbRequirements.TabIndex = 1;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(61, 114);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.ReadOnly = true;
            txtFilePath.Size = new Size(283, 27);
            txtFilePath.TabIndex = 2;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(350, 114);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(94, 29);
            btnBrowse.TabIndex = 3;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(350, 149);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(94, 29);
            btnUpload.TabIndex = 4;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // dgvDocuments
            // 
            dgvDocuments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDocuments.Location = new Point(61, 265);
            dgvDocuments.Name = "dgvDocuments";
            dgvDocuments.RowHeadersWidth = 51;
            dgvDocuments.Size = new Size(595, 187);
            dgvDocuments.TabIndex = 5;
            // 
            // FrmMyDocuments
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(930, 588);
            Controls.Add(dgvDocuments);
            Controls.Add(btnUpload);
            Controls.Add(btnBrowse);
            Controls.Add(txtFilePath);
            Controls.Add(cmbRequirements);
            Controls.Add(Label);
            Name = "FrmMyDocuments";
            Text = "Form1";
            Load += FrmMyDocuments_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDocuments).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbRequirements;
        private TextBox txtFilePath;
        private Button btnBrowse;
        private Button btnUpload;
        private DataGridView dgvDocuments;
        private Label Label;
    }
}
