// <copyright file="BarcodeScannerListenerConfigurationElementCollection.cs" > 
//  
// </copyright>

namespace Oranikle.Studio.Controls.Barcode
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A collection of barcode scanner listener configuration elements.
    /// </summary>
    [SuppressMessage(
        "Microsoft.Design",
        "CA1010",
        Justification = "This is just part of the design of the configuration classes.")]
    public class BarcodeScannerListenerConfigurationElementCollection : 
        ConfigurationElementCollection
    {
        /// <summary>
        /// Creates a new element in the collection.
        /// </summary>
        /// <returns>the created element</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new BarcodeScannerListenerConfigurationElement();
        }

        /// <summary>
        /// Gets the key for the element.
        /// </summary>
        /// <param name="element">the element to get the key for</param>
        /// <returns>the key of the element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            BarcodeScannerListenerConfigurationElement myElement =
                (BarcodeScannerListenerConfigurationElement)element;

            return myElement.Id;
        }
    }
}
