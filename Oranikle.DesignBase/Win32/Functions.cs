using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Oranikle.Studio.Controls.NWin32
{
  /// <summary>
  /// Windows API Functions
  /// </summary>
  public class WindowsAPI
  {
    #region Constructors
    // No need to construct this object
    private WindowsAPI()
    {
    }
    #endregion
    
    #region Constans values
    public const string TOOLBARCLASSNAME = "ToolbarWindow32";
    public const string REBARCLASSNAME = "ReBarWindow32";
    public const string PROGRESSBARCLASSNAME = "msctls_progress32";
    public const string SCROLLBAR = "SCROLLBAR";
    #endregion

    #region CallBacks
    public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
    #endregion

    #region Kernel32.dll functions
    [DllImport("kernel32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
    public static extern int GetCurrentThreadId();
    [DllImport("kernel32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
    public static extern int GetLastError();
    [DllImport("kernel32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
    public static extern void SetLastError( int error );
    #endregion
  
    #region Gdi32.dll functions
    [DllImport("gdi32.dll")]
    static public extern bool StretchBlt(IntPtr hDCDest, int XOriginDest, int YOriginDest, int WidthDest, int HeightDest,
      IntPtr hDCSrc,  int XOriginScr, int YOriginSrc, int WidthScr, int HeightScr, uint Rop);
    [DllImport("gdi32.dll")]
    static public extern IntPtr CreateCompatibleDC(IntPtr hDC);
    [DllImport("gdi32.dll")]
    static public extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int Width, int Heigth);
    [DllImport("gdi32.dll")]
    static public extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
    [DllImport("gdi32.dll")]
    static public extern bool BitBlt(IntPtr hDCDest, int XOriginDest, int YOriginDest, int WidthDest, int HeightDest,
      IntPtr hDCSrc,  int XOriginScr, int YOriginSrc, uint Rop);
    [DllImport("gdi32.dll")]
    static public extern IntPtr DeleteDC(IntPtr hDC);
    [DllImport("gdi32.dll")]
    static public extern bool PatBlt(IntPtr hDC, int XLeft, int YLeft, int Width, int Height, uint Rop);
    [DllImport("gdi32.dll")]
    static public extern bool DeleteObject(IntPtr hObject);
    [DllImport("gdi32.dll")]
    static public extern uint GetPixel(IntPtr hDC, int XPos, int YPos);
    [DllImport("gdi32.dll")]
    static public extern int SetMapMode(IntPtr hDC, int fnMapMode);
    [DllImport("gdi32.dll")]
    static public extern int GetObjectType(IntPtr handle);
    [DllImport("gdi32")]
    public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO_FLAT bmi, 
      int iUsage, ref int ppvBits, IntPtr hSection, int dwOffset);
    [DllImport("gdi32")]
    public static extern int GetDIBits(IntPtr hDC, IntPtr hbm, int StartScan, int ScanLines, int lpBits, BITMAPINFOHEADER bmi, int usage);
    [DllImport("gdi32")]
    public static extern int GetDIBits(IntPtr hdc, IntPtr hbm, int StartScan, int ScanLines, int lpBits, ref BITMAPINFO_FLAT bmi, int usage);
    [DllImport("gdi32")]
    public static extern IntPtr GetPaletteEntries(IntPtr hpal, int iStartIndex, int nEntries, byte[] lppe);
    [DllImport("gdi32")]
    public static extern IntPtr GetSystemPaletteEntries(IntPtr hdc, int iStartIndex, int nEntries, byte[] lppe);
    [DllImport("gdi32")]
    public static extern uint SetDCBrushColor(IntPtr hdc,  uint crColor);
    [DllImport("gdi32")]
    public static extern IntPtr CreateSolidBrush(uint crColor);
    [DllImport("gdi32")]
    public static extern int SetBkMode(IntPtr hDC, BackgroundMode mode);
    [DllImport("gdi32")]
    public static extern int SetViewportOrgEx(IntPtr hdc,  int x, int y,  int param);
    [DllImport("gdi32")]
    public static extern uint SetTextColor(IntPtr hDC, uint colorRef);
    [DllImport("gdi32")]
    public static extern int SetStretchBltMode(IntPtr hDC, int StrechMode);
    #endregion

    #region Uxtheme.dll functions
    [DllImport("uxtheme.dll")]
    static public extern int SetWindowTheme(IntPtr hWnd, string AppID, string ClassID);
    #endregion
  
    #region User32.dll functions
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern IntPtr GetDC(IntPtr hWnd);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern void PostQuitMessage( int exitCode );
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern IntPtr GetDesktopWindow();
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern bool ShowWindow(IntPtr hWnd, short State);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern bool UpdateWindow(IntPtr hWnd);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int Width, int Height, uint flags);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern bool OpenClipboard(IntPtr hWndNewOwner);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern bool CloseClipboard();
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern bool EmptyClipboard();
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern IntPtr SetClipboardData( uint Format, IntPtr hData);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint Item, ref RECT rc);
    [DllImport("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
    public static extern IntPtr GetParent(IntPtr hWnd);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int msg, ref int wParam, ref int lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref MSG lParam );
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern int SendMessage( IntPtr hWnd, int msg, int wParam, ref TOOLINFO lParam );
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern int SendMessage(IntPtr hWnd, SpinControlMsg msg, int wParam, ref UDACCEL lParam );
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref POINT lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTON lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTONINFO lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref REBARBANDINFO lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TVITEM lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref LVITEM lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref HDITEM lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref HD_HITTESTINFO hti);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref PARAFORMAT2 format);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int SendDlgItemMessage( IntPtr hWnd, int Id, int msg, int wParam, int lParam );
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr SetWindowsHookEx(int hookid, HookProc pfnhook, IntPtr hinst, int threadid);
    [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
    public static extern bool UnhookWindowsHookEx(IntPtr hhook);
    [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
    public static extern IntPtr CallNextHookEx(IntPtr hhook, int code, IntPtr wparam, IntPtr lparam);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr SetFocus(IntPtr hWnd);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public extern static int DrawText(IntPtr hdc, string lpString, int nCount, ref RECT lpRect, int uFormat);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public extern static IntPtr SetParent(IntPtr hChild, IntPtr hParent);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public extern static IntPtr GetDlgItem(IntPtr hDlg, int nControlID);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public extern static int GetClientRect(IntPtr hWnd, ref RECT rc);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public extern static int InvalidateRect(IntPtr hWnd,  IntPtr rect, int bErase);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    public extern static int InvalidateRect(IntPtr hWnd,  ref RECT rect, int bErase);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool WaitMessage();
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool PeekMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax, uint wFlag);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool GetMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool TranslateMessage(ref MSG msg);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool DispatchMessage(ref MSG msg);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr LoadCursor(IntPtr hInstance, uint cursor);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr SetCursor(IntPtr hCursor);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr GetFocus();
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool ReleaseCapture();
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool ClientToScreen(IntPtr hWnd, ref POINT pt);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENTS tme);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern ushort GetKeyState(int virtKey);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern int GetClassName(IntPtr hWnd,  out STRINGBUFFER ClassName, int nMaxCount);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern System.Int32 GetWindowLong( IntPtr hWnd, int nIndex );
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRegion, uint flags);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern IntPtr GetWindowDC(IntPtr hWnd);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern int FillRect(IntPtr hDC, ref RECT rect, IntPtr hBrush);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern int GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT wp);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern int SetWindowText(IntPtr hWnd, string text);
    [DllImport("User32.dll", CharSet=CharSet.Auto)]
    public static extern int GetWindowText(IntPtr hWnd, out STRINGBUFFER text, int maxCount);
    [DllImport("user32.dll", CharSet=CharSet.Auto)] 
    static public extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer); 
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int GetSystemMetrics(int nIndex);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int SetScrollInfo(IntPtr hwnd,  int bar, ref SCROLLINFO si, int fRedraw);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int ShowScrollBar(IntPtr hWnd, int bar,  int show);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int EnableScrollBar(IntPtr hWnd, uint flags, uint arrows);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int BringWindowToTop(IntPtr hWnd);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int GetScrollInfo(IntPtr hwnd, int bar, ref SCROLLINFO si);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy, 
      ref RECT rcScroll, ref RECT rcClip, IntPtr UpdateRegion, ref RECT rcInvalidated, uint flags);
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int IsWindow(IntPtr hWnd);
    [DllImport("shell32.Dll", CharSet=CharSet.Auto)]
    static public extern int Shell_NotifyIcon( NotifyCommand cmd, ref NOTIFYICONDATA data );
    [DllImport("User32.Dll", CharSet=CharSet.Auto)]
    static public extern int TrackPopupMenuEx( IntPtr hMenu, uint uFlags, int x, 
      int y, IntPtr hWnd, IntPtr ignore );
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int GetCursorPos( ref POINT pnt );
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int GetCaretPos( ref POINT pnt );
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int ValidateRect( IntPtr hWnd, ref RECT rc );
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern IntPtr FindWindowEx( IntPtr hWnd, IntPtr hChild, string strClassName, string strName );
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern IntPtr FindWindowEx( IntPtr hWnd, IntPtr hChild, string strClassName, IntPtr strName );
    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    static public extern int AnimateWindow( IntPtr hWnd, uint dwTime, uint dwFlags );
    #endregion

    #region Common Controls functions
    [DllImport("comctl32.dll")]
    public static extern bool InitCommonControlsEx(INITCOMMONCONTROLSEX icc);
    [DllImport("comctl32.dll")]
    public static extern bool InitCommonControls();
    [DllImport("comctl32.dll", EntryPoint="DllGetVersion")]
    public extern static int GetCommonControlDLLVersion(ref DLLVERSIONINFO dvi);
    [DllImport("comctl32.dll")]
    public static extern IntPtr ImageList_Create(int width, int height, uint flags, int count, int grow);
    [DllImport("comctl32.dll")]
    public static extern bool ImageList_Destroy(IntPtr handle);
    [DllImport("comctl32.dll")]
    public static extern int ImageList_Add(IntPtr imageHandle, IntPtr hBitmap, IntPtr hMask);
    [DllImport("comctl32.dll")]
    public static extern bool ImageList_Remove(IntPtr imageHandle, int index);
    [DllImport("comctl32.dll")]
    public static extern bool ImageList_BeginDrag(IntPtr imageHandle, int imageIndex, int xHotSpot, int yHotSpot);
    [DllImport("comctl32.dll")]
    public static extern bool ImageList_DragEnter(IntPtr hWndLock, int x, int y);
    [DllImport("comctl32.dll")]
    public static extern bool ImageList_DragMove(int x, int y);
    [DllImport("comctl32.dll")]
    public static extern bool ImageList_DragLeave(IntPtr hWndLock);
    [DllImport("comctl32.dll")]
    public static extern void ImageList_EndDrag();
    #endregion

    #region Win32 Macro-Like helpers
    public static int GET_X_LPARAM(int lParam)
    {
      return (lParam & 0xffff);
    }
   

    public static int GET_Y_LPARAM(int lParam)
    {
      return (lParam >> 16);
    }

    public static Point GetPointFromLPARAM(int lParam)
    {
      return new Point(GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam));
    }

    public static int LOW_ORDER(int param)
    {
      return (param & 0xffff);
    }

    public static int HIGH_ORDER(int param)
    {
      return (param >> 16);
    }


    public static int MAKELONG( int x, int y )
    {
      return (x + ( y << 16 ));
    }
    #endregion

    #region Shell32.Dll
    [DllImport("shell32.dll")]
    static public extern IntPtr ShellExecute( IntPtr hwnd,
      string lpOperation, string lpFile, string lpParameters, string lpDirectory,
      int nShowCmd );


    /// <summary>
    /// Retrieves a pointer to the Shell's IMalloc interface.
    /// </summary>
    /// <param name="hObject">Address of a pointer that receives the Shell's IMalloc interface pointer.</param>
    /// <returns></returns>
    [DllImport("shell32.dll")]
    public static extern Int32 SHGetMalloc(
      out IntPtr hObject);    
    
    /// <summary>
    /// Retrieves the path of a folder as an PIDL.
    /// </summary>
    /// <param name="hwndOwner">Handle to the owner window.</param>
    /// <param name="nFolder">A CSIDL value that identifies the folder to be located</param>
    /// <param name="hToken">Token that can be used to represent a particular user</param>
    /// <param name="dwReserved">Reserved</param>
    /// <param name="ppidl">Address of a pointer to an item identifier list structure specifying the folder's location relative to the root of the namespace (the desktop).</param>
    /// <returns></returns>
    [DllImport("shell32.dll")]
    public static extern Int32 SHGetFolderLocation(
      IntPtr hwndOwner,            
      Int32 nFolder,               
      IntPtr hToken,               
      UInt32 dwReserved,        
      out IntPtr ppidl);         

    /// <summary>
    /// Converts an item identifier list to a file system path. 
    /// </summary>
    /// <param name="pidl">Address of an item identifier list that specifies a file or directory location relative to the root of the namespace (the desktop). </param>
    /// <param name="pszPath">Address of a buffer to receive the file system path.</param>
    /// <returns></returns>
    [DllImport("shell32.dll")]
    public static extern Int32 SHGetPathFromIDList(
      IntPtr pidl,                
      StringBuilder pszPath);     

    /// <summary>
    /// Takes the CSIDL of a folder and returns the pathname.
    /// </summary>
    /// <param name="hwndOwner">Handle to an owner window.</param>
    /// <param name="nFolder">A CSIDL value that identifies the folder whose path is to be retrieved.</param>
    /// <param name="hToken">An access token that can be used to represent a particular user.</param>
    /// <param name="dwFlags">Flags to specify which path is to be returned. It is used for cases where the folder associated with a CSIDL may be moved or renamed by the user. </param>
    /// <param name="pszPath">Pointer to a null-terminated string which will receive the path.</param>
    /// <returns></returns>
    [DllImport("shell32.dll")]
    public static extern Int32 SHGetFolderPath(
      IntPtr hwndOwner,             
      Int32 nFolder,                 
      IntPtr hToken,                 
      UInt32 dwFlags,                
      StringBuilder pszPath);       

    /// <summary>
    /// Translates a Shell namespace object's display name into an item 
    /// identifier list and returns the attributes of the object. This function is 
    /// the preferred method to convert a string to a pointer to an item identifier 
    /// list (PIDL). 
    /// </summary>
    /// <param name="pszName">Pointer to a zero-terminated wide string that contains the display name  to parse. </param>
    /// <param name="pBndContext">Optional bind context that controls the parsing operation. This parameter is normally set to NULL.</param>
    /// <param name="ppidl">Address of a pointer to a variable of type ITEMIDLIST that receives the item identifier list for the object.</param>
    /// <param name="AttrToQuery">ULONG value that specifies the attributes to query.</param>
    /// <param name="ResultAttr">Pointer to a ULONG. On return, those attributes that are true for the object and were requested in AttrToQuery will be set. </param>
    /// <returns></returns>
    [DllImport("shell32.dll")]
    public static extern Int32 SHParseDisplayName(
      [MarshalAs(UnmanagedType.LPWStr)]
      String pszName,                
      IntPtr pBndContext,                    
      out IntPtr ppidl,              
      UInt32 AttrToQuery,                
      out UInt32 ResultAttr);         

    /// <summary>
    /// Retrieves the IShellFolder interface for the desktop folder,
    /// which is the root of the Shell's namespace. 
    /// </summary>
    /// <param name="ppshf">Address that receives an IShellFolder interface pointer for the desktop folder.</param>
    /// <returns></returns>
    [DllImport("shell32.dll")]
    public static extern Int32 SHGetDesktopFolder(
      out IntPtr ppshf);        

    /// <summary>
    /// This function takes the fully-qualified pointer to an item
    /// identifier list (PIDL) of a namespace object, and returns a specified
    /// interface pointer on the parent object.
    /// </summary>
    /// <param name="pidl">The item's PIDL. </param>
    /// <param name="riid">The REFIID of one of the interfaces exposed by the item's parent object.</param>
    /// <param name="ppv">A pointer to the interface specified by riid. You must release the object when you are finished.</param>
    /// <param name="ppidlLast">// The item's PIDL relative to the parent folder. This PIDL can be used with many of the methods supported by the parent folder's interfaces. If you set ppidlLast to NULL, the PIDL will not be returned. </param>
    /// <returns></returns>
    [DllImport("shell32.dll")]
    public static extern Int32 SHBindToParent(
      IntPtr pidl,            
      [MarshalAs(UnmanagedType.LPStruct)]
      Guid riid,                   
      out IntPtr ppv,             
      ref IntPtr ppidlLast);    

    /// <summary>
    /// Accepts a STRRET structure returned by
    /// ShellFolder::GetDisplayNameOf that contains or points to a string, and then
    /// returns that string as a BSTR.
    /// </summary>
    /// <param name="pstr">Pointer to a STRRET structure.</param>
    /// <param name="pidl">Pointer to an ITEMIDLIST uniquely identifying a file object or subfolder relative to the parent folder.</param>
    /// <param name="pbstr">Pointer to a variable of type BSTR that contains the converted string.</param>
    /// <returns></returns>
    [DllImport("shlwapi.dll")]
    public static extern Int32 StrRetToBSTR(
      ref STRRET pstr,    
      IntPtr pidl,        
      [MarshalAs(UnmanagedType.BStr)]
      out String pbstr);    

    /// <summary>
    /// Takes a STRRET structure returned by IShellFolder::GetDisplayNameOf,
    /// converts it to a string, and places the result in a buffer. 
    /// </summary>
    /// <param name="pstr">Pointer to the STRRET structure. When the function returns, this pointer will no longer be valid.</param>
    /// <param name="pidl">Pointer to the item's ITEMIDLIST structure.</param>
    /// <param name="pszBuf">Buffer to hold the display name. It will be returned as a null-terminated string. If cchBuf is too small, the name will be truncated to fit.</param>
    /// <param name="cchBuf">Size of pszBuf, in characters. If cchBuf is too small, the string will be truncated to fit. </param>
    /// <returns></returns>
    [DllImport("shlwapi.dll")]
    public static extern Int32 StrRetToBuf(
      ref STRRET pstr,    
      IntPtr pidl,        
      StringBuilder pszBuf,  
      UInt32 cchBuf);        

    /// <summary>
    /// Displays a dialog box that enables the user to select a Shell folder. 
    /// </summary>
    /// <param name="lbpi">// Pointer to a BROWSEINFO structure that contains information used to display the dialog box.</param>
    /// <returns></returns>
    [DllImport("shell32.dll")]
    public static extern IntPtr SHBrowseForFolder(
      ref BROWSEINFO lbpi);      

    public delegate IntPtr BrowseCallbackProc(IntPtr hwnd, int uMsg, IntPtr lParam, IntPtr lpData);
    #endregion
  }
}
