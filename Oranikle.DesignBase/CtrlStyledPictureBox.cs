using System.Drawing;
using System.Windows.Forms;
using Oranikle.Studio.Controls.Properties;

namespace Oranikle.Studio.Controls
{

    public class CtrlStyledPictureBox : System.Windows.Forms.PictureBox, Oranikle.Studio.Controls.IControlWithBorder
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
                borderDrawer.BorderColor = value;
                Invalidate();
            }
        }

        public CtrlStyledPictureBox()
        {
            borderDrawer = new Oranikle.Studio.Controls.BorderDrawer();
            borderDrawer.RegisterFocusHoverControl(this);
        }

        public void RefreshNullableImage(bool isNull)
        {
            if (isNull)
                Image = Oranikle.Studio.Controls.Properties.Resources.BlankPlaceHolder;
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if (BorderStyle != System.Windows.Forms.BorderStyle.None)
                borderDrawer.DrawBorder(ref m, Width, Height);
        }

    } // class CtrlStyledPictureBox

}

