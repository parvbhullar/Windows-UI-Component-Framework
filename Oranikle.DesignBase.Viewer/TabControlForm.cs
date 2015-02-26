using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Oranikle.DesignBase.Viewer
{
    public partial class TabControlForm : Form
    {
        public TabControlForm()
        {
            InitializeComponent();
        }
        int i = 1;
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ViewerForm form = new ViewerForm();

            form.Text = "Tab "+ i.ToString();

            //if (tabControl1.Contains(form))
            //    return;
            //foreach (TabPage tp in tabControl1.TabPages)
            //{
             
            //}
            {

                tabControl1.TabPages.Add(form);
                //i++;
            }
        }

        private void tabControl1_Load(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form1 form1 = new Form1();
            //tabControl1.TabPages.Add(form1);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewerForm form2 = new ViewerForm();
            tabControl1.TabPages.Add(form2);
        }
    }
}
