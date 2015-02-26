using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Csla;

namespace Oranikle.Studio.Controls
{

    public class BaseUserControl : System.Windows.Forms.UserControl
    {

        public enum SaveStatus
        {
            NoNeed,
            SaveSucceeded,
            SaveFailed,
            WillRetry
        }

        public delegate void EmptyDelegate();
        public delegate bool PreSaveOperation(object parameter);
        public delegate Oranikle.Studio.Controls.BaseUserControl.SaveStatus SaveRecordDelegate(Oranikle.Studio.Controls.BaseUserControl.PreSaveOperation op, object opParameter);

        public class SaveMessages
        {

            private string _InitialMessage;
            private string _NoNeed;
            private string _SaveFailed;
            private string _SaveSucceed;

            public string InitialMessage
            {
                get
                {
                    return _InitialMessage;
                }
                set
                {
                    _InitialMessage = value;
                }
            }

            public string NoNeed
            {
                get
                {
                    return _NoNeed;
                }
                set
                {
                    _NoNeed = value;
                }
            }

            public string SaveFailed
            {
                get
                {
                    return _SaveFailed;
                }
                set
                {
                    _SaveFailed = value;
                }
            }

            public string SaveSucceed
            {
                get
                {
                    return _SaveSucceed;
                }
                set
                {
                    _SaveSucceed = value;
                }
            }

            public SaveMessages(string initialMessage, string noNeed, string saveSucceed, string saveFailed)
            {
                InitialMessage = initialMessage;
                NoNeed = noNeed;
                SaveSucceed = saveSucceed;
                SaveFailed = saveFailed;
            }

        } // class SaveMessages

        public const int WM_CHAR = 258;
        public const int WM_ERASEBKGND = 20;
        public const int WM_LBUTTONCLICK = 513;
        public const int WM_NCCALCSIZE = 131;
        public const int WM_NCPAINT = 133;
        public const int WM_PAINT = 15;
        public const int WM_PARENTNOTIFY = 528;

        private static bool designMode;
        public static int RichTextBoxIndentPixel;

        public static new bool DesignMode
        {
            get
            {
                return Oranikle.Studio.Controls.BaseUserControl.designMode;
            }
        }

        public BaseUserControl()
        {
            SetStyle(System.Windows.Forms.ControlStyles.UserMouse, true);
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
            SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
            DoubleBuffered = true;
        }

        static BaseUserControl()
        {
            Oranikle.Studio.Controls.BaseUserControl.designMode = true;
            Oranikle.Studio.Controls.BaseUserControl.RichTextBoxIndentPixel = 10;
        }

        protected bool EqualNullableSmartDate(System.Nullable<Csla.SmartDate> d1, System.Nullable<Csla.SmartDate> d2)
        {
            if (!d1.HasValue && !d2.HasValue)
                return true;
            if (!d1.HasValue && d2.HasValue)
                return false;
            if (!d2.HasValue && d1.HasValue)
                return false;
            Csla.SmartDate smartDate = d1.Value;
            return smartDate.Equals(d2.Value);
        }

        protected System.Nullable<Csla.SmartDate> GetSmartDateFromString(string s)
        {
            System.Nullable<Csla.SmartDate> nullable;
            System.Nullable<Csla.SmartDate> nullable1;
            System.Nullable<Csla.SmartDate> nullable2;

            if ((s == null) || s == System.String.Empty || (s.Length == 0))
            {
                nullable1 = new System.Nullable<Csla.SmartDate>();
                return nullable1;
            }
            try
            {
                nullable2 = new System.Nullable<Csla.SmartDate>(new Csla.SmartDate(s));
            }
            catch 
            {
                nullable = new System.Nullable<Csla.SmartDate>();
                nullable2 = nullable;
            }
            return nullable2;
        }

        public void ShowInitialMessage(Oranikle.Studio.Controls.BaseUserControl.SaveMessages messages)
        {
            if ((ParentForm is Oranikle.Studio.Controls.IFormWithStatusBar))
            {
                string s = null;
                if (messages != null)
                    s = messages.InitialMessage;
                if (s != null)
                    ((Oranikle.Studio.Controls.IFormWithStatusBar)ParentForm).StartProgressBar(s);
            }
        }

        public void ShowLoadRecordFinish(string successMessage, string failMessage, bool success)
        {
            ShowOperationFinish(successMessage, failMessage, success);
        }

        public void ShowLoadRecordStart(string startMessage)
        {
            ShowOperationStart(startMessage);
        }

        public void ShowLoadVersionFinish(string successMessage, string failMessage, bool success)
        {
            ShowOperationFinish(successMessage, failMessage, success);
        }

        public void ShowLoadVersionStart(string startMessage)
        {
            ShowOperationStart(startMessage);
        }

