using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oranikle.Studio.Controls;

namespace Oranikle.DesignBase.Viewer
{
    public partial class ViewerForm : Form
    {
        public ViewerForm()
        {
            BaseUserControl.SetDesignMode(false);
            InitializeComponent();
        }
        public class Student
        {
            public string StudentName { get; set; }
        }

        private void ViewerForm_Load(object sender, EventArgs e)
        {
            //CtrlDropdownHostList ctrlDropdownHostList1 = new CtrlDropdownHostList();
            //ctrlDropdownHostList1.Location = new Point(100, 100);
            ctrlDropdownHostList1.ShowGridHeader = false;
            ctrlDropdownHostList1.ShowAddLink = false;
            ctrlDropdownHostList1.HeaderTitle = "Item";
            ctrlDropdownHostList1.Parent = this;
            List<Student> list = new List<Student>();
            for (int i = 1; i < 11; i++)
            {
                Student stu = new Student();
                stu.StudentName = "Parvinder" + i.ToString();
                list.Add(stu);
                ListViewItem item = new ListViewItem(stu.StudentName);
                lstView.Items.Add(item);
            }
            ctrlDropdownHostList1.Items.AddRange(list.ToArray());

            colSN.Items.AddRange(list.ToArray());
            
        }
        private void ButtonClick_Load(object sender, EventArgs e)
        {
            if (styledDataGridView1.CurrentRow != null)
            { 
                Student stu = (Student)styledDataGridView1.CurrentRow.Cells[0].Value;
                MessageBox.Show(stu.StudentName);
            }
        }

        private void styledButton2_Click(object sender, EventArgs e)
        {
            WizardDemoForm wizardDemoForm = new WizardDemoForm();
            wizardDemoForm.ShowDialog();
        }
    }
}
