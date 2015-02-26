using System;
using System.Windows.Forms;
using System.Collections;


namespace Oranikle.Studio.Controls.General
{
	/// <summary>
	/// CustomDragger provides easy way for handling Drag&Drop operations
	/// </summary>
  public class CustomDragger 
  {
    #region Class constants
    // Dragging begins after mouse was moved by DraggerDelay pixels
    private const int DraggerDelay = 3;     
    #endregion

    #region Class Variables
    private int           mouseX;
    private int           mouseY;
    private bool          dragging; 
    protected object      obj;
    private Control       m_parent;
    private Control       m_accepter;
    private bool          buttonDowned;
    #endregion

    #region Internal Classes
    public class DragDropInternal  
    {
      public const int DEF_MEMORY_RESERVE = 16;

      private ArrayList m_dataTypes;
      private ArrayList m_dataValues;


      public DragDropInternal()
      {
        m_dataTypes = new ArrayList( DEF_MEMORY_RESERVE );
        m_dataValues = new ArrayList( DEF_MEMORY_RESERVE );
      }

      public void Add( string type, object value )
      {
        m_dataTypes.Add( type );
        m_dataValues.Add( value );
      }

      public ArrayList listByType (string type)
      {
        ArrayList res = new ArrayList( DEF_MEMORY_RESERVE );
        
        for ( int i = 0; i < m_dataTypes.Count; i++ )
        {
          if ( (string)m_dataTypes[i] == type ) 
          {
            res.Add( m_dataValues[i] );
          }
        }
        
        return res;
      }
      
      public bool isEmpty()
      {
        return ( m_dataTypes.Count == 0 );
      }

      public void Clear()
      {
        m_dataTypes.Clear();
        m_dataValues.Clear();
      }
    }

    
    #region Class Events Arguments wrappers
    public class DataRequestArgs : EventArgs
    {
      private DragDropInternal m_data;
      private int m_x;
      private int m_y;

      public int X
      {
        get{return m_x;}
      }
      public int Y
      {
        get{return m_y;}
      }

      
      public DragDropInternal Data
      {
        get
        {
          return m_data;
        }
      }

      public DataRequestArgs( DragDropInternal data, int X, int Y )
      {
        m_data = data;
        m_x = X;
        m_y = Y;
      }
    }

    public class DropEffectsArgs : EventArgs
    {
      private DragDropEffects m_effects;
      private int m_keyState;
      private int m_x;
      private int m_y;
      private DragDropInternal m_data;

      public int KeyState 
      {
        get
        {
          return m_keyState;
        }
      }

      public int X
      {
        get{return m_x;}
      }
      public int Y
      {
        get{return m_y;}
      }

      public DragDropInternal Data
      {
        get
        {
          return m_data;
        }
      }

      public DragDropEffects Effects
      {
        get
        {
          return m_effects;
        }
        set
        {
          m_effects = value;
        }
      }


      public DropEffectsArgs( DragDropInternal data, DragDropEffects effects, int X, int Y, int keyState )
      {
        m_effects = effects;
        m_x = X;
        m_y = Y;
        m_keyState = keyState;
        m_data = data;
      }
    }
    
    public class DropRequestArgs : EventArgs
    {
      private DragDropInternal m_data;
      private int m_keyState;
      private int m_x;
      private int m_y;

      public int KeyState 
      {
        get
        {
          return m_keyState;
        }
      }

      public int X
      {
        get{return m_x;}
      }
      public int Y
      {
        get{return m_y;}
      }

      public DragDropInternal data
      {
        get
        {
          return m_data;
        }
      }


      public DropRequestArgs( DragDropInternal data, int X, int Y, int keyState )
      {
        m_data = data;
        m_x = X;
        m_y = Y;
        m_keyState = keyState;
      }
    }
    #endregion
    
    #endregion

    #region Class delegates
    public delegate void DataRequestEventHandler( object sender, DataRequestArgs e );
    public delegate void DropEffectsEventHandler( object sender, DropEffectsArgs e );
    public delegate void DropDataEventHandler( object sender, DropRequestArgs e );
    #endregion
    
    #region Class events declaration
    public event DataRequestEventHandler  OnDataRequest;
    public event DropEffectsEventHandler  OnEffectsRequest;
    public event DropDataEventHandler     OnDataDrop;
    #endregion
    
    #region Class constructor
    public CustomDragger( Control parent, Control accepter ) 
    {
      m_parent = parent;
      m_accepter = accepter;
      
      AttachEvents();
    }
    #endregion

