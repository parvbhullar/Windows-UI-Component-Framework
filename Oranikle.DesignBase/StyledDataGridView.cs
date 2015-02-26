using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using log4net.Repository.Hierarchy;
using Oranikle.Studio.Controls.Barcode;


namespace Oranikle.Studio.Controls
{
    public class StyledDataGridView: System.Windows.Forms.DataGridView, Oranikle.Studio.Controls.IEndEditToDataBinding, Oranikle.Studio.Controls.ICustomEnableControl, System.ComponentModel.ISupportInitialize
    {
        private static readonly log4net.ILog log;

        private Oranikle.Studio.Controls.IColumnSettingsManager _ColumnSettingsManager;
        private string _ColumnSettingsTableName;
        private bool _CustomActive;
        private bool _CustomEnabled;
        private bool _CustomReadOnly;
        private bool _EditOnSingleClick;
        private bool _HasCustomContextMenuItems;
        private bool _LossSelectionOnUnfocus;

        private System.Windows.Forms.ContextMenuStrip columnHeaderContextMenuStrip;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private int HeightOfRealAndNewRows;
        private bool rememberedAllowAddRow;
        private System.Collections.Generic.List<int> RowDividerIndex;
        private bool showGridBorder;

        private static Oranikle.Studio.Controls.IColumnSettingsManager _DefaultColumnSettingsManager;

        public event System.EventHandler BarcodeScanned;
        public event System.EventHandler FocusedInNullRow;
        public event System.EventHandler OnAddRow;
        public event System.EventHandler CellValueChanging;
        delegate void SetColumnIndex(int i);

        public Oranikle.Studio.Controls.IColumnSettingsManager ColumnSettingsManager
        {
            get
            {
                return _ColumnSettingsManager;
            }
            set
            {
                _ColumnSettingsManager = value;
            }
        }

        public string ColumnSettingsTableName
        {
            get
            {
                return _ColumnSettingsTableName;
            }
            set
            {
                _ColumnSettingsTableName = value;
            }
        }

        public bool CustomActive
        {
            get
            {
                return _CustomActive;
            }
            set
            {
                _CustomActive = value;
                RefreshCustomEnabled();
            }
        }

        public bool CustomEnabled
        {
            get
            {
                return _CustomEnabled;
            }
        }

        public bool CustomReadOnly
        {
            get
            {
                return _CustomReadOnly;
            }
            set
            {
                _CustomReadOnly = value;
                RefreshCustomEnabled();
            }
        }
        public string DefaultColumnName
        {
            get;
            set;
        }
        public bool AllowToAddRows { get; set; }

        public int MinimumRowCount { get; set; }

        public bool ShowGridFooter { get; set; }

        [DefaultValue(true)]
        public bool ConvertEnterToTab { get; set; }


