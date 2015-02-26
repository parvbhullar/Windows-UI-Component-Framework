using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using Csla;
namespace Oranikle.Studio.Controls
{
    [DefaultBindingProperty("Value")]
    [DefaultEvent("ValueChanged")]
    public class CtrlDropdownHostList : CtrlDropdownHost, IDropdownDisplayer, IDataGridViewEditingControl
    {

        public enum RelatedItemSource
        {
            DataBindingsBindingSource = 1,
            DataGridViewDataBoundObject = 2
        }

        private delegate void RefreshItems_Callback();

        private bool _AllowPicked;
        private CtrlDropdownHostList.RelatedItemSource _ItemSource;
        private string _ItemSourceProperty;
        private object _NullValue;
        private IContainer components;
        protected DataGridView dataGridView;
        protected bool editingControlValueChanged;
        protected int rowIndex;

        [System.ComponentModel.Description("This is fired when the selected value is changed.")]
        [System.ComponentModel.Category("Value")]
        [System.ComponentModel.Browsable(true)]
        public event System.EventHandler ValueChanged;

        [System.ComponentModel.Description("This is fired when the add new link of control is clicked")]
        [System.ComponentModel.Category("Click")]
        [System.ComponentModel.Browsable(true)]
        public event System.EventHandler AddNew_Clicked;

        [System.ComponentModel.Description("This is fired when the button1 of control is clicked")]
        [System.ComponentModel.Category("Click")]
        [System.ComponentModel.Browsable(true)]
        public event System.EventHandler Button1_Clicked;

        [System.ComponentModel.Description("This is fired when the button2 of control is clicked")]
        [System.ComponentModel.Category("Click")]
        [System.ComponentModel.Browsable(true)]
        public event System.EventHandler Button2_Clicked;

        private CtrlDropDownListForm mySubform;


