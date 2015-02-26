using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public interface IColumnSettingsManager
    {

        void ColumnWidthChanged(Oranikle.Studio.Controls.StyledDataGridView sender, System.Windows.Forms.DataGridViewColumnEventArgs e);

        void DgvEndInit(Oranikle.Studio.Controls.StyledDataGridView sender);

    }
}