        [System.ComponentModel.DefaultValue(true)]
        public bool EditOnSingleClick
        {
            get
            {
                return _EditOnSingleClick;
            }
            set
            {
                _EditOnSingleClick = value;
            }
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.Browsable(false)]
        public new bool Enabled
        {
            get
            {
                return CustomEnabled;
            }
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.Browsable(false)]
        public int WorkingCellCount
        {
            get
            {
                int count = 0;
                if (this.CurrentRow != null)
                {
                    foreach (DataGridViewCell col in this.CurrentRow.Cells)
                    {
                        if (col.Visible && !col.ReadOnly)
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }

        public bool LossSelectionOnUnfocus
        {
            get
            {
                return _LossSelectionOnUnfocus;
            }
            set
            {
                _LossSelectionOnUnfocus = value;
            }
        }

        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.Browsable(false)]
        public new bool ReadOnly
        {
            get
            {
                return CustomReadOnly;
            }
            set
            {
                CustomReadOnly = value;
            }
        }

        public bool ShowGridBorder
        {
            get
            {
                return showGridBorder;
            }
            set
            {
                showGridBorder = value;
            }
        }

        public static Oranikle.Studio.Controls.IColumnSettingsManager DefaultColumnSettingsManager
        {
            get
            {
                return Oranikle.Studio.Controls.StyledDataGridView._DefaultColumnSettingsManager;
            }
            set
            {
                Oranikle.Studio.Controls.StyledDataGridView._DefaultColumnSettingsManager = value;
            }
        }

        public StyledDataGridView()
        {
            //if (!DesignMode)
            LP.Validate();
           

            showGridBorder = true;
            HeightOfRealAndNewRows = -1;
            RowDividerIndex = new System.Collections.Generic.List<int>();
            _CustomEnabled = true;
            _CustomActive = true;
            _EditOnSingleClick = true;
            SuspendLayout();
            AllowUserToResizeRows = false;
            RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(228, 241, 249);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            AutoGenerateColumns = false;
            BackgroundColor = System.Drawing.Color.FromArgb(200, 226, 243);
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(136, 198, 237);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            ColumnHeadersHeight = 35;
            ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(136, 198, 237);
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 136);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            EnableHeadersVisualStyles = true;
            RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            RowHeadersWidth = 25;
            TabIndex = 4;
            GridColor = System.Drawing.SystemColors.Window;
            DoubleBuffered = true;
            CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(StyledDataGridView_CellDoubleClick);
            CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(StyledDataGridView_CellClick);
            CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(StyledDataGridView_CellEnter);
            this.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(StyledDataGridView_EditingControlShowing);

           
            ResumeLayout();
           
        }

        void listener_BarcodeScanned(object sender, EventArgs e)
        {
            this.BarcodeScanned.Invoke(sender, e);
        }

      

        void StyledDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                TextBox tb = (TextBox)e.Control;
                tb.TextChanged += new EventHandler(tb_TextChanged);
                //CellValueChanging
            }
        }

        void tb_TextChanged(object sender, EventArgs e)
        {
            System.EventHandler eventHandler = CellValueChanging;
            if (eventHandler != null)
            {
                eventHandler(this, null);
            }
        }


        private void StyledDataGridView_RowProcesser()
        {
            if (AllowUserToDeleteRows && this.CurrentRow != null && this.DefaultColumnName != null && this.DefaultColumnName != string.Empty && this.CurrentRow.Cells[this.DefaultColumnName] != null && this.CurrentCell.ColumnIndex >= this.CurrentRow.Cells[this.DefaultColumnName].ColumnIndex && this.CurrentRow.Cells[this.DefaultColumnName].EditedFormattedValue == null)
            {
                if (this.Rows.Count > 1)
                { 
                    List<int> list = new List<int>();
                    list.Add(this.CurrentRow.Index);
                    DeleteRows(list);
                }
                System.EventHandler eventHandler = FocusedInNullRow;
                if (eventHandler != null)
                {
                    eventHandler(this, null);
                }
            }
            else if (AllowToAddRows && this.DefaultColumnName != null && this.DefaultColumnName != string.Empty && this.CurrentRow != null && this.CurrentRow.Cells[this.DefaultColumnName].Value != null && this.Rows.Count - 1 < this.CurrentRow.Index + 1 && !IsWorkingCellExists(this.CurrentCell.ColumnIndex))
            {
                this.Rows.Add();
            }
           
        }

        private void RemoveSelection()
        {
            if (this.CurrentRow != null)
            {
                if (!this.ContainsFocus && this.CurrentRow.Cells[GetDefaultColumnIndex].Value == null)
                    this.ClearSelection();
            }
        }

        public bool IsWorkingCellExists(int currentIndex)
        {
            if (this.CurrentRow != null)
            {
                foreach (DataGridViewCell col in this.CurrentRow.Cells)
                {
                    if (col.ColumnIndex > currentIndex)
                        if (col.Visible && !col.ReadOnly)
                        {
                            return true;
                        }
                }
            }
            return false;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
           
        }

