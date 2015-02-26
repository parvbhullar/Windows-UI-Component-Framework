#region Using directives
using System;
using System.Collections;
using System.Text;
#endregion

namespace Oranikle.Studio.Controls.General
{
	/// <summary>
	/// Summary description for BoolFlagsMap.
	/// </summary>
	public class BooleanFlagsMap
	{
    #region Class members
    private BitArray  m_bits;
    private Hashtable m_hash;
    #endregion

    #region Class Properties
    public bool this[ string name ]
    {
      get
      {
        CreateOnCheckExist( name );
        return m_bits[ (int)m_hash[ name ] ];
      }
      set
      {
        CreateOnCheckExist( name );
        m_bits[ (int)m_hash[ name ] ] = value;
      }
    }

    public bool this[ int index ]
    {
      get
      {
        return m_bits[ index ];
      }
      set
      {
        m_bits[ index ] = value;
      }
    }

    public int  Count
    {
      get
      {
        return m_bits.Count;
      }
    }
    #endregion
    
    #region Class Helper Methods
    private void CreateOnCheckExist( string name )
    {
      if( !m_hash.Contains( name ) )
      {
        int pos = m_hash.Count;
        
        // resize bit map, new element automaticaly set to FALSE
        m_bits.Length = pos+1; 
        
        m_hash[ name ] = pos;
      }
    }
    public string Dump()
    {
      StringBuilder output = new StringBuilder( 8192 );

      foreach( string name in m_hash.Keys )
      {
        output.Append( 
          string.Format( "\r\nName: {0,30} == {1}", name, m_bits[ (int)m_hash[ name ] ] ) );
      }

      return output.ToString();
    }
    #endregion
    
    #region Class Initialize/Finalize methods
    public BooleanFlagsMap()
		{
      m_bits = new BitArray( 16, false );
      m_hash = new Hashtable( 16 );
		}
    #endregion
	}
}
