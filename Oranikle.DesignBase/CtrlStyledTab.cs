using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{

    [System.Drawing.ToolboxBitmap(typeof(Oranikle.Studio.Controls.CtrlStyledTab))]
    public class CtrlStyledTab : System.Windows.Forms.TabControl
    {

        internal class TabpageExCollectionEditor : System.ComponentModel.Design.CollectionEditor
        {

            public TabpageExCollectionEditor(System.Type type) : base(type)
            {
            }

            protected override System.Type CreateCollectionItemType()
            {
                return typeof(System.Windows.Forms.TabPage);
            }

        } // class TabpageExCollectionEditor

        private const int nMargin = 5;

        private System.Drawing.Color _BorderColor;
        private bool _DontSlantMiddle;
        private int _LeftSpacing;
        private System.Drawing.Font _TabFont;
        private System.Windows.Forms.TabPage[] _TabPageOrder;
        private int _TabSlant;
        private System.Drawing.Color _TabTextColor;
        private System.Drawing.StringAlignment _TabTextHAlignment;
        private System.Drawing.StringAlignment _TabTextVAlignment;
        private System.Drawing.Color _TagPageSelectedColor;
        private System.Drawing.Color _TagPageUnselectedColor;
        private bool allowNextSelection;
        private System.ComponentModel.Container components;
        private System.Drawing.Color mBackColor;
        private Oranikle.Studio.Controls.NativeUpDown UpDown;

        public new System.Windows.Forms.TabAlignment Alignment
        {
            get
            {
                return base.Alignment;
            }
            set
            {
                System.Windows.Forms.TabAlignment tabAlignment = value;
                if ((tabAlignment != System.Windows.Forms.TabAlignment.Top) && (tabAlignment != System.Windows.Forms.TabAlignment.Bottom))
                    tabAlignment = System.Windows.Forms.TabAlignment.Top;
                base.Alignment = tabAlignment;
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public bool DontSlantMiddle
        {
            get
            {
                return _DontSlantMiddle;
            }
            set
            {
                _DontSlantMiddle = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public int LeftSpacing
        {
            get
            {
                return _LeftSpacing;
            }
            set
            {
                _LeftSpacing = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Browsable(false)]
        public new bool Multiline
        {
            get
            {
                return base.Multiline;
            }
            set
            {
                base.Multiline = false;
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Browsable(true)]
        public System.Drawing.Color myBackColor
        {
            get
            {
                return mBackColor;
            }
            set
            {
                mBackColor = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Font TabFont
        {
            get
            {
                return _TabFont;
            }
            set
            {
                _TabFont = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public int TabSlant
        {
            get
            {
                return _TabSlant;
            }
            set
            {
                _TabSlant = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color TabTextColor
        {
            get
            {
                return _TabTextColor;
            }
            set
            {
                _TabTextColor = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.StringAlignment TabTextHAlignment
        {
            get
            {
                return _TabTextHAlignment;
            }
            set
            {
                _TabTextHAlignment = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.StringAlignment TabTextVAlignment
        {
            get
            {
                return _TabTextVAlignment;
            }
            set
            {
                _TabTextVAlignment = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color TagPageSelectedColor
        {
            get
            {
                return _TagPageSelectedColor;
            }
            set
            {
                _TagPageSelectedColor = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color TagPageUnselectedColor
        {
            get
            {
                return _TagPageUnselectedColor;
            }
            set
            {
                _TagPageUnselectedColor = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        public CtrlStyledTab()
        {
            _TabSlant = 2;
            _TagPageSelectedColor = System.Drawing.Color.White;
            _TagPageUnselectedColor = System.Drawing.Color.LightGray;
            _TabTextVAlignment = System.Drawing.StringAlignment.Center;
            _BorderColor = System.Drawing.Color.DarkGray;
            _TabTextColor = System.Drawing.Color.FromArgb(64, 64, 64);
            _TabFont = new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            mBackColor = System.Drawing.Color.Transparent;
            InitializeComponent();
            SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
            SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            ControlAdded += new System.Windows.Forms.ControlEventHandler(CtrlColourTab_ControlAdded);
            ControlRemoved += new System.Windows.Forms.ControlEventHandler(CtrlColourTab_ControlRemoved);
            SelectedIndexChanged += new System.EventHandler(CtrlColourTab_SelectedIndexChanged);
            SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            TabStop = false;
        }

        private void CtrlColourTab_ControlAdded(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            SetTabsColor();
        }

        private void CtrlColourTab_ControlRemoved(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            SetTabsColor();
        }

        private void CtrlColourTab_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Invalidate();
        }

        internal void DrawControl(System.Drawing.Graphics g)
        {
            System.Drawing.Brush brush;

            if (!Visible)
                return;
            System.Drawing.Rectangle rectangle1 = ClientRectangle;
            System.Drawing.Rectangle rectangle2 = DisplayRectangle;
            if (mBackColor == System.Drawing.Color.Transparent)
                brush = new System.Drawing.SolidBrush(Parent.BackColor);
            else
                brush = new System.Drawing.SolidBrush(mBackColor);
            g.FillRectangle(brush, rectangle1);
            brush.Dispose();
            System.Drawing.Size size = System.Windows.Forms.SystemInformation.Border3DSize;
            int i1 = size.Width;
            System.Drawing.Pen pen = new System.Drawing.Pen(_BorderColor);
            rectangle2.Inflate(i1, i1);
            g.DrawRectangle(pen, rectangle2);
            pen.Dispose();
            for (int i2 = 0; i2 < TabCount; i2++)
            {
                DrawTab(g, TabPages[i2], i2);
            }
            if (SelectedTab != null)
            {
                System.Windows.Forms.TabPage tabPage = SelectedTab;
                //tabPage.BackColor;
                pen = new System.Drawing.Pen(_TagPageSelectedColor);
                rectangle2.Offset(1, 1);
                rectangle2.Width -= 2;
                rectangle2.Height -= 2;
                g.DrawRectangle(pen, rectangle2);
                rectangle2.Width--;
                rectangle2.Height--;
                g.DrawRectangle(pen, rectangle2);
                pen.Dispose();
            }
        }

        internal void DrawTab(System.Drawing.Graphics g, System.Windows.Forms.TabPage tabPage, int nIndex)
        {
            System.Drawing.Brush brush;
            System.Drawing.Point[] pointArr;

            System.Drawing.Rectangle rectangle1 = GetTabRect(nIndex);
            System.Drawing.RectangleF rectangleF = GetTabRect(nIndex);
            Oranikle.Studio.Controls.CtrlStyledTabPage ctrlStyledTabPage = tabPage as Oranikle.Studio.Controls.CtrlStyledTabPage;
            if (ctrlStyledTabPage != null)
            {
                System.Nullable<int> nullable1 = ctrlStyledTabPage.TabWidth;
                if (nullable1.HasValue)
                {
                    System.Nullable<int> nullable = ctrlStyledTabPage.TabWidth;
                    rectangle1.Width = nullable.Value;
                    System.Nullable<int> nullable2 = ctrlStyledTabPage.TabWidth;
                    rectangleF.Width = (float)nullable2.Value;
                }
            }
            if (_LeftSpacing > 0)
            {
                rectangle1 = new System.Drawing.Rectangle(rectangle1.X + _LeftSpacing, rectangle1.Y, rectangle1.Width, rectangle1.Height);
                rectangleF = new System.Drawing.RectangleF(rectangleF.X + (float)_LeftSpacing, rectangleF.Y, rectangleF.Width, rectangleF.Height);
            }
            bool flag = SelectedIndex == nIndex;
            if (Alignment == System.Windows.Forms.TabAlignment.Top)
            {
                if (_DontSlantMiddle)
                {
                    if (nIndex == 0)
                    {
                        pointArr = new System.Drawing.Point[6];
                        pointArr[0] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom);
                        pointArr[1] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top + _TabSlant);
                        pointArr[2] = new System.Drawing.Point(rectangle1.Left + _TabSlant, rectangle1.Top);
                        pointArr[3] = new System.Drawing.Point(rectangle1.Right, rectangle1.Top);
                        pointArr[4] = new System.Drawing.Point(rectangle1.Right, rectangle1.Bottom);
                        pointArr[5] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom);
                    }
                    else if (nIndex == (TabCount - 1))
                    {
                        pointArr = new System.Drawing.Point[6];
                        pointArr[0] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom);
                        pointArr[1] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top);
                        pointArr[2] = new System.Drawing.Point(rectangle1.Right - _TabSlant, rectangle1.Top);
                        pointArr[3] = new System.Drawing.Point(rectangle1.Right, rectangle1.Top + _TabSlant);
                        pointArr[4] = new System.Drawing.Point(rectangle1.Right, rectangle1.Bottom);
                        pointArr[5] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom);
                    }
                    else
                    {
                        pointArr = new System.Drawing.Point[5];
                        pointArr[0] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom);
                        pointArr[1] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top);
                        pointArr[2] = new System.Drawing.Point(rectangle1.Right, rectangle1.Top);
                        pointArr[3] = new System.Drawing.Point(rectangle1.Right, rectangle1.Bottom);
                        pointArr[4] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom);
                    }
                }
                else
                {
                    pointArr = new System.Drawing.Point[7];
                    pointArr[0] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom);
                    pointArr[1] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top + _TabSlant);
                    pointArr[2] = new System.Drawing.Point(rectangle1.Left + _TabSlant, rectangle1.Top);
                    pointArr[3] = new System.Drawing.Point(rectangle1.Right - _TabSlant, rectangle1.Top);
                    pointArr[4] = new System.Drawing.Point(rectangle1.Right, rectangle1.Top + _TabSlant);
                    pointArr[5] = new System.Drawing.Point(rectangle1.Right, rectangle1.Bottom);
                    pointArr[6] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom);
                }
            }
            else if (_DontSlantMiddle)
            {
                if (nIndex == 0)
                {
                    pointArr = new System.Drawing.Point[6];
                    pointArr[0] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top);
                    pointArr[1] = new System.Drawing.Point(rectangle1.Right, rectangle1.Top);
                    pointArr[2] = new System.Drawing.Point(rectangle1.Right, rectangle1.Bottom);
                    pointArr[3] = new System.Drawing.Point(rectangle1.Left + _TabSlant, rectangle1.Bottom);
                    pointArr[4] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom - _TabSlant);
                    pointArr[5] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top);
                }
                else if (nIndex == (TabCount - 1))
                {
                    pointArr = new System.Drawing.Point[6];
                    pointArr[0] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top);
                    pointArr[1] = new System.Drawing.Point(rectangle1.Right, rectangle1.Top);
                    pointArr[2] = new System.Drawing.Point(rectangle1.Right, rectangle1.Bottom - _TabSlant);
                    pointArr[3] = new System.Drawing.Point(rectangle1.Right - _TabSlant, rectangle1.Bottom);
                    pointArr[4] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom);
                    pointArr[5] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top);
                }
                else
                {
                    pointArr = new System.Drawing.Point[5];
                    pointArr[0] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top);
                    pointArr[1] = new System.Drawing.Point(rectangle1.Right, rectangle1.Top);
                    pointArr[2] = new System.Drawing.Point(rectangle1.Right, rectangle1.Bottom);
                    pointArr[3] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom);
                    pointArr[4] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top);
                }
            }
            else
            {
                pointArr = new System.Drawing.Point[7];
                pointArr[0] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top);
                pointArr[1] = new System.Drawing.Point(rectangle1.Right, rectangle1.Top);
                pointArr[2] = new System.Drawing.Point(rectangle1.Right, rectangle1.Bottom - _TabSlant);
                pointArr[3] = new System.Drawing.Point(rectangle1.Right - _TabSlant, rectangle1.Bottom);
                pointArr[4] = new System.Drawing.Point(rectangle1.Left + _TabSlant, rectangle1.Bottom);
                pointArr[5] = new System.Drawing.Point(rectangle1.Left, rectangle1.Bottom - _TabSlant);
                pointArr[6] = new System.Drawing.Point(rectangle1.Left, rectangle1.Top);
            }
            if (nIndex == SelectedIndex)
                brush = new System.Drawing.SolidBrush(_TagPageSelectedColor);
            else
                brush = new System.Drawing.SolidBrush(_TagPageUnselectedColor);
            g.FillPolygon(brush, pointArr);
            brush.Dispose();
            g.DrawPolygon(new System.Drawing.Pen(_BorderColor), pointArr);
            if (flag)
            {
                System.Drawing.Pen pen = new System.Drawing.Pen(_TagPageSelectedColor);
                switch (Alignment)
                {
                    case System.Windows.Forms.TabAlignment.Top:
                        g.DrawLine(pen, rectangle1.Left + 1, rectangle1.Bottom, rectangle1.Right - 1, rectangle1.Bottom);
                        g.DrawLine(pen, rectangle1.Left + 1, rectangle1.Bottom + 1, rectangle1.Right - 1, rectangle1.Bottom + 1);
                        break;

                    case System.Windows.Forms.TabAlignment.Bottom:
                        g.DrawLine(pen, rectangle1.Left + 1, rectangle1.Top, rectangle1.Right - 1, rectangle1.Top);
                        g.DrawLine(pen, rectangle1.Left + 1, rectangle1.Top - 1, rectangle1.Right - 1, rectangle1.Top - 1);
                        g.DrawLine(pen, rectangle1.Left + 1, rectangle1.Top - 2, rectangle1.Right - 1, rectangle1.Top - 2);
                        break;
                }
                pen.Dispose();
            }
            if ((tabPage.ImageIndex >= 0) && (ImageList != null) && (ImageList.Images[tabPage.ImageIndex] != null))
            {
                int i1 = 8, i2 = 2;
                System.Drawing.Image image = ImageList.Images[tabPage.ImageIndex];
                System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle(rectangle1.X + i1, rectangle1.Y + 1, image.Width, image.Height);
                float f = (float)(i1 + image.Width + i2);
                rectangle2.Y += (rectangle1.Height - image.Height) / 2;
                rectangleF.X += f;
                rectangleF.Width -= f;
                g.DrawImage(image, rectangle2);
            }
            if (ImageList == null)
            {
                int i3 = 12;
                rectangleF.X += (float)i3;
                rectangleF.Width -= (float)i3;
            }
            System.Drawing.StringFormat stringFormat = new System.Drawing.StringFormat();
            if (ctrlStyledTabPage != null)
            {
                System.Nullable<System.Drawing.StringAlignment> nullable4 = ctrlStyledTabPage.TabTextAlignment;
                if (nullable4.HasValue)
                    goto label_1;
                System.Nullable<System.Drawing.StringAlignment> nullable3 = ctrlStyledTabPage.TabTextAlignment;
                stringFormat.Alignment = (System.Drawing.StringAlignment)nullable3.Value;
            }
            else
            {
                goto label_1;
                
            }
        label_1:
            { stringFormat.Alignment = _TabTextHAlignment; }


            stringFormat.LineAlignment = _TabTextVAlignment;
            stringFormat.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
            stringFormat.FormatFlags = System.Drawing.StringFormatFlags.NoWrap;
            brush = new System.Drawing.SolidBrush(_TabTextColor);
            g.DrawString(tabPage.Text, _TabFont, brush, rectangleF, stringFormat);

        

        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        private void InsertTabPage(System.Windows.Forms.TabPage[] originalOrder, System.Windows.Forms.TabPage page)
        {
            if (TabPages.Contains(page))
                return;
            if (originalOrder == null)
                TabPages.Add(page);
            System.Collections.Generic.Dictionary<System.Windows.Forms.TabPage,int> dictionary = new System.Collections.Generic.Dictionary<System.Windows.Forms.TabPage,int>();
            for (int i1 = 0; i1 < originalOrder.Length; i1++)
            {
                dictionary.Add(originalOrder[i1], i1);
            }
            if (!dictionary.ContainsKey(page))
                TabPages.Add(page);
            int i2 = dictionary[page];// (page);
            for (int i3 = 0; i3 < TabPages.Count; i3++)
            {
                System.Windows.Forms.TabPage tabPage = TabPages[i3];
                if (dictionary.ContainsKey(tabPage) && (dictionary[tabPage] > i2))
                {
                    TabPages.Insert(i3, page);
                    return;
                }
            }
            TabPages.Add(page);
        }

        public void SaveOriginalTabPageOrder()
        {
            _TabPageOrder = new System.Windows.Forms.TabPage[TabPages.Count];
            for (int i = 0; i < TabPages.Count; i++)
            {
                System.Collections.IEnumerator ienumerator = TabPages.GetEnumerator();
                try
                {
                    while (ienumerator.MoveNext())
                    {
                        _TabPageOrder[i] = TabPages[i];
                    }
                }
                finally
                {
                    System.IDisposable idisposable = ienumerator as System.IDisposable;
                    if (idisposable != null)
                        idisposable.Dispose();
                }
            }
        }

        public new void SelectTab(System.Windows.Forms.TabPage p)
        {
            allowNextSelection = true;
            SelectedTab = p;
        }

        private void SetTabsColor()
        {
            foreach (System.Windows.Forms.TabPage tabPage in TabPages)
            {
                tabPage.BackColor = _TagPageSelectedColor;
            }
        }

        public void ShowTabPage(bool show, System.Windows.Forms.TabPage page)
        {
            if (show)
            {
                InsertTabPage(_TabPageOrder, page);
                return;
            }
            if (TabPages.Contains(page))
                TabPages.Remove(page);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetTabsColor();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Tab))
            {
                if (this.TabCount > this.SelectedIndex + 1)
                    this.SelectTab(this.TabPages[this.SelectedIndex + 1]);
                else
                    this.SelectTab(this.TabPages[0]);
            }
            //if (keyData == Keys.Tab)
            //{
            //    if (this.SelectedTab != null)
            //    {
            //        Control found = GetLastControl(null, this.SelectedTab.Controls);
            //        if (found != null && found.Focused)
            //        {
            //            if (this.TabCount > this.SelectedIndex + 1)
            //                this.SelectTab(this.TabPages[this.SelectedIndex + 1]);
            //            else
            //                this.SelectTab(this.TabPages[0]);
            //        }
            //    }
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private Control GetLastControl(Control found, Control.ControlCollection controlCollection)
        {
            foreach (Control control in controlCollection)
            {
                if (control.Enabled && (found == null || control.TabIndex > found.TabIndex))
                {
                    if (control.Controls.Count > 0)
                    {
                        found = GetLastControl(control, control.Controls);
                    }
                    else 
                    {
                        found = control;
                    }
                }
            }
            return found;
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            System.Drawing.Point point = e.Location;
            for (int i = 0; i < TabCount; i++)
            {
                System.Drawing.Rectangle rectangle = GetTabRect(i);
                rectangle.Offset(_LeftSpacing, 0);
                if (rectangle.Contains(point) && (SelectedIndex != i))
                {
                    allowNextSelection = true;
                    SelectedIndex = i;
                    return;
                }
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawControl(e.Graphics);
        }

        protected override void OnSelecting(System.Windows.Forms.TabControlCancelEventArgs e)
        {
            if (e.Action == System.Windows.Forms.TabControlAction.Selecting)
            {
                e.Cancel = !allowNextSelection;
                allowNextSelection = false;
            }
            if (!e.Cancel)
                base.OnSelecting(e);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 528)
            {
                System.IntPtr intPtr = m.WParam;
                if ((ushort)(intPtr.ToInt32() & 65535) == 1)
                {
                    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(16);
                    Oranikle.Studio.Controls.Win32.RealGetWindowClass(m.LParam, stringBuilder, 16);
                    if (stringBuilder.ToString() == "msctls_updown32")
                    {
                        if (UpDown != null)
                            UpDown.ReleaseHandle();
                        UpDown = new Oranikle.Studio.Controls.NativeUpDown();
                        UpDown.AssignHandle(m.LParam);
                    }
                }
            }
            base.WndProc(ref m);
        }
    } // class CtrlStyledTab

}

