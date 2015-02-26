// <copyright file="BarcodeScannerListenerConfigurationElement.cs" > 
//  
// </copyright>

namespace Oranikle.Studio.Controls.Barcode
{
    using System.Configuration;

    /// <summary>
    /// A barcode scanner configuration element.
    /// </summary>
    public class BarcodeScannerListenerConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the hardware ID.
        /// </summary>
        [ConfigurationProperty("id", IsRequired = true, IsKey = true)]
        public string Id
        {
            get { return (string)this["id"]; }
            set { this["id"] = value; }
        }
    }
}
