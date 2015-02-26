using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public class ExtendedDecimalNumberFormatter : System.ICustomFormatter, System.IFormatProvider
    {

        public const int MAX_DECIMAL_PLACES = 5;

        private System.Globalization.NumberFormatInfo _Nfi;

        public System.Globalization.NumberFormatInfo Nfi
        {
            get
            {
                return _Nfi;
            }
            set
            {
                _Nfi = value;
            }
        }

        public ExtendedDecimalNumberFormatter(System.Globalization.NumberFormatInfo nfi)
        {
            _Nfi = nfi;
        }

        public string Format(string format, object arg, System.IFormatProvider formatProvider)
        {
            if (arg == null)
                return "";
            if (!arg.GetType().IsAssignableFrom(typeof(decimal)))
            {
                if (arg is System.IFormattable)
                    return ((System.IFormattable)arg).ToString(format, formatProvider);
                return arg.ToString();
            }
            decimal dec = (decimal)arg;
            if (!format.Trim().ToUpper().StartsWith("B"))
                return dec.ToString(format, Nfi);
            return dec.ToString(GetFormatStringForValue(dec), Nfi);
        }

        public string FormatString(System.Nullable<decimal> val)
        {
            if (!val.HasValue)
                return "";
            decimal dec = val.Value;
            return dec.ToString(GetFormatStringForValue(val.Value), Nfi);
        }

        public object GetFormat(System.Type formatType)
        {
            if (formatType == typeof(System.ICustomFormatter))
                return this;
            return null;
        }

        private string GetFormatStringForValue(decimal val)
        {
            if (System.Decimal.Round(val, Nfi.CurrencyDecimalDigits) == val)
                return "N";
            for (int i1 = Nfi.CurrencyDecimalDigits + 1; i1 < 5; i1++)
            {
                if (System.Decimal.Round(val, i1) == val)
                    return "N" + i1.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            int i2 = 5;
            return "N" + i2.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

    } 
}
