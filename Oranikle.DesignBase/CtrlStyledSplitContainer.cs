using System.ComponentModel;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{

    public class CtrlStyledSplitContainer : System.Windows.Forms.SplitContainer
    {

        [System.ComponentModel.DefaultValue(false)]
        public new bool TabStop
        {
            get
            {
                return base.TabStop;
            }
            set
            {
                base.TabStop = value;
            }
        }

        public CtrlStyledSplitContainer()
        {
            TabStop = false;
        }

    } // class CtrlStyledSplitContainer

}

