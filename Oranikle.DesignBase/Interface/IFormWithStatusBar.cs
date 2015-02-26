using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public interface IFormWithStatusBar
    {

        void EndProgressBar(Oranikle.Studio.Controls.EnumStatusBarStatusType.StatusType type, string statusText);

        void ShowStatusbarText(Oranikle.Studio.Controls.EnumStatusBarStatusType.StatusType type, string s);

        void ShowStatusbarText(string s);

        void StartProgressBar(string statusText);

    }
}
