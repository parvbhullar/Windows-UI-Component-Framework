using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public interface IDisplayDataSource : System.Collections.ICollection, System.Collections.IEnumerable
    {
        void AddUnknownItem(object id);
    }
}
