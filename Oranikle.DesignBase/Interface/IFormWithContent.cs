using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public interface IFormWithContent
    {

        event Oranikle.Studio.Controls.FormClickedEventHandler UnicastFormClicked;

        Oranikle.Studio.Controls.WeakEvent<System.EventArgs> OnMoveEvent
        {
            get;
        }

    }
}
