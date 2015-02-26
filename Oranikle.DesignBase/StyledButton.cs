using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;


namespace Oranikle.Studio.Controls
{

    public class StyledButton : System.Windows.Forms.Button
    {

        private System.Drawing.Color _BackColor2;
        private System.Drawing.Drawing2D.LinearGradientMode _BackFillMode;
        private System.Drawing.Color _BorderColor;
        private System.Nullable<System.Drawing.Size> _OverriddenSize;

        private static System.Drawing.Color _BttnBackColor1;
        private static System.Drawing.Color _BttnBackColor2;
        private static System.Drawing.Drawing2D.LinearGradientMode _BttnBackFillMode;
        private static System.Drawing.Color _BttnBorderColor;
        private static System.Drawing.Color _BttnClickBackColor1;
        private static System.Drawing.Color _BttnClickBackColor2;
        private static System.Drawing.Color _BttnClickBorderColor;
        private static System.Drawing.Color _BttnClickTextColor;
        private static System.Drawing.Color _BttnDisableBackColor1;
        private static System.Drawing.Color _BttnDisableBackColor2;
        private static System.Drawing.Color _BttnDisableBorderColor;
        private static System.Drawing.Color _BttnDisableTextColor;
        private static System.Drawing.Color _BttnMouseOverBackColor1;
        private static System.Drawing.Color _BttnMouseOverBackColor2;
        private static System.Drawing.Color _BttnMouseOverBorderColor;
        private static System.Drawing.Color _BttnMouseOverTextColor;
        private static System.Drawing.Color _BttnTextColor;

        public System.Drawing.Color BackColor2
        {
            get
            {
                return _BackColor2;
            }
            set
            {
            }
        }

        public System.Drawing.Drawing2D.LinearGradientMode BackFillMode
        {
            get
            {
                return _BackFillMode;
            }
            set
            {
            }
        }

