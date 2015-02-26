#region file using directives
using System;
using System.Drawing;
using Oranikle.Studio.Controls.NWin32;
#endregion

namespace Oranikle.Studio.Controls.General
{
  /// <summary>
  /// Summary description for ColorConvert.
  /// </summary>
  public class ColorUtil
  {
    #region Class static members
    static Color  backgroundColor = Color.Empty;
    static Color  selectionColor  = Color.Empty;
    static Color  controlColor    = Color.Empty;
    static Color  pressedColor    = Color.Empty;
    static Color  checkedColor    = Color.Empty;
    static Color  borderColor     = Color.Empty;

    static SolidBrush  m_brushBack;
    static SolidBrush  m_brushSelect;
    static SolidBrush  m_brushCtrl;
    static SolidBrush  m_brushPress;
    static SolidBrush  m_brushCheck;
    static SolidBrush  m_brushBorder;

    static Pen    m_penBack;
    static Pen    m_penSelect;
    static Pen    m_penCtrl;
    static Pen    m_penPress;
    static Pen    m_penCheck;
    static Pen    m_penBorder;
    
    static bool   useCustomColor  = false;
    #endregion

    #region Initialization/Constructor
    // No need to construct this object
    private ColorUtil(){}

    
    static public bool UsingCustomColor
    {
      get
      { 
        return useCustomColor;
      }
    }

    #endregion

    #region Systemcolors names
    static public string[] SystemColorNames =
    {
      "ActiveBorder", "ActiveCaption", "ActiveCaptionText", "AppWorkspace", "Control", "ControlDark", "ControlDarkDark",
      "ControlLight", "ControlLightLight", "ControlText", "Desktop", "GrayText", "HighLight", "HighLightText",
      "HotTrack", "InactiveBorder", "InactiveCaption", "InactiveCaptionText", "Info", "InfoText", "Menu", "MenuText",
      "ScrollBar", "Window", "WindowFrame", "WindowText" 
    };
    #endregion

    #region Conversion between RGB and Hue, Saturation and Luminosity function helpers
    static public void HSLToRGB(float h, float s, float l, ref float r, ref float g, ref float b)
    {
      // given h,s,l,[240 and r,g,b [0-255]
      // convert h [0-360], s,l,r,g,b [0-1]
      h=(h/240)*360;
      s /= 240;
      l /= 240;
      r /= 255;
      g /= 255;
      b /= 255;

      // Begin Foley
      float m1,m2;

      // Calc m2
      if (l<=0.5f)
      {
        //m2=(l*(l+s)); seems to be typo in Foley??, replace l for 1
        m2=(l*(1+s));
      }
      else
      {
        m2=(l+s-l*s);
      }

      //calc m1
      m1=2.0f*l-m2;

      //calc r,g,b in [0-1]
      if (s==0.0f)
      { // Achromatic: There is no hue
        // leave out the UNDEFINED part, h will always have value
        r=g=b=l;
      }
      else
      { // Chromatic: There is a hue
        r= getRGBValue(m1,m2,h+120.0f);
        g= getRGBValue(m1,m2,h);
        b= getRGBValue(m1,m2,h-120.0f);
      }

      // End Foley
      // convert to 0-255 ranges
      r*=255;
      g*=255;
      b*=255;

    }

    static private float getRGBValue(float n1, float n2, float hue)
    {
      // Helper function for the HSLToRGB function above
      if (hue>360.0f)
      {
        hue-=360.0f;
      }
      else if (hue<0.0f)
      {
        hue+=360.0f;
      }

      if (hue<60.0)
      {
        return n1+(n2-n1)*hue/60.0f;
      }
      else if (hue<180.0f)
      {
        return n2;
      }
      else if (hue<240.0f)
      {
        return n1+(n2-n1)*(240.0f-hue)/60.0f;
      }
      else
      {
        return n1;
      }
    }

