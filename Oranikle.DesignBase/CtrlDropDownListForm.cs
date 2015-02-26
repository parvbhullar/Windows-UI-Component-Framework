using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
    public partial class CtrlDropDownListForm : CtrlDropDownForm
    {
        protected enum EnumLinkIsSelected
        {
            None,
            AddNew,
            Search
        }

        private delegate void EmptyDelegate();

        private struct GetListParams
        {

            public bool allowPicked;
            public object currentValue;
            public int item;

            public GetListParams(int item, object currentValue, bool allowPicked)
            {
                this.item = item;
                this.currentValue = currentValue;
                this.allowPicked = allowPicked;
            }

        }

        private const int HeightWithoutRows = 70;
        private const int MaxShownRows = 8;
        private const int MinShownRows = 1;
        private const int RowHeight = 22;
        private ObjectCollection objectList = new ObjectCollection();
       
        protected CtrlDropDownListForm.EnumLinkIsSelected linkIsSelected;

        public CtrlDropDownListForm()
        {
            if (!DesignMode)
                Visible = false;

            InitializeComponent();
            Util.RegisterAndTranslateContainer(this);
            ColorSchemeConstants.ApplyBlackAndWhiteTheme(ctrlListingTable);
            if (!DesignMode)
                ctrlListingTable.DataError += new DataGridViewDataErrorEventHandler(ctrlListingTable_DataError);
            objectList = new ObjectCollection();
            
        }

   

        #region Properties
        public ObjectCollection Items
        {
            get
            {
                if (!DesignMode)
                {
                    if (objectList == null)
                        objectList = new ObjectCollection();
                    return objectList;
                }
                return null;
            }
            set
            {
                objectList = value;
            }
        }

        public int SelectedIndex
        {
            get;
            set;
        }

        public bool ShowAddLink
        {
            get
            {
                return pnlTop.Visible;
            }
            set
            {
                pnlTop.Visible = value;
            }
        }

        public bool ShowEditControl
        {
            get
            {
                return pnlMiddel.Visible;
            }
            set
            {
                pnlMiddel.Visible = value;
            }
        }

        public bool ShowDeleteButton
        {
            get
            {
                return btnDelete.Visible;
            }
            set
            {
                btnDelete.Visible = value;
            }
        }

        public StyledButton Button1
        {
            get
            {
                return btnEdit;
            }
            set
            {
                btnEdit = value;
            }
        }

        public StyledButton Button2
        {
            get
            {
                return btnDelete;
            }
            set
            {
                btnDelete = value;
            }
        }


        public bool ShowGridHeader
        {
            get
            {
                //if (!DesignMode)
                return ctrlListingTable.ColumnHeadersVisible;
            }
            set
            {
                //if (!DesignMode)
                ctrlListingTable.ColumnHeadersVisible = value;
            }
        }

        public string HeaderTitle
        {
            get
            {
                if (ctrlListingTable.Columns[0] != null)
                { return ctrlListingTable.Columns[0].HeaderText; }
                return string.Empty;
            }
            set
            {
                if (ctrlListingTable.Columns[0] != null)
                { ctrlListingTable.Columns[0].HeaderText = value; }
            }
        }

        public bool IsSelectionList
        {
            get { return colSelect.Visible; }
            set {

                colSelect.Visible = value;
                if (value)
                {
                    lnkAddNew.Text = "< Unselect All .. >";
                    ShowAddLink = true;
                }
                else
                {
                    lnkAddNew.Text = "< Add New .. >";
                }
            }
        }
        public ObjectCollection selectedItems;
        public ObjectCollection SelectedItems
        {
            get {
                List<object> list = new List<object>();
                for (int i = 0; i < ctrlListingTable.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell selectedCell = (DataGridViewCheckBoxCell)ctrlListingTable.Rows[i].Cells["colSelect"];
                      if (bool.Parse(selectedCell.EditedFormattedValue.ToString()))
                      {
                          list.Add(ctrlListingTable.Rows[i].Tag);
                      }
                }
                if (selectedItems == null)
                    selectedItems = new ObjectCollection();
                selectedItems.Clear();
                selectedItems.AddRange(list.ToArray());
                return selectedItems;
            }
            set
            {
                selectedItems = value;
            }
        }

        public bool LinkIsSelected
        {
            get
            {
                return linkIsSelected != CtrlDropDownListForm.EnumLinkIsSelected.None;
            }
        }

        public DataGridView ListingTable
        {
            get
            {
                return ctrlListingTable;
            }
        }
        #endregion

        private void CtrlDropDownListForm_Load(object sender, EventArgs e)
        {
            
        }
        #region Events and Functions
        protected void _EntityForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            base.InitializeFromHost();
        }

        private void CtrlDropDownListForm_Disposed(object sender, EventArgs e)
        {
        }

        private void CtrlDropDownListForm_SavedWithNewObject(object sender, int newObjectId)
        {
            if (IsDisposed)
                return;
            if ((Host == null) || Host.IsDisposed || (Host.Subform != this))
                return;
            Host.Value = newObjectId;
        }

        private void ctrlListingTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }

        protected void ctrlListingTable_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                HandleClickCell(e.RowIndex, e.ColumnIndex);
                return;
            }
            //e.RowIndex;
        }

        private void ctrlListingTable_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        protected void DoEditSelectedRow()
        {
        }

        protected object DoGetDefaultValue()
        {
            return null;
        }

        protected object DoGetIdFromCacheObject(object o)
        {
            //return ((LocationQuantity)o).LocationId;
            return o;
        }

        protected object DoGetIdFromText(string p)
        {
            if (Host.Value != null)
                UpdateFromText("");
            if (Host.Value == null) return null;
            return  p;
        }

        protected string DoGetTextFromCacheObject(object o)
        {
            if (o == null) return string.Empty;
            return o.ToString();
        }

        public void DoHandleAddNew()
        {
            //if (pnlTop.Visible)
            {
                Host.OnAddNew_Clicked();
            }
        }

        protected void DoInitListingColumns()
        {
            ctrlListingTable.CellFormatting += new DataGridViewCellFormattingEventHandler(ctrlListingTable_CellFormatting);
        }

        protected void DoUpdateAddNewLinkUI(bool selected)
        {
            if (selected)
            {
                lnkAddNew.Font = new Font(lnkAddNew.Font, FontStyle.Bold);
                return;
            }
            lnkAddNew.Font = new Font(lnkAddNew.Font, FontStyle.Regular);
        }

        private void GetList(object param)
        {
            try
            {

            }
            catch
            {

            }
        }

        private int GetTotalShownRows()
        {
            return (ListingTable.Height - ListingTable.ColumnHeadersHeight) / ListingTable.RowTemplate.Height;
        }

        protected void HandleClickCell(int rowIndex, int columnIndex)
        {
            if ((ListingTable.SelectedRows.Count <= 0) || (rowIndex < 0) || (columnIndex < 0))
                return;
            if (SetSelectionCell())
            {
                return;
            }
            if (Host != null)
                Host.Value = DoGetIdFromCacheObject(ListingTable.SelectedRows[0].Tag);
            if (Host != null)
            {
                Host.Focus();
                SendKeys.Send("\t");
            }
        }

        public void HandlerAddNew()
        {
            if (IsDisposed || !IsHandleCreated)
                return;
            Host.ParentForm.Activate();
            Host.Dismissed = true;
            UpdateVisibility();
            base.BeginInvoke(new CtrlDropDownListForm.EmptyDelegate(DoHandleAddNew));
        }

        public void HandlerSelectCurrent()
        {
            if (Host == null)
                return;
            if (linkIsSelected == CtrlDropDownListForm.EnumLinkIsSelected.AddNew)
            {
                HandlerAddNew();
                return;
            }
            if ((Host != null) && (ListingTable != null) && (ListingTable.SelectedRows.Count > 0))
            {
               
                if (!IsSelectionList)
                {
                    Host.Value = DoGetIdFromCacheObject(ListingTable.SelectedRows[0].Tag);
                    Host.Text = DoGetTextFromCacheObject(ListingTable.SelectedRows[0].Tag);
                }
                return;
            }
            if (Host != null)
            {
                Host.Value = DoGetIdFromText(Host.Text);
                return;
            }
            Host.Value = DoGetDefaultValue();
        }

        public void HandlerSelectDown()
        {
            switch (linkIsSelected)
            {
                case CtrlDropDownListForm.EnumLinkIsSelected.AddNew:
                    if (ListingTable.Rows.Count == 0)
                    {
                        linkIsSelected = CtrlDropDownListForm.EnumLinkIsSelected.AddNew;
                    }
                    else
                    {
                        int i1 = 0;
                        SetSelectedRow(i1);
                        linkIsSelected = CtrlDropDownListForm.EnumLinkIsSelected.None;
                    }
                    break;

                case CtrlDropDownListForm.EnumLinkIsSelected.None:
                    int i2 = 0;
                    if (ListingTable.SelectedRows.Count > 0)
                        i2 = ListingTable.SelectedRows[0].Index + 1;
                    if (!SetSelectedRow(i2))
                        linkIsSelected = CtrlDropDownListForm.EnumLinkIsSelected.AddNew;
                    break;
            }
            UpdateLinkUI();
        }

        public void HandlerSelectNull()
        {
            if (Host == null)
                return;
            Host.Text = "";
            Host.Value = ((CtrlDropdownHostList)Host).NullValue;
            //Host.ParentForm.Activate();
        }

        public void HandlerSelectPageDown()
        {
            int i1 = GetTotalShownRows() - 1;
            for (int i2 = 0; i2 < i1; i2++)
            {
                HandlerSelectDown();
            }
        }

        public void HandlerSelectPageUp()
        {
            int i1 = GetTotalShownRows() - 1;
            for (int i2 = 0; i2 < i1; i2++)
            {
                HandlerSelectUp();
            }
        }

        public void HandlerSelectUp()
        {
            switch (linkIsSelected)
            {
                case CtrlDropDownListForm.EnumLinkIsSelected.AddNew:
                    if (ListingTable.Rows.Count == 0)
                    {
                        linkIsSelected = CtrlDropDownListForm.EnumLinkIsSelected.AddNew;
                    }
                    else
                    {
                        int i1 = ListingTable.Rows.Count - 1;
                        SetSelectedRow(i1);
                        linkIsSelected = CtrlDropDownListForm.EnumLinkIsSelected.None;
                    }
                    break;

                case CtrlDropDownListForm.EnumLinkIsSelected.None:
                    int i2 = ListingTable.Rows.Count - 1;
                    if (ListingTable.SelectedRows.Count > 0)
                        i2 = ListingTable.SelectedRows[0].Index - 1;
                    if (!SetSelectedRow(i2))
                        linkIsSelected = CtrlDropDownListForm.EnumLinkIsSelected.AddNew;
                    break;
            }
            UpdateLinkUI();
        }

        public bool HasFocus()
        {
            return Form.ActiveForm == this;
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            if (pnlMiddel.Visible)
            {
                if (IsDisposed || !IsHandleCreated)
                    return;
                Host.ParentForm.Activate();
                Host.Dismissed = true;
                UpdateVisibility();
                Host.OnEdit_Clicked();
                Host.OnValueChanged();
            }
        }
        void btnDelete_Click(object sender, EventArgs e)
        {
            if (pnlMiddel.Visible && btnDelete.Visible)
            {
                if (IsDisposed || !IsHandleCreated)
                    return;
                Host.ParentForm.Activate();
                Host.Dismissed = true;
                UpdateVisibility();
                Host.OnDelete_Clicked();
                Host.OnValueChanged();
            }
        }

        protected void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkIsSelected = EnumLinkIsSelected.AddNew;
            HandlerAddNew();
        }

        public void OnMouseWheelPublic(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        private void ReAdjustHeight()
        {
            int i = objectList == null ? 0 : objectList.Count;
            if (i < 1)
                i = 1;
            if (i > 10)
                i = 10;
            Height = 90 + (i * 22);

        }

        public void ResetDataSource()
        {
            //locationQuantityListBindingSource.DataSource = objectList;
            SetText();
            ctrlListingTable.Columns["colSelect"].ReadOnly = false;
            if (!DesignMode && (IsSelectionList && ctrlListingTable.Rows.Count == 0) || !IsSelectionList)
            {
                ctrlListingTable.Rows.Clear();
                if (objectList == null) return;
                int index = 0;
                foreach (object obj in objectList)
                {
                    ctrlListingTable.Rows.Add();
                    DataGridViewRow row = ctrlListingTable.Rows[ctrlListingTable.Rows.Count - 1];
                    row.Cells[1].Value = obj;
                    row.Tag = obj;
                    if (!IsSelectionList)
                    {
                        if (Host != null && Host.Value == null && this.SelectedIndex == index)
                            Host.Value = obj;
                    }
                    index++;
                }
            }
        }
        public void ResetDataSource(bool isForcefullly)
        {
            //locationQuantityListBindingSource.DataSource = objectList;
            SetText();
            ctrlListingTable.Columns["colSelect"].ReadOnly = false;
            if (!DesignMode && isForcefullly)
            {
                ctrlListingTable.Rows.Clear();
                if (objectList == null) return;
                int index = 0;
                foreach (object obj in objectList)
                {
                    ctrlListingTable.Rows.Add();
                    DataGridViewRow row = ctrlListingTable.Rows[ctrlListingTable.Rows.Count - 1];
                    row.Cells[1].Value = obj;
                    row.Tag = obj;
                    if (!IsSelectionList)
                    {
                        if (Host != null && Host.Value == null && this.SelectedIndex == index)
                            Host.Value = obj;
                    }
                    index++;
                }
            }
        }

        protected bool SetSelectedRow(int rowIndex)
        {
            if ((rowIndex < 0) || (rowIndex >= ListingTable.Rows.Count))
            {
                foreach (DataGridViewRow dataGridViewRow in ListingTable.SelectedRows)
                {
                    dataGridViewRow.Selected = false;
                }
                return false;
            }
            ListingTable.Rows[rowIndex].Selected = true;
            if (ListingTable.Rows[rowIndex].Cells["colSelect"].Visible == true)
                ListingTable.CurrentCell = ListingTable.Rows[rowIndex].Cells["colSelect"];
            ListingTable.FirstDisplayedScrollingRowIndex = rowIndex;
            if (Host != null && !IsSelectionList)
            {
                Host.Value = DoGetIdFromCacheObject(ListingTable.Rows[rowIndex].Tag);
                Host.SelectAllText();
            }
            return true;
        }

        private void splitContainer1_Panel2_Click(object sender, EventArgs e)
        {
            HandlerSelectNull();
            if (Host != null)
            {
                Host.Focus();
                SendKeys.Send("\t");
            }
        }

        private void splitContainer2_Panel1_Click(object sender, EventArgs e)
        {
            HandlerSelectNull();
            if (Host != null)
            {
                Host.Focus();
                SendKeys.Send("\t");
            }
        }

        protected void UpdateLinkUI()
        {
            DoUpdateAddNewLinkUI(linkIsSelected == CtrlDropDownListForm.EnumLinkIsSelected.AddNew);
        }

        //private void UpdateLocationQuantityItem(objectList newList)
        //{
        //    if ((Host == null) || IsDisposed || !IsHandleCreated)
        //        return;
        //    locations = newList;
        //    ResetDataSource();
        //    base.UpdateFromText(Host.Text);
        //    ReAdjustHeight();
        //}


        protected override int DoGetMinimumWidth()
        {
            return 100;
        }

        public override bool GetExpectedVisibility()
        {
            if (DesignMode)
                return false;
            if (base.GetExpectedVisibility())
                return true;
            return false;
        }

        protected override void InitializeFromHost()
        {
            if ((Host == null) || (ListingTable == null))
                return;
            bool flag = ((CtrlDropdownHostList)Host).AllowPicked;
            ListingTable.ClearSelection();
            object nullable = ((CtrlDropdownHostList)Host).Value;

            ListingTable.DefaultCellStyle.Font = Host.TextFont;

            ResetDataSource();
            ListingTable.ClearSelection();
            linkIsSelected = CtrlDropDownListForm.EnumLinkIsSelected.None;
            UpdateLinkUI();
            ReAdjustHeight();
            object obj1 = Host.Value;
            if (obj1 == null) return;
            foreach (DataGridViewRow dataGridViewRow1 in ListingTable.Rows)
            {
                if (DoGetIdFromCacheObject(dataGridViewRow1.Tag).Equals(obj1))
                {
                    dataGridViewRow1.Selected = true;
                    try
                    {
                        ListingTable.FirstDisplayedScrollingRowIndex = dataGridViewRow1.Index;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    return;
                }
            }
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            //if (!IsSelectionList)
            UpdateVisibility();
            SetText();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            TopMost = true;
            DoInitListingColumns();
            Disposed += new EventHandler(CtrlDropDownListForm_Disposed);
            
        }
       

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int i1 = e.Delta * SystemInformation.MouseWheelScrollLines / 240;
            if (i1 < 0)
            {
                i1 *= -1;
                for (int i2 = 0; i2 < i1; i2++)
                {
                    HandlerSelectDown();
                }
                return;
            }
            if (i1 > 0)
            {
                for (int i3 = 0; i3 < i1; i3++)
                {
                    HandlerSelectUp();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                HandlerSelectUp();
                return true;
            }
            if (keyData == Keys.Down)
            {
                HandlerSelectDown();
                return true;
            }
            if ((keyData == Keys.Tab) || (keyData == Keys.Enter))
            {
                HandlerSelectCurrent();
                if (linkIsSelected == CtrlDropDownListForm.EnumLinkIsSelected.AddNew)
                {
                    return true;
                }
                if (IsSelectionList)
                {
                    if (Host != null)
                        Host.Focus();
                    //if (keyData == Keys.Enter)
                    {
                        
                        SendKeys.Send("\t");
                        return true;
                    }
                    
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }

            if (keyData == Keys.Space)
            {
                SetSelectionCell();
            }
            if (keyData == Keys.Prior)
            {
                HandlerSelectPageUp();
                return true;
            }
            if (keyData == Keys.PageDown)
            {
                HandlerSelectPageDown();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public bool SetSelectionCell()
        {
            if (IsSelectionList)
            {
                ctrlListingTable.CurrentRow.Cells["colSelect"].Value = ctrlListingTable.CurrentRow.Cells["colSelect"].Value == null ? true : (!(bool)ctrlListingTable.CurrentRow.Cells["colSelect"].Value);
                //SetText();
                return true;
            }
            return false;
        }

        public void SetText()
        {
            if (Host == null)
                return;
            if (IsSelectionList)
            {
                if (SelectedItems.Count == 0)
                    Host.Text = "All";
                else
                    Host.Text = "Selected";
            }
        }

        public override void UpdateFromText(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                ListingTable.ClearSelection();
                linkIsSelected = CtrlDropDownListForm.EnumLinkIsSelected.None;
                if (Host != null)
                    Host.Value = null;
                return;
            }
            if (linkIsSelected == EnumLinkIsSelected.AddNew)
            {
                ResetDataSource();
            }
            for (int i = 0; i < ListingTable.RowCount; i++)
            {
                string s = DoGetTextFromCacheObject(ListingTable.Rows[i].Tag);
                if (s.StartsWith(text, true, null))
                {
                    ListingTable.ClearSelection();
                    ListingTable.Rows[i].Selected = true;
                    if (ListingTable.Rows[i].Cells["colSelect"].Visible == true)
                        ListingTable.CurrentCell = ListingTable.Rows[i].Cells["colSelect"];
                    try
                    {
                        ListingTable.FirstDisplayedScrollingRowIndex = i;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    linkIsSelected = CtrlDropDownListForm.EnumLinkIsSelected.None;
                    return;
                }
            }
            ListingTable.ClearSelection();
            linkIsSelected = CtrlDropDownListForm.EnumLinkIsSelected.None;
            if (Host != null)
            {
                Host.Value = null;
                //Host.Text = null;
            }
        }

        #endregion

        public void UncheckAll()
        {
            if (IsSelectionList)
            {
                foreach (DataGridViewRow row in ctrlListingTable.Rows)
                {
                    if (row.Cells.Count > 0 && row.Cells["colSelect"].Visible)
                    {
                        row.Cells["colSelect"].Value = false;
                    }
                }
                selectedItems = new ObjectCollection();
                SetText();
            }
        }



        internal void SetCheckedItem(int index)
        {
            if (IsSelectionList)
            {
                foreach (DataGridViewRow row in ctrlListingTable.Rows)
                {
                    if (index == row.Index)
                    {
                        if (row.Cells.Count > 0 && row.Cells["colSelect"].Visible)
                        {
                            row.Cells["colSelect"].Value = true;
                            if (!selectedItems.Contains(row.Tag))
                            {
                                selectedItems.Add(row.Tag);    
                            }
                        }
                    }
                }
               //try it
                SetText();
            }
        }
    }
}
