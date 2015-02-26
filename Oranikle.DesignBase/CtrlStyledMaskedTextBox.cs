using System;
using System.Drawing;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{

    public class CtrlStyledMaskedTextBox : System.Windows.Forms.MaskedTextBox, Oranikle.Studio.Controls.IControlWithBorder
    {

        protected Oranikle.Studio.Controls.BorderDrawer borderDrawer;

        public System.Drawing.Color BorderColor
        {
            get
            {
                return borderDrawer.BorderColor;
            }
            set
            {
            }
        }

        public CtrlStyledMaskedTextBox()
        {
            borderDrawer = new Oranikle.Studio.Controls.BorderDrawer();
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            borderDrawer.RegisterFocusHoverControl(this);
        }

        protected override void OnLocationChanged(System.EventArgs e)
        {
            base.OnLocationChanged(e);
            Invalidate();
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if (BorderStyle != System.Windows.Forms.BorderStyle.None)
                borderDrawer.DrawBorder(ref m, Width, Height);
        }

    } // class CtrlStyledMaskedTextBox

}