    static public void RGBToHSL(int r, int g, int b, ref float h, ref float s, ref float l)
    {

      //Computer Graphics - Foley p.595
      float delta;
      float fr = (float)r/255;
      float fg = (float)g/255;
      float fb = (float)b/255;
      float max = Math.Max(fr,Math.Max(fg,fb));
      float min = Math.Min(fr,Math.Min(fg,fb));

      //calc the lightness
      l = (max+min)/2;

      if (max==min)
      {
        //should be undefined but this works for what we need
        s = 0;
        h = 240.0f;
      }
      else
      {

        delta = max-min;

        //calc the Saturation
        if (l < 0.5)
        {
          s = delta/(max+min);
        }
        else
        {
          s = delta/(2.0f-(max+min));
        }

        //calc the hue
        if (fr==max)
        {
          h = (fg-fb)/delta;
        }
        else if (fg==max)
        {
          h = 2.0f + (fb-fr)/delta;
        }
        else if (fb==max)
        {
          h = 4.0f + (fr-fg)/delta;
        }

        //convert hue to degrees
        h*=60.0f;
        if (h<0.0f)
        {
          h+=360.0f;
        }
      }
      //end foley

      //convert to 0-255 ranges
      //h [0-360], h,l [0-1]
      l*=240;
      s*=240;
      h=(h/360)*240;

    }
    #endregion

    #region Visual Studio .NET colors calculation helpers
    static public Color VSNetBackgroundColor
    {
      get
      {
        if ( useCustomColor && backgroundColor != Color.Empty )
          return backgroundColor;
        else
          return CalculateColor(SystemColors.Window, SystemColors.Control, 220);
      }
      set
      {
        // Flag that we are going to use custom colors instead
        // of calculating the color based on the system colors
        // -- this is a way of hooking up into the VSNetColors that I use throughout
        // the Oranikle.Studio.Controls
        useCustomColor = true;
        backgroundColor = value;
      }
    }

    static public Brush VSNetBackgroundBrush
    {
      get
      {
        // if system colors changed by user then re-create brush
        if( m_brushBack == null )
        {
          m_brushBack = new SolidBrush( VSNetBackgroundColor );
        }

        m_brushBack.Color = VSNetBackgroundColor;

        return m_brushBack;
      }
    }

    static public Pen   VSNetBackgroundPen
    {
      get
      {
        if( m_penBack == null )
          m_penBack = new Pen( VSNetBackgroundBrush );
        
        m_penBack.Color = VSNetBackgroundColor;

        return m_penBack;
      }
    }
    
    
    static public Color VSNetSelectionColor
    {
      get
      {
        if ( useCustomColor && selectionColor != Color.Empty )
          return selectionColor;
        else
          return CalculateColor(SystemColors.Highlight, SystemColors.Window, 70);
      }
      set
      {
        // Flag that we are going to use custom colors instead
        // of calculating the color based on the system colors
        // -- this is a way of hooking up into the VSNetColor that I use throughout
        // the Oranikle.Studio.Controls
        useCustomColor = true;
        selectionColor = value;
      }
    }

    static public Brush VSNetSelectionBrush
    {
      get
      {
        if( m_brushSelect == null )
          m_brushSelect = new SolidBrush( VSNetSelectionColor );

        m_brushSelect.Color = VSNetSelectionColor;

        return m_brushSelect;
      }
    }
    static public Pen   VSNetSelectionPen
    {
      get
      {
        if( m_penSelect == null )
          m_penSelect = new Pen( VSNetSelectionBrush );
        
        m_penSelect.Color = VSNetSelectionColor;
        
        return m_penSelect;
      }
    }

    
    static public Color VSNetControlColor
    {
      get
      {
        if( useCustomColor && controlColor != Color.Empty )
          return controlColor;
        else
          return CalculateColor( SystemColors.Control, VSNetBackgroundColor, 195 );
      }
      set
      {
        // Flag that we are going to use custom colors instead
        // of calculating the color based on the system colors
        // -- this is a way of hooking up into the VSNetColors that I use throughout
        // the Oranikle.Studio.Controls
        useCustomColor = true;
        controlColor = value;
      }

    }

    static public Brush VSNetControlBrush
    {
      get
      {
        if( m_brushCtrl == null )
          m_brushCtrl = new SolidBrush( VSNetControlColor );

        m_brushCtrl.Color = VSNetControlColor;
        
        return m_brushCtrl;
      }
    }
  
