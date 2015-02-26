using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;

namespace Oranikle.Studio.Controls
{
    public enum HitText : int
    {
        String = 0,
        Number = 1,
        NegativeNumber = 2,
        Currency = 3,
        Credit_Debit = 4,
        Yes_No = 5,
        Y_N = 6,
        Check_DD = 7
    }

    public class CustomTextControl : StyledTextBox
    {
        [Serializable]
        public enum CreditDebit : int
        {
            Cr = 0,
            Dr = 1
        }
        [Serializable]
        public enum YesNo : int
        {
            Yes = 0,
            No = 1
        }
        [Serializable]
        public enum YN : int
        {
            Y = 0,
            N = 1
        }
        bool allowSpace = false;
        private HitText hitText;
        private int selectType = 0;
        private bool _value = true;
        private bool onDropDownCloseFocus = true;
        private object[] displayList;
        private int selectedIndex;
        private object selectedItem;
        private List<Control> childControls = new List<Control>();
        private int addX = 0;
        private int addY = 0;

        public CustomTextControl()
        {
            //if (!DesignMode)
            LP.Validate();
            DecimalFormat = "N2";
            displayList = new object[0];
            this.TextChanged += new EventHandler(CustomTextControl_TextChanged);
            SetChildsVisibilty();
        }

        bool isTrue = true;
        void CustomTextControl_TextChanged(object sender, EventArgs e)
        {
            if (!ContainsFocus && isTrue)
            {
                if (this.HitText == HitText.Currency)
                {
                    isTrue = false;
                    if (this.Text == string.Empty)
                        Text = "";
                    else
                    {
                        decimal val;
                        if (decimal.TryParse(this.Text, out val))
                        {
                            Text = val.ToString(DecimalFormat);
                        }
                    }
                }
            }
            else
                isTrue = true;
        }

        [DefaultValue("N2")]
        public string DecimalFormat
        {
            get;
            set;
        }


        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetChildsVisibilty();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            switch (hitText)
            {
                case HitText.Credit_Debit:
                    {
                        this.Text = ((CreditDebit)selectType).ToString();
                        break;
                    }
                case HitText.Yes_No:
                    {
                        this.Text = ((YesNo)selectType).ToString();
                        break;
                    }
                case HitText.Y_N:
                    {
                        this.Text = ((YN)selectType).ToString();
                        break;
                    }
            }
        }

        private System.Drawing.Point GetLocation()
        {

            return new System.Drawing.Point(GetParentX(this) + AddX, GetParentY(this) + AddY);
        }

        public int AddX
        {
            get { return addX; }
            set { addX = value; }
        }
        public int AddY
        {
            get { return addY; }
            set { addY = value; }
        }

        private int GetParentX(Control cntrl)
        {
            if (cntrl.Parent != null)
            {
                return cntrl.Location.X + GetParentX(cntrl.Parent);
            }
            return cntrl.Location.X;
        }

