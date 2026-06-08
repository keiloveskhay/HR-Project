using System;
using System.Windows.Forms;

namespace IDk
{
    public class AddEducationForm : Form
    {
        private TextBox inst;
        private TextBox degree;
        private TextBox field;
        private TextBox year;
        private Button okBtn;
        private Button cancelBtn;
        public EducationEntry Education { get; private set; }

        public AddEducationForm()
        {
            Text = "Add Education";
            Width = 480;
            Height = 240;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            var lInst = new Label { Text = "Institution:", Left = 10, Top = 10, Width = 100 };
            inst = new TextBox { Left = 120, Top = 10, Width = 320 };
            var lDeg = new Label { Text = "Degree:", Left = 10, Top = 40, Width = 100 };
            degree = new TextBox { Left = 120, Top = 40, Width = 320 };
            var lField = new Label { Text = "Field of study:", Left = 10, Top = 70, Width = 100 };
            field = new TextBox { Left = 120, Top = 70, Width = 320 };
            var lYear = new Label { Text = "Year:", Left = 10, Top = 100, Width = 100 };
            year = new TextBox { Left = 120, Top = 100, Width = 120 };
            okBtn = new Button { Text = "OK", Left = 120, Top = 140, Width = 100 };
            cancelBtn = new Button { Text = "Cancel", Left = 240, Top = 140, Width = 100 };
            okBtn.Click += OkClicked;
            cancelBtn.Click += (s, e) => DialogResult = DialogResult.Cancel;
            Controls.AddRange(new Control[] { lInst, inst, lDeg, degree, lField, field, lYear, year, okBtn, cancelBtn });
        }

        private void OkClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inst.Text)) { MessageBox.Show("Institution required."); return; }
            int yr = 0; int.TryParse(year.Text, out yr);
            Education = new EducationEntry { Institution = inst.Text.Trim(), Degree = degree.Text.Trim(), FieldOfStudy = field.Text.Trim(), Year = yr };
            DialogResult = DialogResult.OK;
        }
    }
}
