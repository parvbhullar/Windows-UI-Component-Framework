using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public interface IMsysBusinessBase
    {
        bool HasDataConflict
        {
            get;
            set;
        }

        bool CanAddObject();

        bool CanDeleteObject();

        bool CanEditObject();

        bool CanGetObject();

        System.Collections.Generic.List<Csla.Validation.BrokenRule> GetFullBrokenRules();

        string GetHumanReadableName();
    }
}
