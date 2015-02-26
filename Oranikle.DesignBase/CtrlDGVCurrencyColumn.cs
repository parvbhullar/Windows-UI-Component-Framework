using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
    public class CtrlDGVCurrencyColumn : DataGridViewColumn
    {
        public CtrlDGVCurrencyColumn()
            : base(new CtrlDGVTextCell())
        {
        }
        private HitText textType = HitText.Currency;
        private DataGridViewCellStyle _CurrencyCellStyle;
        private bool isDisposed;

        private static ExtendedDecimalNumberFormatter _CurrencyFormat;

        public bool IsDisposed
        {
            get
            {
                return isDisposed;
            }
            set
            {
                isDisposed = value;
            }
        }
        public HitText TextType
        {
            get { return textType; }
            set { textType = value; }
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
                    !value.GetType().IsAssignableFrom(typeof(CtrlDGVTextCell)))
                {
                    throw new InvalidCastException("Must be a CtrlDGVTextCell");
                }
                base.CellTemplate = value;
            }
        }
    }
}
