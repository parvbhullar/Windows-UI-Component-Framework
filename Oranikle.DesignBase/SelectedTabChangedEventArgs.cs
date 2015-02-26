using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public class SelectedTabChangedEventArgs : System.EventArgs
    {

        public readonly Oranikle.Studio.Controls.CtrlTabStripButton SelectedTabAfter;
        public readonly Oranikle.Studio.Controls.CtrlTabStripButton SelectedTabBefore;

        public bool Cancel;

        public SelectedTabChangedEventArgs(Oranikle.Studio.Controls.CtrlTabStripButton beforeTab, Oranikle.Studio.Controls.CtrlTabStripButton afterTab)
        {
            SelectedTabBefore = beforeTab;
            SelectedTabAfter = afterTab;
        }

    } 
}
