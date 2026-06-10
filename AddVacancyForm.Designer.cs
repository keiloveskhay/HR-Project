using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class AddVacancyForm
    {
        private IContainer components = null;

        private Label lblJobTitle;
        private Label lblDepartment;
        private Label lblEmploymentType;
        private Label lblSlots;
        private Label lblDescription;
        private Label lblQualifications;

        private TextBox txtJobTitle;
        private ComboBox cmbDepartment;
        private ComboBox cmbEmploymentType;
        private NumericUpDown numSlots;
        private TextBox txtDescription;
        private TextBox txtQualifications;

        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();

            lblJobTitle = new Label();
            lblDepartment = new Label();
            lblEmploymentType = new Label();
            lblSlots = new Label();
            lblDescription = new Label();
            lblQualifications = new Label();

            txtJobTitle = new TextBox();
            cmbDepartment = new ComboBox();
            cmbEmploymentType = new ComboBox();
            numSlots = new NumericUpDown();
            txtDescription = new TextBox();
            txtQualifications = new TextBox();

            btnSave = new Button();
            btnCancel = new Button();

            ((ISupportInitialize)(numSlots)).BeginInit();
            SuspendLayout();

            // lblJobTitle
            lblJobTitle.AutoSize = true;
            lblJobTitle.Location = new Point(20, 20);
            lblJobTitle.Text = "Job Title";

            // txtJobTitle
            txtJobTitle.Location = new Point(150, 20);
            txtJobTitle.Size = new Size(300, 27);

            // lblDepartment
            lblDepartment.AutoSize = true;
            lblDepartment.Location = new Point(20, 60);
            lblDepartment.Text = "Department";

            // cmbDepartment
            cmbDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDepartment.Location = new Point(150, 60);
            cmbDepartment.Size = new Size(300, 28);

            // lblEmploymentType
            lblEmploymentType.AutoSize = true;
            lblEmploymentType.Location = new Point(20, 100);
            lblEmploymentType.Text = "Employment Type";

            // cmbEmploymentType
            cmbEmploymentType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEmploymentType.Location = new Point(150, 100);
            cmbEmploymentType.Size = new Size(300, 28);

            // lblSlots
            lblSlots.AutoSize = true;
            lblSlots.Location = new Point(20, 140);
            lblSlots.Text = "Slots";

            // numSlots
            numSlots.Location = new Point(150, 140);
            numSlots.Minimum = 1;
            numSlots.Maximum = 100;
            numSlots.Value = 1;

            // lblDescription
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(20, 180);
            lblDescription.Text = "Description";

            // txtDescription
            txtDescription.Location = new Point(150, 180);
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(500, 100);

            // lblQualifications
            lblQualifications.AutoSize = true;
            lblQualifications.Location = new Point(20, 300);
            lblQualifications.Text = "Qualifications";

            // txtQualifications
            txtQualifications.Location = new Point(150, 300);
            txtQualifications.Multiline = true;
            txtQualifications.ScrollBars = ScrollBars.Vertical;
            txtQualifications.Size = new Size(500, 100);

            // btnSave
            btnSave.Location = new Point(150, 420);
            btnSave.Size = new Size(120, 35);
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;

            // btnCancel
            btnCancel.Location = new Point(290, 420);
            btnCancel.Size = new Size(120, 35);
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;

            // AddVacancyForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 500);
            Controls.Add(lblJobTitle);
            Controls.Add(txtJobTitle);
            Controls.Add(lblDepartment);
            Controls.Add(cmbDepartment);
            Controls.Add(lblEmploymentType);
            Controls.Add(cmbEmploymentType);
            Controls.Add(lblSlots);
            Controls.Add(numSlots);
            Controls.Add(lblDescription);
            Controls.Add(txtDescription);
            Controls.Add(lblQualifications);
            Controls.Add(txtQualifications);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            Name = "AddVacancyForm";
            Text = "Add Vacancy";

            Load += AddVacancyForm_Load;

            ((ISupportInitialize)(numSlots)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}