// <copyright file="BarcodeScannedEventArgs.cs" > 
//  
// </copyright>

namespace Oranikle.Studio.Controls.Barcode
{
    using System;

    /// <summary>
    /// Contains information about a barcode that was scanned.
    /// </summary>
    public class BarcodeScannedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the BarcodeScannedEventArgs class.
        /// </summary>
        /// <param name="barcode">the barcode that was scanned</param>
        /// <param name="deviceInfo">information about the device that sent the
        /// barcode</param>
        /// <exception cref="ArgumentNullException">if the barcode or deviceInfo are
        /// null</exception>
        public BarcodeScannedEventArgs(string barcode, BarcodeScannerDeviceInfo deviceInfo)
        {
            if (barcode == null)
            {
                throw new ArgumentNullException("barcode");
            }

            if (deviceInfo == null)
            {
                throw new ArgumentNullException("deviceInfo");
            }

            this.Barcode = barcode;
            this.DeviceInfo = deviceInfo;
        }

        /// <summary>
        /// Gets the barcode that was scanned.
        /// </summary>
        public string Barcode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets information about the device that sent the barcode.
        /// </summary>
        public BarcodeScannerDeviceInfo DeviceInfo
        {
            get;
            private set;
        }
    }
}
