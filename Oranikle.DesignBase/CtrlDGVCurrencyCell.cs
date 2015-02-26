using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
    public class CtrlDGVCurrencyCell : DataGridViewTextBoxCell
    {

        public override Type EditType
        {
            get
            {
                return typeof(CtrlCurrency);
            }
        }
        public override object DefaultNewRowValue
        {
            get
            {
                return null;
            }
        }

        public override Type FormattedValueType
        {
            get
            {
                return typeof(object);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(object);
            }
        }

        public CtrlDGVCurrencyCell()
        {
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            CtrlCurrency ctrlCurrency = DataGridView.EditingControl as CtrlCurrency;
            if (Value != null)
                ctrlCurrency.Text = Value.ToString();
        }
    }
}
