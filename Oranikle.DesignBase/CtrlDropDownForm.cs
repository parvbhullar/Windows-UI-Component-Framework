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
    public partial class CtrlDropDownForm : Form, Oranikle.Studio.Controls.ICustomResizeForm
    {
        protected static readonly log4net.ILog log;

        private int _PreferredWidth;
        private Oranikle.Studio.Controls.CtrlDropdownHost host;
        private int oldHoriOffset;
        private int oldVerOffset;

        public static int SW_SHOWNOACTIVATE;

        public CtrlDropDownForm()
        {
            Visible = false;
        }

        static CtrlDropDownForm()
        {
            Oranikle.Studio.Controls.CtrlDropDownForm.log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            Oranikle.Studio.Controls.CtrlDropDownForm.SW_SHOWNOACTIVATE = 4;
        }

        public Oranikle.Studio.Controls.CtrlDropdownHost Host
        {
            get
            {
                return host;
            }
            set
            {
                if (host != value)
                {
                    host = value;
                    DoAfterHostChanged();
                }
            }
        }

        public int PreferredWidth
        {
            get
            {
                return _PreferredWidth;
            }
            set
            {
                _PreferredWidth = value;
            }
        }

        private void CtrlDropDownForm_FormClicked(object sender, Oranikle.Studio.Controls.FormClickedEventArgs e)
        {
            if ((Host == null) || Host.IsDisposed)
                return;
            System.Drawing.Rectangle rectangle = Host.RectangleToScreen(Host.ClientRectangle);
            if (rectangle.Contains(e.ClickLocation))
                return;
            if ((Host != null) && (Host.ParentForm != null))
                Host.Dismissed = true;
            UpdateVisibility();
        }

        public void CustomSetSize(int width, int height)
        {
            PreferredWidth = width;
            Size = new System.Drawing.Size(width, height);
        }

        public void HideSubForm()
        {
            DoBeforeHideSubForm();
            if ((Host != null) && (Host.ParentForm is Oranikle.Studio.Controls.IFormWithContent))
                (Host.ParentForm as Oranikle.Studio.Controls.IFormWithContent).UnicastFormClicked -= new Oranikle.Studio.Controls.FormClickedEventHandler(CtrlDropDownForm_FormClicked);
            //if ((System.Windows.Forms.Form.ActiveForm == this) && (Host != null) && (Host.ParentForm != null))
            //    Host.ParentForm.Activate();
            Visible = false;
        }

        public void Reposition(int horizontalOffset, int verticalOffset)
        {
            int i5, i6;

            if ((host == null) || host.Disposing || host.IsDisposed)
                return;
            System.Drawing.Rectangle rectangle1 = host.RectangleToScreen(host.ClientRectangle);
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            System.Drawing.Rectangle rectangle2 = System.Windows.Forms.Screen.FromControl(host).Bounds;
            int i1 = rectangle2.Right - (rectangle1.Left + horizontalOffset);
            int i2 = rectangle1.Right + horizontalOffset - rectangle2.Left;
            int i3 = rectangle2.Bottom - (rectangle1.Bottom + verticalOffset);
            int i4 = rectangle1.Top + verticalOffset - rectangle2.Top;
            if ((Width > i1) && (Width > i2))
            {
                System.Drawing.Size size1 = MinimumSize;
                PreferredWidth = System.Math.Max(System.Math.Max(i1, i2), size1.Width);
                SetBoundedWidth();
            }
            if ((Height > i4) && (Height > i3))
            {
                System.Drawing.Size size2 = MinimumSize;
                Height = System.Math.Max(System.Math.Max(i4, i3), size2.Height);
            }
            if (i1 >= Width)
                i5 = rectangle1.Left + horizontalOffset;
            else
                i5 = rectangle1.Right + horizontalOffset - Width;
            if (i3 >= Height)
                i6 = rectangle1.Bottom + verticalOffset;
            else
                i6 = rectangle1.Top + verticalOffset - Height;
            Location = new System.Drawing.Point(i5, i6);
            oldHoriOffset = horizontalOffset;
            oldVerOffset = verticalOffset;
        }

        public void RepositionWithOldOffset()
        {
            Reposition(oldHoriOffset, oldVerOffset);
        }

        protected void SetBoundedWidth()
        {
            int i = PreferredWidth;
            if (i < DoGetMinimumWidth())
                i = DoGetMinimumWidth();
            if ((Host != null) && (i < Host.Width))
                i = Host.Width;
            Width = i;
            RepositionWithOldOffset();
        }

        public void SetHost(Oranikle.Studio.Controls.CtrlDropdownHost host)
        {
            SetHost(host, 0, 0);
        }

        public void SetHost(Oranikle.Studio.Controls.CtrlDropdownHost host, int horizontalOffset, int verticalOffset)
        {
            Host = host;
            InitializeFromHost();
            Reposition(horizontalOffset, verticalOffset);
        }

        public void ShowWindowWithoutFocus()
        {
            SetBoundedWidth();
            DoBeforeVisible();
            Oranikle.Studio.Controls.CtrlDropDownForm.ShowWindow(Handle, Oranikle.Studio.Controls.CtrlDropDownForm.SW_SHOWNOACTIVATE);
            DoAfterVisible();
        }

        public void UpdateVisibility()
        {
            bool flag = GetExpectedVisibility();
            if (Visible != GetExpectedVisibility())
            {
                if (flag)
                {
                    if ((Host != null) && (Host.ParentForm is Oranikle.Studio.Controls.IFormWithContent))
                        (Host.ParentForm as Oranikle.Studio.Controls.IFormWithContent).UnicastFormClicked -= new Oranikle.Studio.Controls.FormClickedEventHandler(CtrlDropDownForm_FormClicked);
                    ShowWindowWithoutFocus();
                    return;
                }
                HideSubForm();
                if ((Host != null) && (Host.ParentForm != null))
                    Host.Dismissed = true;
            }
        }

        void CtrlDropDownForm_LocationChanged(object sender, EventArgs e)
        {
            
            HideSubForm();
        }

        protected virtual void DoAfterHostChanged()
        {
        }

        protected virtual void DoAfterVisible()
        {
        }

        protected virtual void DoBeforeHideSubForm()
        {
        }

        protected virtual void DoBeforeVisible()
        {
        }

        protected virtual int DoGetMinimumWidth()
        {
            return 0;
        }

        public virtual bool GetExpectedVisibility()
        {
           // Oranikle.Studio.Controls.CtrlDropDownForm.log.DebugFormat("Active Form {0}", System.Windows.Forms.Form.ActiveForm == null ? "null" : System.Windows.Forms.Form.ActiveForm.GetType().ToString());
            if (host == null)
            {
                Oranikle.Studio.Controls.CtrlDropDownForm.log.Debug("Invisible due to no host");
                return false;
            }
            if (host.Dismissed)
            {
                Oranikle.Studio.Controls.CtrlDropDownForm.log.Debug("Invisible due to already being dismissed");
                return false;
            }
            //
            if (host.ParentForm!= null && !(host.ParentForm is Form) && (System.Windows.Forms.Form.ActiveForm != host.ParentForm) && (System.Windows.Forms.Form.ActiveForm != this))
            {
                Oranikle.Studio.Controls.CtrlDropDownForm.log.Debug("Invisible due to host form lossing focus.");
                return false;
            }
            //if (!host.ContainsFocus && !host.Parent.ContainsFocus)
            //{
            //    Oranikle.Studio.Controls.CtrlDropDownForm.log.Debug("Invisible due to host not having focus");
            //    return false;
            //}
            if (host.ContainsFocus)
            {
                Oranikle.Studio.Controls.CtrlDropDownForm.log.Debug("Visible due to host having focus");
                return true;
            }
            if (System.Windows.Forms.Form.ActiveForm == this)
            {
                Oranikle.Studio.Controls.CtrlDropDownForm.log.Debug("Visible due to being active form");
                return true;
            }
            
            Oranikle.Studio.Controls.CtrlDropDownForm.log.Debug("Base says invisible");
            return false;
        }

        protected virtual void InitializeFromHost()
        {
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
                Hide();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData)
        {
            if ((keyData == System.Windows.Forms.Keys.Escape) && (Host != null))
            {
                Host.Dismissed = true;
                UpdateVisibility();
                return true;
            }
            return base.ProcessCmdKey(ref m, keyData);
        }

        protected override bool ProcessKeyEventArgs(ref System.Windows.Forms.Message m)
        {
            if ((m.Msg == 258) && (Host != null))
            {
                Host.SendMessageToMe(m.Msg, m.WParam, m.LParam);
                return true;
            }
            return false;
        }

        public virtual void UpdateFromText(string text)
        {

        }

        [System.Runtime.InteropServices.PreserveSig]
        [System.Runtime.InteropServices.DllImport("user32.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public static extern bool ShowWindow(System.IntPtr hWnd, int nCmdShow);

    }
}