    #region Class events attachments
    protected void AttachEvents()
    {
      if( m_parent != null && m_accepter != null )
      {
        m_parent.Disposed   += new EventHandler( OnParentDisposed );
        m_parent.MouseDown  += new MouseEventHandler( MouseDown );
        m_parent.MouseMove  += new MouseEventHandler( MouseMove );
        m_parent.MouseUp    += new MouseEventHandler( MouseUp );
        m_parent.MouseLeave += new EventHandler( MouseLeave );

        m_accepter.Disposed += new EventHandler( OnParentDisposed );
        m_accepter.DragOver += new DragEventHandler( DragOver );
        m_accepter.DragDrop += new DragEventHandler( DragDrop );
      }
    }

    protected void DetachEvents()
    {
      if( m_parent != null && m_accepter != null )
      {
        m_parent.Disposed   -= new EventHandler( OnParentDisposed );
        m_parent.MouseDown  -= new MouseEventHandler( MouseDown );
        m_parent.MouseMove  -= new MouseEventHandler( MouseMove );
        m_parent.MouseUp    -= new MouseEventHandler( MouseUp );
        m_parent.MouseLeave -= new EventHandler( MouseLeave );

        m_accepter.Disposed -= new EventHandler( OnParentDisposed );
        m_accepter.DragOver -= new DragEventHandler( DragOver );
        m_accepter.DragDrop -= new DragEventHandler( DragDrop );
      }
    }
    #endregion
  
    #region Mouse event handlers
    private void OnParentDisposed( object sender, EventArgs e )
    {
      DetachEvents();
    }

    private void BeginDragging ( object sender )
    {
      dragging = true;
      DragDropInternal data = new DragDropInternal();
      
      if( OnDataRequest != null ) 
      {
        System.Drawing.Point point = new System.Drawing.Point ( mouseX, mouseY );
        //point = ((Control)sender).PointToClient(point);
        OnDataRequest( sender, new DataRequestArgs ( data, point.X, point.Y ) );
      }
      
      if( !data.isEmpty() )
      {
        m_parent.DoDragDrop( data, 
          DragDropEffects.All | DragDropEffects.Link | DragDropEffects.Copy | DragDropEffects.Scroll);
      }
    }

    private void MouseMove( object sender, System.Windows.Forms.MouseEventArgs e )
    {
      if( !dragging && e.Button == MouseButtons.Left ) 
      {
        if( Math.Abs(mouseX - e.X) >= DraggerDelay || Math.Abs(mouseY - e.Y) >= DraggerDelay ) 
        {
          BeginDragging( sender );
        }
      }
    }
    
    private void MouseLeave( object sender, EventArgs e )
    {
      if( !dragging && buttonDowned ) 
      {
        BeginDragging( sender );
      }
    }
    
    private void MouseDown( object sender, System.Windows.Forms.MouseEventArgs e )
    {
      buttonDowned = false;
      
      if( e.Button == MouseButtons.Left )
      {
        mouseX = e.X;
        mouseY = e.Y;
        buttonDowned = true;
        dragging = false;
      }
    }

    private void MouseUp( object sender, System.Windows.Forms.MouseEventArgs e )
    {
      buttonDowned = false;
    }
    
    private void DragOver( object sender, System.Windows.Forms.DragEventArgs e )
    {                        
      System.Drawing.Point point = new System.Drawing.Point ( e.X, e.Y );
      point = ( ( Control )sender ).PointToClient( point );
        
      DragDropInternal data = new DragDropInternal();
        
      DropEffectsArgs arg = new DropEffectsArgs( ( DragDropInternal )e.Data.GetData( data.GetType() ), 
        DragDropEffects.Copy, point.X, point.Y, e.KeyState );
        
      if ( OnEffectsRequest != null )
      {
        OnEffectsRequest( sender, arg );
      }
        
      e.Effect = arg.Effects;
    }

    private void DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {
      if ( OnDataDrop != null )
      {
        DragDropInternal data = new DragDropInternal();
        
        if ( e.Data.GetDataPresent(data.GetType()) ) 
        {
          System.Drawing.Point point = new System.Drawing.Point ( e.X, e.Y );
          point = ((Control)sender).PointToClient(point);
          
          OnDataDrop( sender, new DropRequestArgs( ( DragDropInternal )e.Data.GetData( data.GetType() ), 
            point.X, point.Y, e.KeyState ) );
        }
      }
    }
    #endregion
  }
}
