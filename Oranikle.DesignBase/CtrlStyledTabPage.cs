using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{

    [System.Drawing.ToolboxBitmap(typeof(Oranikle.Studio.Controls.CtrlStyledTabPage))]
    public class CtrlStyledTabPage : System.Windows.Forms.TabPage
    {

        private System.Drawing.Font _TabFont;
        private System.Nullable<System.Drawing.StringAlignment> _TabTextAlignment;
        private System.Nullable<int> _TabWidth;

        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Font TabFont
        {
            get
            {
                return _TabFont;
            }
            set
            {
                _TabFont = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public System.Nullable<System.Drawing.StringAlignment> TabTextAlignment
        {
            get
            {
                return _TabTextAlignment;
            }
            set
            {
                _TabTextAlignment = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public System.Nullable<int> TabWidth
        {
            get
            {
                return _TabWidth;
            }
            set
            {
                _TabWidth = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        public new bool UseVisualStyleBackColor
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public CtrlStyledTabPage()
        {
            _TabWidth = new System.Nullable<int>();
            base.UseVisualStyleBackColor = false;
        }
    }

}

