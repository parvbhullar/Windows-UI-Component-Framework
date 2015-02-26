using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using log4net;

namespace Oranikle.Studio.Controls
{

    public class CtrlStyledToolStripTextBox : System.Windows.Forms.ToolStripTextBox
    {

        private static readonly log4net.ILog log;

        protected System.Drawing.Color borderColor;

        public CtrlStyledToolStripTextBox()
        {
            borderColor = Oranikle.Studio.Controls.BorderDrawer.BORDER_UNFOCUS_COLOR;
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        static CtrlStyledToolStripTextBox()
        {
            Oranikle.Studio.Controls.CtrlStyledToolStripTextBox.log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        private void newParent_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = Bounds;
            rectangle.X--;
            rectangle.Y--;
            rectangle.Width++;
            rectangle.Height++;
            e.Graphics.DrawRectangle(new System.Drawing.Pen(borderColor), rectangle);
        }

        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            borderColor = Oranikle.Studio.Controls.BorderDrawer.BORDER_FOCUS_COLOR;
            Invalidate();
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            borderColor = Oranikle.Studio.Controls.BorderDrawer.BORDER_UNFOCUS_COLOR;
            Invalidate();
        }

        protected override void OnParentChanged(System.Windows.Forms.ToolStrip oldParent, System.Windows.Forms.ToolStrip newParent)
        {
            base.OnParentChanged(oldParent, newParent);
            if (oldParent != null)
                oldParent.Paint -= new System.Windows.Forms.PaintEventHandler(newParent_Paint);
            if (newParent != null)
                newParent.Paint += new System.Windows.Forms.PaintEventHandler(newParent_Paint);
        }

    } // class CtrlStyledToolStripTextBox

}

