using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;

namespace Oranikle.Studio.Controls
{
    public class CtrlCurrency : StyledTextBox, IDataGridViewEditingCell, IControlByOptions
    {
        private CtrlDropdownHostList.RelatedItemSource _ItemSource;
        private bool _Nullable;
        private Nullable<decimal> _NullableValue;
        private decimal _Value;
        private bool isDisposed;
        private Nullable<decimal> oldValue;
        private bool valueChanged;
        DataGridView dataGridView;
        int rowIndex;

        private static ExtendedDecimalNumberFormatter _CurrencyFormat;

        public event EventHandler ValueChangedEvent;

        [DefaultValue(null)]
        public object EditingCellFormattedValue
        {
            get
            {
                if (_Nullable)
                    return NullableValue;
                return Value;
            }
            set
            {
                Nullable<decimal> nullable;

                if (_Nullable)
                {
                    if (value == null)
                    {
                        nullable = new Nullable<decimal>();
                        NullableValue = nullable;
                        return;
                    }
                    NullableValue = new Nullable<decimal>((decimal)value);
                    return;
                }
                Value = (decimal)value;
            }
        }

        [DefaultValue(false)]
        public bool EditingCellValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        public new bool IsDisposed
        {
            get
            {
                return isDisposed;
            }
            set
            {
                isDisposed = value;
            }
        }

        public bool Nullable
        {
            get
            {
                return _Nullable;
            }
            set
            {
                _Nullable = value;
                if (_Nullable)
                    UpdateDisplayFromNullableValue();
                else
                    UpdateDisplayFromValue();
                Invalidate();
            }
        }

        public CtrlDropdownHostList.RelatedItemSource ItemSource
        {
            get
            {
                return _ItemSource;
            }
            set
            {
                _ItemSource = value;
            }
        }

        [Category("Data")]
        [Description("The actual value of the currency in System.Decimal.")]
        [Bindable(true)]
        [DefaultValue(null)]
        public Nullable<decimal> NullableValue
        {
            get
            {
                return _NullableValue;
            }
            set
            {
                if (!_Nullable)
                    return;
                Nullable<decimal> nullable2 = _NullableValue;
                _NullableValue = value;
                if (!value.HasValue)
                    Text = "";
                else
                    Text = CtrlCurrency.CurrencyFormat.FormatString(value);
                valueChanged = true;
                Nullable<decimal> nullable1 = nullable2;
                Nullable<decimal> nullable = _NullableValue;
                if (!(nullable1.GetValueOrDefault() != nullable.GetValueOrDefault() || (nullable1.HasValue != nullable.HasValue)))
                    RaiseValueChangedEvent();
            }
        }

        [Category("Data")]
        [Bindable(true)]
        [DefaultValue(0)]
        [Description("The actual value of the currency in System.Decimal.")]
        public decimal Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Nullable)
                    return;
                decimal dec = _Value;
                _Value = value;
                Text = CtrlCurrency.CurrencyFormat.FormatString(new Nullable<decimal>(value));
                valueChanged = true;
                if (dec != _Value)
                    RaiseValueChangedEvent();
            }
        }

        public static ExtendedDecimalNumberFormatter CurrencyFormat
        {
            get
            {
                if (CtrlCurrency._CurrencyFormat == null)
                    CtrlCurrency.UpdateCurrencyFormatFromCache();
                return CtrlCurrency._CurrencyFormat;
            }
        }

        public CtrlCurrency()
        {
            
            _Nullable = true;
            _NullableValue = new Nullable<decimal>();
            TextAlign = HorizontalAlignment.Right;
            if (!DesignMode)
                Init();
        }

        static CtrlCurrency()
        {
        }

        private void CtrlCurrency_CacheUpdatedEvent(object sender, EventArgs e)
        {
            CtrlCurrency.UpdateCurrencyFormatFromCache();
        }

        public object GetEditingCellFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Text;
        }

        private void Init()
        {
            //CurrencyCacheListManager.Instance.WeakSubscribe(new EventHandler(CtrlCurrency_CacheUpdatedEvent));
            CtrlCurrency.UpdateCurrencyFormatFromCache();
            //OptionControlsManager.Instance.RegisterOptionControls(this);
        }

        public void PrepareEditingCellForEdit(bool selectAll)
        {
            Focus();
        }

        private void RaiseValueChangedEvent()
        {
            EventHandler eventHandler = ValueChangedEvent;
            if (eventHandler != null)
                eventHandler(this, new EventArgs());
        }

        public void RefreshUIFromCompanyOrPersonalOptions()
        {
            CtrlCurrency.UpdateCurrencyFormatFromCache();
            if (_Nullable)
            {
                UpdateDisplayFromNullableValue();
                return;
            }
            UpdateDisplayFromValue();
        }

        private void TryToParseString()
        {
            Nullable<decimal> nullable;

            string s = Text;
            if ((s == null) || s == "")
            {
                if (_Nullable)
                {
                    nullable = new Nullable<decimal>();
                    NullableValue = nullable;
                    return;
                }
                Value = oldValue.Value;
                return;
            }
            decimal dec = 0M;
            if (!Decimal.TryParse(s, NumberStyles.Currency, CtrlCurrency.CurrencyFormat.Nfi, out dec))
            {
                if (_Nullable)
                {
                    NullableValue = oldValue;
                    return;
                }
                Value = oldValue.Value;
                return;
            }
            if (_Nullable)
            {
                NullableValue = new Nullable<decimal>(dec);
                return;
            }
            Value = dec;
        }

        private void UpdateDisplayFromNullableValue()
        {
            NullableValue = _NullableValue;
        }

        private void UpdateDisplayFromValue()
        {
            Value = _Value;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            isDisposed = true;
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (_Nullable)
            {
                oldValue = NullableValue;
                return;
            }
            oldValue = new Nullable<decimal>(Value);
        }

        protected override void OnLeave(EventArgs e)
        {
            TryToParseString();
            base.OnLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Select();
        }

        private static void UpdateCurrencyFormatFromCache()
        {
            CtrlCurrency._CurrencyFormat = new ExtendedDecimalNumberFormatter(System.Globalization.CultureInfo.CurrentCulture.NumberFormat);
        }


        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;
            string keyInput = e.KeyChar.ToString();
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
            else if (e.KeyChar == ' ')
            {

            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

    }
}
