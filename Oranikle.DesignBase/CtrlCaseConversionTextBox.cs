using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    public class CtrlCaseConversionTextBox : Oranikle.Studio.Controls.StyledTextBox, Oranikle.Studio.Controls.IInitializeClass
    {

        private Oranikle.Studio.Controls.CaseConversionModes mode;
        private string preChangeTextValue;

        public Oranikle.Studio.Controls.CaseConversionModes Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        public CtrlCaseConversionTextBox()
        {
            preChangeTextValue = "";
        }

        public void InitAfterLogin()
        {
        }

        public void InitAtStartupBeforeLogin()
        {
            Oranikle.Studio.Controls.CaseConversion.UseMacSpelling = new Oranikle.Studio.Controls.CaseConversion.QueryDelegate(UseMacSpellingMethodAlways);
        }

        private bool UseMacSpellingMethodAlways(string word)
        {
            return true;
        }

        protected override void OnEnter(System.EventArgs e)
        {
            preChangeTextValue = Text;
            base.OnEnter(e);
        }

        protected override void OnLeave(System.EventArgs e)
        {
            if (Text.ToLower() == preChangeTextValue.ToLower())
                goto label_1;
            switch (mode)
            {
                case Oranikle.Studio.Controls.CaseConversionModes.FirstLetters:
                    Text = Oranikle.Studio.Controls.CaseConversion.ConvertUpperCaseWords(Text);
                    break;

                case Oranikle.Studio.Controls.CaseConversionModes.FirstWord:
                    Text = Oranikle.Studio.Controls.CaseConversion.ConvertUpperCaseFirstWord(Text);
                    break;

                case Oranikle.Studio.Controls.CaseConversionModes.None:
                
                break;
            }
        label_1:
            base.OnLeave(e);
        }

    } 
}
