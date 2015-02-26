using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public class FormClickedEventArgs : System.EventArgs
    {

        private System.Drawing.Point _ClickLocation;

        public System.Drawing.Point ClickLocation
        {
            get
            {
                return _ClickLocation;
            }
        }

        public FormClickedEventArgs(System.Drawing.Point clickLocation)
        {
            _ClickLocation = clickLocation;
        }

    }
}
