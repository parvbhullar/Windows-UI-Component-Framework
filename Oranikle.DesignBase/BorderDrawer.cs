using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    [System.Diagnostics.DebuggerStepThrough]
    public class BorderDrawer
    {

        private System.Drawing.Color borderColor;

        public static System.Drawing.Color BORDER_FOCUS_COLOR;
        public static System.Drawing.Color BORDER_UNFOCUS_COLOR;

        public System.Drawing.Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
            }
        }

        public BorderDrawer()
        {
            borderColor = Oranikle.Studio.Controls.BorderDrawer.BORDER_UNFOCUS_COLOR;
        }

        static BorderDrawer()
        {
            Oranikle.Studio.Controls.BorderDrawer.BORDER_UNFOCUS_COLOR = System.Drawing.Color.LightGray;
            
            //Oranikle.Studio.Controls.BorderDrawer.BORDER_FOCUS_COLOR = System.Drawing.Color.DarkGray;
            Oranikle.Studio.Controls.BorderDrawer.BORDER_FOCUS_COLOR = System.Drawing.Color.FromArgb(60, 176, 232); //System.Drawing.Color.Gold;
        }

        private void ctrl_GotFocus(object sender, System.EventArgs e)
        {
            borderColor = Oranikle.Studio.Controls.BorderDrawer.BORDER_FOCUS_COLOR;
            ((System.Windows.Forms.Control)sender).Invalidate();
        }

        private void ctrl_LostFocus(object sender, System.EventArgs e)
        {
            borderColor = Oranikle.Studio.Controls.BorderDrawer.BORDER_UNFOCUS_COLOR;
            ((System.Windows.Forms.Control)sender).Invalidate();
        }

        public void DrawBorder(ref System.Windows.Forms.Message message, int x, int y, int width, int height)
        {
            if (message.Msg == 15)
            {
                System.IntPtr intPtr = Oranikle.Studio.Controls.BorderDrawer.GetWindowDC(message.HWnd);
                if (intPtr != System.IntPtr.Zero)
                {
                    System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHdc(intPtr);
                    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(x, y, width, height);
                    System.Windows.Forms.ControlPaint.DrawBorder(graphics, rectangle, borderColor, System.Windows.Forms.ButtonBorderStyle.Solid);
                    message.Result = (System.IntPtr)1;
                    Oranikle.Studio.Controls.BorderDrawer.ReleaseDC(message.HWnd, intPtr);
                    graphics.Dispose();
                }
            }
        }

        public void DrawBorder(ref System.Windows.Forms.Message message, int width, int height)
        {
            DrawBorder(ref message, 0, 0, width, height);
        }

        public void RegisterFocusHoverControl(System.Windows.Forms.Control ctrl)
        {
            ctrl.GotFocus += new System.EventHandler(ctrl_GotFocus);
            ctrl.LostFocus += new System.EventHandler(ctrl_LostFocus);
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
