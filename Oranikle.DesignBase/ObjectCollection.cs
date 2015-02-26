using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
    public class ObjectCollection : CollectionBase
    {
        public event EventHandler Item_Added;
		/// <summary>
		/// Constructor requires the  wizard that owns this collection
		/// </summary>
		/// <param name="parent">Wizard</param>
        public ObjectCollection()
            : base()
		{
		}

		/// <summary>
		/// Returns the wizard that owns this collection
		/// </summary>
        

		/// <summary>
		/// Finds the Page in the collection
		/// </summary>
		public object this[ int index ]  
		{
			get  
			{
				return( (object) List[index] );
			}
			set  
			{
				List[index] = value;
			}
		}

		/// <summary>
		/// Adds a object into the Collection
		/// </summary>
		/// <param name="value">The page to add</param>
		/// <returns></returns>
		public int Add(object value )  
		{
            int result = List.Add(value);
            OnItem_Added();
			return result;
		}

        public virtual void OnItem_Added()
        {
            if (Item_Added != null)
                Item_Added.Invoke(this, System.EventArgs.Empty);
        }

		/// <summary>
		/// Adds an array of pages into the collection. Used by the Studio Designer generated coed
		/// </summary>
		/// <param name="pages">Array of pages to add</param>
		public void AddRange(object[] objectArray)
		{
			// Use external to validate and add each entry
			foreach(object obj in objectArray)
			{
				this.Add(obj);
			}
		}

		/// <summary>
		/// Finds the position of the page in the colleciton
		/// </summary>
		/// <param name="value">Page to find position of</param>
		/// <returns>Index of Page in collection</returns>
		public int IndexOf( object value )  
		{
			return( List.IndexOf( value ) );
		}

		/// <summary>
		/// Adds a new page at a particular position in the Collection
		/// </summary>
		/// <param name="index">Position</param>
		/// <param name="value">Page to be added</param>
		public void Insert( int index, object value )  
		{
			List.Insert(index, value );
		}


		/// <summary>
		/// Removes the given page from the collection
		/// </summary>
		/// <param name="value">Page to remove</param>
		public void Remove( object value )  
		{
			//Remove the item
			List.Remove( value );
		}

		/// <summary>
		/// Detects if a given Page is in the Collection
		/// </summary>
		/// <param name="value">Page to find</param>
		/// <returns></returns>
		public bool Contains( object value )  
		{
			// If value is not of type Int16, this will return false.
			return( List.Contains( value ) );
		}

		/// <summary>
		/// Propgate when a external designer modifies the pages
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
			//Showthe page added
		}
	
		/// <summary>
		/// Propogates when external designers remove items from page
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete (index, value);
			//If the page that was added was the one that was visible
		}

       
    }
}
