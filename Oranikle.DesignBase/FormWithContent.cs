using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
    public class FormWithContent : System.Windows.Forms.Form, Oranikle.Studio.Controls.IFormWithContent
    {

        private static readonly log4net.ILog log;

        private Oranikle.Studio.Controls.FormClickedEventHandler events;
        private Oranikle.Studio.Controls.WeakEvent<System.EventArgs> onMoveEvent;

        public event Oranikle.Studio.Controls.FormClickedEventHandler UnicastFormClicked
        {
            add
            {
                events = value;
            }
            remove
            {
                if (events == value)
                    events = null;
            }
        }

        public Oranikle.Studio.Controls.WeakEvent<System.EventArgs> OnMoveEvent
        {
            get
            {
                return onMoveEvent;
            }
        }

        public FormWithContent()
        {
            onMoveEvent = new Oranikle.Studio.Controls.WeakEvent<System.EventArgs>();
         
        }

        static FormWithContent()
        {
            Oranikle.Studio.Controls.FormWithContent.log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        protected virtual void OnFormClicked(Oranikle.Studio.Controls.FormClickedEventArgs e)
        {
            if (events != null)
                events(this, e);
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        }

        protected override void OnMove(System.EventArgs e)
        {
            base.OnMove(e);
            OnMoveEvent.Invoke(this, e);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 528)
            {
                System.IntPtr intPtr = m.WParam;
                if (intPtr.ToInt32() == 513)
                {
                    System.Drawing.Point point = PointToScreen(new System.Drawing.Point((int)m.LParam));
                    OnFormClicked(new Oranikle.Studio.Controls.FormClickedEventArgs(point));
                }
            }
            base.WndProc(ref m);
        }
      
    }
}
