using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Oranikle.Studio.Controls
{
     
     public class CtrlDGVDropdownListColumn : DataGridViewColumn
    {

        private string _ItemSourceProperty;

        public string ItemSourceProperty
        {
            get
            {
                return _ItemSourceProperty;
            }
            set
            {
                _ItemSourceProperty = value;
            }
        }

        private ObjectCollection items;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObjectCollection Items
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

        bool showTextTip = false;
        public bool ShowTextTip
        {
            get { return showTextTip; }
            set { showTextTip = value; }
        }

        private bool showAddLink = false;
        public bool ShowAddLink
        {
            get
            {
                return showAddLink;

            }
            set
            {
                showAddLink = value;
            }
        }
        public bool EmptyTextOnNullValue { get; set; }

        private bool textReadOnly = false;
        public bool TextReadOnly
        {
            get
            {
                return textReadOnly;

            }
            set
            {
                textReadOnly = value;
            }
        }
        private bool showButtonControl = false;
        public bool ShowButtonControl
        {
            get
            {
                return showButtonControl;
            }
            set
            {
                showButtonControl = value;
            }
        }
        private bool showButton2 = false;
        public bool ShowButton2
        {
            get
            {
                return showButton2;
            }
            set
            {
                showButton2 = value;
            }
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if ((value != null) && !value.GetType().IsAssignableFrom(typeof(CtrlDGVDropdownListCell)))
                    throw new InvalidCastException("Must be a CtrlDGVDropdownListCell");
                base.CellTemplate = value;
            }
        }

        public CtrlDGVDropdownListColumn() : base(new CtrlDGVDropdownListCell())
        {
            _ItemSourceProperty = "Id";
            this.Items = new ObjectCollection();
        }

    }
}
