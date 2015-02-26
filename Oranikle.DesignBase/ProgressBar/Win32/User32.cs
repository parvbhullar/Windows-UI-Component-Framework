using System;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Oranikle.Studio.Controls.ProgressBar.Win32
{
    public partial class User32
    {
        #region Struct

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public override string ToString()
            {
                return "{left=" + left.ToString() + ", " + "top=" + top.ToString() + ", " +
                    "right=" + right.ToString() + ", " + "bottom=" + bottom.ToString() + "}";
            }
        }
        
        #endregion

        #region UnmanagedMethods
        
        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool SetWindowText(IntPtr hwnd, String lpString);
        
        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int GetWindowTextLength(IntPtr hWnd);

        /* Sample Code for GetWindowText API
            public static string GetText(IntPtr hWnd)
            {
                // Allocate correct string length first
                int length = GetWindowTextLength(hWnd);
                StringBuilder sb = new StringBuilder(length + 1);
                GetWindowText(hWnd, sb, sb.Capacity);
                return sb.ToString();
            } */

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(
            IntPtr hwnd,
            int wMsg,
            int wParam,
            int lParam);
        
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(
            IntPtr hwnd,
            int wMsg,
            IntPtr wParam,
            IntPtr lParam);

        [DllImport("user32")]
        internal static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern bool ReleaseCapture();

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hwndParent);
        
        /// <summary>
        /// The GetParent function retrieves a handle to the specified window's parent or owner.
        /// </summary>
        /// <param name="hwnd">Handle to the window whose parent window handle is to be retrieved.</param>
        /// <returns>If the window is a child window, the return value is a handle to the parent window. If the window is a top-level window, the return value is a handle to the owner window. If the window is a top-level unowned window or if the function fails, the return value is NULL.</returns>
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hwnd);

        /// <summary>
        /// The FindWindowEx function retrieves a handle to a window whose class name and window name match the specified strings. The function searches child windows, beginning with the one following the specified child window.
        /// </summary>
        /// <param name="hwndParent">Handle to the parent window whose child windows are to be searched.</param>
        /// <param name="hwndChildAfter">Handle to a child window.</param>
        /// <param name="lpszClass">Specifies class name.</param>
        /// <param name="lpszWindow">Pointer to a null-terminated string that specifies the window name (the window's title).</param>
        /// <returns>If the function succeeds, the return value is a handle to the window that has the specified class and window names.If the function fails, the return value is NULL.</returns>
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern IntPtr FindWindowEx(
            IntPtr hwndParent,
            IntPtr hwndChildAfter,
            [MarshalAs(UnmanagedType.LPTStr)]
			string lpszClass,
            [MarshalAs(UnmanagedType.LPTStr)]
			string lpszWindow);

        /// <summary>
        /// The MoveWindow function changes the position and dimensions of the specified window.
        /// </summary>
        /// <param name="hwnd">Handle to the window.</param>
        /// <param name="X">Specifies the new position of the left side of the window.</param>
        /// <param name="Y">Specifies the new position of the top of the window.</param>
        /// <param name="nWidth">Specifies the new width of the window.</param>
        /// <param name="nHeight">Specifies the new height of the window.</param>
        /// <param name="bRepaint">If the bRepaint parameter is TRUE, the system sends the WM_PAINT message to the window procedure immediately after moving the window (that is, the MoveWindow function calls the UpdateWindow function). If bRepaint is FALSE, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</param>
        /// <returns>If the function succeeds, the return value is true.If the function fails, the return value is false.</returns>
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern bool MoveWindow(
            IntPtr hwnd,
            int X,
            int Y,
            int nWidth,
            int nHeight,
            bool bRepaint);

        /// <summary>
        /// The InvalidateRect function adds a rectangle to the specified window's update region.
        /// </summary>
        /// <param name="hwnd">Handle to window.</param>
        /// <param name="rect">Rectangle coordinates.</param>
        /// <param name="bErase">Erase state.</param>
        /// <returns>If the function succeeds, the return value is true.If the function fails, the return value is false.</returns>
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern bool InvalidateRect(
            IntPtr hwnd,
            ref Rectangle rect,
            bool bErase);

        /// <summary>
        /// The ValidateRect function validates the client area within a rectangle by removing the rectangle from the update region of the specified window.
        /// </summary>
        /// <param name="hwnd">Handle to window.</param>
        /// <param name="rect">Validation rectangle coordinates.</param>
        /// <returns>If the function succeeds, the return value is true.If the function fails, the return value is false.</returns>
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern bool ValidateRect(
            IntPtr hwnd,
            ref Rectangle rect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern int GetWindowLong(
            IntPtr hWnd,
            int dwStyle);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetDesktopWindow();

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern int GetClientRect(
            IntPtr hwnd,
            ref RECT rc);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern int GetClientRect(
            IntPtr hwnd,
            [In, Out] ref Rectangle rect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern bool GetWindowRect(
            IntPtr hWnd,
            [In, Out] ref Rectangle rect);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern int GetWindowRect(
            IntPtr hwnd,
            ref RECT rc);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern int ShowWindow(
            IntPtr hWnd,
            int nCmdShow);

        [DllImport("user32", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate,
                          IntPtr hrgnUpdate, uint flags); 

        // Changes flags that modify attributes of the layered window such as alpha(opacity).
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey,
           byte bAlpha, uint dwFlags);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern uint SetWindowLong(
            IntPtr hWnd,
            int nIndex,
            int dwNewLong);

        /// <summary>
        /// Changes the size, position, and Z order of a child, pop-up, or top-level window.
        /// These windows are ordered according to their appearance on the screen.
        /// The topmost window receives the highest rank and is the first window in the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="hWndAfter">A handle to the window to precede the positioned window in the Z order. This parameter must be a window handle or one of the following values.</param>
        /*  
            HWND_BOTTOM : 1
            Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows. 
            HWND_NOTOPMOST : -2
            Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
            HWND_TOP : 0
            Places the window at the top of the Z order.
            HWND_TOPMOST : -1
            Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated. */
        /// <param name="X">Specifies the new position of the left side of the window, in client coordinates.</param>
        /// <param name="Y">Specifies the new position of the top of the window, in client coordinates.</param>
        /// <param name="Width">Specifies the new width of the window, in pixels.</param>
        /// <param name="Height">Specifies the new height of the window, in pixels.</param>
        /// <param name="flags">Specifies the window sizing and positioning flags. This parameter can be a combination of the following values.</param>
        /// <returns>If the function succeeds, the return value is nonzero, if the function fails, the return value is zero.</returns>
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern int SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndAfter,
            int X,
            int Y,
            int Width,
            int Height,
            FlagsSetWindowPos flags);

        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
           int Y, int cx, int cy, uint uFlags);
        
        [DllImport("user32", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        internal static extern bool SystemParametersInfoSet(uint action, uint param, uint vparam, uint init);

        #region SystemMenuAPI

        [DllImport("user32")]
        internal static extern IntPtr CreateMenu();

        [DllImport("user32")]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32")]
        internal static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32")]
        internal static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32")]
        internal static extern IntPtr CreatePopupMenu();

        [DllImport("user32")]
        internal static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, MenuFlags uFlags);

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool InsertMenu(IntPtr hmenu, uint position, MenuFlags flags,
               uint item_id, [MarshalAs(UnmanagedType.LPTStr)]string item_text);
        
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal static extern bool AppendMenu(IntPtr hMenu, MenuFlags uFlags, uint uIDNewItem, string lpNewItem);
        
        #endregion

        #endregion
    }
}