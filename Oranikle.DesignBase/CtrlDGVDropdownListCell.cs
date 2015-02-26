using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Oranikle.Studio.Controls
{
    public class CtrlDGVDropdownListCell : DataGridViewTextBoxCell
    {
        private object[] items;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object[] Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }

        public override object DefaultNewRowValue
        {
            get
                {
                return null;
            }
        }

        public override Type EditType
        {
            get
            {
                
                return typeof(CtrlDropdownHostList);
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

        public CtrlDGVDropdownListCell()
        {
            
        }

        string tipText = string.Empty;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TipText
        {
            get { return tipText; }
            set {
                this.ToolTipText = value; 
                tipText = value; }
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value != null )
                return value.ToString();
            return null;
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            CtrlDGVDropdownListColumn ctrlDGVDropdownListColumn = DataGridView.Columns[ColumnIndex] as CtrlDGVDropdownListColumn;
            CtrlDropdownHostList ctrlDropdownHostList = DataGridView.EditingControl as CtrlDropdownHostList;
            ctrlDropdownHostList.HorizontalOffset = 0;
            ctrlDropdownHostList.VerticalOffset = 0;
            ctrlDropdownHostList.ShowDropdownOnEntry = true;
            ctrlDropdownHostList.SelectedIndex = -1;
            if (ctrlDGVDropdownListColumn != null)
            {
                ctrlDropdownHostList.Items = ctrlDGVDropdownListColumn.Items;
                ctrlDropdownHostList.ShowAddLink = ctrlDGVDropdownListColumn.ShowAddLink;
                ctrlDropdownHostList.ShowButtonControl = ctrlDGVDropdownListColumn.ShowButtonControl;
                ctrlDropdownHostList.ShowButton2 = ctrlDGVDropdownListColumn.ShowButton2;
                ctrlDropdownHostList.TextReadOnly = ctrlDGVDropdownListColumn.TextReadOnly;
                ctrlDropdownHostList.EmptyTextOnNullValue = ctrlDGVDropdownListColumn.EmptyTextOnNullValue;
            }
            ctrlDropdownHostList.Value = Value;
        }

    }
}
