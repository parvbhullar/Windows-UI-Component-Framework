using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public interface IObjectWithTimestamp
    {

        byte[] Timestamp
        {
            get;
        }

    }
}
