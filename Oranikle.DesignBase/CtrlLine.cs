using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
    [System.ComponentModel.Designer(typeof(Oranikle.Studio.Controls.CtrlLineDesigner))]
    public class CtrlLine : System.Windows.Forms.Control
    {

        private int angle;
        private bool blend;
        private System.Drawing.Color bottomLineColor;
        private int lineWidth;
        private System.Windows.Forms.Orientation orientation;

        private System.Drawing.Color topLineColor;

        public event System.EventHandler OrientationChanged;

        [System.ComponentModel.Description("")]
        [System.ComponentModel.Browsable(false)]
        public int Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("If true then the two colors will be blended together.")]
        [System.ComponentModel.DefaultValue(false)]
        public bool Blend
        {
            get
            {
                return blend;
            }
            set
            {
                blend = value;
                Invalidate();
            }
        }

        [System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "ControlLightLight")]
        [System.ComponentModel.Description("")]
        public System.Drawing.Color BottomLineColor
        {
            get
            {
                return bottomLineColor;
            }
            set
            {
                bottomLineColor = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Description("The width of each line.")]
        [System.ComponentModel.DefaultValue(1)]
        public int LineWidth
        {
            get
            {
                return lineWidth;
            }
            set
            {
                lineWidth = value;
                base.OnResize(null);
            }
        }

        [System.ComponentModel.DefaultValue(System.Windows.Forms.Orientation.Horizontal)]
        [System.ComponentModel.Description("")]
        public System.Windows.Forms.Orientation Orientation
        {
            get
            {
                return orientation;
            }
            set
            {
                orientation = value;
                if (orientation == System.Windows.Forms.Orientation.Horizontal)
                {
                    Width = Height;
                    Angle = 90;
                }
                else
                {
                    Height = Width;
                    Angle = 0;
                }
                base.OnResize(null);
                if (OrientationChanged != null)
                    OrientationChanged(this, new System.EventArgs());
            }
        }

        [System.ComponentModel.Description("")]
        [System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "ControlDark")]
        public System.Drawing.Color TopLineColor
        {
            get
            {
                return topLineColor;
            }
            set
            {
                topLineColor = value;
                Invalidate();
            }
        }

        [System.ComponentModel.Browsable(false)]
        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public override System.Drawing.Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public override System.Drawing.Font Font
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

        [System.ComponentModel.Browsable(false)]
        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        public CtrlLine()
        {
            SetStyle(System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            lineWidth = 1;
            topLineColor = System.Drawing.SystemColors.ControlDark;
            bottomLineColor = System.Drawing.SystemColors.ControlLightLight;
            orientation = System.Windows.Forms.Orientation.Horizontal;
            blend = false;
            angle = 90;
            TabStop = false;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle1, rectangle2, rectangle3;

            System.Drawing.Graphics graphics = e.Graphics;
            System.Drawing.SolidBrush solidBrush1 = new System.Drawing.SolidBrush(topLineColor);
            System.Drawing.SolidBrush solidBrush2 = new System.Drawing.SolidBrush(bottomLineColor);
            if (orientation == System.Windows.Forms.Orientation.Horizontal)
            {
                if (blend)
                {
                    rectangle1 = new System.Drawing.Rectangle(0, 0, Width, Height);
                    graphics.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(rectangle1, topLineColor, bottomLineColor, (float)angle, false), rectangle1);
                    return;
                }
                rectangle2 = new System.Drawing.Rectangle(0, 0, Width, lineWidth);
                rectangle3 = new System.Drawing.Rectangle(0, lineWidth, Width, lineWidth * 2);
                graphics.FillRectangle(solidBrush1, rectangle2);
                graphics.FillRectangle(solidBrush2, rectangle3);
                return;
            }
            if (blend)
            {
                rectangle1 = new System.Drawing.Rectangle(0, 0, Width, Height);
                graphics.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(rectangle1, topLineColor, bottomLineColor, (float)angle, false), rectangle1);
                return;
            }
            rectangle2 = new System.Drawing.Rectangle(0, 0, lineWidth, Height);
            rectangle3 = new System.Drawing.Rectangle(lineWidth, 0, lineWidth * 2, Height);
            graphics.FillRectangle(solidBrush1, rectangle2);
            graphics.FillRectangle(solidBrush2, rectangle3);
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
        }

        protected override void OnResize(System.EventArgs e)
        {
            if (orientation == System.Windows.Forms.Orientation.Horizontal)
                Height = lineWidth * 2;
            else
                Width = lineWidth * 2;
            Invalidate();
        }

    } 
}
