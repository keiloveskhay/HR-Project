using System;
using System.Windows.Forms;

namespace IDk
{
    public partial class ManageProfileForm : Form
    {
        private readonly string _email;
        private Applicant _applicant;
        private ListBox educationList;
        private ListBox skillsList;
        private ListBox workList;
        private Button addEducationBtn;
        private Button addSkillBtn;
        private Button addWorkBtn;
        private Button changePasswordBtn;
        private Button closeBtn;

        public ManageProfileForm(string email)
        {
            _email = email;
            Text = $"Manage Profile - {email}";
            Width = 700;
            Height = 520;
            InitializeComponents();
            LoadData();
        }

        private void InitializeComponents()
        {
            var educationLabel = new Label { Text = "Education", Left = 20, Top = 0, Width = 100 };
            educationList = new ListBox { Left = 20, Top = 20, Width = 420, Height = 140 };
            addEducationBtn = new Button { Text = "Add Education", Left = 460, Top = 20, Width = 120 };
            addEducationBtn.Click += (s, e) => AddEducation();

            var skillsLabel = new Label { Text = "Skills", Left = 20, Top = 180, Width = 100 };
            skillsList = new ListBox { Left = 20, Top = 200, Width = 420, Height = 80 };
            addSkillBtn = new Button { Text = "Add Skill", Left = 460, Top = 200, Width = 120 };
            addSkillBtn.Click += (s, e) => AddSkill();

            var workLabel = new Label { Text = "Work Experience", Left = 20, Top = 300, Width = 150 };
            workList = new ListBox { Left = 20, Top = 320, Width = 420, Height = 140 };
            addWorkBtn = new Button { Text = "Add Work", Left = 460, Top = 320, Width = 120 };
            addWorkBtn.Click += (s, e) => AddWork();

            changePasswordBtn = new Button { Text = "Change Password", Left = 460, Top = 260, Width = 120 };
            changePasswordBtn.Click += (s, e) => ChangePassword();

            closeBtn = new Button { Text = "Close", Left = 460, Top = 420, Width = 120 };
            closeBtn.Click += (s, e) => Close();

            Controls.AddRange(new Control[] { educationLabel, educationList, addEducationBtn, skillsLabel, skillsList, addSkillBtn, workLabel, workList, addWorkBtn, changePasswordBtn, closeBtn });
        }

        private void LoadData()
        {
            _applicant = ProfileService.GetByEmail(_email);
            if (_applicant == null) { MessageBox.Show("Profile not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); Close(); return; }
            educationList.Items.Clear();
            foreach (var e in _applicant.Education) educationList.Items.Add(e.ToString());
            skillsList.Items.Clear();
            foreach (var s in _applicant.Skills) skillsList.Items.Add(s);
            workList.Items.Clear();
            foreach (var w in _applicant.WorkExperiences) workList.Items.Add(w.ToString());
        }

        private void AddEducation()
        {
            using var f = new AddEducationForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                var e = f.Education;
                if (e != null)
                {
                    ProfileService.AddEducation(_applicant.Id, e);
                    LoadData();
                }
            }
        }

        private void AddSkill()
        {
            using var f = new AddSkillForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                var skill = f.Skill;
                if (!string.IsNullOrWhiteSpace(skill))
                {
                    ProfileService.AddSkill(_applicant.Id, skill);
                    LoadData();
                }
            }
        }

        private void AddWork()
        {
            using var f = new AddWorkExperienceForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                var w = f.Work;
                if (w != null)
                {
                    ProfileService.AddWorkExperience(_applicant.Id, w);
                    LoadData();
                }
            }
        }

        private void ChangePassword()
        {
            using var f = new ChangePasswordForm(_email);
            f.ShowDialog();
        }
    }
}
