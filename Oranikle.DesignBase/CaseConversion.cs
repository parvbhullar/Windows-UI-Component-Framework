using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public class CaseConversion
    {

        public delegate bool QueryDelegate(string word);

        //[System.Serializable]
        //private class AddExceptionCommand : Oranikle.Studio.Controls.ArchonCommandBase
        //{

        //    public string exception;

        //    public AddExceptionCommand(string exception)
        //    {
        //        this.exception = exception;
        //    }

        //    protected override void DataPortal_ExecuteBody()
        //    {
        //        Oranikle.Studio.Controls.CaseConversion.caseConversionExceptions = new System.Collections.Generic.List<string>();
        //        using (Oranikle.Studio.Controls.ConnectionManager connectionManager = Oranikle.Studio.Controls.ConnectionManager.GetManager())
        //        {
        //            System.Data.SqlClient.SqlConnection sqlConnection = connectionManager.Connection;
        //            using (System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand())
        //            {
        //                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //                sqlCommand.CommandText = "_UI_CaseConversion_AddException";
        //                sqlCommand.Parameters.AddWithValue("Value", exception);
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }

        //} // class AddExceptionCommand

        //[System.Serializable]
        //private class LoadExceptionCommand : Oranikle.Studio.Controls.ArchonCommandBase
        //{

        //    public System.Collections.Generic.List<string> caseConversionExceptions;

        //    public LoadExceptionCommand()
        //    {
        //    }

        //    protected override void DataPortal_ExecuteBody()
        //    {
        //        caseConversionExceptions = new System.Collections.Generic.List<string>();
        //        using (Oranikle.Studio.Controls.ConnectionManager connectionManager = Oranikle.Studio.Controls.ConnectionManager.GetManager())
        //        {
        //            System.Data.SqlClient.SqlConnection sqlConnection = connectionManager.Connection;
        //            using (System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand())
        //            {
        //                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //                sqlCommand.CommandText = "_UI_CaseConversion_GetExceptions";
        //                using (Oranikle.Studio.Controls.NullableDataReader nullableDataReader = new Oranikle.Studio.Controls.NullableDataReader(sqlCommand.ExecuteReader()))
        //                {
        //                    while (nullableDataReader.Read())
        //                    {
        //                        caseConversionExceptions.Add(nullableDataReader.GetString("Value"));
        //                    }
        //                }
        //            }
        //        }
        //    }

        //} // class LoadExceptionCommand

        private static readonly log4net.ILog log;

        private static System.Collections.Generic.List<string> caseConversionExceptions;
        private static object exceptionLock;
        private static System.Collections.Generic.Dictionary<string, string> exceptionMapping;
        private static string pattern;
        private static Oranikle.Studio.Controls.CaseConversion.QueryDelegate useMacSpelling;

        public static Oranikle.Studio.Controls.CaseConversion.QueryDelegate UseMacSpelling
        {
            get
            {
                return Oranikle.Studio.Controls.CaseConversion.useMacSpelling;
            }
            set
            {
                Oranikle.Studio.Controls.CaseConversion.useMacSpelling = value;
            }
        }

        public CaseConversion()
        {
        }

        static CaseConversion()
        {
            Oranikle.Studio.Controls.CaseConversion.log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            Oranikle.Studio.Controls.CaseConversion.exceptionLock = new System.Object();
            Oranikle.Studio.Controls.CaseConversion.caseConversionExceptions = null;
            Oranikle.Studio.Controls.CaseConversion.exceptionMapping = null;
            Oranikle.Studio.Controls.CaseConversion.useMacSpelling = new Oranikle.Studio.Controls.CaseConversion.QueryDelegate(Oranikle.Studio.Controls.CaseConversion.DummyQueryDelegate);
            Oranikle.Studio.Controls.CaseConversion.pattern = "(\\W*)(\\w+)(\\W*)";
        }

        //public static string CheckException(string original)
        //{
        //    string s;

        //    try
        //    {
        //        if (Oranikle.Studio.Controls.CaseConversion.exceptionMapping == null)
        //            Oranikle.Studio.Controls.CaseConversion.LoadExceptions();
        //        lock (Oranikle.Studio.Controls.CaseConversion.exceptionLock)
        //        {
        //            if (Oranikle.Studio.Controls.CaseConversion.exceptionMapping.ContainsKey(original.ToLower()))
        //            {
        //                return Oranikle.Studio.Controls.CaseConversion.exceptionMapping.get_Item(original.ToLower());
        //            }
        //            return null;
        //        }
        //    }
        //    catch (System.Exception e)
        //    {
        //        Oranikle.Studio.Controls.CaseConversion.log.Error("Error while trying to check case conversion exceptions", e);
        //        s = null;
        //    }
        //    return s;
        //}

        public static string ConvertUpperCaseFirstWord(string original)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(Oranikle.Studio.Controls.CaseConversion.pattern);
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            System.Text.RegularExpressions.MatchCollection matchCollection = regex.Matches(original);
            System.Text.RegularExpressions.Regex.Matches(original, Oranikle.Studio.Controls.CaseConversion.pattern);
            string s = "";
            foreach (System.Text.RegularExpressions.Match match in matchCollection)
            {
                if (match == matchCollection[0])
                    Oranikle.Studio.Controls.CaseConversion.ProcessMatch(match, ref s, ref stringBuilder);
                else
                    stringBuilder.Append(match.Groups[0].ToString());
            }
            return stringBuilder.ToString();
        }

        public static string ConvertUpperCaseWords(string original)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(Oranikle.Studio.Controls.CaseConversion.pattern);
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            System.Text.RegularExpressions.MatchCollection matchCollection = regex.Matches(original);
            System.Text.RegularExpressions.Regex.Matches(original, Oranikle.Studio.Controls.CaseConversion.pattern);
            string s = "";
            foreach (System.Text.RegularExpressions.Match match in matchCollection)
            {
                Oranikle.Studio.Controls.CaseConversion.ProcessMatch(match, ref s, ref stringBuilder);
            }
            return stringBuilder.ToString();
        }

        private static bool DummyQueryDelegate(string word)
        {
            return false;
        }

        //private static void LoadExceptions()
        //{
        //    Oranikle.Studio.Controls.CaseConversion.LoadExceptionCommand loadExceptionCommand = new Oranikle.Studio.Controls.CaseConversion.LoadExceptionCommand();
        //    try
        //    {
        //        Csla.DataPortal.Execute<Oranikle.Studio.Controls.CaseConversion.LoadExceptionCommand>(loadExceptionCommand);
        //    }
        //    catch (System.Exception e)
        //    {
        //        Oranikle.Studio.Controls.CaseConversion.log.Error("Error when loading case conversion exceptions", e);
        //        if (Oranikle.Studio.Controls.CaseConversion.caseConversionExceptions == null)
        //            Oranikle.Studio.Controls.CaseConversion.caseConversionExceptions = new System.Collections.Generic.List<string>();
        //        if (Oranikle.Studio.Controls.CaseConversion.exceptionMapping == null)
        //            Oranikle.Studio.Controls.CaseConversion.exceptionMapping = new System.Collections.Generic.Dictionary<string, string>();
        //        return;
        //    }
        //    lock (Oranikle.Studio.Controls.CaseConversion.exceptionLock)
        //    {
        //        Oranikle.Studio.Controls.CaseConversion.caseConversionExceptions = loadExceptionCommand.caseConversionExceptions;
        //        Oranikle.Studio.Controls.CaseConversion.exceptionMapping = new System.Collections.Generic.Dictionary<string, string>();
        //        System.Collections.Generic.List<string>.Enumerator enumerator = Oranikle.Studio.Controls.CaseConversion.caseConversionExceptions.GetEnumerator();
        //        try
        //        {
        //            while (enumerator.MoveNext())
        //            {
        //                string s = enumerator.get_Current();
        //                Oranikle.Studio.Controls.CaseConversion.exceptionMapping.set_Item(s.ToLower(), s);
        //            }
        //        }
        //        finally
        //        {
        //            enumerator.Dispose();
        //        }
        //    }
        //}

        private static void ProcessMatch(System.Text.RegularExpressions.Match match, ref string previousDelimiter, ref System.Text.StringBuilder output)
        {
            output.Append(match.Groups[1].ToString());
            string s1 = match.Groups[2].ToString();
            string s2 = match.Groups[3].ToString();
            if (s1 == s1.ToLower())
            {
                bool flag = false;
                try
                {
                    string s3 = null; //Oranikle.Studio.Controls.CaseConversion.CheckException(s1);
                    if (s3 != null)
                    {
                        flag = true;
                        s1 = s3;
                    }
                    if (!flag && previousDelimiter == "'" && (s1 == "s" || s1 == "t" || s1 == "m" || s1 == "re" || s1 == "ve" || s1 == "d" || s1 == "ll"))
                        flag = true;
                    if (!flag && (s1.Length > 2) && s1.StartsWith("mc"))
                    {
                        char ch1 = s1[2];
                        s1 = "Mc" + ch1.ToString().ToUpper() + s1.Substring(3);
                        flag = true;
                    }
                    if (!flag && s1.StartsWith("mac") && (s1.Length > 4))
                    {
                        if (Oranikle.Studio.Controls.CaseConversion.UseMacSpelling(s1))
                        {
                            char ch2 = s1[3];
                            s1 = "Mac" + ch2.ToString().ToUpper() + s1.Substring(4);
                        }
                        else
                        {
                            char ch3 = s1[0];
                            s1 = ch3.ToString().ToUpper() + s1.Substring(1);
                        }
                        //Oranikle.Studio.Controls.CaseConversion.AddExceptionCommand addExceptionCommand = new Oranikle.Studio.Controls.CaseConversion.AddExceptionCommand(s1);
                        //Csla.DataPortal.Execute<Oranikle.Studio.Controls.CaseConversion.AddExceptionCommand>(addExceptionCommand);
                        //Oranikle.Studio.Controls.CaseConversion.LoadExceptions();
                        flag = true;
                    }
                }
                catch (System.Exception e)
                {
                    Oranikle.Studio.Controls.CaseConversion.log.Error("Error when processing match for Case Conversion", e);
                }
                if (!flag)
                {
                    char ch4 = s1[0];
                    s1 = ch4.ToString().ToUpper() + s1.Substring(1);
                }
            }
            previousDelimiter = s2;
            output.Append(s1);
            output.Append(s2);
        }

    } 
}
