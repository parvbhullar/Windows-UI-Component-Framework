using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
    public partial class CtrlTip : UserControl
    {
        public CtrlTip()
        {
            //if (!DesignMode)
            LP.Validate();
            InitializeComponent();
        }

        public string TipText
        {
            get { return lblText.Text; }
            set { lblText.Text = value; }
        }

        public Color TipBorderColor
        {
            get { return ctrlUIBase.BorderColor; }
            set { ctrlUIBase.BorderColor = value; }
        }

        public Color TipForeColor
        {
            get { return lblText.ForeColor; }
            set { lblText.ForeColor = value; }
        }

        public Color TipBackColor
        {
            get { return ctrlUIBase.BackColor; }
            set {
                lblText.BackColor = value;
                ctrlUIBase.BackColor = value; }
        }

        public BorderStyle TipBorderStyle
        {
            get {
                return ctrlUIBase.BorderStyle; 
            }
            set
            {
                ctrlUIBase.BorderStyle = value;
            }
        }
    }
}