        void StyledDataGridView_CellEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.ContainsFocus && EditOnSingleClick && (e.RowIndex == this.CurrentCell.RowIndex) && (e.ColumnIndex == this.CurrentCell.ColumnIndex) && Enabled && !ReadOnly)
                    ((Oranikle.Studio.Controls.StyledDataGridView)sender).BeginEdit(true);

                if (this.ContainsFocus && !this.ReadOnly && this.UnFocusOnReadOnlyCell && this.CurrentCell != null && this.CurrentCell.ReadOnly)
                {
                    // SendKeys.Send("\t");
                    NextCell();
                }
                
            }
            catch (Exception ex)
            {
                log.Error("Error In DGV", ex);
            }
         
        }

        [DefaultValue(false)]
        public bool UnFocusOnReadOnlyCell
        { get; set; }

        public int GetDefaultColumnIndex
        {
            get
            {
                try
                {
                    return this.Columns[this.DefaultColumnName].Index;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
        public void SetCurrentRow(int rowIndex)
        {
            try
            {
                int cellIndex = GetValidCellIndex();
                if (this.Rows.Count - 1 >= rowIndex && cellIndex > -1)
                {
                    this.Rows[rowIndex].Selected = true;
                    this.CurrentCell = this.Rows[rowIndex].Cells[cellIndex];
                }
            }
            catch (Exception ex)
            { 
                
            }
        }

        private int GetValidCellIndex()
        {
            int cIndex = -1;

            foreach (DataGridViewColumn column in this.Columns)
            {
                if (column.Visible)
                {
                    return column.Index;
                }
            }
            return cIndex;

        }
        public void RemoveUncommitedRows()
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in this.Rows)
            {
                if (!row.IsNewRow && !row.Equals(this.CurrentRow))
                    rows.Add(row);
            }
            this.AllowUserToAddRows = false;
            this.Rows.Clear();
            foreach (DataGridViewRow row in rows)
            {
                if (!row.IsNewRow)
                    this.Rows.Add(row);
            }
        }
        static StyledDataGridView()
        {
            Oranikle.Studio.Controls.StyledDataGridView.log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        public void AddColumnHeaderContextMenuStripItem(System.Windows.Forms.ToolStripItem item)
        {
            columnHeaderContextMenuStrip.Items.Add(item);
            foreach (System.Windows.Forms.DataGridViewColumn dataGridViewColumn in Columns)
            {
                dataGridViewColumn.HeaderCell.ContextMenuStrip = columnHeaderContextMenuStrip;
            }
        }
        public void AddContextMenuStripItem(System.Windows.Forms.ToolStripItem item)
        {
            _HasCustomContextMenuItems = true;
            ContextMenuStrip = contextMenuStrip1;
            contextMenuStrip1.Items.Add(item);
        }
        public void AddRowDivider(int rowIndex)
        {
            RowDividerIndex.Add(rowIndex);
            Invalidate();
        }
        private void StyledDataGridView_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            //if (EditOnSingleClick && (e.RowIndex >= 0) && (e.ColumnIndex >= 0) && Enabled && !ReadOnly)
            if (EditOnSingleClick && (e.RowIndex >= 0) && (e.ColumnIndex >= 0) && Enabled && !ReadOnly)
                ((Oranikle.Studio.Controls.StyledDataGridView)sender).BeginEdit(true);
        }
       

        private void StyledDataGridView_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0) && Enabled && !ReadOnly)
                ((Oranikle.Studio.Controls.StyledDataGridView)sender).BeginEdit(true);
        }

        private void StyledDataGridView_ColumnWidthChanged(object sender, System.Windows.Forms.DataGridViewColumnEventArgs e)
        {
            if (ColumnSettingsManager != null)
                ColumnSettingsManager.ColumnWidthChanged(this, e);
        }

        void System.ComponentModel.ISupportInitialize.BeginInit()
        {
        }

        private void ClearLatestEditingControl()
        {
            System.Type type = typeof(System.Windows.Forms.DataGridView);
            System.Reflection.FieldInfo fieldInfo = type.GetField("latestEditingControl", System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (fieldInfo != null)
                fieldInfo.SetValue(this, null);
        }

        private void DeleteRows(System.Collections.Generic.List<int> selectedIndex)
        {
            selectedIndex.Sort();
            for (int i = selectedIndex.Count - 1; i >= 0; i--)
            {
                Oranikle.Studio.Controls.IMsysBusinessBase iarchonBusinessBase = Rows[selectedIndex[i]].DataBoundItem as Oranikle.Studio.Controls.IMsysBusinessBase;
                if (!Rows[selectedIndex[i]].IsNewRow && ((iarchonBusinessBase == null) || iarchonBusinessBase.CanDeleteObject()))
                {
                    Oranikle.Studio.Controls.ICustomDeleteFromDGV icustomDeleteFromDGV = iarchonBusinessBase as Oranikle.Studio.Controls.ICustomDeleteFromDGV;
                    if ((icustomDeleteFromDGV == null) || icustomDeleteFromDGV.HandleDeleteFromDGV())
                    {
                        Rows.RemoveAt(selectedIndex[i]);
                        
                    }
                }
            }
            AddMinimumRows();
        }

        private void AddMinimumRows()
        {
            if (AllowToAddRows)
            {
                for (int j = 0; j < MinimumRowCount; j++)
                {
                    this.Rows.Add();
                }
            }
        }

        private void DeleteSelectedRows()
        {
            if (SelectedRows.Count == 0)
            {
                if (CurrentCell == null)
                    return;
                System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
                int i = CurrentCell.RowIndex;
                if ((CurrentCell.RowIndex >= 0) && (CurrentCell.RowIndex < Rows.Count) && !Rows[i].ReadOnly)
                    list.Add(CurrentCell.RowIndex);
                DeleteRows(list);
                return;
            }
            System.Collections.Generic.List<int> list1 = new System.Collections.Generic.List<int>();
            foreach (System.Windows.Forms.DataGridViewRow dataGridViewRow in SelectedRows)
            {
                if (!dataGridViewRow.ReadOnly)
                    list1.Add(dataGridViewRow.Index);
            }
            DeleteRows(list1);
        }

        private void deleteSelectedRowsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            DeleteSelectedRows();
        }

        public void EndEditToDataBinding(System.Windows.Forms.ContainerControl mainControl)
        {
            if (IsCurrentCellInEditMode)
            {
                EndEdit();
                if (IsCurrentCellInEditMode)
                    CancelEdit();
                mainControl.ActiveControl = null;
            }
        }

        void System.ComponentModel.ISupportInitialize.EndInit()
        {
            System.Windows.Forms.ToolStripMenuItem toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem.Name = "deleteSelectedRowsToolStripMenuItem";
            toolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            toolStripMenuItem.Text = "Delete Selected Rows";
            toolStripMenuItem.Click += new System.EventHandler(deleteSelectedRowsToolStripMenuItem_Click);
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            contextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            System.Windows.Forms.ToolStripItem[] toolStripItemArr = new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem };
           // contextMenuStrip1.Items.AddRange(toolStripItemArr);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            contextMenuStrip1.Size = new System.Drawing.Size(179, 48);
            columnHeaderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            columnHeaderContextMenuStrip.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
            columnHeaderContextMenuStrip.Name = "columnHeaderContextMenuStrip";
            columnHeaderContextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            columnHeaderContextMenuStrip.Size = new System.Drawing.Size(174, 32);
            if (AllowUserToDeleteRows || _HasCustomContextMenuItems)
                ContextMenuStrip = contextMenuStrip1;
            else
                ContextMenuStrip = null;
            rememberedAllowAddRow = AllowUserToAddRows;
            RefreshCustomEnabled();
            if (ColumnSettingsManager == null)
                ColumnSettingsManager = Oranikle.Studio.Controls.StyledDataGridView.DefaultColumnSettingsManager;
            if (ColumnSettingsManager != null)
                ColumnSettingsManager.DgvEndInit(this);
            ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(StyledDataGridView_ColumnWidthChanged);
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            SuspendLayout();
            AccessibleName = "";
            this.CellValueChanged += new DataGridViewCellEventHandler(StyledDataGridView_CellValueChanged);
            // EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            ResumeLayout(false);
        }

        void StyledDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private bool MoveToNextCell()
        {
            if ((CurrentCell != null) && (CurrentCell.ColumnIndex == (ColumnCount - 1)) && (CurrentCell.RowIndex == (RowCount - 1)))
            {
                EndEdit();
                if (AllowUserToAddRows)
                {
                    ((System.Windows.Forms.BindingSource)DataSource).AddNew();
                    CurrentCell = Rows[RowCount - 1].Cells[0];
                }
                return true;
            }
            if ((CurrentCell != null) && (CurrentCell.ColumnIndex == (ColumnCount - 1)) && ((CurrentCell.RowIndex + 1) != NewRowIndex) && (Rows[CurrentCell.RowIndex + 1].Cells.Count > 1))
            {
                CurrentCell = Rows[CurrentCell.RowIndex + 1].Cells[1];
                return true;
            }
            return false;
        }

        public bool NextCell()
        {
            try
            {
                if ((CurrentCell != null) && (Rows[CurrentCell.RowIndex].Cells.Count > CurrentCell.ColumnIndex + 1))
                {
                    //EndEdit();
                    int nextindex = Math.Min(this.Columns.Count - 1, this.CurrentCell.ColumnIndex + 1);
                    SetColumnIndex method = new SetColumnIndex(MoveMethod);
                    this.BeginInvoke(method, nextindex);
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error", ex);
            }
            return false;
        }

        private void MoveMethod(int columnIndex)
        {
            try
            {
                if (columnIndex > this.ColumnCount - 1)
                {
                    SetCurrentRow(this.CurrentRow.Index + 1);
                    return;
                }
                if (columnIndex < CurrentCell.ColumnIndex)
                    columnIndex = CurrentCell.ColumnIndex;

                if (this.CurrentRow.Cells[columnIndex].Visible)
                    if (this.UnFocusOnReadOnlyCell && !this.CurrentRow.Cells[columnIndex].ReadOnly)
                        this.CurrentCell = this.CurrentRow.Cells[columnIndex];
                    else
                        MoveMethod(columnIndex + 1);
                else
                    MoveMethod(columnIndex + 1);
            }
            catch (Exception ex)
            {
                log.Error("Error in DGV", ex);
            }
            //this.BeginEdit(true);
        }

        public new bool ProcessRightKey(System.Windows.Forms.Keys keyData)
        {
            System.Windows.Forms.Keys keys = (System.Windows.Forms.Keys)(keyData & System.Windows.Forms.Keys.KeyCode);
            if ((keys == System.Windows.Forms.Keys.Enter) && !MoveToNextCell())
                return base.ProcessRightKey(keyData);
            return base.ProcessRightKey(keyData);
        }

        public void RaiseBarcodeScanned(object sender, System.EventArgs e)
        {
            System.EventHandler eventHandler = BarcodeScanned;
            if (eventHandler != null)
                eventHandler(sender, e);
        }

        private void RefreshCustomEnabled()
        {
            bool flag = !CustomReadOnly && CustomActive;
            if (_CustomEnabled != flag)
            {
                _CustomEnabled = flag;
                if (flag)
                {
                    base.ReadOnly = false;
                    if (rememberedAllowAddRow)
                        AllowUserToAddRows = true;
                    StandardTab = false;
                }
                else
                {
                    base.ReadOnly = true;
                    AllowUserToAddRows = false;
                }
                StandardTab = !flag;
            }
        }

        public void RemoveRowDivider(int rowIndex)
        {
            RowDividerIndex.Remove(rowIndex);
            Invalidate();
        }

        public void ScrollToIndex(int index)
        {
            if (Rows.Count == 0)
                return;
            if (index < 0)
                index = 0;
            else if (index >= Rows.Count)
                index = Rows.Count - 1;
            int i = -1;
            foreach (System.Windows.Forms.DataGridViewCell dataGridViewCell in Rows[index].Cells)
            {
                if (dataGridViewCell.Visible)
                    i = dataGridViewCell.ColumnIndex;
            }
            if (i >= 0)
            {
                CurrentCell = null;
                CurrentCell = Rows[index].Cells[i];
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnAllowUserToDeleteRowsChanged(System.EventArgs e)
        {
            base.OnAllowUserToDeleteRowsChanged(e);
            if (AllowUserToDeleteRows || _HasCustomContextMenuItems)
            {
                ContextMenuStrip = contextMenuStrip1;
                return;
            }
            ContextMenuStrip = null;
        }

        protected override void OnCellBeginEdit(System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            base.OnCellBeginEdit(e);
            ClearLatestEditingControl();

        }

        protected override void OnCellFormatting(System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            base.OnCellFormatting(e);
            if ((e.CellStyle.FormatProvider is System.ICustomFormatter))
            {
                System.ICustomFormatter icustomFormatter = e.CellStyle.FormatProvider.GetFormat(typeof(System.ICustomFormatter)) as System.ICustomFormatter;
                if (icustomFormatter != null)
                {
                    e.Value = icustomFormatter.Format(e.CellStyle.Format, e.Value, e.CellStyle.FormatProvider);
                    e.FormattingApplied = true;
                }
            }
        }

        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {

            
            base.OnKeyPress(e);
        }     

        protected override void OnCellParsing(System.Windows.Forms.DataGridViewCellParsingEventArgs e)
        {
            int i = e.ColumnIndex;
            if ((i >= 0) && (i < Columns.Count) && (Columns[i] is Oranikle.Studio.Controls.ICustomDGVParser))
            {
                Oranikle.Studio.Controls.ICustomDGVParser icustomDGVParser = Columns[i] as Oranikle.Studio.Controls.ICustomDGVParser;
                icustomDGVParser.CustomDGVParse(this, e);
                return;
            }
            base.OnCellParsing(e);
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
        {
            base.OnDataError(false, e);
        }

        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {
                if (!AllowUserToDeleteRows)
                    return;
                DeleteSelectedRows();
                return;
            }
            base.OnKeyDown(e);
            
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            if (_LossSelectionOnUnfocus)
            {
                System.Console.Out.Write("DGV lost focus");
                ClearSelection();
            }
        }

        
        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if ((e.Y > HeightOfRealAndNewRows) && (HeightOfRealAndNewRows > 0))
                EndEdit();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            
            System.Drawing.Color color1;
            System.Drawing.Rectangle rectangle2, rectangle3;

            try
            {
                base.OnPaint(e);
            }
            catch (System.NullReferenceException)
            {
                //goto label_1;
                return;
            }
            System.Collections.Generic.List<int>.Enumerator enumerator = RowDividerIndex.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    int i1 = enumerator.Current;
                    System.Drawing.Rectangle rectangle1 = GetRowDisplayRectangle(i1, true);
                    if (!rectangle1.IsEmpty)
                    {
                        int i2 = 0;
                        int i3 = Width - 1;
                        int i4 = rectangle1.Bottom;
                        e.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.DarkGray), new System.Drawing.Point(i2, i4), new System.Drawing.Point(i3, i4));
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
            }
            int i5 = Rows.Count;
            if (i5 == 0)
            {
                if (RowHeadersVisible)
                    rectangle2 = new System.Drawing.Rectangle(0, ColumnHeadersHeight - RowTemplate.Height, Width, RowTemplate.Height);
                else
                    rectangle2 = new System.Drawing.Rectangle(1, ColumnHeadersHeight - RowTemplate.Height, Width - 1, RowTemplate.Height);
            }
            else
            {
                rectangle2 = GetRowDisplayRectangle(i5 - 1, true);
            }
            if (rectangle2.IsEmpty)
            {
                return;
            }
            HeightOfRealAndNewRows = rectangle2.Y + rectangle2.Height;
            int i6 = rectangle2.Height;
            rectangle3 = new System.Drawing.Rectangle();
            rectangle3.X = rectangle2.X;
            rectangle3.Y = rectangle2.Y;
            rectangle3.Width = rectangle2.Width;
            rectangle3.Height = rectangle2.Height;
            while (rectangle3.Y < Height)
            {
                bool flag = false;
                rectangle3.X = rectangle2.X;
                rectangle3.Width = rectangle2.Width;
                rectangle3.Y += i6;
                i5++;
                if (RowHeadersVisible)
                {
                    System.Drawing.Rectangle rectangle4 = new System.Drawing.Rectangle(rectangle2.X, rectangle3.Y, RowHeadersWidth - 1, i6 - 1);
                    e.Graphics.FillRectangle(new System.Drawing.SolidBrush(RowHeadersDefaultCellStyle.BackColor), rectangle4);
                    e.Graphics.DrawLine(new System.Drawing.Pen(GridColor), rectangle4.X, rectangle4.Y, rectangle4.X, rectangle4.Y + rectangle4.Height);
                    e.Graphics.DrawLine(new System.Drawing.Pen(GridColor), rectangle4.X, rectangle4.Y + rectangle4.Height, rectangle4.X + rectangle4.Width, rectangle3.Y + rectangle4.Height);
                    e.Graphics.DrawLine(new System.Drawing.Pen(GridColor), rectangle4.X + rectangle4.Width, rectangle4.Y, rectangle4.X + rectangle4.Width, rectangle4.Y + rectangle4.Height);
                    flag = true;
                    rectangle3.X += RowHeadersWidth;
                }
                if ((i5 % 2) == 0)
                    color1 = AlternatingRowsDefaultCellStyle.BackColor;
                else if (RowsDefaultCellStyle.BackColor != System.Drawing.Color.Empty)
                    color1 = RowsDefaultCellStyle.BackColor;
                else
                    color1 = DefaultCellStyle.BackColor;
                foreach (System.Windows.Forms.DataGridViewColumn dataGridViewColumn in Columns)
                {
                    if (dataGridViewColumn.Visible)
                    {
                        rectangle3.Width = dataGridViewColumn.Width;
                        e.Graphics.FillRectangle(new System.Drawing.SolidBrush(color1), rectangle3);
                        e.Graphics.DrawLine(new System.Drawing.Pen(GridColor), rectangle3.X, rectangle3.Y + i6 - 1, rectangle3.X + Width - 1, rectangle3.Y + i6 - 1);
                        e.Graphics.DrawLine(new System.Drawing.Pen(GridColor), rectangle3.X + rectangle3.Width - 1, rectangle3.Y, rectangle3.X + rectangle3.Width - 1, rectangle3.Y + i6 - 1);
                        if (!flag)
                        {
                            e.Graphics.DrawLine(new System.Drawing.Pen(GridColor), rectangle3.X - 1, rectangle3.Y, rectangle3.X - 1, rectangle3.Y + i6 - 1);
                            flag = true;
                        }
                        //if (ShowGridFooter)
                        //{
                        //    System.Drawing.Rectangle rectangleFooter = new System.Drawing.Rectangle(rectangle3.X, Height - rectangle3.Height, rectangle3.Width, i6 - 1);
                        //    e.Graphics.DrawLine(new System.Drawing.Pen(GridColor), rectangleFooter.X - 1, rectangleFooter.Y, rectangleFooter.X - 1, rectangleFooter.Y + i6 - 1);
                        //    e.Graphics.FillRectangle(new System.Drawing.SolidBrush(AlternatingRowsDefaultCellStyle.BackColor), rectangleFooter);
                        //    //Height = Height - rectangle3.Height;
                        //    StringFormat format = new StringFormat();
                        //    format.Alignment = TextHelper.TranslateAligment(TextHelper.TranslateGridColumnAligment(dataGridViewColumn.DefaultCellStyle.Alignment));
                        //    format.LineAlignment = TextHelper.TranslateAligment(TextHelper.TranslateGridColumnAligment(DefaultCellStyle.Alignment));
                        //    e.Graphics.DrawString("Output", this.DefaultCellStyle.Font, new System.Drawing.SolidBrush(this.DefaultCellStyle.ForeColor),
                        //        rectangleFooter,
                        //        format);
                        //}

                        rectangle3.X += dataGridViewColumn.Width;
                    }
                }
            }
            if (ShowGridBorder)
                e.Graphics.DrawRectangle(new System.Drawing.Pen(GridColor), 0, 0, Width - 1, Height - 1);
            if ((!Enabled || !CustomEnabled) && !CustomActive)
            {
                System.Drawing.Color color2 = System.Drawing.SystemColors.Control;
                System.Drawing.Color color3 = System.Drawing.SystemColors.Control;
                System.Drawing.Color color4 = System.Drawing.SystemColors.Control;
                e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(140, color2.R, color3.G, color4.B)), new System.Drawing.Rectangle(0, 0, Width, Height));
            }
            
        }

        

        protected override void OnRowsAdded(System.Windows.Forms.DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);
            if ((e.RowIndex >= 0) && (Rows[e.RowIndex] != null))
                Rows[e.RowIndex].MinimumHeight = 22;

            //
            System.EventHandler eventHandler = OnAddRow ;
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }

        protected override void OnScroll(System.Windows.Forms.ScrollEventArgs e)
        {
            base.OnScroll(e);
            System.Reflection.MethodInfo methodInfo = typeof(System.Windows.Forms.DataGridView).GetMethod("AdjustShrinkingRows", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            try
            {
                object[] objArr = new object[] {
                                                 11, 
                                                 1, 
                                                 1 };
                methodInfo.Invoke(this, objArr);
            }
            catch (System.Exception e1)
            {
                Oranikle.Studio.Controls.StyledDataGridView.log.Debug(e1);
            }
        }

        protected override void OnSelectionChanged(System.EventArgs e)
        {
            base.OnSelectionChanged(e);
            if (_LossSelectionOnUnfocus && !Focused)
                ClearSelection();
        }

        protected override void OnUserDeletingRow(System.Windows.Forms.DataGridViewRowCancelEventArgs e)
        {
            base.OnUserDeletingRow(e);
            //Oranikle.Studio.Controls.IMsysBusinessBase iarchonBusinessBase = e.Row.DataBoundItem as Oranikle.Studio.Controls.IMsysBusinessBase;
            //if ((iarchonBusinessBase != null) && !iarchonBusinessBase.CanDeleteObject())
            //    e.Cancel = true;
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
           
            if (keyData == System.Windows.Forms.Keys.Escape)
            {
                System.ComponentModel.ICancelAddNew icancelAddNew = DataSource as System.ComponentModel.ICancelAddNew;
                System.ComponentModel.IBindingList ibindingList = DataSource as System.ComponentModel.IBindingList;
                if ((ibindingList != null) && (icancelAddNew != null) && (CurrentCell != null) && (ibindingList.Count > CurrentCell.RowIndex))
                {
                    int i = ibindingList.Count;
                    icancelAddNew.CancelNew(CurrentCell.RowIndex);
                    if (ibindingList.Count != i)
                        return true;
                }
            }
            if (keyData == System.Windows.Forms.Keys.Enter && ConvertEnterToTab)
            {
                this.StyledDataGridView_RowProcesser();
                return ProcessTabKey(System.Windows.Forms.Keys.Tab);
            }
            if (keyData == System.Windows.Forms.Keys.Tab)
            {
                    this.StyledDataGridView_RowProcesser();
            }
            if (keyData == Keys.Up || keyData == Keys.Down)
            {
                if (this.Rows.Count > 0 && this.SelectedRows.Count > 0 && this.SelectedRows[0].Cells.Count > 0 && this.SelectedRows[0].Cells[0].Visible == true)
                    this.CurrentCell = this.SelectedRows[0].Cells[0];
            }
           
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override bool ProcessDataGridViewKey(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                return true;
                //return ProcessTabKey(System.Windows.Forms.Keys.Tab);
            }
            return base.ProcessDataGridViewKey(e);
        }

        protected override bool ProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            System.Windows.Forms.Keys keys = (System.Windows.Forms.Keys)(keyData & System.Windows.Forms.Keys.KeyCode);
            if (keys == System.Windows.Forms.Keys.Enter)
                return ProcessTabKey(System.Windows.Forms.Keys.Tab);
            return base.ProcessDialogKey(keyData);
        }

        public override void Sort(System.Windows.Forms.DataGridViewColumn dataGridViewColumn, System.ComponentModel.ListSortDirection direction)
        {
            base.Sort(dataGridViewColumn, direction);
        }

        public override void Sort(System.Collections.IComparer comparer)
        {
            base.Sort(comparer);
        }

    }
}
