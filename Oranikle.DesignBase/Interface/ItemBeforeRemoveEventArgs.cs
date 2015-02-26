using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public class ItemBeforeRemoveEventArgs : System.EventArgs
    {

        public readonly Oranikle.Studio.Controls.CtrlTabStripButton RemovingTab;

        public bool Cancel;

        public ItemBeforeRemoveEventArgs(Oranikle.Studio.Controls.CtrlTabStripButton removingTab)
        {
            RemovingTab = removingTab;
        }

    }
}
