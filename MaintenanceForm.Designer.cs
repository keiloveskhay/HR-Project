using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HR_Project
{
    partial class MaintenanceForm
    {
        private IContainer components = null;

        private ListBox deptList;
        private TextBox deptBox;
        private Button addDeptBtn;

        private ListBox roleList;
        private TextBox roleBox;
        private Button addRoleBtn;

        private ListBox typeList;
        private TextBox typeBox;
        private Button addTypeBtn;

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
            this.deptList = new System.Windows.Forms.ListBox();
            this.deptBox = new System.Windows.Forms.TextBox();
            this.addDeptBtn = new System.Windows.Forms.Button();

            this.roleList = new System.Windows.Forms.ListBox();
            this.roleBox = new System.Windows.Forms.TextBox();
            this.addRoleBtn = new System.Windows.Forms.Button();

            this.typeList = new System.Windows.Forms.ListBox();
            this.typeBox = new System.Windows.Forms.TextBox();
            this.addTypeBtn = new System.Windows.Forms.Button();

            this.SuspendLayout();

            this.deptList.Location = new System.Drawing.Point(12, 30);
            this.deptList.Size = new System.Drawing.Size(250, 300);
            this.deptList.Name = "deptList";
            this.deptList.SelectedIndexChanged += new System.EventHandler(this.deptList_SelectedIndexChanged);

            this.deptBox.Location = new System.Drawing.Point(12, 340);
            this.deptBox.Size = new System.Drawing.Size(160, 23);
            this.deptBox.Name = "deptBox";

            this.addDeptBtn.Location = new System.Drawing.Point(180, 338);
            this.addDeptBtn.Size = new System.Drawing.Size(80, 25);
            this.addDeptBtn.Text = "Add Dept";
            this.addDeptBtn.Name = "addDeptBtn";
            this.addDeptBtn.Click += new System.EventHandler(this.addDeptBtn_Click);

            this.roleList.Location = new System.Drawing.Point(280, 30);
            this.roleList.Size = new System.Drawing.Size(250, 300);
            this.roleList.Name = "roleList";

            this.roleBox.Location = new System.Drawing.Point(280, 340);
            this.roleBox.Size = new System.Drawing.Size(160, 23);
            this.roleBox.Name = "roleBox";

            this.addRoleBtn.Location = new System.Drawing.Point(450, 338);
            this.addRoleBtn.Size = new System.Drawing.Size(80, 25);
            this.addRoleBtn.Text = "Add Role";
            this.addRoleBtn.Name = "addRoleBtn";
            this.addRoleBtn.Click += new System.EventHandler(this.addRoleBtn_Click);

            this.typeList.Location = new System.Drawing.Point(550, 30);
            this.typeList.Size = new System.Drawing.Size(250, 300);
            this.typeList.Name = "typeList";

            this.typeBox.Location = new System.Drawing.Point(550, 340);
            this.typeBox.Size = new System.Drawing.Size(160, 23);
            this.typeBox.Name = "typeBox";

            this.addTypeBtn.Location = new System.Drawing.Point(720, 338);
            this.addTypeBtn.Size = new System.Drawing.Size(80, 25);
            this.addTypeBtn.Text = "Add Type";
            this.addTypeBtn.Name = "addTypeBtn";
            this.addTypeBtn.Click += new System.EventHandler(this.addTypeBtn_Click);

            this.ClientSize = new System.Drawing.Size(820, 420);
            this.Controls.Add(this.deptList);
            this.Controls.Add(this.deptBox);
            this.Controls.Add(this.addDeptBtn);

            this.Controls.Add(this.roleList);
            this.Controls.Add(this.roleBox);
            this.Controls.Add(this.addRoleBtn);

            this.Controls.Add(this.typeList);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.addTypeBtn);

            this.Name = "MaintenanceForm";
            this.Text = "Maintenance Module";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}