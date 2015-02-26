using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using Oranikle.Studio.Controls.NWin32;


namespace Oranikle.Studio.Controls.General
{
  /*
  /// <summary>
  /// Summary description for GDIUtil.
  /// </summary>
  public class GDIUtil
  {
    #region Constructors
    // We won't instatiate any object
    private GDIUtil()
    {
      
    }
    #endregion

    #region Methods
    /// <summary>
    /// Draw 3D styled Rectangle. 
    /// </summary>
    /// <param name="g">Graphics canvas where rectangle must drawed</param>
    /// <param name="rc">Rectangle coordinates</param>
    /// <param name="clrTL">Color of Top Left corner of rectangle</param>
    /// <param name="clrBR">Color of Bottom Right corner of rectangle</param>
    static  public void Draw3DRect( Graphics g, Rectangle rc, Color clrTL, Color clrBR )
    {
      Draw3DRect( g, rc.Left, rc.Top, rc.Width, rc.Height, clrTL, clrBR );
    }

    static  public void Draw3DRect( Graphics g, int x, int y, int width, int height, Color clrTL, Color clrBR )
    {
      using( Brush brushTL = new SolidBrush( clrTL ) )
      {
        using( Brush brushBR = new SolidBrush( clrBR ) )
        {
          g.FillRectangle( brushTL, x, y, width - 1, 1 );
          g.FillRectangle( brushTL, x, y, 1, height - 1 );
          g.FillRectangle( brushBR, x + width, y, -1, height );
          g.FillRectangle( brushBR, x, y + height, width, -1 );
        }
      }
    }

    static public void StrechBitmap(Graphics gDest, Rectangle rcDest, Bitmap bitmap)
    {

      // Draw From bitmap
      IntPtr hDCTo = gDest.GetHdc();
      WindowsAPI.SetStretchBltMode(hDCTo, (int)StrechModeFlags.HALFTONE);
      IntPtr hDCFrom = WindowsAPI.CreateCompatibleDC(hDCTo);
          
      IntPtr hOldFromBitmap = WindowsAPI.SelectObject(hDCFrom, bitmap.GetHbitmap());
      WindowsAPI.StretchBlt(hDCTo, rcDest.Left , rcDest.Top, rcDest.Width, rcDest.Height, hDCFrom, 
        0 , 0, bitmap.Width, bitmap.Height, (int)PatBltTypes.SRCCOPY);
                
      // Cleanup
      WindowsAPI.SelectObject(hDCFrom, hOldFromBitmap);
      gDest.ReleaseHdc(hDCTo);

    }

    static public void StrechBitmap(Graphics gDest, Rectangle rcDest, Rectangle rcSource, Bitmap bitmap)
    {

      // Draw From bitmap
      IntPtr hDCTo = gDest.GetHdc();
      WindowsAPI.SetStretchBltMode(hDCTo, (int)StrechModeFlags.COLORONCOLOR);
      IntPtr hDCFrom = WindowsAPI.CreateCompatibleDC(hDCTo);
          
      IntPtr hOldFromBitmap = WindowsAPI.SelectObject(hDCFrom, bitmap.GetHbitmap());
      WindowsAPI.StretchBlt(hDCTo, rcDest.Left , rcDest.Top, rcDest.Width, rcDest.Height, hDCFrom, 
        rcSource.Left , rcSource.Top, rcSource.Width, rcSource.Height, (int)PatBltTypes.SRCCOPY);
                
      // Cleanup
      WindowsAPI.SelectObject(hDCFrom, hOldFromBitmap);
      gDest.ReleaseHdc(hDCTo);

    }

    static public Bitmap GetStrechedBitmap(Graphics gDest, Rectangle rcDest, Bitmap bitmap)
    {

      // Draw To bitmap
      Bitmap newBitmap = new Bitmap(rcDest.Width, rcDest.Height);
      Graphics gBitmap = Graphics.FromImage(newBitmap); 
      IntPtr hDCTo = gBitmap.GetHdc();
      WindowsAPI.SetStretchBltMode(hDCTo, (int)StrechModeFlags.COLORONCOLOR);
      IntPtr hDCFrom = WindowsAPI.CreateCompatibleDC(hDCTo);
                  
      IntPtr hOldFromBitmap = WindowsAPI.SelectObject(hDCFrom, bitmap.GetHbitmap());
      WindowsAPI.StretchBlt(hDCTo, rcDest.Left , rcDest.Top, rcDest.Width, rcDest.Height, hDCFrom, 
        0 , 0, bitmap.Width, bitmap.Height, (int)PatBltTypes.SRCCOPY);
                
      // Cleanup
      WindowsAPI.SelectObject(hDCFrom, hOldFromBitmap);
      gBitmap.ReleaseHdc(hDCTo);

      return newBitmap;
    }

    static public Bitmap GetTileBitmap( Rectangle rcDest, Bitmap bitmap )
    {

      Bitmap tiledBitmap = new Bitmap( rcDest.Width, rcDest.Height );
      using( Graphics g = Graphics.FromImage(tiledBitmap) )
      {
        for ( int i = 0; i < tiledBitmap.Width; i += bitmap.Width )
        {
          for ( int j = 0; j < tiledBitmap.Height; j += bitmap.Height )
          {
            g.DrawImage( bitmap, new Point( i, j ) );         
          }
        }
      }

      return tiledBitmap;
    }

    static public void DrawArrowGlyph( Graphics g, Rectangle rc, bool up, Brush brush )
    {
      // Draw arrow glyph with the default size of 5 pixel wide and 3 pixel high
      DrawArrowGlyph( g, rc, 5, 3, up, brush );
    }

    static public void DrawArrowGlyph( Graphics g, Rectangle rc, int arrowWidth, int arrowHeight, 
      bool up, Brush brush )
    {
      
      // TIP: use an odd number for the arrowWidth and 
      // arrowWidth/2+1 for the arrowHeight 
      // so that the arrow gets the same pixel number
      // on the left and on the right and get symetrically painted
      Point[] pts = new Point[3];
      int yMiddle = rc.Top + rc.Height/2 - arrowHeight/2+1;
      int xMiddle = rc.Left + rc.Width/2;
      int yArrowHeight  = yMiddle + arrowHeight;
      int xArrowWidthR  = xMiddle + arrowWidth/2;
      int xArrowWidthL  = xMiddle - arrowWidth/2;
      
      if( up )
      {
        pts[0] = new Point( xMiddle, yMiddle-2 );
        pts[1] = new Point( xArrowWidthL - 1, yArrowHeight - 1 );
        pts[2] = new Point( xArrowWidthR + 1, yArrowHeight - 1 );
        
      }
      else
      {
        pts[0] = new Point( xArrowWidthL, yMiddle );
        pts[1] = new Point( xArrowWidthR + 1,  yMiddle );
        pts[2] = new Point( xMiddle, yArrowHeight );
      }

      g.FillPolygon( brush, pts );
    }

    #endregion
  }
  */
}
