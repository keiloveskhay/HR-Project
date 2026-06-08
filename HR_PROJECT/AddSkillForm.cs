using System;
using System.Windows.Forms;

namespace IDk
{
    public class AddSkillForm : Form
    {
        private TextBox skillBox;
        private Button okBtn;
        private Button cancelBtn;
        public string Skill { get; private set; }

        public AddSkillForm()
        {
            Text = "Add Skill";
            Width = 400;
            Height = 160;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            var lbl = new Label { Text = "Skill:", Left = 10, Top = 10, Width = 100 };
            skillBox = new TextBox { Left = 120, Top = 10, Width = 240 };
            okBtn = new Button { Text = "OK", Left = 120, Top = 50, Width = 100 };
            cancelBtn = new Button { Text = "Cancel", Left = 240, Top = 50, Width = 100 };
            okBtn.Click += (s, e) => { Skill = skillBox.Text.Trim(); if (string.IsNullOrWhiteSpace(Skill)) { MessageBox.Show("Enter a skill."); return; } DialogResult = DialogResult.OK; };
            cancelBtn.Click += (s, e) => DialogResult = DialogResult.Cancel;
            Controls.AddRange(new Control[] { lbl, skillBox, okBtn, cancelBtn });
        }
    }
}
