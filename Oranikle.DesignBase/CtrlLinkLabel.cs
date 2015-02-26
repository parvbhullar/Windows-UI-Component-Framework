using System.Drawing;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{

    public class CtrlLinkLabel : System.Windows.Forms.LinkLabel
    {

        public CtrlLinkLabel()
        {
        }

        public void ChangeLinkColor(System.Drawing.Color c)
        {
            ActiveLinkColor = c;
            DisabledLinkColor = c;
            ForeColor = c;
            LinkColor = c;
            VisitedLinkColor = c;
        }

        public static void ChangeLinkColor(System.Windows.Forms.LinkLabel l, System.Drawing.Color c)
        {
            l.ActiveLinkColor = c;
            l.DisabledLinkColor = c;
            l.ForeColor = c;
            l.LinkColor = c;
            l.VisitedLinkColor = c;
        }

    } // class CtrlLinkLabel

}

