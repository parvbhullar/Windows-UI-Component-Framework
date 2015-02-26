using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{

    public class CtrlStickyNote : Oranikle.Studio.Controls.BaseUserControl
    {

        public enum Colour
        {
            Yellow = 1,
            Blue = 2,
            Red = 3
        }

        private Oranikle.Studio.Controls.CtrlStickyNote.Colour _CurrentColour;
        private bool _Enabled;
        private int _StickyNoteId;
        private System.ComponentModel.IContainer components;
        private Oranikle.Studio.Controls.CtrlStyledPictureBox ctrlBlue;
        private Oranikle.Studio.Controls.StyledPanel ctrlCustomPanel1;
        private Oranikle.Studio.Controls.CtrlStyledPictureBox ctrlRed;
        private Oranikle.Studio.Controls.CtrlStyledPictureBox ctrlStyledPictureBox1;
        private Oranikle.Studio.Controls.CtrlStyledPictureBox ctrlYellow;
        private bool dragging;
        private int mousex;
        private int mousey;
        private bool resizeMouseDown;
        private int resizemousex;
        private int resizemousey;
        private Oranikle.Studio.Controls.StyledTextBox richTextBox1;
        private Oranikle.Studio.Controls.CtrlStyledSplitContainer splitContainer1;
        private Oranikle.Studio.Controls.CtrlStyledSplitContainer splitContainer2;
        private Oranikle.Studio.Controls.CtrlStyledSplitContainer splitContainer3;

        public Oranikle.Studio.Controls.CtrlStickyNote.Colour CurrentColour
        {
            get
            {
                return _CurrentColour;
            }
            set
            {
                _CurrentColour = value;
                ChangeToColour(value);
            }
        }

        public new bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
                richTextBox1.Enabled = _Enabled;
                splitContainer1.Panel2.Enabled = _Enabled;
                ctrlBlue.Enabled = _Enabled;
                ctrlRed.Enabled = _Enabled;
                ctrlYellow.Enabled = _Enabled;
                ctrlStyledPictureBox1.Enabled = _Enabled;
            }
        }

        public int StickyNoteId
        {
            get
            {
                return _StickyNoteId;
            }
            set
            {
                _StickyNoteId = value;
            }
        }

        public bool IsReadOnly
        {
            get { return richTextBox1.ReadOnly; }
            set { richTextBox1.ReadOnly = value; }
        }

        public override string Text
        {
            get
            {
                return richTextBox1.Text;
            }
            set
            {
                richTextBox1.Text = value;
            }
        }

        public string HelpText
        {
            get
            {
                return richTextBox1.Text;
            }
            set
            {
                richTextBox1.Text = value;
            }
        }

        public CtrlStickyNote()
        {
            _Enabled = true;
            InitializeComponent();
            SetDefaultControlStyles();
        }

        private void ChangeToColour(Oranikle.Studio.Controls.CtrlStickyNote.Colour c)
        {
            switch (c)
            {
                case Oranikle.Studio.Controls.CtrlStickyNote.Colour.Yellow:
                    BackColor = System.Drawing.Color.Gold;
                    ctrlCustomPanel1.BackColor = System.Drawing.Color.Gold;
                    splitContainer1.BackColor = System.Drawing.Color.Gold;
                    splitContainer1.Panel2.BackColor = System.Drawing.Color.Gold;
                    richTextBox1.BackColor = System.Drawing.Color.Gold;
                    break;

                case Oranikle.Studio.Controls.CtrlStickyNote.Colour.Blue:
                    BackColor = System.Drawing.Color.LightSteelBlue;
                    ctrlCustomPanel1.BackColor = System.Drawing.Color.LightSteelBlue;
                    splitContainer1.BackColor = System.Drawing.Color.LightSteelBlue;
                    splitContainer1.Panel2.BackColor = System.Drawing.Color.LightSteelBlue;
                    richTextBox1.BackColor = System.Drawing.Color.LightSteelBlue;
                    break;

                case Oranikle.Studio.Controls.CtrlStickyNote.Colour.Red:
                    BackColor = System.Drawing.Color.LightCoral;
                    ctrlCustomPanel1.BackColor = System.Drawing.Color.LightCoral;
                    splitContainer1.BackColor = System.Drawing.Color.LightCoral;
                    splitContainer1.Panel2.BackColor = System.Drawing.Color.LightCoral;
                    richTextBox1.BackColor = System.Drawing.Color.LightCoral;
                    break;
            }
            _CurrentColour = c;
        }

        private void ctrlBlue_Click(object sender, System.EventArgs e)
        {
            ChangeToColour(Oranikle.Studio.Controls.CtrlStickyNote.Colour.Blue);
        }

        private void ctrlRed_Click(object sender, System.EventArgs e)
        {
            ChangeToColour(Oranikle.Studio.Controls.CtrlStickyNote.Colour.Red);
        }

        private void ctrlYellow_Click(object sender, System.EventArgs e)
        {
            ChangeToColour(Oranikle.Studio.Controls.CtrlStickyNote.Colour.Yellow);
        }

        private void Drag_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!_Enabled)
                return;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                dragging = true;
                mousex = e.X;
                mousey = e.Y;
                Invalidate();
                System.Drawing.Point point1 = Parent.PointToClient(System.Windows.Forms.Control.MousePosition);
                System.Drawing.Point point2 = Location;
                int i1 = point1.X - point2.X;
                System.Drawing.Point point3 = Location;
                int i2 = point1.Y - point3.Y;
                System.Drawing.Size size1 = Parent.ClientSize;
                int i3 = size1.Width - Width;
                System.Drawing.Size size2 = Parent.ClientSize;
                int i4 = size2.Height - Height;
                System.Windows.Forms.Cursor.Clip = Parent.RectangleToScreen(new System.Drawing.Rectangle(i1, i2, i3, i4));
            }
        }

        private void Drag_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Drawing.Point point1;

            if (!_Enabled)
                return;
            if (dragging)
            {
                point1 = new System.Drawing.Point();
                System.Drawing.Point point2 = Location;
                point1.X = e.X - mousex + point2.X;
                System.Drawing.Point point3 = Location;
                point1.Y = e.Y - mousey + point3.Y;
                Location = point1;
                Invalidate();
            }
        }

        private void Drag_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!_Enabled)
                return;
            dragging = false;
            System.Windows.Forms.Cursor.Clip = System.Drawing.Rectangle.Empty;
            Invalidate();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(Oranikle.Studio.Controls.CtrlStickyNote));
            ctrlCustomPanel1 = new Oranikle.Studio.Controls.StyledPanel();
            splitContainer1 = new Oranikle.Studio.Controls.CtrlStyledSplitContainer();
            splitContainer3 = new Oranikle.Studio.Controls.CtrlStyledSplitContainer();
            ctrlStyledPictureBox1 = new Oranikle.Studio.Controls.CtrlStyledPictureBox();
            richTextBox1 = new Oranikle.Studio.Controls.StyledTextBox();
            splitContainer2 = new Oranikle.Studio.Controls.CtrlStyledSplitContainer();
            ctrlRed = new Oranikle.Studio.Controls.CtrlStyledPictureBox();
            ctrlYellow = new Oranikle.Studio.Controls.CtrlStyledPictureBox();
            ctrlBlue = new Oranikle.Studio.Controls.CtrlStyledPictureBox();
            ctrlCustomPanel1.SuspendLayout();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            //ctrlStyledPictureBox1.BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.SuspendLayout();
            //ctrlRed.BeginInit();
            //ctrlYellow.BeginInit();
            //ctrlBlue.BeginInit();
            SuspendLayout();
            ctrlCustomPanel1.BackColor = System.Drawing.Color.Gold;
            ctrlCustomPanel1.BackColor2 = System.Drawing.Color.White;
            ctrlCustomPanel1.BorderColor = System.Drawing.Color.Silver;
            ctrlCustomPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ctrlCustomPanel1.Controls.Add(splitContainer1);
            ctrlCustomPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            ctrlCustomPanel1.Location = new System.Drawing.Point(0, 0);
            ctrlCustomPanel1.Name = "ctrlCustomPanel1";
            ctrlCustomPanel1.Padding = new System.Windows.Forms.Padding(2);
            ctrlCustomPanel1.Size = new System.Drawing.Size(153, 142);
            ctrlCustomPanel1.StickyParent = null;
            ctrlCustomPanel1.TabIndex = 0;
            ctrlCustomPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(Drag_MouseMove);
            ctrlCustomPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(Drag_MouseDown);
            ctrlCustomPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(Drag_MouseUp);
            splitContainer1.BackColor = System.Drawing.Color.Gold;
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(2, 2);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            splitContainer1.Panel1.Controls.Add(splitContainer3);
            splitContainer1.Panel2.BackColor = System.Drawing.Color.Gold;
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(Drag_MouseMove);
            splitContainer1.Panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(Drag_MouseDown);
            splitContainer1.Panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(Drag_MouseUp);
            splitContainer1.Panel2MinSize = 19;
            splitContainer1.Size = new System.Drawing.Size(149, 138);
            splitContainer1.SplitterDistance = 115;
            splitContainer1.TabIndex = 1;
            splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer3.IsSplitterFixed = true;
            splitContainer3.Location = new System.Drawing.Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            splitContainer3.Panel1.Controls.Add(ctrlStyledPictureBox1);
            splitContainer3.Panel1.Padding = new System.Windows.Forms.Padding(0, 2, 2, 0);
            splitContainer3.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(Drag_MouseMove);
            splitContainer3.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(Drag_MouseDown);
            splitContainer3.Panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(Drag_MouseUp);
            splitContainer3.Panel1MinSize = 14;
            splitContainer3.Panel2.Controls.Add(richTextBox1);
            splitContainer3.Size = new System.Drawing.Size(149, 115);
            splitContainer3.SplitterDistance = 14;
            splitContainer3.SplitterWidth = 1;
            splitContainer3.TabIndex = 0;
            ctrlStyledPictureBox1.BorderColor = System.Drawing.Color.LightGray;
            ctrlStyledPictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            ctrlStyledPictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            ctrlStyledPictureBox1.Image = (System.Drawing.Image)componentResourceManager.GetObject("ctrlStyledPictureBox1.Image");
            ctrlStyledPictureBox1.Location = new System.Drawing.Point(135, 2);
            ctrlStyledPictureBox1.Name = "ctrlStyledPictureBox1";
            ctrlStyledPictureBox1.Size = new System.Drawing.Size(12, 12);
            ctrlStyledPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            ctrlStyledPictureBox1.TabIndex = 0;
            ctrlStyledPictureBox1.TabStop = false;
            ctrlStyledPictureBox1.Click += new System.EventHandler(toolStripStatusLabel1_Click);
            richTextBox1.AcceptsReturn = true;
            richTextBox1.BackColor = System.Drawing.Color.Gold;
            richTextBox1.BorderColor = System.Drawing.Color.LightGray;
            richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            richTextBox1.ConvertEnterToTab = true;
            richTextBox1.ConvertEnterToTabForDialogs = false;
            richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            richTextBox1.Location = new System.Drawing.Point(0, 0);
            richTextBox1.Multiline = true;
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(149, 100);
            richTextBox1.TabIndex = 1;
            richTextBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(Drag_MouseMove);
            richTextBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(Drag_MouseDown);
            richTextBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(Drag_MouseUp);
            splitContainer2.BackColor = System.Drawing.Color.Transparent;
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new System.Drawing.Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Panel1.Controls.Add(ctrlRed);
            splitContainer2.Panel1.Controls.Add(ctrlYellow);
            splitContainer2.Panel1.Controls.Add(ctrlBlue);
            splitContainer2.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(Drag_MouseMove);
            splitContainer2.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(Drag_MouseDown);
            splitContainer2.Panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(Drag_MouseUp);
            splitContainer2.Panel1MinSize = 19;
            splitContainer2.Panel2.BackgroundImage = (System.Drawing.Image)componentResourceManager.GetObject("splitContainer2.Panel2.BackgroundImage");
            splitContainer2.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            splitContainer2.Panel2.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            splitContainer2.Panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(splitContainer2_Panel2_MouseMove);
            splitContainer2.Panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(splitContainer2_Panel2_MouseDown);
            splitContainer2.Panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(splitContainer2_Panel2_MouseUp);
            splitContainer2.Panel2MinSize = 19;
            splitContainer2.Size = new System.Drawing.Size(149, 19);
            splitContainer2.SplitterDistance = 129;
            splitContainer2.SplitterWidth = 1;
            splitContainer2.TabIndex = 0;
            ctrlRed.BackColor = System.Drawing.Color.LightCoral;
            ctrlRed.BorderColor = System.Drawing.Color.Gray;
            ctrlRed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ctrlRed.Cursor = System.Windows.Forms.Cursors.Hand;
            ctrlRed.Location = new System.Drawing.Point(35, 6);
            ctrlRed.Name = "ctrlRed";
            ctrlRed.Size = new System.Drawing.Size(10, 10);
            ctrlRed.TabIndex = 5;
            ctrlRed.TabStop = false;
            ctrlRed.Click += new System.EventHandler(ctrlRed_Click);
            ctrlYellow.BackColor = System.Drawing.Color.Gold;
            ctrlYellow.BorderColor = System.Drawing.Color.Gray;
            ctrlYellow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ctrlYellow.Cursor = System.Windows.Forms.Cursors.Hand;
            ctrlYellow.Location = new System.Drawing.Point(3, 6);
            ctrlYellow.Name = "ctrlYellow";
            ctrlYellow.Size = new System.Drawing.Size(10, 10);
            ctrlYellow.TabIndex = 3;
            ctrlYellow.TabStop = false;
            ctrlYellow.Click += new System.EventHandler(ctrlYellow_Click);
            ctrlBlue.BackColor = System.Drawing.Color.LightSteelBlue;
            ctrlBlue.BorderColor = System.Drawing.Color.Gray;
            ctrlBlue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ctrlBlue.Cursor = System.Windows.Forms.Cursors.Hand;
            ctrlBlue.Location = new System.Drawing.Point(19, 6);
            ctrlBlue.Name = "ctrlBlue";
            ctrlBlue.Size = new System.Drawing.Size(10, 10);
            ctrlBlue.TabIndex = 4;
            ctrlBlue.TabStop = false;
            ctrlBlue.Click += new System.EventHandler(ctrlBlue_Click);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.Gold;
            Controls.Add(ctrlCustomPanel1);
            DoubleBuffered = false;
            MinimumSize = new System.Drawing.Size(80, 80);
            Name = "CtrlStickyNote";
            Size = new System.Drawing.Size(153, 142);
            ctrlCustomPanel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.Panel2.PerformLayout();
            splitContainer3.ResumeLayout(false);
            //ctrlStyledPictureBox1.EndInit();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.ResumeLayout(false);
            //ctrlRed.EndInit();
            //ctrlYellow.EndInit();
            //ctrlBlue.EndInit();
            ResumeLayout(false);
        }

        private void lblHideNotes_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            //Oranikle.Studio.Controls.IStickyControl istickyControl = (Oranikle.Studio.Controls.IStickyControl)Parent;
            //istickyControl.HideAllStickies();
        }

        private void SetDefaultControlStyles()
        {
            SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            SetStyle(System.Windows.Forms.ControlStyles.UserMouse, true);
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, false);
            SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
            SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(System.Windows.Forms.ControlStyles.ContainerControl, true);
            UpdateStyles();
            _CurrentColour = Oranikle.Studio.Controls.CtrlStickyNote.Colour.Yellow;
        }

        private void splitContainer2_Panel2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!_Enabled)
                return;
            resizeMouseDown = true;
            resizemousex = e.X;
            resizemousey = e.Y;
        }

        private void splitContainer2_Panel2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!_Enabled)
                return;
            if (resizeMouseDown)
            {
                int i1 = e.X - resizemousex + Width;
                int i2 = e.Y - resizemousey + Height;
                System.Drawing.Size size1 = MinimumSize;
                if (i1 < size1.Width)
                {
                    System.Drawing.Size size2 = MinimumSize;
                    i1 = size2.Width;
                }
                System.Drawing.Size size3 = MinimumSize;
                if (i2 < size3.Height)
                {
                    System.Drawing.Size size4 = MinimumSize;
                    i2 = size4.Height;
                }
                Size = new System.Drawing.Size(i1, i2);
            }
        }

        private void splitContainer2_Panel2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!_Enabled)
                return;
            resizeMouseDown = false;
        }

        private void toolStripStatusLabel1_Click(object sender, System.EventArgs e)
        {
            this.Visible = false;
            //Oranikle.Studio.Controls.IStickyControl istickyControl = (Oranikle.Studio.Controls.IStickyControl)Parent;
            //istickyControl.DeleteSticky(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

    } // class CtrlStickyNote

}

