using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public interface IControlByOptions
    {

        event System.EventHandler Disposed;

        bool IsDisposed
        {
            get;
        }

        void RefreshUIFromCompanyOrPersonalOptions();

    }
}
