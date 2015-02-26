using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public class StyledTextBox : System.Windows.Forms.TextBox, Oranikle.Studio.Controls.IControlWithBorder, Oranikle.Studio.Controls.IEndEditToDataBinding
    {

        private static readonly log4net.ILog log;

        private bool _ConvertEnterToTab;
        private bool _ConvertEnterToTabForDialogs;
        protected Oranikle.Studio.Controls.BorderDrawer borderDrawer;

        [System.NonSerialized]
        [System.ComponentModel.Description("This is fired when the enter key is pressed.  It will later be translated into a tab key, so KeyDown will not be fired for enter.")]
        private Oranikle.Studio.Controls.WeakEvent<System.EventArgs> enterPressed;

        public System.Drawing.Color BorderColor
        {
            get
            {
                return borderDrawer.BorderColor;
            }
            set
            {
                borderDrawer.BorderColor = value;
            }
        }

        public bool ConvertEnterToTab
        {
            get
            {
                return _ConvertEnterToTab;
            }
            set
            {
                _ConvertEnterToTab = value;
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

        public Oranikle.Studio.Controls.WeakEvent<System.EventArgs> EnterPressed
        {
            get
            {
                return enterPressed;
            }
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                if (value == System.Drawing.Color.Transparent)
                {
                    if (Parent == null)
                        base.BackColor = System.Drawing.Color.White;
                    else
                        base.BackColor = GetParentColor(Parent);
                }
                else
                {
                    base.BackColor = value;
                }
                Invalidate();
            }
        }

        public StyledTextBox()
        {
            //if (!DesignMode)
              LP.Validate();
            _ConvertEnterToTab = true;
            borderDrawer = new Oranikle.Studio.Controls.BorderDrawer();
            enterPressed = new Oranikle.Studio.Controls.WeakEvent<System.EventArgs>();
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ShortcutsEnabled = true;
            borderDrawer.RegisterFocusHoverControl(this);
        }

        static StyledTextBox()
        {
            Oranikle.Studio.Controls.StyledTextBox.log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void EndEditToDataBinding(System.Windows.Forms.ContainerControl mainControl)
        {
            mainControl.ActiveControl = null;
        }

        private System.Drawing.Color GetParentColor(System.Windows.Forms.Control c)
        {
            if (c.BackColor == System.Drawing.Color.Transparent)
            {
                if (c.Parent == null)
                    return System.Drawing.Color.White;
                return GetParentColor(c.Parent);
            }
            return c.BackColor;
        }

        public System.Windows.Forms.Control PreviousControl()
        {
            System.Windows.Forms.Form form = FindForm();
            System.Windows.Forms.Control control = this;
            do
            {
                control = form.GetNextControl(control, false);
            } while ((control != null) && !control.TabStop);
            return control;
        }

        public System.IntPtr SendMessageToMe(int msg, System.IntPtr wParam, System.IntPtr lParam)
        {
            return Oranikle.Studio.Controls.StyledTextBox.SendMessage(new System.Runtime.InteropServices.HandleRef(Handle, Handle), msg, wParam, lParam);
        }

        public virtual void OnEnterPressed()
        {
            enterPressed.Invoke(this, System.EventArgs.Empty);
        }

        protected override void OnLocationChanged(System.EventArgs e)
        {
            base.OnLocationChanged(e);
            Invalidate();
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys k)
        {
            if ((m.Msg == 256) && (k == System.Windows.Forms.Keys.Enter) && !AcceptsReturn && ConvertEnterToTab)
            {
                System.Windows.Forms.Form form = FindForm();
                if (ConvertEnterToTabForDialogs || (form == null) || (form.AcceptButton == null))
                {
                    System.Windows.Forms.SendKeys.Send("\t");
                    OnEnterPressed();
                    return true;
                }
                return base.ProcessCmdKey(ref m, k);
            }
            if ((m.Msg == 256) && (k == (System.Windows.Forms.Keys.Back | System.Windows.Forms.Keys.Control)))
            {
                if (Text.Length == 0)
                    return true;
                if (Text.LastIndexOf(' ') == (Text.Length - 1))
                {
                    int i = Text.Substring(0, Text.Length - 1).LastIndexOf(' ');
                    if (i < 0)
                        i = 0;
                    Text = Text.Remove(i);
                }
                else
                {
                    Text = Text.Remove(Text.LastIndexOf(' ') + 1);
                }
                SelectionStart = Text.Length;
                SelectionLength = 0;
                return true;
            }
            return base.ProcessCmdKey(ref m, k);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if ((BorderStyle != System.Windows.Forms.BorderStyle.None) && Visible)
                borderDrawer.DrawBorder(ref m, Width, Height);
        }

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern System.IntPtr SendMessage(System.Runtime.InteropServices.HandleRef hWnd, int msg, System.IntPtr wParam, System.IntPtr lParam);

    } 

}
