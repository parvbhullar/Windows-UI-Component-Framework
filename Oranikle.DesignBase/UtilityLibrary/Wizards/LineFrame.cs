using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System.ComponentModel.Design;
using System.Windows.Forms.Design;

using Oranikle.Studio.Controls.General;


namespace Oranikle.Studio.Controls.Wizards
{
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof(Oranikle.Studio.Controls.Wizards.LineFrame), "Oranikle.Studio.Controls.Wizards.LineFrame.bmp")]
  [Designer( "Oranikle.Studio.Controls.Designers.LineFrameDesigner, Oranikle.Studio.Controls.Designers, Version=1.0.4.0, Culture=neutral, PublicKeyToken=fe06e967d3cf723d" )]
  public class LineFrame : System.Windows.Forms.Control
  {
    #region Class constants
    public enum TLineStyle
    {
      Horizontal,
      Vertical
    }
    #endregion

    #region Class members
    static private Point     m_pntStart = new Point( 0, 1 );
    static private GDIUtils  m_gdi      = new GDIUtils();
    
    private Point       m_pntEnd = new Point( 0, 1 );
    private TLineStyle  m_enStyle = TLineStyle.Horizontal;
    #endregion

    #region Class Properties
    [ Category( "Appearance" ), 
    DefaultValue( typeof( TLineStyle ), "Horizontal" ), 
    Description( "GET/SET style of line" ) ]
    public TLineStyle LineStyle
    {
      get
      {
        return m_enStyle;
      }
      set
      {
        if( value != m_enStyle )
        {
          m_enStyle = value;
          OnLineStyleChanged();
        }
      }
    }

    protected override System.Drawing.Size DefaultSize
    {
      get
      {
        if( m_enStyle == TLineStyle.Horizontal )
          return new Size( 75, 4 );
        else
          return new Size( 4, 75 );
      }
    }
    #endregion

    #region Class Initialize/Finilize code
    public LineFrame()
    {
      ControlStyles styleTrue = ControlStyles.ResizeRedraw | 
        ControlStyles.DoubleBuffer | 
        ControlStyles.AllPaintingInWmPaint |
        ControlStyles.UserPaint | 
        ControlStyles.FixedHeight;

      ControlStyles styleFalse = ControlStyles.SupportsTransparentBackColor |
        ControlStyles.Selectable;

      SetStyle( styleTrue, true );
      SetStyle( styleFalse, false );
      
      base.Size = new Size( 75, 4 );
      base.TabStop = false;
      
      m_gdi.Dark = m_gdi.DarkDark;
    }

    protected override void Dispose( bool disposing )
    {
      if( disposing == true )
      {
        //m_gdi.Dispose();
      }

      base.Dispose( disposing );
    }
    #endregion

    #region Class Overrides
    protected virtual void OnLineStyleChanged()
    {
      SetStyle( ControlStyles.FixedHeight, ( m_enStyle == TLineStyle.Horizontal ) );
      SetStyle( ControlStyles.FixedWidth,  ( m_enStyle == TLineStyle.Vertical ) );

      m_pntEnd.X = 1;
      m_pntEnd.Y = Height;

      Invalidate();
    }

    protected override void OnSizeChanged(System.EventArgs e)
    {
      base.OnSizeChanged( e );

      if( m_enStyle == TLineStyle.Horizontal )
      {
        base.Size = new Size( Size.Width, 4 );
        m_pntEnd.X = Width;
        m_pntEnd.Y = 1;
      }
      else
      {
        base.Size = new Size( 4, Size.Height );
        m_pntEnd.X = 1;
        m_pntEnd.Y = Height;
      }
    }

    protected override void OnPaint( PaintEventArgs pe )
    {
      m_gdi.Draw3DLine( pe.Graphics, 
        ( m_enStyle == TLineStyle.Horizontal ) ? m_pntStart : new Point( 1, 0 ) , 
        m_pntEnd );
    }
    #endregion

  }
}