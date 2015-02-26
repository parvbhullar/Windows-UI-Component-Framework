using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

using Oranikle.Studio.Controls.NWin32;


namespace Oranikle.Studio.Controls.General
{
  /// <summary>
  /// Summary description for TextUtil.
  /// </summary>
  public class TextUtil
  {
    #region Class private constants
    private static int DEF_TEXTFORMAT = ( int )DrawTextFormatFlags.DT_SINGLELINE | 
      ( int )DrawTextFormatFlags.DT_LEFT | 
      ( int )DrawTextFormatFlags.DT_CALCRECT;
    #endregion

    #region Constructor
    /// <summary>
    /// To prevent creaion of class instance we declare default constructor as 
    /// a private method
    /// </summary>
    private TextUtil()
    {
    }
    #endregion

    #region Methods
    static private IntPtr GetGraphicsHDC( Graphics g )
    {
      IntPtr hdc = ( g != null ) ? g.GetHdc() : WindowsAPI.GetDC( IntPtr.Zero );
      return hdc;
    }

    static private void   ReleaseGraphicHDC( Graphics g, IntPtr hdc )
    {
      if( g != null )
      {
        g.ReleaseHdc( hdc );
      }
      else
      {
        WindowsAPI.ReleaseDC( IntPtr.Zero, hdc );
      }
    }


    public static Size GetTextSize( Graphics g, string text, Font font )
    {
      IntPtr hdc = GetGraphicsHDC( g );
      
      IntPtr hFont = font.ToHfont();
      IntPtr hOldFont = WindowsAPI.SelectObject( hdc, hFont );
      
      RECT rect = new RECT();
      WindowsAPI.DrawText( hdc, text, text.Length, ref rect, DEF_TEXTFORMAT );
      WindowsAPI.SelectObject( hdc, hOldFont );
      WindowsAPI.DeleteObject( hFont );

      ReleaseGraphicHDC( g, hdc );

      return ( Size )rect;
    }

    public static Size GetTextSize( Graphics g, string text, Font font, ref Rectangle rc, DrawTextFormatFlags drawFlags )
    {
      IntPtr hdc = GetGraphicsHDC( g );
      IntPtr hFont = font.ToHfont();
      IntPtr hOldFont = WindowsAPI.SelectObject( hdc, hFont );
      
      NWin32.RECT rect = new NWin32.RECT();
      rect.left   = rc.Left;
      rect.right  = rc.Right;
      rect.top    = rc.Top;
      rect.bottom = rc.Bottom;
    
      WindowsAPI.DrawText( hdc, text, text.Length, ref rect, (int)drawFlags );
      WindowsAPI.SelectObject( hdc, hOldFont );
      WindowsAPI.DeleteObject( hFont );

      ReleaseGraphicHDC( g, hdc );
              
      return ( Size )rect;
    }

    
    public static void DrawText( Graphics graphics, string text, Font font, Rectangle rect )
    {
      IntPtr hdc = graphics.GetHdc();
      IntPtr fontHandle = font.ToHfont();
      IntPtr currentFontHandle = WindowsAPI.SelectObject(hdc, fontHandle);
      WindowsAPI.SetBkMode(hdc, BackgroundMode.TRANSPARENT);
                      
      RECT rc = new RECT();
      rc.left = rect.Left;
      rc.top = rect.Top;
      rc.right = rc.left + rect.Width;
      rc.bottom = rc.top + rect.Height;
      
      WindowsAPI.DrawText(hdc, text, text.Length, ref rc, 
        (int)(DrawTextFormatFlags.DT_SINGLELINE | DrawTextFormatFlags.DT_LEFT 
        | DrawTextFormatFlags.DT_MODIFYSTRING| DrawTextFormatFlags.DT_WORD_ELLIPSIS));
      WindowsAPI.SelectObject(hdc, currentFontHandle);
      WindowsAPI.DeleteObject(fontHandle);
      graphics.ReleaseHdc(hdc);
    }

    public static void DrawReverseString( Graphics g, string drawText, Font drawFont, Rectangle drawRect, Brush drawBrush, StringFormat drawFormat )
    {
      GraphicsContainer container = g.BeginContainer();

      // The text will be rotated around the origin (0,0) and so needs moving
      // back into position by using a transform
      g.TranslateTransform(drawRect.Left * 2 + drawRect.Width, 
        drawRect.Top * 2 + drawRect.Height);

      // Rotate the text by 180 degress to reverse the direction 
      g.RotateTransform(180);

      // Draw the string as normal and let then transforms do the work
      g.DrawString(drawText, drawFont, drawBrush, drawRect, drawFormat);

      g.EndContainer(container);
    }


    public static Size MeasureText( string text, Font fnt, int width )
    {
      Bitmap bmp = new Bitmap( 1, 1 );
      Graphics g = Graphics.FromImage( bmp );

      StringFormat format = new StringFormat();
      format.Alignment      = StringAlignment.Near;
      format.LineAlignment  = StringAlignment.Near;
      format.Trimming       = StringTrimming.None;
      format.FormatFlags    = StringFormatFlags.LineLimit;
      format.HotkeyPrefix   = HotkeyPrefix.Hide;

      SizeF size = g.MeasureString( text, fnt, width, format );

      g.Dispose();
      bmp.Dispose();

      return new Size( (int)( size.Width + 0.5 ), (int)( size.Height + 0.5 ) );
    }
    #endregion
  }
}
