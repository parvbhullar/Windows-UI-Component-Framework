using System.Drawing;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{

    public class CtrlStyledListBox : System.Windows.Forms.ListBox, Oranikle.Studio.Controls.IControlWithBorder
    {

        private Oranikle.Studio.Controls.BorderDrawer borderDrawer;

        public System.Drawing.Color BorderColor
        {
            get
            {
                return borderDrawer.BorderColor;
            }
            set
            {
                borderDrawer.BorderColor = value;
                Invalidate();
            }
        }

        public CtrlStyledListBox()
        {
            borderDrawer = new Oranikle.Studio.Controls.BorderDrawer();
            borderDrawer.RegisterFocusHoverControl(this);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            borderDrawer.DrawBorder(ref m, Width, Height);
        }

    } // class CtrlStyledListBox

}