    static public Pen   VSNetControlPen
    {
      get
      {
        if( m_penCtrl == null )
          m_penCtrl = new Pen( VSNetControlBrush );
      
        m_penCtrl.Color = VSNetControlColor;

        return m_penCtrl;
      }
    }

    
    static public Color VSNetPressedColor
    {
      get
      {
        if ( useCustomColor && pressedColor != Color.Empty )
          return pressedColor;
        else
          return CalculateColor(SystemColors.Highlight, ColorUtil.VSNetSelectionColor, 70);
      }
      set
      {
        // Flag that we are going to use custom colors instead
        // of calculating the color based on the system colors
        // -- this is a way of hooking up into the VSNetColors that I use throughout
        // the Oranikle.Studio.Controls
        useCustomColor = true;
        pressedColor = value;
      }
    }

    static public Brush VSNetPressedBrush
    {
      get
      {
        if( m_brushPress == null )
          m_brushPress = new SolidBrush( VSNetPressedColor );

        m_brushPress.Color = VSNetPressedColor;
        
        return m_brushPress;
      }
    }
    
    static public Pen   VSNetPressedPen
    {
      get
      {
        if( m_penPress == null )
          m_penPress = new Pen( VSNetPressedBrush );
        
        m_penPress.Color = VSNetPressedColor;

        return m_penPress;
      }
    }

    
    static public Color VSNetCheckedColor
    {
      get
      {
        if ( useCustomColor && pressedColor != Color.Empty )
          return checkedColor;
        else
          return CalculateColor(SystemColors.Highlight,  SystemColors.Window, 30);
      }
      set
      {
        // Flag that we are going to use custom colors instead
        // of calculating the color based on the system colors
        // -- this is a way of hooking up into the VSNetColors that I use throughout
        // the Oranikle.Studio.Controls
        useCustomColor = true;
        checkedColor = value;
      }
    }

    static public Brush VSNetCheckedBrush
    {
      get
      {
        if( m_brushCheck == null )
          m_brushCheck = new SolidBrush( VSNetCheckedColor );

        m_brushCheck.Color = VSNetCheckedColor;

        return m_brushCheck;
      }
    }
    
    static public Pen   VSNetCheckedPen
    {
      get
      {
        if( m_penCheck == null )
          m_penCheck = new Pen( VSNetCheckedBrush );

        m_penCheck.Color = VSNetCheckedColor;
        
        return m_penCheck;
      }
    }

    
    static public Color VSNetBorderColor
    {
      get
      {
        if ( useCustomColor && borderColor != Color.Empty )
          return borderColor;
        else
        {
          // This color is the default color unless we are using
          // custom colors
          return SystemColors.Highlight;
        }
      }
      set
      {
        // Flag that we are going to use custom colors instead
        // of calculating the color based on the system colors
        // -- this is a way of hooking up into the VSNetColors that I use throughout
        // the Oranikle.Studio.Controls
        useCustomColor = true;
        borderColor = value;
      }
    }

    static public Brush VSNetBorderBrush
    {
      get
      {
        if( m_brushBorder == null )
          m_brushBorder = new SolidBrush( VSNetBorderColor );

        m_brushBorder.Color = VSNetBorderColor;

        return m_brushBorder;
      }
    }
  
    static public Pen   VSNetBorderPen
    {
      get
      {
        if( m_penBorder == null )
          m_penBorder = new Pen( VSNetBorderBrush );

        m_penBorder.Color = VSNetBorderColor;
      
        return m_penBorder;
      }
    }


    private static Color CalculateColor( Color front, Color back, int alpha )
    {

      // Use alpha blending to brigthen the colors but don't use it
      // directly. Instead derive an opaque color that we can use.
      // -- if we use a color with alpha blending directly we won't be able
      // to paint over whatever color was in the background and there
      // would be shadows of that color showing through
      Color frontColor = Color.FromArgb(255, front);
      Color backColor = Color.FromArgb(255, back);

      float frontRed = frontColor.R;
      float frontGreen = frontColor.G;
      float frontBlue = frontColor.B;
      float backRed = backColor.R;
      float backGreen = backColor.G;
      float backBlue = backColor.B;

      float fRed = frontRed*alpha/255 + backRed*((float)(255-alpha)/255);
      byte newRed = (byte)fRed;
      float fGreen = frontGreen*alpha/255 + backGreen*((float)(255-alpha)/255);
      byte newGreen = (byte)fGreen;
      float fBlue = frontBlue*alpha/255 + backBlue*((float)(255-alpha)/255);
      byte newBlue = (byte)fBlue;

      return  Color.FromArgb(255, newRed, newGreen, newBlue);

    }
    #endregion

