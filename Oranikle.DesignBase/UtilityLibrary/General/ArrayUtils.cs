using System;
using System.Collections;
using System.Reflection;


namespace Oranikle.Studio.Controls.General
{
  /// <summary>
  /// Special method which can be useful in work with arrays
  /// </summary>
  public class ArrayUtils
  {
    /// <summary>
    /// Private constructor to prevent creation of class instance
    /// </summary>
    private ArrayUtils(){}
    /// <summary>
    /// Copy property value of object stored in source into destination array.
    /// </summary>
    /// <param name="source">List of object which property must be read</param>
    /// <param name="array">Destination array</param>
    /// <param name="property">Source Object Property Name</param>
    /// <param name="startIndx">Start index of source object</param>
    public static void CopyTo( IList source, Array array, string property, int index )
    {
      ArrayList list = new ArrayList( source.Count );
      PropertyInfo keyProp = null;

      int iCount = 0;
      foreach( object obj in source )
      {
        if( keyProp == null )
        {
          keyProp =  obj.GetType().GetProperty( property );
          if( keyProp == null )
            throw new ArgumentException( "Property Name of object is wrong", "property" );
        }

        if( iCount >= index )
        {
          list.Add( keyProp.GetValue( obj, null ) );
        }

        iCount++;
      }

      list.CopyTo( array );
      list.Clear();
    }
    /// <summary>
    /// Copy property value of object stored in source into destination array.
    /// </summary>
    /// <param name="source">List of object which property must be read</param>
    /// <param name="array">Destination array</param>
    /// <param name="property">Source Object Property Name</param>
    public static void CopyTo( IList source, Array array, string property )
    {
      CopyTo( source, array, property, 0 );
    }
    /// <summary>
    /// Copy property value of object stored in source into destination array.
    /// </summary>
    /// <param name="source">Source array of objects</param>
    /// <param name="array">Destination array</param>
    /// <param name="property">Source Object Property Name</param>
    /// <param name="index">Start index in source array</param>
    public static void CopyTo( ICollection source, Array array, string property, int index )
    {
      ArrayList list = new ArrayList( source.Count );
      IEnumerator enums = source.GetEnumerator();
      int iCount = 0;
      
      PropertyInfo keyProp = null;

      while( enums.MoveNext() )
      {
        if( keyProp == null )
        {
          keyProp = enums.Current.GetType().GetProperty( property );
          
          if( keyProp == null )
            throw new ArgumentException( "Property Name of object is wrong", "property" );
        }

        if( iCount >= index )
          list.Add( keyProp.GetValue( enums.Current, null ) );

        iCount++;
      }

      list.CopyTo( array );
      list.Clear();
    }
    /// <summary>
    /// Copy property value of object stored in source into destination array.
    /// </summary>
    /// <param name="source">Source array of objects</param>
    /// <param name="array">Destination array</param>
    /// <param name="property">Source Object Property Name</param>
    public static void CopyTo( ICollection source, Array array, string property )
    {
      CopyTo( source, array, property, 0 );
    }
    /// <summary>
    /// Create from array of objects and dictionary.
    /// </summary>
    /// <param name="source">Source array of objects</param>
    /// <param name="output">Dictionary which will be filled in excution</param>
    /// <param name="key">Name of key propererty</param>
    /// <param name="value">Name of Value property</param>
    public static void CopyTo( ICollection source, IDictionary output, string key, string value )
    {
      IEnumerator enums = source.GetEnumerator();
      PropertyInfo keyProp = null, valProp = null;

      while( enums.MoveNext() )
      {
        if( keyProp == null )
        {
          keyProp = enums.Current.GetType().GetProperty( key );
          valProp = enums.Current.GetType().GetProperty( value );
          
          if( keyProp == null )
            throw new ArgumentException( "Property Name of object is wrong", "key" );
          
          if( valProp == null )
            throw new ArgumentException( "Property Name of object is wrong", "value" );
        }

        object keyVal = keyProp.GetValue( enums.Current, null );
        object valVal = valProp.GetValue( enums.Current, null );
        
        output[ keyVal ] = valVal;
      }
    }
  }
}
