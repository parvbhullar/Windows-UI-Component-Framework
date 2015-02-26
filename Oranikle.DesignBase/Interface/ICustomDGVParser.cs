using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public interface ICustomDGVParser
    {

        void CustomDGVParse(Oranikle.Studio.Controls.StyledDataGridView sender, System.Windows.Forms.DataGridViewCellParsingEventArgs e);

    }
}
