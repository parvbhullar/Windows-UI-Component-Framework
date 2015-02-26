using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
    public class CtrlStyledCheckBoxColumn : DataGridViewColumn
    {
        public CtrlStyledCheckBoxColumn()
            : base(new CtrlStyledCheckBoxCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(CtrlStyledCheckBoxCell)))
                {
                    throw new InvalidCastException("Must be a ListDataGridViewCell");
                }
                base.CellTemplate = value;
            }
        }
    }
}
