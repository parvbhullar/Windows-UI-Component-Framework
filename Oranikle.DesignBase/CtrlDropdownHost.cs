using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Oranikle.Studio.Controls
{
    public class CtrlDropdownHost : System.Windows.Forms.UserControl, Oranikle.Studio.Controls.IControlWithBorder, Oranikle.Studio.Controls.IEndEditToDataBinding, Oranikle.Studio.Controls.IResizeToPanel
    {

        protected delegate void EmptyDelegate();

        protected static readonly log4net.ILog log;

        private System.Drawing.Color _BorderColor;
        private System.Windows.Forms.BorderStyle _BorderStyle;
        private Oranikle.Studio.Controls.BorderDrawer borderDrawer;
        private System.ComponentModel.IContainer components;
        private bool dismissed;
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        private Oranikle.Studio.Controls.IDropdownDisplayer displayer;
        [System.NonSerialized]
        [System.ComponentModel.Description("This is fired when the enter key is pressed.  It will later be translated into a tab key, so KeyDown will not be fired for enter.")]
        private Oranikle.Studio.Controls.WeakEvent<System.EventArgs> enterPressed;
        private Oranikle.Studio.Controls.WeakEvent<System.EventArgs> valueChanged;
        private Oranikle.Studio.Controls.WeakEvent<System.EventArgs> addNewClicked;
        private Oranikle.Studio.Controls.WeakEvent<System.EventArgs> editClicked;
        private Oranikle.Studio.Controls.WeakEvent<System.EventArgs> deleteClicked;
        private int horizontalOffset;
        private bool keepFocusOnTextbox;
        protected System.Windows.Forms.Panel pnlButton;
        private bool showDropdownOnEntry;
        private Oranikle.Studio.Controls.CtrlStyledSplitContainer splitContainer1;
        private Oranikle.Studio.Controls.CtrlDropDownForm subform;
        protected Oranikle.Studio.Controls.StyledTextBox textBox;
        protected object _value;
        private int verticalOffset;

        public System.Drawing.Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
                borderDrawer.BorderColor = value;
                Invalidate();
            }
        }

        [System.ComponentModel.DefaultValue(System.Windows.Forms.BorderStyle.FixedSingle)]
        public new System.Windows.Forms.BorderStyle BorderStyle
        {
            get
            {
                return _BorderStyle;
            }
            set
            {
                _BorderStyle = value;
                Invalidate();
            }
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.Browsable(false)]
        public bool Dismissed
        {
            get
            {
                return dismissed;
            }
            set
            {
                dismissed = value;
            }
        }

        protected Oranikle.Studio.Controls.IDropdownDisplayer Displayer
        {
            get
            {
                return displayer;
            }
            set
            {
                displayer = value;
            }
        }

        public Oranikle.Studio.Controls.WeakEvent<System.EventArgs> EnterPressed
        {
            get
            {
                return enterPressed;
            }
        }

     
        public int HorizontalOffset
        {
            get
            {
                return horizontalOffset;
            }
            set
            {
                horizontalOffset = value;
            }
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.Browsable(false)]
        public bool KeepFocusOnTextbox
        {
            get
            {
                return keepFocusOnTextbox;
            }
            set
            {
                keepFocusOnTextbox = value;
            }
        }

        public bool ShowDropdownOnEntry
        {
            get
            {
                return showDropdownOnEntry;
            }
            set
            {
                showDropdownOnEntry = value;
            }
        }


        public Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                textBox.Font = base.Font;

            }
        }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Oranikle.Studio.Controls.CtrlDropDownForm Subform
        {
            get
            {
                return subform;
            }
            set
            {
                subform = value;
                subform.SuspendLayout();
                System.Drawing.Size size = subform.Size;
                System.Drawing.Point point = subform.Location;
                subform.Location = new System.Drawing.Point(9999, 9999);
                subform.Size = new System.Drawing.Size(1, 1);
                subform.ShowWindowWithoutFocus();
                subform.Hide();
                subform.Location = point;
                subform.Size = size;
                subform.ResumeLayout();
            }
        }

        public System.Drawing.Color TextColor
        {
            get
            {
                return textBox.ForeColor;
            }
            set
            {
                textBox.ForeColor = value;
            }
        }

        public System.Drawing.Font TextFont
        {
            get
            {
                return textBox.Font;
            }
            set
            {
                textBox.Font = value;
            }
        }

        public bool TextReadOnly
        {
            get { return textBox.ReadOnly; }
            set { textBox.ReadOnly = value; }
        }

        public int VerticalOffset
        {
            get
            {
                return verticalOffset;
            }
            set
            {
                verticalOffset = value;
            }
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        [System.ComponentModel.Browsable(true)]
        public sealed override string Text
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }

        public virtual object Value
        {
            get
            {
                return _value;
            }
            set
            {
                this._value = value;
                    string s = GetDisplayerValueOrNull(value);
                if (s != null)
                { textBox.Text = s; }
                //else
                    //textBox.Text = System.String.Empty;
            }
        }

        public CtrlDropdownHost()
        {
            borderDrawer = new Oranikle.Studio.Controls.BorderDrawer();
            horizontalOffset = 0;
            _BorderColor = System.Drawing.Color.LightGray;
            keepFocusOnTextbox = true;
            enterPressed = new Oranikle.Studio.Controls.WeakEvent<System.EventArgs>();
            valueChanged = new Oranikle.Studio.Controls.WeakEvent<System.EventArgs>();
            addNewClicked = new Oranikle.Studio.Controls.WeakEvent<System.EventArgs>();
            editClicked = new Oranikle.Studio.Controls.WeakEvent<System.EventArgs>();
            deleteClicked = new Oranikle.Studio.Controls.WeakEvent<System.EventArgs>();
            _BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            InitializeComponent();
            SetDefaultControlStyles();
            base.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox.LostFocus += new System.EventHandler(ChildLostFocus);
            pnlButton.LostFocus += new System.EventHandler(ChildLostFocus);
            textBox.GotFocus += new System.EventHandler(ChildGotFocus);
            pnlButton.GotFocus += new System.EventHandler(ChildGotFocus);
            textBox.EnterPressed.AddHandler(new System.EventHandler(textBox_EnterPressed));
        }

        public void Host_deleteClicked(object sender, EventArgs e)
        {
            deleteClicked.Invoke(sender, e);
        }

        public void Host_editClicked(object sender, EventArgs e)
        {
            editClicked.Invoke(sender, e);
        }

        static CtrlDropdownHost()
        {
            Oranikle.Studio.Controls.CtrlDropdownHost.log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        protected void ChildGotFocus(object sender, System.EventArgs e)
        {
            MaybeGotFocus();
        }

        protected void ChildLostFocus(object sender, System.EventArgs e)
        {
            MaybeLostFocus();
        }

        public void EndEditToDataBinding(System.Windows.Forms.ContainerControl mainControl)
        {
            foreach (System.Windows.Forms.Binding binding in DataBindings)
            {
                binding.WriteValue();
            }
        }

        //int Oranikle.Studio.Controls.IResizeToPanel.get_Width()
        //{
        //    return base.Width;
        //}

        private string GetDisplayerValue(object value)
        {
            if (value == null)
                return "";
            if (displayer == null)
                return value.ToString();
            return displayer.GetDisplay(value).ToString();
        }

        private string GetDisplayerValueOrNull(object value)
        {
            if (value == null)
                return null;
            if (displayer == null)
                return null;
            return displayer.GetDisplayOrNull(value).ToString();
        }

        private void InitializeComponent()
        {
            this.pnlButton = new System.Windows.Forms.Panel();
            this.splitContainer1 = new Oranikle.Studio.Controls.CtrlStyledSplitContainer();
            this.textBox = new Oranikle.Studio.Controls.StyledTextBox();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButton
            // 
            this.pnlButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pnlButton.BackgroundImage = global::Oranikle.Studio.Controls.Properties.Resources.ComboBoxTransparent;
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButton.Location = new System.Drawing.Point(154, 1);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(16, 21);
            this.pnlButton.TabIndex = 1;
            this.pnlButton.Click += new System.EventHandler(this.pnlButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(1, 1);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.Panel1MinSize = 1;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel2.Controls.Add(this.textBox);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.splitContainer1.Panel2MinSize = 13;
            this.splitContainer1.Size = new System.Drawing.Size(153, 21);
            this.splitContainer1.SplitterDistance = 1;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 2;
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.BorderColor = System.Drawing.Color.LightGray;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.ConvertEnterToTab = true;
            this.textBox.ConvertEnterToTabForDialogs = false;
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox.Location = new System.Drawing.Point(2, 0);
            this.textBox.Margin = new System.Windows.Forms.Padding(0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(151, 14);
            this.textBox.TabIndex = 0;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox.BackColorChanged += new System.EventHandler(this.textBox_BackColorChanged);
            // 
            // CtrlDropdownHost
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlButton);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "CtrlDropdownHost";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(171, 23);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        protected void MaybeGotFocus()
        {
            //if (Focused) goto label_0;
            //if (textBox.Focused) goto label_0;
            //if (pnlButton.Focused) goto label_0;
            //this.SelectAllText();
            if (Subform == null)
                return;
            if (!Subform.Visible)
                return;
            if (borderDrawer.BorderColor == Oranikle.Studio.Controls.BorderDrawer.BORDER_FOCUS_COLOR)
                return;
            borderDrawer.BorderColor = Oranikle.Studio.Controls.BorderDrawer.BORDER_FOCUS_COLOR;
            Invalidate();
        }

        protected void MaybeLostFocus()
        {
            if ((Subform != null) && !Subform.GetExpectedVisibility())
                Subform.UpdateVisibility();
            if (!Focused && !textBox.Focused && !pnlButton.Focused && (Subform != null) && !Subform.Visible && borderDrawer.BorderColor != Oranikle.Studio.Controls.BorderDrawer.BORDER_UNFOCUS_COLOR)
            {
                borderDrawer.BorderColor = Oranikle.Studio.Controls.BorderDrawer.BORDER_UNFOCUS_COLOR;
                Invalidate();
            }
        }

        private void ParentForm_Move(object sender, System.EventArgs e)
        {
            RepositionSubform();
        }

        private void pnlButton_Click(object sender, System.EventArgs e)
        {
            if ((Subform == null) || !Subform.Visible)
            {
                textBox.Focus();
                ShowSubform();
                return;
            }
            Subform.HideSubForm();
        }

        private void RepositionSubform()
        {
            if (subform != null)
                subform.Reposition(HorizontalOffset, VerticalOffset);
        }

        public void SelectAllText()
        {
            textBox.SelectAll();
        }

        public System.IntPtr SendMessageToMe(int msg, System.IntPtr wParam, System.IntPtr lParam)
        {
            return Oranikle.Studio.Controls.CtrlDropdownHost.SendMessage(new System.Runtime.InteropServices.HandleRef(Handle, Handle), msg, wParam, lParam);
        }

        //void Oranikle.Studio.Controls.IResizeToPanel.set_Width(object obj)
        //{
        //    base.Width = obj;
        //}

        private void SetDefaultControlStyles()
        {
            SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            SetStyle(System.Windows.Forms.ControlStyles.UserMouse, true);
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
            SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(System.Windows.Forms.ControlStyles.ContainerControl, true);
            UpdateStyles();
        }
       
        public void ShowSubform()
        {
            Dismissed = false;
            subform.SetHost(this, horizontalOffset, verticalOffset);
            subform.UpdateVisibility();
            if ((ParentForm != null) && !ParentForm.IsDisposed)
            {
                //ParentForm.TopMost = false;
                //ParentForm.BringToFront();
            }
            subform.TopMost = true;
            //subform.BringToFront();
            if (keepFocusOnTextbox)
            {
                textBox.Focus();
                return;
            }
            //subform.Activate();
            
        }

        private void textBox_BackColorChanged(object sender, System.EventArgs e)
        {
            if (splitContainer1.BackColor != textBox.BackColor)
                splitContainer1.BackColor = textBox.BackColor;
        }

        private void textBox_EnterPressed(object sender, System.EventArgs e)
        {
            OnEnterPressed();
        }

        private void textBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                Dismissed = true;
                if (Subform != null)
                    Subform.UpdateVisibility();
            }
            OnKeyDown(e);
        }

        private void textBox_TextChanged(object sender, System.EventArgs e)
        {
            base.OnTextChanged(e);
            //OnValueChanged();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        protected virtual bool IsNullRepresentation(object newVal, object nullValue)
        {
            if ((newVal != null) && !newVal.Equals(nullValue) && !newVal.Equals(0))
                return newVal == System.DBNull.Value;
            return true;
        }

        protected override void OnEnabledChanged(System.EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (Enabled)
            {
                splitContainer1.BackColor = System.Drawing.Color.White;
                textBox.BackColor = System.Drawing.Color.White;
                pnlButton.BackgroundImage = Oranikle.Studio.Controls.Properties.Resources.ComboBoxTransparent;
                pnlButton.BackColor = System.Drawing.Color.White;
            }
            else
            {
                splitContainer1.BackColor = System.Drawing.SystemColors.Control;
                textBox.BackColor = System.Drawing.SystemColors.Control;
                pnlButton.BackgroundImage = Oranikle.Studio.Controls.Properties.Resources.ComboBoxTransparentDisabled;
                pnlButton.BackColor = System.Drawing.SystemColors.Control;
            }
            Invalidate();
        }

        protected override void OnEnter(System.EventArgs e)
        {
            base.OnEnter(e);
            if (showDropdownOnEntry)
                ShowSubform();
        }

        public virtual void OnEnterPressed()
        {
            enterPressed.Invoke(this, System.EventArgs.Empty);
        }

        public virtual void OnValueChanged()
        {
            if (textBox.Text == string.Empty)
                this.Value = null;
            valueChanged.Invoke(this, System.EventArgs.Empty);
        }

        public virtual void OnAddNew_Clicked()
        {
            addNewClicked.Invoke(this, System.EventArgs.Empty);
        }

        public virtual void OnEdit_Clicked()
        {

            editClicked.Invoke(this, System.EventArgs.Empty);
        }

        public virtual void OnDelete_Clicked()
        {
            deleteClicked.Invoke(this, System.EventArgs.Empty);
        }

        public virtual void OnEnterPressedForceDatabinding()
        {
            foreach (System.Windows.Forms.Binding binding in DataBindings)
            {
                binding.WriteValue();
            }
            OnEnterPressed();
        }

        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            MaybeGotFocus();
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && (ParentForm != null) && (ParentForm is System.Windows.Forms.Form))
                ((System.Windows.Forms.Form)ParentForm).Move += new EventHandler(ParentForm_Move); //.AddHandler(new System.EventHandler<System.EventArgs>(ParentForm_Move));

        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            MaybeLostFocus();
        }

        protected override void OnParentChanged(System.EventArgs e)
        {
            base.OnParentChanged(e);
            if (!DesignMode && (ParentForm != null) && (ParentForm is System.Windows.Forms.Form))
                ((System.Windows.Forms.Form)ParentForm).Move += new EventHandler(ParentForm_Move);
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            if (subform != null)
                subform.Reposition(HorizontalOffset, VerticalOffset);
        }

        protected override void OnSizeChanged(System.EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (keyData == System.Windows.Forms.Keys.F2)
            {
                ShowSubform();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override bool ProcessKeyEventArgs(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 258)
            {
                textBox.SendMessageToMe(m.Msg, m.WParam, m.LParam);
                return true;
            }
            return base.ProcessKeyEventArgs(ref m);
        }

        protected virtual bool SwitchingBetweenNullRepresentations(object newVal, object nullValue)
        {
            if (IsNullRepresentation(Value, nullValue))
                return IsNullRepresentation(newVal, nullValue);
            return false;
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            borderDrawer.DrawBorder(ref m, base.Width, Height);
        }

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern System.IntPtr SendMessage(System.Runtime.InteropServices.HandleRef hWnd, int msg, System.IntPtr wParam, System.IntPtr lParam);

    } 
}
