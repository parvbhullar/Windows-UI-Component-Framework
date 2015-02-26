using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public interface IDlgSearch
    {

        Oranikle.Studio.Controls.ISearchDialogCriteria SearchDialogCriteria
        {
            get;
            set;
        }

        void Close();

    }
}
