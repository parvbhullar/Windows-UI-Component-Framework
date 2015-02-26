using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public interface IInitializeClass
    {

        void InitAfterLogin();

        void InitAtStartupBeforeLogin();

    }
}
