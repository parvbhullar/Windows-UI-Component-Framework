using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
    public class CtrlDGVTextCell : DataGridViewTextBoxCell 
    {
        public CtrlDGVTextCell()
            : base()
        {
            // Use the short date format.
        }

        private HitText textType = HitText.Currency;

        public HitText TextType
        {
            get { return textType; }
            set { textType = value; }
        }

        protected override void OnKeyPress(KeyPressEventArgs e, int rowIndex)
        {
            //base.OnKeyPress(e, rowIndex);
        } 


        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            CtrlTextCell ctl = DataGridView.EditingControl as CtrlTextCell;
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            if (this.OwningColumn is CtrlDGVCurrencyColumn)
            {
                CtrlDGVCurrencyColumn column = this.OwningColumn as CtrlDGVCurrencyColumn;
                this.TextType = column.TextType;
                
            }
            else if(this.OwningColumn is CtrlDGVCreditDebitColumn)
            {
                CtrlDGVCreditDebitColumn column = this.OwningColumn as CtrlDGVCreditDebitColumn;
                this.TextType = column.TextType;
            }
            else if (this.OwningColumn is CtrlDGVYesNoColumn)
            {
                CtrlDGVYesNoColumn column = this.OwningColumn as CtrlDGVYesNoColumn;
                this.TextType = column.TextType;
            }
            ctl.TextType = this.TextType;
            ctl.TextAlign = TextHelper.TranslateGridColumnAligment(this.Style.Alignment);
            if (Value == null)
            {

            }
            else
            {
                ctl.Text = this.Value.ToString();
            }
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value == null)
                return string.Empty;
            return value.ToString();
        }

        public object SelectedItem
        {
            get;
            set;
        }
      
        public override Type EditType
        {
            get
            {
                // Return the type of the editing control that CalendarCell uses.
                return typeof(CtrlTextCell);
                //return base.EditType;
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.
                return typeof(String);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
