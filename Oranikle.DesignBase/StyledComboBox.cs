using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
   
    [System.ComponentModel.DefaultEvent("SelectedIndexChanged")]
    public class StyledComboBox : System.Windows.Forms.ComboBox, Oranikle.Studio.Controls.IControlWithBorder, Oranikle.Studio.Controls.IEndEditToDataBinding
    {
        
        private bool _autoAdjustItemHeight;
        private bool _ConvertEnterToTabForDialogs;
        private System.Drawing.Color _separatorColor;
        private int _separatorMargin;
        private System.Drawing.Drawing2D.DashStyle _separatorStyle;
        private int _separatorWidth;
        private Oranikle.Studio.Controls.BorderDrawer borderDrawer;

        [System.ComponentModel.Description("This is fired when the enter key is pressed.  It will later be translated into a tab key, so KeyDown will not be fired for enter.")]
        [System.ComponentModel.Category("Key")]
        [System.ComponentModel.Browsable(true)]
        public event System.EventHandler EnterPressed;

        [System.ComponentModel.Description("Gets or sets Auto Adjust Item Height")]
        [System.ComponentModel.Category("Separator")]
        public bool AutoAdjustItemHeight
        {
            get
            {
                return _autoAdjustItemHeight;
            }
            set
            {
                _autoAdjustItemHeight = value;
            }
        }

        public System.Drawing.Color BorderColor
        {
            get
            {
                return borderDrawer.BorderColor;
            }
            set
            {
            }
        }

        public bool ConvertEnterToTabForDialogs
        {
            get
            {
                return _ConvertEnterToTabForDialogs;
            }
            set
            {
                _ConvertEnterToTabForDialogs = value;
            }
        }

        [System.ComponentModel.Description("Gets or sets the Separator Color")]
        [System.ComponentModel.Category("Separator")]
        public System.Drawing.Color SeparatorColor
        {
            get
            {
                return _separatorColor;
            }
            set
            {
                _separatorColor = value;
            }
        }

        [System.ComponentModel.Category("Separator")]
        [System.ComponentModel.Description("Gets or sets the Separator Margin")]
        public int SeparatorMargin
        {
            get
            {
                return _separatorMargin;
            }
            set
            {
                _separatorMargin = value;
            }
        }

        [System.ComponentModel.Description("Gets or sets the Separator Style")]
        [System.ComponentModel.Category("Separator")]
        public System.Drawing.Drawing2D.DashStyle SeparatorStyle
        {
            get
            {
                return _separatorStyle;
            }
            set
            {
                _separatorStyle = value;
            }
        }

        [System.ComponentModel.Category("Separator")]
        [System.ComponentModel.Description("Gets or sets the Separator Width")]
        public int SeparatorWidth
        {
            get
            {
                return _separatorWidth;
            }
            set
            {
                _separatorWidth = value;
            }
        }

        public StyledComboBox()
        {
            //if (!DesignMode)
            LP.Validate();
            borderDrawer = new Oranikle.Studio.Controls.BorderDrawer();
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            _separatorStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            _separatorColor = System.Drawing.Color.FromArgb(90, 90, 90);
            _separatorMargin = 1;
            _separatorWidth = 1;
            _autoAdjustItemHeight = false;
            borderDrawer.RegisterFocusHoverControl(this);
        }

        protected void BaseOnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
        }

        public void EndEditToDataBinding(System.Windows.Forms.ContainerControl mainControl)
        {
            foreach (System.Windows.Forms.Binding binding in DataBindings)
            {
                binding.WriteValue();
            }
            mainControl.ActiveControl = null;
        }

        [System.Diagnostics.DebuggerStepThrough]
        private void HandleDrawSimpleBackground(ref System.Windows.Forms.Message message)
        {
            if ((message.Msg == 15) && (DropDownStyle == System.Windows.Forms.ComboBoxStyle.Simple))
            {
                System.IntPtr intPtr = Oranikle.Studio.Controls.StyledComboBox.GetWindowDC(message.HWnd);
                if (intPtr != System.IntPtr.Zero)
                {
                    System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHdc(intPtr);
                    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(0, 0, Width, Height);
                    System.Drawing.Color color = Enabled ? System.Drawing.Color.FromArgb(90, 90, 90) : System.Drawing.SystemColors.Control;
                    System.Drawing.Pen pen = new System.Drawing.Pen(color, 6.0F);
                    graphics.DrawRectangle(pen, rectangle);
                    message.Result = (System.IntPtr)1;
                    Oranikle.Studio.Controls.StyledComboBox.ReleaseDC(message.HWnd, intPtr);
                    pen.Dispose();
                    graphics.Dispose();
                }
            }
        }

        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            int i1 = e.Index;
            if (i1 < 0)
                return;
            if (Items[i1].ToString() == "-----")
                return;
            bool flag = false;
            if (((i1 - 1) > 0) && Items[i1 - 1].ToString() == "-----")
                flag = true;
            if (!Enabled)
            {
                System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.SystemColors.Control);
                e.Graphics.FillRectangle(brush, ClientRectangle);
                brush.Dispose();
            }
            else
            {
                e.DrawBackground();
            }
            System.Drawing.Graphics graphics = e.Graphics;
            System.Drawing.Rectangle rectangle1 = e.Bounds;
            System.Drawing.Point point1 = rectangle1.Location;
            int i2 = point1.Y + _separatorWidth - 8;
            if (flag)
            {
                System.Drawing.Pen pen = new System.Drawing.Pen(_separatorColor, (float)_separatorWidth);
                pen.DashStyle = _separatorStyle;
                System.Drawing.Rectangle rectangle2 = e.Bounds;
                System.Drawing.Point point2 = rectangle2.Location;
                System.Drawing.Rectangle rectangle3 = e.Bounds;
                System.Drawing.Point point3 = rectangle3.Location;
                System.Drawing.Rectangle rectangle4 = e.Bounds;
                graphics.DrawLine(pen, point2.X + _separatorMargin, i2, point3.X + rectangle4.Width - _separatorMargin, i2);
                pen.Dispose();
            }
            System.Drawing.Rectangle rectangle5 = e.Bounds;
            System.Drawing.Point point4 = rectangle5.Location;
            i2 = point4.Y + _separatorWidth;
            string s = Items[i1].ToString();
            if (!System.String.IsNullOrEmpty(DisplayMember))
            {
                System.Reflection.PropertyInfo propertyInfo = Items[i1].GetType().GetProperty(DisplayMember);
                s = propertyInfo.GetValue(Items[i1], null).ToString();
            }
            System.Drawing.Color color = ForeColor;
            if (System.Windows.Forms.DrawItemState.Selected == (System.Windows.Forms.DrawItemState.Selected & e.State))
                color = System.Drawing.SystemColors.HighlightText;
            if (System.Windows.Forms.DrawItemState.Disabled == (System.Windows.Forms.DrawItemState.Disabled & e.State))
                color = System.Drawing.SystemColors.GrayText;
            System.Drawing.Rectangle rectangle6 = e.Bounds;
            System.Windows.Forms.TextRenderer.DrawText(graphics, s, e.Font, rectangle6.Location, color);
            base.OnDrawItem(e);
        }

        public virtual void OnEnterPressed()
        {
            if (EnterPressed != null)
                EnterPressed(this, System.EventArgs.Empty);
        }

        protected override void OnMeasureItem(System.Windows.Forms.MeasureItemEventArgs e)
        {
            if (_autoAdjustItemHeight)
                e.ItemHeight += _separatorWidth;
            base.OnMeasureItem(e);
        }

        protected override void OnSizeChanged(System.EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys k)
        {
            if ((m.Msg == 256) && (k == System.Windows.Forms.Keys.Enter))
            {
                System.Windows.Forms.Form form = FindForm();
                if (ConvertEnterToTabForDialogs || (form == null) || (form.AcceptButton == null))
                {
                    System.Windows.Forms.SendKeys.Send("\t");
                    OnEnterPressed();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref m, k);
        }

        public virtual void ReadDataBindings()
        {
            foreach (System.Windows.Forms.Binding binding in DataBindings)
            {
                binding.ReadValue();
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            HandleDrawSimpleBackground(ref m);
            borderDrawer.DrawBorder(ref m, Width, Height);
        }

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr GetDCEx(System.IntPtr hwnd, System.IntPtr hrgnclip, uint fdwOptions);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern System.IntPtr GetWindowDC(System.IntPtr hwnd);

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private static extern int ReleaseDC(System.IntPtr hwnd, System.IntPtr hDC);

    } 
}
