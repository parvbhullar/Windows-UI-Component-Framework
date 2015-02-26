using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Oranikle.Studio.Controls.NWin32
{
  
  /// <summary>
  /// Structures to interoperate with the Windows 32 API  
  /// </summary>

  /*
   * HANDLE - IntPtr
   * BYTE - Byte
   * SHORT - Int16
   * WORD - UInt16
   * INT - Int32
   * UINT - UInt32
   * LONG - INt32
   * BOOL - Int32
   * DWORD - UInt32
   * ULONG - UInt32
   * CHAR - Char
   * LPSTR - String
   * FLOAT - Single
   * DOUBLE - Double
  */

  #region CREATESTRUCT
  [StructLayout(LayoutKind.Sequential)]
  public struct CREATESTRUCT
  {
    IntPtr    lpCreateParams; 
    IntPtr    hInstance; 
    IntPtr    hMenu; 
    IntPtr    hwndParent; 
    int       cy; 
    int       cx; 
    int       y; 
    int       x; 
    Int32     style; 
    string    lpszName; 
    string    lpszClass; 
    UInt32    dwExStyle; 
  }
  #endregion

  #region SpinControls
  [StructLayout(LayoutKind.Sequential)]
  public struct UDACCEL
  {
    public  UInt32 nSec;
    public  UInt32 nInc;
  }
  #endregion
  
  #region SIZE
  [StructLayout(LayoutKind.Sequential)]
  public struct SIZE
  {
    public int cx;
    public int cy;
  }
  #endregion

  #region RECT
  [StructLayout(LayoutKind.Sequential)]
  public struct RECT
  {
    public int left;
    public int top;
    public int right;
    public int bottom;

    public static implicit operator Rectangle( RECT rect ) 
    {
      return Rectangle.FromLTRB( rect.left, rect.top, rect.right, rect.bottom );
    }
    
    public static implicit operator Size( RECT rect ) 
    {
      return new Size(rect.right - rect.left, rect.bottom - rect.top);
    }

    public static explicit operator RECT( Rectangle rect )
    {
      RECT rc = new RECT();
      
      rc.left = rect.Left;
      rc.right = rect.Right;
      rc.top = rect.Top;
      rc.bottom = rect.Bottom;

      return rc;
    }
  }

  #endregion

  #region INITCOMMONCONTROLSEX
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public class INITCOMMONCONTROLSEX 
  {
    public int dwSize;
    public int dwICC;
  }
  #endregion

  #region TBBUTTON
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct TBBUTTON 
  {
    public int iBitmap;
    public int idCommand;
    public byte fsState;
    public byte fsStyle;
    public byte bReserved0;
    public byte bReserved1;
    public int dwData;
    public int iString;
  }
  #endregion

  #region POINT
  [StructLayout(LayoutKind.Sequential)]
  public struct POINT
  {
    public int x;
    public int y;
    
    
    public POINT ( int X, int Y )
    {
      x = X;
      y = Y;
    }
    /// <summary>
    /// Point creation from lParam `s data
    /// </summary>
    /// <param name="lParam"></param>
    public POINT ( int lParam )
    {
      x = (lParam & 0xffff);
      y = (lParam >> 16);
    }
    
    public static implicit operator System.Drawing.Point( POINT p )
    {
      return new System.Drawing.Point( p.x, p.y );
    }
    
    public static implicit operator POINT( System.Drawing.Point p )
    {
      return new POINT( p.X, p.Y );
    }

  }
  #endregion

  #region NMHDR
  [StructLayout(LayoutKind.Sequential)]
  public struct NMHDR
  {
    public IntPtr hwndFrom;
    public int idFrom;
    public int code;
  }
  #endregion

  #region NMCUSTOMDRAW
  [StructLayout(LayoutKind.Sequential)]
  public struct NMCUSTOMDRAW
  {
    public NMHDR hdr;
    public int dwDrawStage;
    public IntPtr hdc;
    public RECT rc;
    public int dwItemSpec;
    public int uItemState;
    public int lItemlParam;
  }
  #endregion

  #region NMTBCUSTOMDRAW
  [StructLayout(LayoutKind.Sequential)]
  public struct NMTBCUSTOMDRAW
  {
    public NMCUSTOMDRAW nmcd;
    public IntPtr hbrMonoDither;
    public IntPtr hbrLines;
    public IntPtr hpenLines;
    public int clrText;
    public int clrMark;
    public int clrTextHighlight;
    public int clrBtnFace;
    public int clrBtnHighlight;
    public int clrHighlightHotTrack;
    public RECT rcText;
    public int nStringBkMode;
    public int nHLStringBkMode;
  }
  #endregion
  
  #region NMLVCUSTOMDRAW
  [StructLayout(LayoutKind.Sequential)]
  public struct NMLVCUSTOMDRAW 
  {
    public NMCUSTOMDRAW nmcd;
    public uint clrText;
    public uint clrTextBk;
    public int iSubItem;
  } 
  #endregion

  #region TOOLTIP CONTROL STRUCTS
    #region TOOLTIPTEXTA || NMTTDISPINFO
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    public struct TOOLTIPTEXTA
    {
      public NMHDR hdr;
      public IntPtr lpszText;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
      public string szText;
      public IntPtr hinst;
      public int uFlags;
    }
    #endregion

    #region TOOLTIPTEXT || NMTTDISPINFO
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
    public struct TOOLTIPTEXT
    {
      public NMHDR hdr;
      public IntPtr lpszText;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
      public string szText;
      public IntPtr hinst;
      public int uFlags;
    }
    #endregion
    
    #region NMTTCUSTOMDRAW
    [StructLayout(LayoutKind.Sequential)]
    public struct NMTTCUSTOMDRAW
    {
      public NMCUSTOMDRAW nmcd;
      public UInt32       uDrawFlags;
    }
    #endregion

    #region NMTTDISPINFO
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
    public struct NMTTDISPINFO
    {
      public NMHDR hdr;
      public IntPtr lpszText;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
      public string szText;
      public IntPtr hinst;
      public int uFlags;
    }
    #endregion

    #region TOOLINFO
    [StructLayout(LayoutKind.Sequential)]
    public struct TOOLINFO
    {
      public UInt32      cbSize; 
      public UInt32      uFlags; 
      public IntPtr      hwnd; 
      public IntPtr      uId;       // UINT_PTR
      public RECT        rect; 
      public IntPtr      hinst; 
      [ MarshalAs( UnmanagedType.LPTStr ) ]
      public string      lpszText;
      //public IntPtr      lpszText; 
      public IntPtr      lParam;
    }
    #endregion

    #region TTHITTESTINFO
    [StructLayout(LayoutKind.Sequential)]
    public struct TTHITTESTINFO
    {
      public IntPtr    hwnd;
      public POINT     pt;
      public TOOLINFO  ti;
    }
    #endregion

    #region TTGETTITLE
    [StructLayout(LayoutKind.Sequential)]
      public struct TTGETTITLE
    {    
      public UInt32  dwSize;
      public UInt32  uTitleBitmap;
      public UInt32  cch;
      public IntPtr  pszTitle;
    }
    #endregion
  #endregion

  #region TBBUTTONINFO
  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
  public struct TBBUTTONINFO
  {
    public int cbSize;
    public int dwMask;
    public int idCommand;
    public int iImage;
    public byte fsState;
    public byte fsStyle;
    public short cx;
    public IntPtr lParam;
    public IntPtr pszText;
    public int cchText;
  }
  #endregion

  #region REBARBANDINFO
  [StructLayout(LayoutKind.Sequential)]
  public struct REBARBANDINFO
  {
    public int cbSize;
    public int fMask;
    public int fStyle;
    public int clrFore;
    public int clrBack;
    public IntPtr lpText;
    public int cch;
    public int iImage;
    public IntPtr hwndChild;
    public int cxMinChild;
    public int cyMinChild;
    public int cx;
    public IntPtr hbmBack;
    public int wID;
    public int cyChild;
    public int cyMaxChild;
    public int cyIntegral;
    public int cxIdeal;
    public int lParam;
    public int cxHeader;
  }
  #endregion

  #region MOUSEHOOKSTRUCT
  [StructLayout(LayoutKind.Sequential)]
  public struct MOUSEHOOKSTRUCT 
  { 
    public POINT     pt; 
    public IntPtr    hwnd; 
    public int       wHitTestCode; 
    public IntPtr    dwExtraInfo; 
  }
  #endregion

  #region NMTOOLBAR
  [StructLayout(LayoutKind.Sequential)]
  public struct NMTOOLBAR 
  {
    public NMHDR    hdr;
    public int        iItem;
    public TBBUTTON     tbButton;
    public int        cchText;
    public IntPtr   pszText;
    public RECT       rcButton; 
  }
  #endregion
  
  #region NMREBARCHEVRON
  [StructLayout(LayoutKind.Sequential)]
  public struct NMREBARCHEVRON
  {
    public NMHDR hdr;
    public int uBand;
    public int wID;
    public int lParam;
    public RECT rc;
    public int lParamNM;
  }
  #endregion

  #region NMREBARCHILDSIZE
  [StructLayout(LayoutKind.Sequential)]
  public struct NMREBARCHILDSIZE
  {
    public NMHDR  hdr;
    public UInt32 uBand;
    public UInt32 wID;
    public RECT   rcChild;
    public RECT   rcBand;
  }; 
  #endregion

  #region BITMAP
  [StructLayout(LayoutKind.Sequential)]
  public struct BITMAP
  {
    public long   bmType; 
    public long   bmWidth; 
    public long   bmHeight; 
    public long   bmWidthBytes; 
    public short  bmPlanes; 
    public short  bmBitsPixel; 
    public IntPtr bmBits; 
  }
  #endregion
 
  #region BITMAPINFO_FLAT
  [StructLayout(LayoutKind.Sequential)]
  public struct BITMAPINFO_FLAT 
  {
    public int      bmiHeader_biSize;
    public int      bmiHeader_biWidth;
    public int      bmiHeader_biHeight;
    public short    bmiHeader_biPlanes;
    public short    bmiHeader_biBitCount;
    public int      bmiHeader_biCompression;
    public int      bmiHeader_biSizeImage;
    public int      bmiHeader_biXPelsPerMeter;
    public int      bmiHeader_biYPelsPerMeter;
    public int      bmiHeader_biClrUsed;
    public int      bmiHeader_biClrImportant;
    [MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1024)]
    public byte[] bmiColors; 
  }
  #endregion

  #region RGBQUAD
  public struct RGBQUAD 
  {
    public byte   rgbBlue;
    public byte   rgbGreen;
    public byte   rgbRed;
    public byte   rgbReserved;
  }
  #endregion
  
  #region BITMAPINFOHEADER
  [StructLayout(LayoutKind.Sequential)]
  public class BITMAPINFOHEADER 
  {
    public int      biSize = Marshal.SizeOf(typeof(BITMAPINFOHEADER));
    public int      biWidth;
    public int      biHeight;
    public short    biPlanes;
    public short    biBitCount;
    public int      biCompression;
    public int      biSizeImage;
    public int      biXPelsPerMeter;
    public int      biYPelsPerMeter;
    public int      biClrUsed;
    public int      biClrImportant;
  }
  #endregion

  #region BITMAPINFO
  [StructLayout(LayoutKind.Sequential)]
  public class BITMAPINFO 
  {
    public BITMAPINFOHEADER bmiHeader = new BITMAPINFOHEADER();
    [MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1024)]
    public byte[] bmiColors; 
  }
  #endregion

  #region PALETTEENTRY
  [StructLayout(LayoutKind.Sequential)]
  public struct PALETTEENTRY 
  {
    public byte   peRed;
    public byte   peGreen;
    public byte   peBlue;
    public byte   peFlags;
  }
  #endregion

  #region MSG
  [StructLayout(LayoutKind.Sequential)]
  public struct MSG 
  {
    public IntPtr hwnd;
    public int message;
    public IntPtr wParam;
    public IntPtr lParam;
    public int time;
    public int pt_x;
    public int pt_y;
  }
  #endregion

  #region HD_HITTESTINFO
  [StructLayout(LayoutKind.Sequential)]
  public struct HD_HITTESTINFO 
  {  
    public POINT pt;  
    public uint flags; 
    public int iItem; 
  }
  #endregion
 
  #region DLLVERSIONINFO
  [StructLayout(LayoutKind.Sequential)]
  public struct DLLVERSIONINFO
  {
    public int cbSize;
    public int dwMajorVersion;
    public int dwMinorVersion;
    public int dwBuildNumber;
    public int dwPlatformID;
  }
  #endregion

  #region PAINTSTRUCT
  [StructLayout(LayoutKind.Sequential)]
  public struct PAINTSTRUCT
  {
    public IntPtr hdc;
    public int fErase;
    public Rectangle rcPaint;
    public int fRestore;
    public int fIncUpdate;
    public int Reserved1;
    public int Reserved2;
    public int Reserved3;
    public int Reserved4;
    public int Reserved5;
    public int Reserved6;
    public int Reserved7;
    public int Reserved8;
  }
  #endregion

  #region BLENDFUNCTION
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct BLENDFUNCTION
  {
    public byte BlendOp;
    public byte BlendFlags;
    public byte SourceConstantAlpha;
    public byte AlphaFormat;
  }

  #endregion
  
  #region TRACKMOUSEEVENTS
  [StructLayout(LayoutKind.Sequential)]
  public struct TRACKMOUSEEVENTS
  {
    public uint cbSize;
    public uint dwFlags;
    public IntPtr hWnd;
    public uint dwHoverTime;
  }
  #endregion

  #region STRINGBUFFER
  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
  public struct STRINGBUFFER
  {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=512)]
    public string szText;
  }
  #endregion

  #region NMTVCUSTOMDRAW
  [StructLayout(LayoutKind.Sequential)]
  public struct NMTVCUSTOMDRAW 
  {
    public NMCUSTOMDRAW nmcd;
    public uint clrText;
    public uint clrTextBk;
    public int iLevel;
  }
  #endregion

  #region NMTREEVIEW
  [StructLayout(LayoutKind.Sequential)]
  public struct NMTREEVIEW 
  {
    public NMHDR   hdr;
    public uint    action;
    public TVITEM  itemOld;
    public TVITEM  itemNew;
    public POINT   ptDrag;
  };

  #endregion

  #region TVITEM
  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
  public struct TVITEM 
  {
    public  uint      mask;
    public  IntPtr    hItem;
    public  uint      state;
    public  uint      stateMask;
    public  IntPtr    pszText;
    public  int       cchTextMax;
    public  int       iImage;
    public  int       iSelectedImage;
    public  int       cChildren;
    public  int       lParam;
  } 
  #endregion

  #region TVHITTESTINFO
  [StructLayout(LayoutKind.Sequential)]
  public struct TVHITTESTINFO 
  {
    public  POINT       pt;
    public  UInt32      flags;
    public  IntPtr      hItem;

    public  TVHitTestFlags Flags
    {
      get
      {
        return (TVHitTestFlags)flags;
      }
      set
      {
        flags = (UInt32)value;
      }
    }
  
  };

  #endregion

  #region TVINSERTSTRUCT
  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
  public struct TVINSERTSTRUCT 
  {
    int hParent; 
    int hInsertAfter; 
    TVITEM item;
  }
  #endregion

	#region LVITEM
  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
  public struct LVITEM
  {
    public	uint mask;
    public	int iItem;
    public	int iSubItem;
    public	uint state;
    public	uint stateMask;
    public	IntPtr pszText;
    public	int cchTextMax;
    public	int iImage;
    public	int lParam;
    public	int iIndent;
  }
	#endregion

	#region HDITEM
  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
  public struct HDITEM
  {
    public	uint    mask;
    public	int     cxy;
    public	IntPtr  pszText;
    public	IntPtr  hbm;
    public	int     cchTextMax;
    public	int     fmt;
    public	int     lParam;
    public	int     iImage;      
    public	int     iOrder;
  }	
	#endregion

  #region WINDOWPLACEMENT
  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
  public struct WINDOWPLACEMENT
  {	
    public uint length; 
    public uint flags; 
    public uint showCmd; 
    public POINT ptMinPosition; 
    public POINT ptMaxPosition; 
    public RECT  rcNormalPosition; 
  }
	#endregion

	#region SCROLLINFO
  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
  public struct SCROLLINFO
  {
    public 	uint   cbSize;
    public 	uint   fMask;
    public 	int    nMin;
    public 	int    nMax;
    public 	uint   nPage;
    public 	int    nPos;
    public 	int    nTrackPos;
  }
	#endregion

  #region NOTIFYICONDATA
  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)] 
  public struct NOTIFYICONDATA
  {
    public UInt32           cbSize;                       // DWORD
    public IntPtr           hWnd;                         // HWND
    public UInt32           uID;                          // UINT
    public NotifyFlags      uFlags;                       // UINT
    public UInt32           uCallbackMessage;             // UINT
    public IntPtr           hIcon;                        // HICON
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=128)]
    public string           szTip;                        // char[128]
    public NotifyState      dwState;                      // DWORD   
    public NotifyState      dwStateMask;                  // DWORD
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=256)]
    public string           szInfo;                       // char[256]
    public UInt32           uTimeoutOrVersion;            // UINT
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=64)]
    public string           szInfoTitle;                  // char[64]
    public NotifyInfoFlags  dwInfoFlags;                  // DWORD
  }
  #endregion

  #region PARAFORMAT2
  /*
  typedef struct _paraformat { 
    UINT cbSize; 
    DWORD dwMask; 
    WORD  wNumbering; 
    WORD  wEffects; 
    LONG  dxStartIndent; 
    LONG  dxRightIndent; 
    LONG  dxOffset; 
    WORD  wAlignment; 
    SHORT cTabCount; 
    LONG  rgxTabs[MAX_TAB_STOPS]; 
    LONG  dySpaceBefore; 
    LONG  dySpaceAfter; 
    LONG  dyLineSpacing; 
    SHORT sStyle; 
    BYTE  bLineSpacingRule; 
    BYTE  bOutlineLevel; 
    WORD  wShadingWeight; 
    WORD  wShadingStyle;
    WORD  wNumberingStart; 
    WORD  wNumberingStyle; 
    WORD  wNumberingTab; 
    WORD  wBorderSpace; 
    WORD  wBorderWidth; 
    WORD  wBorders; 
  } PARAFORMAT2;
 */


  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
  public struct PARAFORMAT2 
  { 
    public UInt32  cbSize; 
    public UInt32  dwMask; 
    public UInt16  wNumbering; 
    public UInt16  wEffects; 
    public Int32   dxStartIndent; 
    public Int32   dxRightIndent; 
    public Int32   dxOffset; 
    public UInt16  wAlignment; 
    public Int16   cTabCount; 
    [MarshalAs( UnmanagedType.ByValArray, SizeConst=32) ]
    public Int32[] rgxTabs; 
    public Int32   dySpaceBefore; 
    public Int32   dySpaceAfter; 
    public Int32   dyLineSpacing; 
    public Int16   sStyle; 
    public Byte    bLineSpacingRule; 
    public Byte    bOutlineLevel; 
    public UInt16  wShadingWeight; 
    public UInt16  wShadingStyle;
    public UInt16  wNumberingStart; 
    public UInt16  wNumberingStyle; 
    public UInt16  wNumberingTab; 
    public UInt16  wBorderSpace; 
    public UInt16  wBorderWidth; 
    public UInt16  wBorders; 
  }  
  #endregion


  [ StructLayout( LayoutKind.Sequential ) ]
  public struct CHARRANGE 
  {
    public int cpMin;
    public int cpMax;
  }
  
  [ StructLayout( LayoutKind.Sequential ) ]
  public struct FORMATRANGE 
  {
    public IntPtr hdc; 
    public IntPtr hdcTarget; 
    public RECT rc; 
    public RECT rcPage; 
    public CHARRANGE chrg; 
  }


  #region Shell
  [ComImport]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("00000002-0000-0000-C000-000000000046")]
  public interface IMalloc 
  {
    // Allocates a block of memory.
    // Return value: a pointer to the allocated memory block.
    [PreserveSig]
    IntPtr Alloc(
      UInt32 cb);        // Size, in bytes, of the memory block to be allocated. 
        
    // Changes the size of a previously allocated memory block.
    // Return value:  Reallocated memory block 
    [PreserveSig]
    IntPtr Realloc(
      IntPtr pv,         // Pointer to the memory block to be reallocated.
      UInt32 cb);        // Size of the memory block (in bytes) to be reallocated.

    // Frees a previously allocated block of memory.
    [PreserveSig]
    void Free(
      IntPtr pv);        // Pointer to the memory block to be freed.

    // This method returns the size (in bytes) of a memory block previously
    // allocated with IMalloc::Alloc or IMalloc::Realloc.
    // Return value: The size of the allocated memory block in bytes 
    [PreserveSig]
    UInt32 GetSize(
      IntPtr pv);        // Pointer to the memory block for which the size
    // is requested.

    // This method determines whether this allocator was used to allocate
    // the specified block of memory.
    // Return value: 1 - allocated 0 - not allocated by this IMalloc instance. 
    [PreserveSig]
    Int16 DidAlloc(
      IntPtr pv);        // Pointer to the memory block

    // This method minimizes the heap as much as possible by releasing unused
    // memory to the operating system, 
    // coalescing adjacent free blocks and committing free pages.
    [PreserveSig]
    void HeapMinimize();
  }

  
  [ComImport]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("000214E6-0000-0000-C000-000000000046")]
  public interface IShellFolder 
  {
    // Translates a file object's or folder's display name into an item identifier list.
    // Return value: error code, if any
    [PreserveSig]
    Int32 ParseDisplayName( 
      IntPtr hwnd,            // Optional window handle
      IntPtr pbc,                 // Optional bind context that controls the
      // parsing operation. This parameter is 
      // normally set to NULL. 
      [MarshalAs(UnmanagedType.LPWStr)] 
      String pszDisplayName,    // Null-terminated UNICODE string with the
      // display name.
      ref UInt32 pchEaten,    // Pointer to a ULONG value that receives the
      // number of characters of the 
      // display name that was parsed.
      out IntPtr ppidl,           // Pointer to an ITEMIDLIST pointer that receives
      // the item identifier list for 
      // the object.
      ref UInt32 pdwAttributes); // Optional parameter that can be used to
    // query for file attributes.
    // this can be values from the SFGAO enum
        
    // Allows a client to determine the contents of a folder by creating an item
    // identifier enumeration object and returning its IEnumIDList interface.
    // Return value: error code, if any
    [PreserveSig]
    Int32 EnumObjects( 
      IntPtr hwnd,            // If user input is required to perform the
      // enumeration, this window handle 
      // should be used by the enumeration object as
      // the parent window to take 
      // user input.
      Int32 grfFlags,             // Flags indicating which items to include in the
      // enumeration. For a list 
      // of possible values, see the SHCONTF enum. 
      out IntPtr ppenumIDList); // Address that receives a pointer to the
    // IEnumIDList interface of the 
    // enumeration object created by this method. 

    // Retrieves an IShellFolder object for a subfolder.
    // Return value: error code, if any
    [PreserveSig]
    Int32 BindToObject( 
      IntPtr pidl,            // Address of an ITEMIDLIST structure (PIDL)
      // that identifies the subfolder.
      IntPtr pbc,                // Optional address of an IBindCtx interface on
      // a bind context object to be 
      // used during this operation.
      Guid riid,                  // Identifier of the interface to return. 
      out IntPtr ppv);        // Address that receives the interface pointer.
        
    // Requests a pointer to an object's storage interface. 
    // Return value: error code, if any
    [PreserveSig]
    Int32 BindToStorage( 
      IntPtr pidl,            // Address of an ITEMIDLIST structure that
      // identifies the subfolder relative 
      // to its parent folder. 
      IntPtr pbc,                // Optional address of an IBindCtx interface on a
      // bind context object to be 
      // used during this operation.
      Guid riid,                  // Interface identifier (IID) of the requested
      // storage interface.
      out IntPtr ppv);        // Address that receives the interface pointer specified by riid.
        
    // Determines the relative order of two file objects or folders, given their
    // item identifier lists. Return value: If this method is successful, the
    // CODE field of the HRESULT contains one of the following values (the code
    // can be retrived using the helper function GetHResultCode): Negative A
    // negative return value indicates that the first item should precede
    // the second (pidl1 < pidl2). 

    // Positive A positive return value indicates that the first item should
    // follow the second (pidl1 > pidl2).  Zero A return value of zero
    // indicates that the two items are the same (pidl1 = pidl2). 
    [PreserveSig]
    Int32 CompareIDs( 
      Int32 lParam,               // Value that specifies how the comparison
      // should be performed. The lower 
      // Sixteen bits of lParam define the sorting rule.
      // The upper sixteen bits of 
      // lParam are used for flags that modify the
      // sorting rule. values can be from 
      // the SHCIDS enum
      IntPtr pidl1,               // Pointer to the first item's ITEMIDLIST structure.
      IntPtr pidl2);              // Pointer to the second item's ITEMIDLIST structure.

    // Requests an object that can be used to obtain information from or interact
    // with a folder object.
    // Return value: error code, if any
    [PreserveSig]
    Int32 CreateViewObject( 
      IntPtr hwndOwner,           // Handle to the owner window.
      Guid riid,                  // Identifier of the requested interface. 
      out IntPtr ppv);        // Address of a pointer to the requested interface. 

    // Retrieves the attributes of one or more file objects or subfolders. 
    // Return value: error code, if any
    [PreserveSig]
    Int32 GetAttributesOf( 
      UInt32 cidl,            // Number of file objects from which to retrieve
      // attributes. 
      [MarshalAs(UnmanagedType.LPArray, SizeParamIndex=0)]
      IntPtr[] apidl,            // Address of an array of pointers to ITEMIDLIST
      // structures, each of which 
      // uniquely identifies a file object relative to
      // the parent folder.
      ref UInt32 rgfInOut);    // Address of a single ULONG value that, on entry,
    // contains the attributes that 
    // the caller is requesting. On exit, this value
    // contains the requested 
    // attributes that are common to all of the
    // specified objects. this value can
    // be from the SFGAO enum
       
    // Retrieves an OLE interface that can be used to carry out actions on the
    // specified file objects or folders.
    // Return value: error code, if any
    [PreserveSig]
    Int32 GetUIObjectOf( 
      IntPtr hwndOwner,        // Handle to the owner window that the client
      // should specify if it displays 
      // a dialog box or message box.
      UInt32 cidl,            // Number of file objects or subfolders specified
      // in the apidl parameter. 
      IntPtr[] apidl,            // Address of an array of pointers to ITEMIDLIST
      // structures, each of which 
      // uniquely identifies a file object or subfolder
      // relative to the parent folder.
      Guid riid,                // Identifier of the COM interface object to return.
      ref UInt32 rgfReserved,    // Reserved. 
      out IntPtr ppv);        // Pointer to the requested interface.

    // Retrieves the display name for the specified file object or subfolder. 
    // Return value: error code, if any
    [PreserveSig]
    Int32 GetDisplayNameOf(
      IntPtr pidl,            // Address of an ITEMIDLIST structure (PIDL)
      // that uniquely identifies the file 
      // object or subfolder relative to the parent folder. 
      UInt32 uFlags,              // Flags used to request the type of display name
      // to return. For a list of 
      // possible values, see the SHGNO enum. 
      out STRRET pName); // Address of a STRRET structure in which to
    // return the display name.
        
    // Sets the display name of a file object or subfolder, changing the item
    // identifier in the process.
    // Return value: error code, if any
    [PreserveSig]
    Int32 SetNameOf( 
      IntPtr hwnd,            // Handle to the owner window of any dialog or
      // message boxes that the client 
      // displays.
      IntPtr pidl,            // Pointer to an ITEMIDLIST structure that uniquely
      // identifies the file object
      // or subfolder relative to the parent folder. 
      [MarshalAs(UnmanagedType.LPWStr)] 
      String pszName,            // Pointer to a null-terminated string that
      // specifies the new display name. 
      UInt32 uFlags,            // Flags indicating the type of name specified by
      // the lpszName parameter. For a list of possible
      // values, see the description of the SHGNO enum. 
      out IntPtr ppidlOut);   // Address of a pointer to an ITEMIDLIST structure
    // which receives the new ITEMIDLIST. 
  }


  /// <summary>
  /// Contains parameters for the SHBrowseForFolder function and
  /// receives information about the folder selected 
  /// by the user.
  /// </summary>
 
  //[StructLayout(LayoutKind.Sequential)]
  public struct BROWSEINFO
  {
    public IntPtr hwndOwner;                // Handle to the owner window for the
    // dialog box.
    public IntPtr pidlRoot;                    // Pointer to an item identifier list
    // (PIDL) specifying the location of
    // the root folder from which to start
    // browsing.
    [MarshalAs(UnmanagedType.LPStr)]        // Address of a buffer to receive the
      // display name of the 
    public String pszDisplayName;            // folder selected by the user.
    [MarshalAs(UnmanagedType.LPStr)]        // Address of a null-terminated string
      // that is displayed 
    public String lpszTitle;                // above the tree view control in the
    // dialog box.
    public UInt32 ulFlags;                    // Flags specifying the options for the
    // dialog box.
    [MarshalAs(UnmanagedType.FunctionPtr)]    // Address of an application-defined
      // function that the 
    public WindowsAPI.BrowseCallbackProc lpfn;               // dialog box calls when an event occurs.
    public Int32 lParam;                    // Application-defined value that the
    // dialog box passes to 
    // the callback function
    public Int32 iImage;                    // Variable to receive the image
    // associated with the selected folder.
  }


  [StructLayout(LayoutKind.Explicit)]
  public struct STRRET
  {
    [FieldOffset(0)]
    public UInt32 uType;                                            // One of the STRRET_* values

    [FieldOffset(4)]
    public IntPtr pOleStr;                                          // must be freed by caller of GetDisplayNameOf

    [FieldOffset(4)]
    public IntPtr pStr;                                                     // NOT USED

    [FieldOffset(4)]
    public UInt32 uOffset;                                          // Offset into SHITEMID

    [FieldOffset(4)]
    public IntPtr cStr;                                                     // Buffer to fill in (ANSI)
  }



  #endregion
}