        private int GetParentY(Control cntrl)
        {
            if (cntrl.Parent != null)
            {
                return cntrl.Location.Y + GetParentY(cntrl.Parent);
            }
            return cntrl.Location.Y;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            switch (hitText)
            {
                case HitText.Credit_Debit:
                    {
                        selectType = Math.Abs(1 - selectType);
                        this.Text = ((CreditDebit)selectType).ToString();
                        break;
                    }
                case HitText.Yes_No:
                    {
                        selectType = Math.Abs(1 - selectType);
                        this.Text = ((YesNo)selectType).ToString();
                        break;
                    }
                case HitText.Y_N:
                    {
                        selectType = Math.Abs(1 - selectType);
                        this.Text = ((YN)selectType).ToString();
                        break;
                    }
            }
        }
        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();
            if (hitText == HitText.Currency)
            {
                if (Char.IsDigit(e.KeyChar))
                {

                }
                else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
                 keyInput.Equals(negativeSign))
                {
                }
                else if (e.KeyChar == '\b')
                {
                }
                //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
                //    {
                //     // Let the edit control handle control and alt key combinations
                //    }
                else if (this.allowSpace && e.KeyChar == ' ')
                {

                }
                else
                {
                    // Swallow this invalid key and beep
                    e.Handled = true;
                    //    MessageBeep();
                }
            }
            else if (hitText == HitText.NegativeNumber)
            {
                long number;
                if (Char.IsDigit(e.KeyChar))
                {

                }
                else if (keyInput.Equals(negativeSign))
                {
                }
                else if (e.KeyChar == '\b')
                {
                }
                //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
                //    {
                //     // Let the edit control handle control and alt key combinations
                //    }
                else if (this.allowSpace && e.KeyChar == ' ')
                {

                }
                else
                {
                    // Swallow this invalid key and beep
                    e.Handled = true;
                    //    MessageBeep();
                }
            }
            else if (hitText == HitText.Number)
            {
                long number;
                if (Char.IsDigit(e.KeyChar))
                {

                }
                else if (e.KeyChar == '\b')
                {
                }
                //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
                //    {
                //     // Let the edit control handle control and alt key combinations
                //    }
                else if (this.allowSpace && e.KeyChar == ' ')
                {

                }
                else
                {
                    // Swallow this invalid key and beep
                    e.Handled = true;
                    //    MessageBeep();
                }
            }
            else if (hitText == HitText.Credit_Debit)
            {

                if (keyInput.ToLower() == "c")
                {
                    selectType = (int)CreditDebit.Cr;
                    this.Text = CreditDebit.Cr.ToString();
                    this.Tag = CreditDebit.Cr;
                    e.Handled = true;
                }
                if (keyInput.ToLower() == "d")
                {
                    selectType = (int)CreditDebit.Dr;
                    this.Text = CreditDebit.Dr.ToString();
                    this.Tag = CreditDebit.Dr;
                    e.Handled = true;
                }
                else
                {
                    // Swallow this invalid key and beep
                    e.Handled = true;
                    //    MessageBeep();
                }

            }
            else if (hitText == HitText.Y_N)
            {
                if (keyInput.ToLower() == "y")
                {
                    selectType = (int)YN.Y;
                    this.Text = YN.Y.ToString();
                    this.Tag = YN.Y;
                    e.Handled = true;
                }
                if (keyInput.ToLower() == "n")
                {
                    selectType = (int)YN.N;
                    this.Text = YN.N.ToString();
                    this.Tag = YN.N;
                    e.Handled = true;
                }
                else
                {
                    // Swallow this invalid key and beep
                    e.Handled = true;
                    //    MessageBeep();
                }
            }
            else if (hitText == HitText.Yes_No)
            {
                if (keyInput.ToLower() == "y")
                {
                    selectType = (int)YesNo.Yes;
                    this.Text = YesNo.Yes.ToString();
                    this.Tag = YesNo.Yes;
                    e.Handled = true;
                }
                if (keyInput.ToLower() == "n")
                {
                    selectType = (int)YesNo.No;
                    this.Text = YesNo.No.ToString();
                    this.Tag = YesNo.No;
                    e.Handled = true;
                }
                else
                {
                    // Swallow this invalid key and beep
                    e.Handled = true;
                    //    MessageBeep();
                }
            }
        }

        public int SelectType
        {
            get { return selectType; }
            set { selectType = value; }
        }

        public int IntValue
        {
            get
            {
                return Int32.Parse(this.Text);
            }
        }

        public decimal DecimalValue
        {
            get
            {
                return Decimal.Parse(this.Text);
            }
        }

        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }

            get
            {
                return this.allowSpace;
            }
        }

        public HitText HitText
        {
            get
            {
                return this.hitText;
            }
            set
            {
                this.hitText = value;
            }
        }

        [DefaultValue(2)]
        public int Decimals
        {
            get;
            set;
        }

        public bool OnDropDownCloseFocus
        {
            get { return this.onDropDownCloseFocus; }
            set { this.onDropDownCloseFocus = value; }
        }

        public object[] DisplayList
        {
            get { return this.displayList; }
            set { this.displayList = value; }
        }

        public bool Value
        {
            get
            {
                if (this.hitText == HitText.Y_N)
                {
                    _value = this.Text == YN.Y.ToString() ? true : false;
                }
                else if (this.hitText == HitText.Yes_No)
                {
                    _value = this.Text == YesNo.Yes.ToString() ? true : false;
                }
                return _value;
            }
            set
            {
                if (this.hitText == HitText.Y_N)
                {
                    this.Text = value ? YN.Y.ToString() : YN.N.ToString();
                }
                else if (this.hitText == HitText.Yes_No)
                {
                    this.Text = value ? YesNo.Yes.ToString() : YesNo.No.ToString();
                }
                _value = value;
            }
        }

        public bool UseValueForChildsVisibilty
        {
            get;
            set;
        }
        public bool ChangeVisibility
        {
            get;
            set;
        }

        public Control ChildControl
        {
            get
            {
                if (this.childControls.Count == 0) return null;
                return this.childControls[this.childControls.Count - 1];
            }
            set { this.childControls.Add(value); }
        }

        public void AddControl(Control cntrl)
        {
            childControls.Add(cntrl);
        }

        public List<Control> GetChildControls()
        {
            return childControls;
        }

        public void ChildsVisibilty(bool visible)
        {
            if (childControls == null) return;
            foreach (Control cntrl in this.childControls)
            {
                if (cntrl != null)
                    cntrl.Visible = visible;
            }
        }

        public void ChildsVisibilty(bool visible, string controlName)
        {
            if (childControls == null) return;
            foreach (Control cntrl in this.childControls)
            {
                if (cntrl != null)
                {
                    if (cntrl.Name == controlName)
                    {
                        cntrl.Visible = visible;
                    }
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            SetChildsVisibilty();
        }

        private void SetChildsVisibilty()
        {
            try
            {
                if (this.hitText == HitText.Y_N)
                {
                    selectType = (int)(YN)Enum.Parse(typeof(YN), this.Text);
                    if (this.ChangeVisibility)
                    {
                        this.ChildsVisibilty(this.UseValueForChildsVisibilty == this.Value);
                    }
                    else
                    {
                        ChildsEnabled(this.UseValueForChildsVisibilty == this.Value);
                    }
                }
                else if (this.hitText == HitText.Yes_No)
                {
                    selectType = (int)(YesNo)Enum.Parse(typeof(YesNo), this.Text);
                    if (this.ChangeVisibility)
                    {
                        this.ChildsVisibilty(this.UseValueForChildsVisibilty == this.Value);
                    }
                    else
                    {
                        ChildsEnabled(this.UseValueForChildsVisibilty == this.Value);
                    }
                }
                else if (this.hitText == HitText.Credit_Debit)
                {
                    selectType = (int)(CreditDebit)Enum.Parse(typeof(CreditDebit), this.Text);
                }
            }
            catch (Exception ex)
            { 
                
            }
        }
        private void ChildsEnabled(bool enable)
        {
            if (childControls == null) return;
            foreach (Control cntrl in this.childControls)
            {
                if (cntrl != null)
                    cntrl.Enabled = enable;
            }
        }

        private void ChildsEnabled(bool enable, string controlName)
        {
            if (childControls == null) return;
            foreach (Control cntrl in this.childControls)
            {
                if (cntrl != null)
                {
                    if (cntrl.Name == controlName)
                    {
                        cntrl.Enabled = enable;
                    }
                }
            }
        }
    }
}


