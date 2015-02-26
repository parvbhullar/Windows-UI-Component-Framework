using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Oranikle.Studio.Controls
{
    public class LP 
    {
        //public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        //{
        //    //License license = new 
        //    // See if the file exists, if it doesn’t – exception out
        //    if (!File.Exists(licenseFile))
        //    {
        //        throw new LicenseException(type, instance,
        //            "A licensing error occurred. The MyCustomLicense licensing file was not found. Please place the license file in this location and try the application again.");
        //    }
        //    return license;
        //}

        public static string LK = string.Empty;
        public static void SetLK(string lk)
        {
            LP.LK = lk;
        }
        public static bool Validate()
        {
            if (Util.CLK != LP.LK )
            { 
               // throw new Exception("A licensing error occurred. The licensing file was not found. Please place the license file in application location and try the application again.");
            }
            return true;
        }
    }
}