        public override void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, System.EventArgs.Empty);
        }

        public override void OnAddNew_Clicked()
        {
            if (AddNew_Clicked != null)
            {
                AddNew_Clicked(this, System.EventArgs.Empty);
            }
            if (IsSelectionList)
            {
                mySubform.UncheckAll();   
            }
        }

        public override void OnEdit_Clicked()
        {
            if (Button1_Clicked != null)
                Button1_Clicked(this, System.EventArgs.Empty);
        }

        public override void OnDelete_Clicked()
        {
            if (Button2_Clicked != null)
                Button2_Clicked(this, System.EventArgs.Empty);
        }

        public ObjectCollection Items
        {
            get
            {
                if (!DesignMode)
                    return mySubform.Items;
                return null;
            }
            set
            {
                if (!DesignMode)
                    mySubform.Items = value;
            }
        }
       
        public ObjectCollection SelectedItems
        {
            get
            {
                return mySubform.SelectedItems;
            }
            
        }

        public void SetCheckedItem(int index)
        {
            mySubform.SetCheckedItem(index);
        }

        [DefaultValue(false)]
        public bool IsSelectionList
        {
            get { return mySubform.IsSelectionList; }
            set { mySubform.IsSelectionList = value; }
        }

        [DefaultValue(false)]
        public bool AllowPicked
        {
            get
            {
                return _AllowPicked;
            }
            set
            {
                _AllowPicked = value;
            }
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
                UpdateDataSource();
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

        public bool ShowGridHeader
        {
            get
            {
                //if (!DesignMode)
                    return mySubform.ShowGridHeader;
            }
            set
            {
                //if (!DesignMode)
                    mySubform.ShowGridHeader = value;
            }
        }

        private bool emptyTextOnNullValue = true;
        public bool EmptyTextOnNullValue
        {
            get
            {
                //if (!DesignMode)
                return emptyTextOnNullValue;
            }
            set
            {
                //if (!DesignMode)
                emptyTextOnNullValue = value;
            }
        }

        public string HeaderTitle
        {
            get
            {
                //if (!DesignMode)
                    return mySubform.HeaderTitle;
            }
            set
            {
                //if (!DesignMode)
                mySubform.HeaderTitle = value; 
            }
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                if (Subform == null) return null;
                if (this.Value != null)
                    Subform.UpdateFromText(this.Value.ToString());
                if (this.Value == null && emptyTextOnNullValue) this.textBox.Text = string.Empty;
                _value = Value;
                return _value;
            }
            set
            {
                Value = value;
                _value = value;
            }
        }

        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return editingControlValueChanged;
            }
            set
            {
                editingControlValueChanged = value;
            }
        }

        public Cursor EditingPanelCursor
        {
            get
            {
                return Cursor;
            }
        }

        public CtrlDropdownHostList.RelatedItemSource ItemSource
        {
            get
            {
                return _ItemSource;
            }
            set
            {
                _ItemSource = value;
            }
        }

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

        [DefaultValue(null)]
        public object NullValue
        {
            get
            {
                return _NullValue;
            }
            set
            {
                _NullValue = value;
            }
        }

        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        [Bindable(true)]
        public override object Value
        {
            get
            {
                return base.Value;
            }
            set
            {
               // if (base.Value != value)
                {
                    bool flag = SwitchingBetweenNullRepresentations(value, NullValue);
                    base.Value = value;
                    if (!flag)
                    {
                        editingControlValueChanged = true;
                        if (EditingControlDataGridView != null)
                            EditingControlDataGridView.NotifyCurrentCellDirty(true);
                    }
                }
            }
        }
        int selectedIndex = -1;
        public int SelectedIndex
        {
            get
            {
                int index = selectedIndex;
                if (Items != null)
                    foreach (object obj in Items)
                    {
                        index++;
                        if (obj.Equals(this.Value))
                        {
                            return index;
                        }
                    }
                return index;
            }
            set {
                int index = -1;
                if (Items != null)
                    foreach (object obj in Items)
                    {
                        index++;
                        if (index == value)
                        {
                            this.Value = obj;
                            break;
                        }
                    }
                selectedIndex = value;
                if (mySubform != null)
                {
                    mySubform.SelectedIndex = value;
                }
            }
        }

        private CtrlDropDownListForm MySubform
        {
            get
            {
                if (this.mySubform == null)
                    this.mySubform = new CtrlDropDownListForm();
                return this.mySubform;
            }
        }

        public CtrlDropdownHostList()
        {
            //if (!DesignMode)
            LP.Validate();
            _ItemSourceProperty = "Id";
            _ItemSource = CtrlDropdownHostList.RelatedItemSource.DataGridViewDataBoundObject;//DataBindingsBindingSource;
            InitializeComponent();
            Disposed += new EventHandler(CtrlDropdownHostList_Disposed);
            textBox.TextChanged += new EventHandler(textBox_TextChanged);
            textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            this.Leave += new EventHandler(CtrlDropdownHostList_Leave);
            Displayer = this;
            textBox.ConvertEnterToTab = false;
            if (!DesignMode)
                Subform = this.MySubform;
        }

        
        void CtrlDropdownHostList_Leave(object sender, EventArgs e)
        {
            if (Subform != null && this.IsSelectionList == false && this.dataGridView == null)
            {
                if (this.Value != null)
                    Subform.UpdateFromText(this.Value.ToString());
                if (this.Value == null && emptyTextOnNullValue)
                {
                    textBox.Text = string.Empty;
                }
            }
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            ForeColor = dataGridViewCellStyle.ForeColor;
            BackColor = dataGridViewCellStyle.BackColor;
            textBox.Font = dataGridViewCellStyle.Font;
            textBox.ForeColor = dataGridViewCellStyle.ForeColor;
            textBox.BackColor = dataGridViewCellStyle.BackColor;
        }

        private void CtrlDropdownHostList_Disposed(object sender, EventArgs e)
        {
            if (Subform != null)
                Subform.Visible = false;
        }

        private void displayDataSourceManager_CacheUpdatedEvent(object sender, EventArgs e)
        {
            UpdateDataSource();
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            return true;
        }

        public object GetDisplay(object value)
        {
            if (DesignMode)
                return null;

            return value;
        }

        public object GetDisplayOrNull(object value)
        {
            if (DesignMode)
                return null;

            return value;
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        private object GetItemFromDGV()
        {
            try
            {
                if ((dataGridView == null) || (dataGridView.CurrentRow == null) || (dataGridView.CurrentRow.Tag == null))
                {
                    return null;
                }
                object obj = dataGridView.CurrentRow.Tag;
                return obj;
            }
            catch (Exception)
            {
            }
            return dataGridView.CurrentRow.Tag;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.Color.White;
            this.pnlButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            // 
            // textBox
            // 
            this.textBox.Size = new System.Drawing.Size(167, 14);
            // 
            // CtrlDropdownHostList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "CtrlDropdownHostList";
            this.EnabledChanged += new System.EventHandler(this.CtrlDropdownHostList_EnabledChanged);
            this.ResumeLayout(false);

        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if (selectAll)
                textBox.SelectAll();
            textBox.Focus();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Subform.Visible)
                ShowSubform();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox.Text == "")
                _value = NullValue;
            Subform.UpdateFromText(textBox.Text);
            OnValueChanged();
        }

        public void UpdateDataSource()
        {
            if (mySubform != null)
            {
                mySubform.ShowAddLink = showAddLink;
                mySubform.ShowDeleteButton = showButton2;
                mySubform.ShowEditControl = showButtonControl;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
            try
            {
                if (mySubform != null)
                    mySubform.Visible = false;
            }
            catch (Exception ex)
            { 
                
            }
        }

        protected virtual string DoCheckDefaultValue(object value)
        {
            Nullable<int> nullable = (Nullable<int>)(value as Nullable<int>);
            if (!(0 == nullable.GetValueOrDefault() && nullable.HasValue))
                return "";
            return "";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                //displayDataSourceManager = EntityTypeInfo.GetDataSourceManager(EnumEntities.Enum.Location);
                UpdateDataSource();
                
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            ((CtrlDropDownListForm)Subform).OnMouseWheelPublic(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Subform.Host != this)
                Subform.SetHost(this);
            if (keyData == Keys.Up)
            {
                ((CtrlDropDownListForm)Subform).HandlerSelectUp();
                return true;
            }
            if (keyData == Keys.Down)
            {
                ((CtrlDropDownListForm)Subform).HandlerSelectDown();
                return true;
            }
            if (keyData == Keys.Tab)
            {
                if (textBox.Text == "")
                    ((CtrlDropDownListForm)Subform).HandlerSelectNull();
                else if (!((CtrlDropDownListForm)Subform).LinkIsSelected)
                    ((CtrlDropDownListForm)Subform).HandlerSelectCurrent();
                if (mySubform != null)
                    mySubform.SetText();
                return base.ProcessCmdKey(ref msg, keyData);
            }
            if (keyData == Keys.Enter)
            {
                if (textBox.Text == "" && !((CtrlDropDownListForm)Subform).LinkIsSelected)
                    ((CtrlDropDownListForm)Subform).HandlerSelectNull();
                else
                    ((CtrlDropDownListForm)Subform).HandlerSelectCurrent();

                if (!((CtrlDropDownListForm)Subform).LinkIsSelected )
                    SendKeys.Send("\t");
                OnEnterPressed();

                if (mySubform != null)
                    mySubform.SetText();
                return true;
            }
            if (keyData == Keys.Space)
            {
                if (mySubform.SetSelectionCell())
                    return true;
            }
            if (keyData == Keys.Prior)
            {
                ((CtrlDropDownListForm)Subform).HandlerSelectPageUp();
                return true;
            }
            if (keyData == Keys.PageDown)
            {
                ((CtrlDropDownListForm)Subform).HandlerSelectPageDown();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void CtrlDropdownHostList_EnabledChanged(object sender, EventArgs e)
        {
           
        }
    }
}
