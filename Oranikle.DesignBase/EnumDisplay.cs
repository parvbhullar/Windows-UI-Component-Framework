using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public class EnumDisplay
    {

        private string _EnglishText;
        private string _name;
        private int _value;

        public string DisplayText
        {
            get
            {
                return Oranikle.Studio.Controls.Util.Translate(_EnglishText);
            }
        }

        public string EnglishText
        {
            get
            {
                return _EnglishText;
            }
            set
            {
                _EnglishText = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public EnumDisplay(System.Type enumType, object enumValue, System.ComponentModel.DescriptionAttribute attr)
        {
            EnglishText = attr.Description;
            Value = (int)enumValue;
            Name = System.Enum.GetName(enumType, enumValue);
        }

        public EnumDisplay(int value, string englishText, string name)
        {
            _value = value;
            _EnglishText = englishText;
            _name = name;
        }

        public EnumDisplay()
        {
        }

        public override string ToString()
        {
            return DisplayText;
        }

        public static int[] ConvertToInt(Oranikle.Studio.Controls.EnumDisplay[] enums)
        {
            int[] iArr = new int[enums.Length];
            for (int i = 0; i < enums.Length; i++)
            {
                iArr[i] = enums[i].Value;
            }
            return iArr;
        }

        public static System.Collections.Generic.List<int> ConvertToList(Oranikle.Studio.Controls.EnumDisplay[] enums)
        {
            return new System.Collections.Generic.List<int>(Oranikle.Studio.Controls.EnumDisplay.ConvertToInt(enums));
        }

        public static Oranikle.Studio.Controls.EnumDisplay GetEnumDisplay(Oranikle.Studio.Controls.EnumDisplay[] enums, object value)
        {
            Oranikle.Studio.Controls.EnumDisplay enumDisplay2;

            Oranikle.Studio.Controls.EnumDisplay[] enumDisplayArr = enums;
            for (int i = 0; i < enumDisplayArr.Length; i++)
            {
                Oranikle.Studio.Controls.EnumDisplay enumDisplay1 = enumDisplayArr[i];
                if (enumDisplay1.Value == (int)value)
                {
                    return enumDisplay1;
                }
            }
            return null;
        }

        public static Oranikle.Studio.Controls.EnumDisplay[] GetEnumDisplayList(System.Type enumType)
        {
            System.Collections.Generic.List<Oranikle.Studio.Controls.EnumDisplay> list = new System.Collections.Generic.List<Oranikle.Studio.Controls.EnumDisplay>();
            foreach (object obj in System.Enum.GetValues(enumType))
            {
                System.Reflection.FieldInfo fieldInfo = enumType.GetField(obj.ToString());
                System.ComponentModel.DescriptionAttribute[] descriptionAttributeArr = fieldInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true) as System.ComponentModel.DescriptionAttribute[];
                if (descriptionAttributeArr.Length > 0)
                    list.Add(new Oranikle.Studio.Controls.EnumDisplay(enumType, obj, descriptionAttributeArr[0]));
            }
            return list.ToArray();
        }

        public static string GetEnumDisplayString(Oranikle.Studio.Controls.EnumDisplay[] enums, object value)
        {
            return Oranikle.Studio.Controls.Util.Translate(Oranikle.Studio.Controls.EnumDisplay.GetEnumDisplayStringHelper(enums, value));
        }

        private static string GetEnumDisplayStringHelper(Oranikle.Studio.Controls.EnumDisplay[] enums, object value)
        {
            Oranikle.Studio.Controls.EnumDisplay enumDisplay = Oranikle.Studio.Controls.EnumDisplay.GetEnumDisplay(enums, value);
            if (enumDisplay != null)
                return enumDisplay.EnglishText;
            return "";
        }

        public static System.Nullable<int> GetEnumValue(Oranikle.Studio.Controls.EnumDisplay[] enums, string display)
        {
            System.Nullable<int> nullable;
            System.Nullable<int> nullable1;

            int i1 = 0;
            if (System.Int32.TryParse(display, out i1))
            {
                Oranikle.Studio.Controls.EnumDisplay[] enumDisplayArr1 = enums;
                for (int i2 = 0; i2 < enumDisplayArr1.Length; i2++)
                {
                    Oranikle.Studio.Controls.EnumDisplay enumDisplay1 = enumDisplayArr1[i2];
                    if (enumDisplay1.Value == i1)
                    {
                        return new System.Nullable<int>(enumDisplay1.Value);
                    }
                }
            }
            Oranikle.Studio.Controls.EnumDisplay[] enumDisplayArr2 = enums;
            for (int i3 = 0; i3 < enumDisplayArr2.Length; i3++)
            {
                Oranikle.Studio.Controls.EnumDisplay enumDisplay2 = enumDisplayArr2[i3];
                if (enumDisplay2.DisplayText.ToLower().Trim() == display.ToLower().Trim())
                {
                    return new System.Nullable<int>(enumDisplay2.Value);
                }
            }
            Oranikle.Studio.Controls.EnumDisplay[] enumDisplayArr3 = enums;
            for (int i4 = 0; i4 < enumDisplayArr3.Length; i4++)
            {
                Oranikle.Studio.Controls.EnumDisplay enumDisplay3 = enumDisplayArr3[i4];
                if (enumDisplay3.EnglishText.ToLower().Trim() == display.ToLower().Trim())
                {
                    return new System.Nullable<int>(enumDisplay3.Value);
                }
            }
            nullable = new System.Nullable<int>();
            return nullable;
        }

    } 
}
