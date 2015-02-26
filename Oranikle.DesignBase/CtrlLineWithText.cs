using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{

    [System.Drawing.ToolboxBitmap(typeof(System.Windows.Forms.GroupBox))]
    [System.ComponentModel.DefaultProperty("Caption")]
    public class CtrlLineWithText : System.Windows.Forms.UserControl
    {

        private string _Caption;
        private int _CaptionMarginSpace;
        private Oranikle.Studio.Controls.CaptionOrizontalAlign _CaptionOrizontalAlign;
        private int _CaptionPadding;
        private System.Drawing.Color _LineColor;
        private Oranikle.Studio.Controls.LineVerticalAlign _LineVerticalAlign;
        private System.ComponentModel.Container components;

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Description("The caption text displayed on the line. If the caption is \"\" (the default) the line is not broken")]
        public string Caption
        {
            get
            {
                return _Caption;
            }
            set
            {
                _Caption = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("The distance in pixels form the control margin to caption text")]
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(16)]
        public int CaptionMarginSpace
        {
            get
            {
                return _CaptionMarginSpace;
            }
            set
            {
                _CaptionMarginSpace = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("Tell where the text caption is aligned in the control")]
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(Oranikle.Studio.Controls.CaptionOrizontalAlign.Left)]
        public Oranikle.Studio.Controls.CaptionOrizontalAlign CaptionOrizontalAlign
        {
            get
            {
                return _CaptionOrizontalAlign;
            }
            set
            {
                _CaptionOrizontalAlign = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("The space in pixels arrownd text caption")]
        [System.ComponentModel.DefaultValue(2)]
        [System.ComponentModel.Category("Appearance")]
        public int CaptionPadding
        {
            get
            {
                return _CaptionPadding;
            }
            set
            {
                _CaptionPadding = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("Color of the line.")]
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue("")]
        public System.Drawing.Color LineColor
        {
            get
            {
                return _LineColor;
            }
            set
            {
                _LineColor = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(Oranikle.Studio.Controls.LineVerticalAlign.Middle)]
        [System.ComponentModel.Description("The vertical alignement of the line within the space of the control")]
        public Oranikle.Studio.Controls.LineVerticalAlign LineVerticalAlign
        {
            get
            {
                return _LineVerticalAlign;
            }
            set
            {
                _LineVerticalAlign = value;
                Invalidate();
            }
        }

        public CtrlLineWithText()
        {
            _Caption = "";
            _CaptionMarginSpace = 16;
            _CaptionPadding = 2;
            _LineColor = System.Drawing.Color.DimGray;
            _LineVerticalAlign = Oranikle.Studio.Controls.LineVerticalAlign.Middle;
            InitializeComponent();
            TabStop = false;
        }

        private void InitializeComponent()
        {
            Name = "CtrlLineWithText";
            Size = new System.Drawing.Size(100, Font.Height);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnFontChanged(System.EventArgs e)
        {
            base.OnResize(e);
            base.OnFontChanged(e);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            int i1, i3, i4;

            base.OnPaint(e);
            switch (LineVerticalAlign)
            {
                case Oranikle.Studio.Controls.LineVerticalAlign.Top:
                    i1 = 0;
                    break;

                case Oranikle.Studio.Controls.LineVerticalAlign.Middle:
                    System.Drawing.Size size1 = Size;
                    i1 = System.Convert.ToInt32(System.Math.Ceiling(new System.Decimal(size1.Height / 2))) - 1;
                    break;

                case Oranikle.Studio.Controls.LineVerticalAlign.Bottom:
                    System.Drawing.Size size2 = Size;
                    i1 = size2.Height - 2;
                    break;

                default:
                    i1 = 0;
                    break;
            }
            System.Drawing.SizeF sizeF = e.Graphics.MeasureString(Caption, Font, Width - (CaptionMarginSpace * 2), System.Drawing.StringFormat.GenericDefault);
            int i2 = System.Convert.ToInt32(sizeF.Width);
            if (Caption == "")
            {
                i3 = i4 = CaptionMarginSpace;
            }
            else
            {
                switch (CaptionOrizontalAlign)
                {
                    case Oranikle.Studio.Controls.CaptionOrizontalAlign.Left:
                        i3 = CaptionMarginSpace;
                        i4 = CaptionMarginSpace + (CaptionPadding * 2) + i2;
                        break;

                    case Oranikle.Studio.Controls.CaptionOrizontalAlign.Center:
                        i3 = ((Width - i2) / 2) - CaptionPadding;
                        i4 = ((Width - i2) / 2) + i2 + CaptionPadding;
                        break;

                    case Oranikle.Studio.Controls.CaptionOrizontalAlign.Right:
                        i3 = Width - (CaptionMarginSpace * 2) - i2;
                        i4 = Width - CaptionMarginSpace;
                        break;

                    default:
                        i3 = i4 = CaptionMarginSpace;
                        break;
                }
            }
            System.Drawing.Point[] pointArr1 = new System.Drawing.Point[3];
            pointArr1[0] = new System.Drawing.Point(0, i1 + 1);
            pointArr1[1] = new System.Drawing.Point(0, i1);
            pointArr1[2] = new System.Drawing.Point(i3, i1);
            e.Graphics.DrawLines(new System.Drawing.Pen(_LineColor, 1.0F), pointArr1);
            System.Drawing.Point[] pointArr2 = new System.Drawing.Point[2];
            pointArr2[0] = new System.Drawing.Point(i4, i1);
            pointArr2[1] = new System.Drawing.Point(Width, i1);
            e.Graphics.DrawLines(new System.Drawing.Pen(_LineColor, 1.0F), pointArr2);
            System.Drawing.Point[] pointArr3 = new System.Drawing.Point[2];
            pointArr3[0] = new System.Drawing.Point(0, i1 + 1);
            pointArr3[1] = new System.Drawing.Point(i3, i1 + 1);
            e.Graphics.DrawLines(new System.Drawing.Pen(System.Drawing.Color.White, 1.0F), pointArr3);
            System.Drawing.Point[] pointArr4 = new System.Drawing.Point[3];
            pointArr4[0] = new System.Drawing.Point(i4, i1 + 1);
            pointArr4[1] = new System.Drawing.Point(Width, i1 + 1);
            pointArr4[2] = new System.Drawing.Point(Width, i1);
            e.Graphics.DrawLines(new System.Drawing.Pen(System.Drawing.Color.White, 1.0F), pointArr4);
            if (Caption != "")
                e.Graphics.DrawString(Caption, Font, new System.Drawing.SolidBrush(ForeColor), (float)(i3 + CaptionPadding), 1.0F);
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Height = Font.Height + 2;
            Invalidate();
        }

    } // class CtrlLineWithText

}

