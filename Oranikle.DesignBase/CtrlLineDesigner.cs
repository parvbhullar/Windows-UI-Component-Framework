using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Oranikle.Studio.Controls
{
    public class CtrlLineDesigner : System.Windows.Forms.Design.ControlDesigner
    {

        public override System.Windows.Forms.Design.SelectionRules SelectionRules
        {
            get
            {
                return base.SelectionRules;
            }
        }

        public CtrlLineDesigner()
        {
        }

    }
}