    #region General functions
    static public Color ColorFromPoint( Graphics g, int x, int y )
    {
      IntPtr hDC = g.GetHdc();
      // Get the color of the pixel first
      uint colorref = WindowsAPI.GetPixel(hDC, x, y);
      byte Red = GetRValue(colorref);
      byte Green = GetGValue(colorref);
      byte Blue = GetBValue(colorref);
      g.ReleaseHdc(hDC);
      return  Color.FromArgb(Red, Green, Blue);
    }

    static public bool IsKnownColor( Color color, ref Color knownColor, bool useTransparent )
    {

      // Using the Color structrure "FromKnowColor" does not work if
      // we did not create the color as a known color to begin with
      // we need to compare the rgbs of both color
      Color currentColor = Color.Empty;
      bool badColor = false;
      for (KnownColor enumValue = 0; enumValue <= KnownColor.YellowGreen; enumValue++)
      {
        currentColor = Color.FromKnownColor(enumValue);
        string colorName = currentColor.Name;
        if ( !useTransparent )
          badColor = (colorName == "Transparent");
        if ( color.A == currentColor.A && color.R == currentColor.R && color.G == currentColor.G
          && color.B == currentColor.B && !currentColor.IsSystemColor
          && !badColor )
        {
          knownColor = currentColor;
          return true;
        }

      }
      return false;

    }

    static public bool IsSystemColor( Color color, ref Color knownColor )
    {

      // Using the Color structrure "FromKnowColor" does not work if
      // we did not create the color as a known color to begin with
      // we need to compare the rgbs of both color
      Color currentColor = Color.Empty;
      for (KnownColor enumValue = 0; enumValue <= KnownColor.YellowGreen; enumValue++)
      {
        currentColor = Color.FromKnownColor(enumValue);
        string colorName = currentColor.Name;
        if ( color.R == currentColor.R && color.G == currentColor.G
          && color.B == currentColor.B && currentColor.IsSystemColor )
        {
          knownColor = currentColor;
          return true;
        }

      }
      return false;
    }

    static public uint GetCOLORREF( Color color )
    {
      return RGB(color.R, color.G, color.B);
    }


    static public Color ColorFromRGBString( string text )
    {
      Color rgbColor = Color.Empty;
      string[] RGBs = text.Split(',');
      if ( RGBs.Length != 3 )
      {
        // If we don't have three pieces of information, then the
        // string is not properly formatted, inform the use
        throw new Exception("RGB color string is not well formed");
      }

      string stringR = RGBs[0];
      string stringG = RGBs[1];
      string stringB = RGBs[2];
      int R, G, B;

      try
      {
        R = Convert.ToInt32(stringR);
        G = Convert.ToInt32(stringG);
        B = Convert.ToInt32(stringB);
        if ( ( R < 0 || R > 255 ) || ( G < 0 || G > 255 ) || ( B < 0 || B > 255 ) )
        {
          throw new Exception("Out of bounds RGB value");
        }
        else
        {
          // Convert to color
          rgbColor = Color.FromArgb(R, G, B);
          // See if we have either a web color or a systgem color
          Color knownColor = Color.Empty;
          bool isKnown = ColorUtil.IsKnownColor( rgbColor, ref knownColor, true);
          if ( !isKnown )
            isKnown = ColorUtil.IsSystemColor(rgbColor, ref knownColor);
          if ( isKnown )
            rgbColor = knownColor;
        }
      }
      catch ( InvalidCastException )
      {
        throw new Exception("Invalid RGB value");
      }

      return rgbColor;
    }
    #endregion

    #region Windows RGB related macros
    static public byte GetRValue(uint color)
    {
      return (byte)color;
    }

    static public byte GetGValue(uint color)
    {
      return ((byte)(((short)(color)) >> 8));
    }

    static public byte GetBValue(uint color)
    {
      return ((byte)((color)>>16));
    }

    static public uint RGB(int r, int g, int b)
    {
      return ((uint)(((byte)(r)|((short)((byte)(g))<<8))|(((short)(byte)(b))<<16)));
    }
    static public uint RGB( Color clr )
    {
      return ((uint)(((byte)(clr.R)|((short)((byte)(clr.G))<<8))|(((short)(byte)(clr.B))<<16)));
    }
    #endregion
  }
}
