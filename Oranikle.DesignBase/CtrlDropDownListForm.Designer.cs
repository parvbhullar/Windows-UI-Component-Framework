using System.Drawing;
using System.Windows.Forms;
namespace Oranikle.Studio.Controls
{
    partial class CtrlDropDownListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ctrlListingTable = new Oranikle.Studio.Controls.StyledDataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new Oranikle.Studio.Controls.StyledButton();
            this.btnEdit = new Oranikle.Studio.Controls.StyledButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlFormResizer1 = new Oranikle.Studio.Controls.CtrlFormResizer();
            this.pnlBottom = new Oranikle.Studio.Controls.StyledPanel();
            this.pnlMiddel = new Oranikle.Studio.Controls.StyledPanel();
            this.pnlTop = new Oranikle.Studio.Controls.StyledPanel();
            this.lnkAddNew = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ctrlListingTable)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlMiddel.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlListingTable
            // 
            this.ctrlListingTable.AllowToAddRows = false;
            this.ctrlListingTable.AllowUserToAddRows = false;
            this.ctrlListingTable.AllowUserToDeleteRows = false;
            this.ctrlListingTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.ctrlListingTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ctrlListingTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.ctrlListingTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(226)))), ((int)(((byte)(243)))));
            this.ctrlListingTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctrlListingTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.ctrlListingTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(198)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ctrlListingTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ctrlListingTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ctrlListingTable.ColumnHeadersVisible = false;
            this.ctrlListingTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.nameDataGridViewTextBoxColumn});
            this.ctrlListingTable.ColumnSettingsManager = null;
            this.ctrlListingTable.ColumnSettingsTableName = null;
            this.ctrlListingTable.ConvertEnterToTab = false;
            this.ctrlListingTable.CustomActive = true;
            this.ctrlListingTable.CustomReadOnly = true;
            this.ctrlListingTable.DefaultColumnName = null;
            this.ctrlListingTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlListingTable.GridColor = System.Drawing.SystemColors.Window;
            this.ctrlListingTable.Location = new System.Drawing.Point(0, 25);
            this.ctrlListingTable.LossSelectionOnUnfocus = false;
            this.ctrlListingTable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlListingTable.MinimumRowCount = 0;
            this.ctrlListingTable.MultiSelect = false;
            this.ctrlListingTable.Name = "ctrlListingTable";
            this.ctrlListingTable.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(198)))), ((int)(((byte)(237)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ctrlListingTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ctrlListingTable.RowHeadersVisible = false;
            this.ctrlListingTable.RowHeadersWidth = 25;
            this.ctrlListingTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ctrlListingTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ctrlListingTable.ShowGridBorder = false;
            this.ctrlListingTable.ShowGridFooter = false;
            this.ctrlListingTable.Size = new System.Drawing.Size(245, 222);
            this.ctrlListingTable.StandardTab = true;
            this.ctrlListingTable.TabIndex = 4;
            this.ctrlListingTable.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ctrlListingTable_CellMouseClick);
            // 
            // colSelect
            // 
            this.colSelect.Frozen = true;
            this.colSelect.HeaderText = "Select";
            this.colSelect.MinimumWidth = 30;
            this.colSelect.Name = "colSelect";
            this.colSelect.ReadOnly = true;
            this.colSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colSelect.Visible = false;
            this.colSelect.Width = 30;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnDelete.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnDelete.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnDelete.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Arial", 9F);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(58, 5);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.OverriddenSize = null;
            this.btnDelete.Size = new System.Drawing.Size(49, 21);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnEdit.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.btnEdit.BackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnEdit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Arial", 9F);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(12, 5);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.OverriddenSize = null;
            this.btnEdit.Size = new System.Drawing.Size(38, 21);
            this.btnEdit.TabIndex = 0;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // ctrlFormResizer1
            // 
            this.ctrlFormResizer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrlFormResizer1.Location = new System.Drawing.Point(227, 2);
            this.ctrlFormResizer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlFormResizer1.Name = "ctrlFormResizer1";
            this.ctrlFormResizer1.Size = new System.Drawing.Size(16, 18);
            this.ctrlFormResizer1.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor2 = System.Drawing.Color.White;
            this.pnlBottom.BorderColor = System.Drawing.SystemColors.Menu;
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.ctrlFormResizer1);
            this.pnlBottom.Controls.Add(this.label1);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 278);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(245, 23);
            this.pnlBottom.StickyParent = null;
            this.pnlBottom.TabIndex = 1;
            // 
            // pnlMiddel
            // 
            this.pnlMiddel.BackColor2 = System.Drawing.Color.White;
            this.pnlMiddel.BorderColor = System.Drawing.SystemColors.Menu;
            this.pnlMiddel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMiddel.Controls.Add(this.btnDelete);
            this.pnlMiddel.Controls.Add(this.btnEdit);
            this.pnlMiddel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMiddel.Location = new System.Drawing.Point(0, 247);
            this.pnlMiddel.Name = "pnlMiddel";
            this.pnlMiddel.Size = new System.Drawing.Size(245, 31);
            this.pnlMiddel.StickyParent = null;
            this.pnlMiddel.TabIndex = 2;
            this.pnlMiddel.Visible = false;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor2 = System.Drawing.Color.White;
            this.pnlTop.BorderColor = System.Drawing.SystemColors.Menu;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.lnkAddNew);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(245, 25);
            this.pnlTop.StickyParent = null;
            this.pnlTop.TabIndex = 5;
            this.pnlTop.Visible = false;
            // 
            // lnkAddNew
            // 
            this.lnkAddNew.ActiveLinkColor = System.Drawing.Color.Black;
            this.lnkAddNew.AutoSize = true;
            this.lnkAddNew.DisabledLinkColor = System.Drawing.Color.Black;
            this.lnkAddNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lnkAddNew.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkAddNew.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lnkAddNew.Location = new System.Drawing.Point(7, 5);
            this.lnkAddNew.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkAddNew.Name = "lnkAddNew";
            this.lnkAddNew.Size = new System.Drawing.Size(99, 15);
            this.lnkAddNew.TabIndex = 0;
            this.lnkAddNew.TabStop = true;
            this.lnkAddNew.Text = "< &Add New .. >";
            this.lnkAddNew.VisitedLinkColor = System.Drawing.Color.Black;
            this.lnkAddNew.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // CtrlDropDownListForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(245, 301);
            this.ControlBox = false;
            this.Controls.Add(this.ctrlListingTable);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlMiddel);
            this.Controls.Add(this.pnlBottom);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(100, 20);
            this.Name = "CtrlDropDownListForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CtrlDropDownListForm";
            this.Load += new System.EventHandler(this.CtrlDropDownListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ctrlListingTable)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlMiddel.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlFormResizer ctrlFormResizer1;
        private Label label1;
        private StyledDataGridView ctrlListingTable;
        private StyledButton btnDelete;
        private StyledButton btnEdit;
        private StyledPanel pnlBottom;
        private StyledPanel pnlMiddel;
        private StyledPanel pnlTop;
        private LinkLabel lnkAddNew;
        private DataGridViewCheckBoxColumn colSelect;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;

    }
}