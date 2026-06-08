using System;
using System.Windows.Forms;

namespace IDk
{
    public class AddWorkExperienceForm : Form
    {
        private TextBox companyBox;
        private TextBox titleBox;
        private TextBox descBox;
        private TextBox startYearBox;
        private TextBox endYearBox;
        private Button okBtn;
        private Button cancelBtn;
        public WorkExperience Work { get; private set; }

        public AddWorkExperienceForm()
        {
            Text = "Add Work Experience";
            Width = 600;
            Height = 320;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            var lCo = new Label { Text = "Company:", Left = 10, Top = 10, Width = 100 };
            companyBox = new TextBox { Left = 120, Top = 10, Width = 440 };
            var lTi = new Label { Text = "Title:", Left = 10, Top = 40, Width = 100 };
            titleBox = new TextBox { Left = 120, Top = 40, Width = 440 };
            var lDesc = new Label { Text = "Description:", Left = 10, Top = 70, Width = 100 };
            descBox = new TextBox { Left = 120, Top = 70, Width = 440, Height = 80, Multiline = true, ScrollBars = ScrollBars.Vertical };
            var lStart = new Label { Text = "Start year:", Left = 10, Top = 160, Width = 100 };
            startYearBox = new TextBox { Left = 120, Top = 160, Width = 120 };
            var lEnd = new Label { Text = "End year (blank if present):", Left = 260, Top = 160, Width = 200 };
            endYearBox = new TextBox { Left = 460, Top = 160, Width = 100 };
            okBtn = new Button { Text = "OK", Left = 120, Top = 200, Width = 100 };
            cancelBtn = new Button { Text = "Cancel", Left = 240, Top = 200, Width = 100 };
            okBtn.Click += OkClicked;
            cancelBtn.Click += (s, e) => DialogResult = DialogResult.Cancel;
            Controls.AddRange(new Control[] { lCo, companyBox, lTi, titleBox, lDesc, descBox, lStart, startYearBox, lEnd, endYearBox, okBtn, cancelBtn });
        }

        private void OkClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(companyBox.Text)) { MessageBox.Show("Company required."); return; }
            if (string.IsNullOrWhiteSpace(titleBox.Text)) { MessageBox.Show("Title required."); return; }
            int sy = 0; int.TryParse(startYearBox.Text, out sy);
            int? ey = null; if (!string.IsNullOrWhiteSpace(endYearBox.Text)) { if (int.TryParse(endYearBox.Text, out var tmp)) ey = tmp; else { MessageBox.Show("End year must be a number."); return; } }
            Work = new WorkExperience { Company = companyBox.Text.Trim(), Title = titleBox.Text.Trim(), Description = descBox.Text.Trim(), StartYear = sy, EndYear = ey };
            DialogResult = DialogResult.OK;
        }
    }
}
