using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class HiringDecisionForm
    {
        private IContainer components = null;

        private ComboBox vacancyBox;
        private ListBox decisionsList;
        private TextBox candName;
        private TextBox candEmail;
        private ComboBox decisionBox;
        private TextBox notesBox;
        private Button addBtn;
        private Button refreshBtn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.vacancyBox = new ComboBox();
            this.decisionsList = new ListBox();
            this.candName = new TextBox();
            this.candEmail = new TextBox();
            this.decisionBox = new ComboBox();
            this.notesBox = new TextBox();
            this.addBtn = new Button();
            this.refreshBtn = new Button();

            this.SuspendLayout();

            // ======================
            // vacancyBox
            // ======================
            this.vacancyBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.vacancyBox.Location = new Point(100, 10);
            this.vacancyBox.Size = new Size(650, 23);

            // ======================
            // decisionsList
            // ======================
            this.decisionsList.Location = new Point(10, 40);
            this.decisionsList.Size = new Size(860, 180);

            // ======================
            // candName
            // ======================
            this.candName.Location = new Point(120, 240);
            this.candName.Size = new Size(300, 23);

            // ======================
            // candEmail
            // ======================
            this.candEmail.Location = new Point(560, 240);
            this.candEmail.Size = new Size(310, 23);

            // ======================
            // decisionBox
            // ======================
            this.decisionBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.decisionBox.Items.AddRange(new object[]
            {
                "Hired",
                "Rejected",
                "Waitlist"
            });
            this.decisionBox.Location = new Point(120, 280);
            this.decisionBox.Size = new Size(200, 23);

            // ======================
            // notesBox
            // ======================
            this.notesBox.Multiline = true;
            this.notesBox.ScrollBars = ScrollBars.Vertical;
            this.notesBox.Location = new Point(120, 320);
            this.notesBox.Size = new Size(750, 80);

            // ======================
            // addBtn
            // ======================
            this.addBtn.Text = "Add Decision";
            this.addBtn.Location = new Point(120, 420);
            this.addBtn.Size = new Size(140, 30);
            this.addBtn.UseVisualStyleBackColor = true;

            // IMPORTANT: matches fixed .cs method
            this.addBtn.Click += new System.EventHandler(this.AddDecision);

            // ======================
            // refreshBtn
            // ======================
            this.refreshBtn.Text = "Refresh";
            this.refreshBtn.Location = new Point(280, 420);
            this.refreshBtn.Size = new Size(120, 30);
            this.refreshBtn.UseVisualStyleBackColor = true;

            this.refreshBtn.Click += (s, e) => LoadDecisions();

            // ======================
            // Form Settings
            // ======================
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 520);
            this.Text = "Hiring Decisions";

            this.Controls.Add(this.vacancyBox);
            this.Controls.Add(this.decisionsList);
            this.Controls.Add(this.candName);
            this.Controls.Add(this.candEmail);
            this.Controls.Add(this.decisionBox);
            this.Controls.Add(this.notesBox);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.refreshBtn);

            this.Name = "HiringDecisionForm";

            this.ResumeLayout(false);
        }
    }
}