        public void ShowOperationFinish(string successMessage, string failMessage, bool success)
        {
            if (success)
            {
                ShowOperationFinishSuccess(successMessage);
                return;
            }
            ShowOperationFinishFail(failMessage);
        }

        public void ShowOperationFinishFail(string failMessage)
        {
            if (ParentForm is Oranikle.Studio.Controls.IFormWithStatusBar)
                return;
            ((Oranikle.Studio.Controls.IFormWithStatusBar)ParentForm).EndProgressBar(Oranikle.Studio.Controls.EnumStatusBarStatusType.StatusType.Error, failMessage);
        }

        public void ShowOperationFinishSuccess(string successMessage)
        {
            if (ParentForm is Oranikle.Studio.Controls.IFormWithStatusBar)
                return;
            ((Oranikle.Studio.Controls.IFormWithStatusBar)ParentForm).EndProgressBar(Oranikle.Studio.Controls.EnumStatusBarStatusType.StatusType.Info, successMessage);
        }

        public void ShowOperationStart(string startMessage)
        {
            if ((ParentForm is Oranikle.Studio.Controls.IFormWithStatusBar))
                ((Oranikle.Studio.Controls.IFormWithStatusBar)ParentForm).StartProgressBar(startMessage);
        }

        public void ShowSaveFailedMessage(Oranikle.Studio.Controls.BaseUserControl.SaveMessages messages)
        {
            if ((ParentForm is Oranikle.Studio.Controls.IFormWithStatusBar))
            {
                string s = null;
                if (messages != null)
                    s = messages.SaveFailed;
                if (s != null)
                    ((Oranikle.Studio.Controls.IFormWithStatusBar)ParentForm).EndProgressBar(Oranikle.Studio.Controls.EnumStatusBarStatusType.StatusType.Error, s);
            }
        }

        public void ShowSaveNoNeedMessage(Oranikle.Studio.Controls.BaseUserControl.SaveMessages messages)
        {
            if ((ParentForm is Oranikle.Studio.Controls.IFormWithStatusBar))
            {
                string s = null;
                if (messages != null)
                    s = messages.NoNeed;
                if (s != null)
                    ((Oranikle.Studio.Controls.IFormWithStatusBar)ParentForm).EndProgressBar(Oranikle.Studio.Controls.EnumStatusBarStatusType.StatusType.Info, s);
            }
        }

        public void ShowSaveResultMessage(Oranikle.Studio.Controls.BaseUserControl.SaveMessages messages, Oranikle.Studio.Controls.BaseUserControl.SaveStatus status)
        {
            switch (status)
            {
                case Oranikle.Studio.Controls.BaseUserControl.SaveStatus.NoNeed:
                    ShowSaveNoNeedMessage(messages);
                    return;

                case Oranikle.Studio.Controls.BaseUserControl.SaveStatus.SaveFailed:
                    ShowSaveFailedMessage(messages);
                    return;

                case Oranikle.Studio.Controls.BaseUserControl.SaveStatus.SaveSucceeded:
                    ShowSaveSucceedMessage(messages);
                    break;

                case Oranikle.Studio.Controls.BaseUserControl.SaveStatus.WillRetry:
                    break;
            }
        }

        public void ShowSaveSucceedMessage(Oranikle.Studio.Controls.BaseUserControl.SaveMessages messages)
        {
            if ((ParentForm is Oranikle.Studio.Controls.IFormWithStatusBar))
            {
                string s = null;
                if (messages != null)
                    s = messages.SaveSucceed;
                if (s != null)
                    ((Oranikle.Studio.Controls.IFormWithStatusBar)ParentForm).EndProgressBar(Oranikle.Studio.Controls.EnumStatusBarStatusType.StatusType.Success, s);
            }
        }

        public static System.Collections.Generic.List<System.Windows.Forms.Control> GetAllChildControls(System.Windows.Forms.Control c)
        {
            System.Collections.Generic.List<System.Windows.Forms.Control> list = new System.Collections.Generic.List<System.Windows.Forms.Control>();
            Oranikle.Studio.Controls.BaseUserControl.GetAllChildControlsHelper(c, list);
            return list;
        }

        protected static void GetAllChildControlsHelper(System.Windows.Forms.Control c, System.Collections.Generic.List<System.Windows.Forms.Control> l)
        {
            l.Add(c);
            foreach (System.Windows.Forms.Control control in c.Controls)
            {
                Oranikle.Studio.Controls.BaseUserControl.GetAllChildControlsHelper(control, l);
            }
        }

        public static void SetDesignMode(bool value)
        {
            Oranikle.Studio.Controls.BaseUserControl.designMode = value;
        }

    } // class BaseUserControl

}

