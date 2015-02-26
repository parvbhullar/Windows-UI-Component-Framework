using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Core;

namespace Oranikle.Studio.Controls
{
    public class Util
    {

        public delegate void RegisterAndTranslateContainerDelegate(System.ComponentModel.IComponent containerComponent);
        public delegate string TranslateDelegate(string defaultText);

        private static Oranikle.Studio.Controls.Util.RegisterAndTranslateContainerDelegate _RegisterAndTranslateContainerDelegate;
        private static Oranikle.Studio.Controls.Util.TranslateDelegate _TranslateDelegate;
        public static string CLK = "1115E767-7ABB-4ada-8C2C-C58CDBACC1A4";
        public Util()
        {
        }

        static Util()
        {
        }

        public static T[] ArrayDeepCopy<T>(T[] array1)
            where T : System.ICloneable
        {
            // decompiler error
            return array1;
        }

        public static bool ArrayEquals<T>(System.Collections.Generic.IEnumerable<T> a1, System.Collections.Generic.IEnumerable<T> a2)
        {
            // decompiler error
            return a1.Equals(a2);
        }

       
        

        public static bool ArrayEquals<T>(T[] a1, T[] a2)
        {
            if ((a1 == null) && (a2 == null))
                return true;
            if ((a1 == null) || (a2 == null))
                return false;
            if (a1.Length != a2.Length)
                return false;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!a1[i].Equals(a2[i]))
                    return false;
            }
            return true;
        }

        private static string ByteArrayToHexArray(byte[] bs)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.Append("{");
            string s = "";
            byte[] bArr = bs;
            for (int i = 0; i < bArr.Length; i++)
            {
                byte b = bArr[i];
                stringBuilder.Append(s);
                stringBuilder.Append(System.String.Format("0x{0:X2}", b));
                s = ", ";
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public static string ByteArrayToHexString(byte[] bs)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.Append("0x");
            string s = "";
            byte[] bArr = bs;
            for (int i = 0; i < bArr.Length; i++)
            {
                byte b = bArr[i];
                stringBuilder.Append(s);
                stringBuilder.Append(System.String.Format("{0:X2}", b));
                s = "";
            }
            return stringBuilder.ToString();
        }

        public static string CodeToHumanReadable(string propName)
        {
            if (propName.EndsWith("Id") && (propName.Length > 2) && System.Char.IsLower(propName[propName.Length - 3]))
                propName = propName.Substring(0, propName.Length - 2);
            Oranikle.Studio.Controls.Util.InsertSpaceBeforeUpperCase(ref propName);
            return propName;
        }

        public static int CompareArray<T>(T[] a1, T[] a2)
            where T : System.IComparable
        {
            if ((a1 == null) && (a2 == null))
                return 0;
            if (a1 == null)
                return -1;
            if (a2 == null)
                return 1;
            if (a1.Length < a2.Length)
                return -1;
            if (a1.Length > a2.Length)
                return 1;
            for (int i1 = 0; i1 < a1.Length; i1++)
            {
                int i2 = a1[i1].CompareTo(a2[i1]);
                if (i2 != 0)
                    return i2;
            }
            return 0;
        }

        public static T GetObjectWithBiggestTimestamp<T>(System.Collections.Generic.IEnumerable<T> list)
            where T : Oranikle.Studio.Controls.IObjectWithTimestamp
        {
            // decompiler error
            return default(T);
        }

        public static string GetProgramFilesPath()
        {
            if ((8 == System.IntPtr.Size) || !System.String.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432")))
                return System.Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            return System.Environment.GetEnvironmentVariable("ProgramFiles");
        }

        public static System.ComponentModel.PropertyDescriptor GetPropertyDescriptor(System.Type itemType, string propertyName)
        {
            System.ComponentModel.PropertyDescriptor propertyDescriptor2;

            foreach (System.ComponentModel.PropertyDescriptor propertyDescriptor1 in System.ComponentModel.TypeDescriptor.GetProperties(itemType))
            {
                if (propertyDescriptor1.Name == propertyName)
                {
                    return propertyDescriptor1;
                }
            }
            return null;
        }

        public static bool HasNewItem<T>(System.Collections.Generic.IEnumerable<T> list)
            where T : Csla.Core.BusinessBase
        {
            bool flag;

            using (System.Collections.Generic.IEnumerator<T> ienumerator = list.GetEnumerator())
            {
                while (ienumerator.MoveNext())
                {
                    Csla.Core.BusinessBase businessBase = ienumerator.Current;
                    if (businessBase.IsNew)
                    {
                        flag = true;
                        return flag;
                    }
                }
            }
            return false;
        }

        public static void InsertSpaceBeforeUpperCase(ref string s)
        {
            for (int i = 1; i < s.Length; i++)
            {
                if (System.Char.IsUpper(s[i]))
                {
                    s = s.Insert(i, " ");
                    i++;
                }
            }
        }

        public static bool IsEmpty(string value)
        {
            return System.String.IsNullOrEmpty(value);
        }

        public static bool IsEmpty(System.Nullable<Csla.SmartDate> date)
        {
            if (date.HasValue)
            {
                Csla.SmartDate smartDate = date.Value;
                return smartDate.IsEmpty;
            }
            return true;
        }

        public static bool IsEmpty(System.Nullable<decimal> value)
        {
            if (value.HasValue)
            {
                System.Nullable<decimal> nullable = value;
                if (nullable.GetValueOrDefault() == (0M))
                    return nullable.HasValue;
                return false;
            }
            return true;
        }

        public static void RegisterAndTranslateContainer(System.ComponentModel.IComponent containerControl)
        {
            if (Oranikle.Studio.Controls.Util._RegisterAndTranslateContainerDelegate != null)
                Oranikle.Studio.Controls.Util._RegisterAndTranslateContainerDelegate(containerControl);
        }

        public static void SetRegisterAndTranslateContainerDelegate(Oranikle.Studio.Controls.Util.RegisterAndTranslateContainerDelegate f)
        {
            Oranikle.Studio.Controls.Util._RegisterAndTranslateContainerDelegate = f;
        }

        public static void SetTranslateDelegate(Oranikle.Studio.Controls.Util.TranslateDelegate f)
        {
            Oranikle.Studio.Controls.Util._TranslateDelegate = f;
        }

        public static string Translate(string s)
        {
            if (s == null)
                return "";
            if (Oranikle.Studio.Controls.Util._TranslateDelegate != null)
                return Oranikle.Studio.Controls.Util._TranslateDelegate(s);
            return s;
        }

    }
}
