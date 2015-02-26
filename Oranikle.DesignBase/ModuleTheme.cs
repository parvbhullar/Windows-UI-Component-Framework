using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public class ModuleTheme
    {
        private System.Drawing.Color _LabelTitleColor;

        public System.Drawing.Color LabelTitleColor
        {
            get
            {
                return _LabelTitleColor;
            }
            set
            {
                _LabelTitleColor = value;
            }
        }

        public ModuleTheme()
        {
        }
    }
}
