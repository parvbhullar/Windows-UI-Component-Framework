using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public interface IDisplayDataSourceManager
    {

        Oranikle.Studio.Controls.IDisplayDataSource DataSource
        {
            get;
        }

        void Unsubscribe(System.EventHandler handler);

        void WeakSubscribe(System.EventHandler handler);

    }
}