        public System.Drawing.Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
            }
        }

        public new System.Windows.Forms.FlatStyle FlatStyle
        {
            get
            {
                return base.FlatStyle;
            }
            set
            {
            }
        }

        public new System.Drawing.Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        public new System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                //base.ForeColor = value;
            }
        }

        public System.Nullable<System.Drawing.Size> OverriddenSize
        {
            get
            {
                return _OverriddenSize;
            }
            set
            {
                _OverriddenSize = value;
                if (_OverriddenSize.HasValue)
                    base.Size = _OverriddenSize.Value;
            }
        }

        public new System.Drawing.Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                if (!_OverriddenSize.HasValue)
                    base.Size = new System.Drawing.Size(value.Width, 21);
            }
        }

        public new bool UseVisualStyleBackColor
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
               // base.BackColor = value;
            }
        }

        public StyledButton()
        {
            //if (!DesignMode)
            LP.Validate();
            _BorderColor = _BttnBorderColor;
            _BackColor2 = _BttnBackColor2;
            _BackFillMode = _BttnBackFillMode;
            base.ForeColor = _BttnTextColor;
            base.BackColor = _BttnBackColor1;
            _BackColor2 = _BttnBackColor2;
            _BorderColor = _BttnBorderColor;
            _BackFillMode = _BttnBackFillMode;
            base.Font = new System.Drawing.Font("Arial", 9.0F);
            base.UseVisualStyleBackColor = false;
            base.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            base.ImageAlign = ContentAlignment.MiddleLeft;
            Invalidate();
        }

        static StyledButton()
        {
            _BttnBackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            _BttnTextColor = System.Drawing.Color.FromArgb(2, 11, 16);
            _BttnBackColor1 = System.Drawing.Color.FromArgb(255, 255, 255);
            _BttnBackColor2 = System.Drawing.Color.FromArgb(255, 255, 255);
            _BttnBorderColor = System.Drawing.Color.FromArgb(27, 161, 226);
            _BttnDisableTextColor = System.Drawing.Color.Silver;
            _BttnDisableBackColor1 = System.Drawing.Color.FromArgb(218, 240, 251);
            _BttnDisableBackColor2 = System.Drawing.Color.FromArgb(218, 240, 251);
            _BttnDisableBorderColor = System.Drawing.Color.FromArgb(201, 233, 249);
            //_BttnMouseOverTextColor = System.Drawing.Color.FromArgb(75, 75, 75);
            //_BttnMouseOverBackColor1 = System.Drawing.Color.FromArgb(240, 240, 225);
            //_BttnMouseOverBackColor2 = System.Drawing.Color.FromArgb(220, 220, 205);
            //_BttnMouseOverBorderColor = System.Drawing.Color.FromArgb(175, 175, 160);

            _BttnMouseOverTextColor = System.Drawing.Color.FromArgb(255, 255, 255);
            _BttnMouseOverBackColor1 = System.Drawing.Color.FromArgb(60, 176, 232);
            _BttnMouseOverBackColor2 = System.Drawing.Color.FromArgb(60, 176, 232);
            _BttnMouseOverBorderColor = System.Drawing.Color.FromArgb(60, 176, 232);

            _BttnClickTextColor = System.Drawing.Color.FromArgb(255, 255, 255);
            _BttnClickBackColor1 = System.Drawing.Color.FromArgb(43, 169, 230);
            _BttnClickBackColor2 = System.Drawing.Color.FromArgb(43, 169, 230);
            _BttnClickBorderColor = System.Drawing.Color.FromArgb(43, 169, 230);
            //_BttnBackFillMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            //_BttnTextColor = System.Drawing.Color.FromArgb(90, 90, 90);
            //_BttnBackColor1 = System.Drawing.Color.FromArgb(245, 245, 245);
            //_BttnBackColor2 = System.Drawing.Color.FromArgb(225, 225, 225);
            //_BttnBorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            //_BttnDisableTextColor = System.Drawing.Color.Silver;
            //_BttnDisableBackColor1 = System.Drawing.Color.FromArgb(245, 245, 245);
            //_BttnDisableBackColor2 = System.Drawing.Color.FromArgb(225, 225, 225);
            //_BttnDisableBorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            ////_BttnMouseOverTextColor = System.Drawing.Color.FromArgb(75, 75, 75);
            ////_BttnMouseOverBackColor1 = System.Drawing.Color.FromArgb(240, 240, 225);
            ////_BttnMouseOverBackColor2 = System.Drawing.Color.FromArgb(220, 220, 205);
            ////_BttnMouseOverBorderColor = System.Drawing.Color.FromArgb(175, 175, 160);

            //_BttnMouseOverTextColor = System.Drawing.Color.FromArgb(75, 75, 75);
            //_BttnMouseOverBackColor1 = CustomColors.ButtonHoverLight; //System.Drawing.Color.FromArgb(240, 240, 225);
            //_BttnMouseOverBackColor2 = CustomColors.ButtonHoverLight;  //System.Drawing.Color.FromArgb(220, 220, 205);
            //_BttnMouseOverBorderColor = System.Drawing.Color.FromArgb(175, 175, 160);

            //_BttnClickTextColor = System.Drawing.Color.FromArgb(90, 90, 90);
            //_BttnClickBackColor1 = System.Drawing.Color.FromArgb(210, 210, 210);
            //_BttnClickBackColor2 = System.Drawing.Color.FromArgb(190, 190, 190);
            //_BttnClickBorderColor = System.Drawing.Color.FromArgb(175, 175, 175);
        }

        protected override void OnEnabledChanged(System.EventArgs e)
        {
            SuspendLayout();
            base.OnEnabledChanged(e);
            if (Enabled)
            {
                base.ForeColor = _BttnTextColor;
                base.BackColor = _BttnBackColor1;
                _BackColor2 = _BttnBackColor2;
                _BorderColor = _BttnBorderColor;
            }
            else
            {
                base.ForeColor = _BttnDisableTextColor;
                base.BackColor = _BttnDisableBackColor1;
                _BackColor2 = _BttnDisableBackColor2;
                _BorderColor = _BttnDisableBorderColor;
            }
            ResumeLayout();
        }

        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            if (Enabled)
            {
                SuspendLayout();
                base.ForeColor = _BttnMouseOverTextColor;
                base.BackColor = _BttnMouseOverBackColor1;
                _BackColor2 = _BttnMouseOverBackColor2;
                _BorderColor = _BttnMouseOverBorderColor;
                ResumeLayout();
            }
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            if (Enabled)
            {
                SuspendLayout();
                base.ForeColor = _BttnTextColor;
                base.BackColor = _BttnBackColor1;
                _BackColor2 = _BttnBackColor2;
                _BorderColor = _BttnBorderColor;
                ResumeLayout();
            }
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs mevent)
        {
            SuspendLayout();
            base.OnMouseDown(mevent);
            base.ForeColor = _BttnClickTextColor;
            base.BackColor = _BttnClickBackColor1;
            _BackColor2 = _BttnClickBackColor2;
            _BorderColor = _BttnClickBorderColor;
            ResumeLayout();
        }

        protected override void OnMouseEnter(System.EventArgs e)
        {
            SuspendLayout();
            base.OnMouseEnter(e);
            base.ForeColor = _BttnMouseOverTextColor;
            base.BackColor = _BttnMouseOverBackColor1;
            _BackColor2 = _BttnMouseOverBackColor2;
            _BorderColor = _BttnMouseOverBorderColor;
            ResumeLayout();
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            SuspendLayout();
            base.OnMouseLeave(e);
            //if (!this.ContainsFocus)
            {
                base.ForeColor = _BttnTextColor;
                base.BackColor = _BttnBackColor1;
                _BackColor2 = _BttnBackColor2;
                _BorderColor = _BttnBorderColor;
            }
            ResumeLayout();
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs mevent)
        {
            SuspendLayout();
            base.OnMouseUp(mevent);
            base.ForeColor = _BttnTextColor;
            base.BackColor = _BttnBackColor1;
            _BackColor2 = _BttnBackColor2;
            _BorderColor = _BttnBorderColor;
            ResumeLayout();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
        {
            pevent.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            pevent.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            pevent.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            System.Drawing.Brush brush1 = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, base.BackColor, _BackColor2, BackFillMode);
            pevent.Graphics.FillRectangle(brush1, ClientRectangle);
            brush1.Dispose();
            System.Drawing.Pen pen = new System.Drawing.Pen(BorderColor, 2.0F);
            System.Drawing.Rectangle rectangle3 = ClientRectangle;
            System.Drawing.Rectangle rectangle4 = ClientRectangle;
            System.Drawing.Rectangle rectangle5 = ClientRectangle;
            System.Drawing.Rectangle rectangle1 = new System.Drawing.Rectangle(rectangle3.Location, new System.Drawing.Size(rectangle4.Width, rectangle5.Height));
            pevent.Graphics.DrawRectangle(pen, rectangle1);
            pen.Dispose();
            System.Drawing.StringFormat stringFormat = new System.Drawing.StringFormat();
            stringFormat.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide;
            stringFormat.FormatFlags = System.Drawing.StringFormatFlags.LineLimit;
            stringFormat.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
            System.Drawing.Brush brush2 = new System.Drawing.SolidBrush(base.ForeColor);
            pevent.Graphics.DrawString(Text, Font, brush2, ClientRectangle, stringFormat);
            if (Image != null)
            {
                System.Drawing.Image image = Image;
                System.Drawing.Rectangle rectangle6 = ClientRectangle;
                System.Drawing.Point point1 = rectangle6.Location;
                System.Drawing.Rectangle rectangle7 = ClientRectangle;
                int i1 = point1.X + 5;
                if (this.ImageAlign == ContentAlignment.MiddleCenter)
                {
                    i1 = point1.X + ((rectangle7.Width - image.Width) / 2);
                }
                System.Drawing.Rectangle rectangle8 = ClientRectangle;
                System.Drawing.Point point2 = rectangle8.Location;
                System.Drawing.Rectangle rectangle9 = ClientRectangle;
                int i2 = point2.Y + ((rectangle9.Height - image.Height) / 2);
                System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle(i1, i2, image.Width, image.Height);
                pevent.Graphics.DrawImage(image, rectangle2);
            }
        }

    } // class StyledButton
}
