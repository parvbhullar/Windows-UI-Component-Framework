using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{

    internal class NativeUpDown : System.Windows.Forms.NativeWindow
    {

        private struct WINDOWPOS
        {

            public int cx;
            public int cy;
            public int flags;
            public System.IntPtr hwnd;
            public System.IntPtr hwndInsertAfter;
            public int x;
            public int y;

        }

        private const int WM_DESTROY = 2;
        private const int WM_NCDESTROY = 130;
        private const int WM_WINDOWPOSCHANGING = 70;

        private System.Drawing.Rectangle _bounds;

        internal System.Drawing.Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
        }

        public NativeUpDown()
        {
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if ((m.Msg == 2) || (m.Msg == 130))
            {
                ReleaseHandle();
            }
            else if (m.Msg == 70)
            {
                Oranikle.Studio.Controls.NativeUpDown.WINDOWPOS windowpos = (Oranikle.Studio.Controls.NativeUpDown.WINDOWPOS)m.GetLParam(typeof(Oranikle.Studio.Controls.NativeUpDown.WINDOWPOS));
                windowpos.x += windowpos.cx;
                System.Runtime.InteropServices.Marshal.StructureToPtr(windowpos, m.LParam, true);
                _bounds = new System.Drawing.Rectangle(windowpos.x, windowpos.y, windowpos.cx, windowpos.cy);
            }
            base.WndProc(ref m);
        }

    } // class NativeUpDown

}

