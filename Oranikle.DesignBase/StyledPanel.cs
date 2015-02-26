using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oranikle.Studio.Controls
{
   
   [System.Drawing.ToolboxBitmap(typeof(System.Windows.Forms.Panel))]
    public class StyledPanel : System.Windows.Forms.Panel, Oranikle.Studio.Controls.IStickyControl
    {

        private static readonly log4net.ILog log;

        private System.Drawing.Color _BackColour2;
        private System.Drawing.Color _BorderColour;
        private System.Windows.Forms.BorderStyle _BorderStyle;
        private int _BorderWidth;
        private int _Curvature;
        private Oranikle.Studio.Controls.StyledPanel_CornerCurveMode _CurveMode;
        private Oranikle.Studio.Controls.StyledPanel_LinearGradientMode _GradientMode;
        private bool _useBasePaintFirst;
        private bool _useGlassGradient;
        private Oranikle.Studio.Controls.StickyManager stickyManager;
        private Oranikle.Studio.Controls.IStickyControl stickyParent;

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("The secondary background color used to paint the control.")]
        [System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "Window")]
        public System.Drawing.Color BackColor2
        {
            get
            {
                return _BackColour2;
            }
            set
            {
                _BackColour2 = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Description("The border color used to paint the control.")]
        [System.ComponentModel.DefaultValue(typeof(System.Drawing.Color), "WindowFrame")]
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color BorderColor
        {
            get
            {
                return _BorderColour;
            }
            set
            {
                _BorderColour = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.DefaultValue(typeof(System.Windows.Forms.BorderStyle), "None")]
        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("The border style used to paint the control.")]
        public new System.Windows.Forms.BorderStyle BorderStyle
        {
            get
            {
                return _BorderStyle;
            }
            set
            {
                _BorderStyle = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("The width of the border used to paint the control.")]
        [System.ComponentModel.DefaultValue(typeof(System.Int32), "1")]
        public int BorderWidth
        {
            get
            {
                return _BorderWidth;
            }
            set
            {
                _BorderWidth = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.DefaultValue(typeof(System.Int32), "0")]
        [System.ComponentModel.Description("The radius of the curve used to paint the corners of the control.")]
        public int Curvature
        {
            get
            {
                return _Curvature;
            }
            set
            {
                _Curvature = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.Description("The style of the curves to be drawn on the control.")]
        [System.ComponentModel.DefaultValue(typeof(Oranikle.Studio.Controls.StyledPanel_CornerCurveMode), "All")]
        [System.ComponentModel.Category("Appearance")]
        public Oranikle.Studio.Controls.StyledPanel_CornerCurveMode CurveMode
        {
            get
            {
                return _CurveMode;
            }
            set
            {
                _CurveMode = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.DefaultValue(typeof(Oranikle.Studio.Controls.StyledPanel_LinearGradientMode), "None")]
        [System.ComponentModel.Description("The gradient direction used to paint the control.")]
        [System.ComponentModel.Category("Appearance")]
        public Oranikle.Studio.Controls.StyledPanel_LinearGradientMode GradientMode
        {
            get
            {
                return _GradientMode;
            }
            set
            {
                _GradientMode = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        public Oranikle.Studio.Controls.StickyManager StickyManager
        {
            get
            {
                if (stickyManager == null)
                    stickyManager = new Oranikle.Studio.Controls.StickyManager();
                return stickyManager;
            }
        }

        public Oranikle.Studio.Controls.IStickyControl StickyParent
        {
            get
            {
                return stickyParent;
            }
            set
            {
                stickyParent = value;
            }
        }

        [System.ComponentModel.Description("To call base.PaintBackround or not.")]
        [System.ComponentModel.DefaultValue(typeof(System.Boolean), "false")]
        [System.ComponentModel.Category("Appearance")]
        public bool UseBasePaintFirst
        {
            get
            {
                return _useBasePaintFirst;
            }
            set
            {
                _useBasePaintFirst = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [System.ComponentModel.DefaultValue(typeof(System.Boolean), "false")]
        [System.ComponentModel.Description("To use glass effect gradient or not.")]
        [System.ComponentModel.Category("Appearance")]
        public bool UseGlassGradient
        {
            get
            {
                return _useGlassGradient;
            }
            set
            {
                _useGlassGradient = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        public StyledPanel()
        {
            _BackColour2 = System.Drawing.Color.White;
            _GradientMode = Oranikle.Studio.Controls.StyledPanel_LinearGradientMode.None;
            _BorderColour = System.Drawing.SystemColors.WindowFrame;
            _BorderWidth = 1;
            _CurveMode = Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.All;
            SetDefaultControlStyles();
            BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        static StyledPanel()
        {
            Oranikle.Studio.Controls.StyledPanel.log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        private System.Drawing.Drawing2D.LinearGradientBrush CreateGlassGradientBrush(System.Drawing.Rectangle Rectangle, System.Drawing.Color Color1, System.Drawing.Color Color2)
        {
            System.Drawing.Drawing2D.LinearGradientBrush linearGradientBrush = null;
            if (_GradientMode == Oranikle.Studio.Controls.StyledPanel_LinearGradientMode.None)
                linearGradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(Rectangle, Color1, Color2, (System.Drawing.Drawing2D.LinearGradientMode)_GradientMode);
            else
                linearGradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(Rectangle, Color1, Color2, (System.Drawing.Drawing2D.LinearGradientMode)_GradientMode);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(1, Rectangle.Height);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.FillRectangle(linearGradientBrush, new System.Drawing.Rectangle(0, 0, 1, Rectangle.Height));
            System.Drawing.Drawing2D.ColorBlend colorBlend = new System.Drawing.Drawing2D.ColorBlend(4);
            colorBlend.Colors[0] = bitmap.GetPixel(0, 0);
            colorBlend.Colors[1] = bitmap.GetPixel(0, bitmap.Height / 3);
            colorBlend.Colors[2] = bitmap.GetPixel(0, bitmap.Height - 1);
            colorBlend.Colors[3] = bitmap.GetPixel(0, bitmap.Height / 3);
            colorBlend.Positions[0] = 0.0F;
            colorBlend.Positions[1] = 0.335F;
            colorBlend.Positions[2] = 0.335F;
            colorBlend.Positions[3] = 1.0F;
            linearGradientBrush.InterpolationColors = colorBlend;
            graphics.Dispose();
            bitmap.Dispose();
            return linearGradientBrush;
        }

        public void DeleteSticky(Oranikle.Studio.Controls.CtrlStickyNote toDelete)
        {
            stickyManager.DeleteSticky(toDelete, this);
            stickyParent.RefreshStickyDisplay();
        }

        private void DrawCurvedPanel(System.Windows.Forms.PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(GetColorBehindCurve());
            pevent.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            pevent.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            pevent.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            System.Drawing.Drawing2D.GraphicsPath graphicsPath = GetCurvedPath();
            System.Drawing.Brush brush = GetBackgroundBrush(pevent);
            pevent.Graphics.FillPath(brush, graphicsPath);
            brush.Dispose();
            if ((_BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle) && (_BorderWidth > 0))
            {
                System.Drawing.Pen pen = GetBorderPen(_BorderColour, _BorderWidth);
                pevent.Graphics.DrawPath(pen, graphicsPath);
                pen.Dispose();
            }
            graphicsPath.Dispose();
        }

        private void DrawRectPanel(System.Windows.Forms.PaintEventArgs pevent)
        {
            if ((_GradientMode == Oranikle.Studio.Controls.StyledPanel_LinearGradientMode.None) || BackColor == _BackColour2)
            {
                pevent.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Invalid;
                pevent.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                pevent.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            }
            else
            {
                pevent.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                pevent.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                pevent.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            }
            System.Drawing.Brush brush = GetBackgroundBrush(pevent);
            pevent.Graphics.FillRectangle(brush, ClientRectangle);
            brush.Dispose();
            if ((_BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle) && (_BorderWidth > 0))
            {
                System.Drawing.Pen pen = GetBorderPen(_BorderColour, _BorderWidth);
                System.Drawing.Rectangle rectangle2 = ClientRectangle;
                System.Drawing.Point point = rectangle2.Location;
                point.X += _BorderWidth / 2;
                point.Y += _BorderWidth / 2;
                System.Drawing.Rectangle rectangle3 = ClientRectangle;
                System.Drawing.Size size = rectangle3.Size;
                size.Width -= _BorderWidth;
                size.Height -= _BorderWidth;
                System.Drawing.Rectangle rectangle1 = new System.Drawing.Rectangle(point, size);
                pevent.Graphics.DrawRectangle(pen, rectangle1);
                pen.Dispose();
            }
        }

        private int GetAdjustedCurve()
        {
            if ((_CurveMode == Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.None) || (_Curvature == 0))
                return 0;
            int i = 0;
            System.Drawing.Rectangle rectangle1 = ClientRectangle;
            if (_Curvature > (rectangle1.Width / 2))
            {
                System.Drawing.Rectangle rectangle2 = ClientRectangle;
                i = Oranikle.Studio.Controls.StyledPanel.DoubleToInt((double)(rectangle2.Width / 2));
            }
            else
            {
                System.Drawing.Rectangle rectangle3 = ClientRectangle;
                if (i > (rectangle3.Height / 2))
                {
                    System.Drawing.Rectangle rectangle4 = ClientRectangle;
                    i = Oranikle.Studio.Controls.StyledPanel.DoubleToInt((double)(rectangle4.Height / 2));
                }
                else
                {
                    i = _Curvature;
                }
            }
            return i;
        }

        private System.Drawing.Brush GetBackgroundBrush(System.Windows.Forms.PaintEventArgs pevent)
        {
            if ((_GradientMode == Oranikle.Studio.Controls.StyledPanel_LinearGradientMode.None) || BackColor == _BackColour2)
                return new System.Drawing.SolidBrush(BackColor);
            if (!_useGlassGradient)
                return new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, BackColor, _BackColour2, (System.Drawing.Drawing2D.LinearGradientMode)_GradientMode);
            return CreateGlassGradientBrush(ClientRectangle, BackColor, _BackColour2);
        }

        private System.Drawing.Pen GetBorderPen(System.Drawing.Color borderColor, int borderWidth)
        {
            return new System.Drawing.Pen(borderColor, (float)borderWidth);
        }

        private System.Drawing.Color GetColorBehindCurve()
        {
            System.Windows.Forms.Control control = Parent;
            while ((control != null) && control.BackColor == System.Drawing.Color.Transparent)
            {
                control = control.Parent;
            }
            if (control == null)
                return System.Drawing.Color.Transparent;
            return control.BackColor;
        }

        protected System.Drawing.Drawing2D.GraphicsPath GetCurvedPath()
        {
            System.Drawing.Rectangle rectangle = ClientRectangle;
            int i1 = GetAdjustedCurve();
            int i2 = 0;
            if (_BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle)
            {
                if (_BorderWidth > 1)
                    i2 = Oranikle.Studio.Controls.StyledPanel.DoubleToInt((double)(BorderWidth / 2));
                else if (_BorderWidth == 1)
                    i2 = 1;
            }
            int i3 = rectangle.Width - 1 - i2;
            int i4 = rectangle.Height - 1 - i2;
            int i5 = 1;
            System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
            if ((_CurveMode & Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.TopRight) != Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.None)
                i5 = i1 * 2;
            else
                i5 = 1;
            graphicsPath.AddArc(i3 - i5, i2, i5, i5, 270.0F, 90.0F);
            if ((_CurveMode & Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.BottomRight) != Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.None)
                i5 = i1 * 2;
            else
                i5 = 1;
            graphicsPath.AddArc(i3 - i5, i4 - i5, i5, i5, 0.0F, 90.0F);
            if ((_CurveMode & Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.BottomLeft) != Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.None)
                i5 = i1 * 2;
            else
                i5 = 1;
            graphicsPath.AddArc(i2, i4 - i5, i5, i5, 90.0F, 90.0F);
            if ((_CurveMode & Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.TopLeft) != Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.None)
                i5 = i1 * 2;
            else
                i5 = 1;
            graphicsPath.AddArc(i2, i2, i5, i5, 180.0F, 90.0F);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        private System.Drawing.Point GetParentFormLocation(System.Windows.Forms.Control c)
        {
            if (c is System.Windows.Forms.Form)
                return c.Location;
            if (c is System.Windows.Forms.SplitterPanel)
                return GetParentFormLocation(c.Parent);
            if (c is System.Windows.Forms.SplitContainer)
            {
                System.Drawing.Point point1 = GetParentFormLocation(c.Parent);
                System.Drawing.Point point3 = c.Location;
                point1.X += point3.X;
                System.Drawing.Point point4 = c.Location;
                point1.Y += point4.Y;
                System.Windows.Forms.SplitContainer splitContainer = (System.Windows.Forms.SplitContainer)c;
                if (splitContainer.Orientation == System.Windows.Forms.Orientation.Horizontal)
                {
                    if (splitContainer.Panel2 == c)
                    {
                        point1.Y += splitContainer.SplitterDistance;
                        point1.Y += splitContainer.SplitterWidth;
                    }
                }
                else if (splitContainer.Panel2 == c)
                {
                    point1.X += splitContainer.SplitterDistance;
                    point1.X += splitContainer.SplitterWidth;
                }
                return point1;
            }
            System.Drawing.Point point2 = GetParentFormLocation(c.Parent);
            System.Drawing.Point point5 = c.Location;
            point2.X += point5.X;
            System.Drawing.Point point6 = c.Location;
            point2.Y += point6.Y;
            return point2;
        }

        public System.Drawing.Point GetParentFormLocation()
        {
            return GetParentFormLocation(this);
        }

        private System.Drawing.Point GetScreenLocation(System.Windows.Forms.Control c)
        {
            if (c is System.Windows.Forms.Form)
            {
                System.Drawing.Point point1 = c.Location;
                point1.Y += 25;
                return point1;
            }
            if (c is System.Windows.Forms.SplitterPanel)
            {
                System.Drawing.Point point2 = GetScreenLocation(c.Parent);
                System.Windows.Forms.SplitContainer splitContainer = (System.Windows.Forms.SplitContainer)c.Parent;
                if (splitContainer.Orientation == System.Windows.Forms.Orientation.Vertical)
                {
                    if ((splitContainer.Panel2 == c) && !splitContainer.Panel1Collapsed)
                    {
                        point2.X += splitContainer.SplitterDistance;
                        point2.X += splitContainer.SplitterWidth;
                        System.Windows.Forms.Padding padding1 = ((System.Windows.Forms.SplitterPanel)c).Padding;
                        point2.X += padding1.Left;
                    }
                }
                else if ((splitContainer.Panel2 == c) && !splitContainer.Panel1Collapsed)
                {
                    point2.Y += splitContainer.SplitterDistance;
                    point2.Y += splitContainer.SplitterWidth;
                    System.Windows.Forms.Padding padding2 = ((System.Windows.Forms.SplitterPanel)c).Padding;
                    point2.Y += padding2.Top;
                }
                return point2;
            }
            if (c is Oranikle.Studio.Controls.StyledPanel)
            {
                System.Drawing.Point point3 = GetScreenLocation(c.Parent);
                System.Drawing.Point point5 = c.Location;
                point3.X += point5.X;
                System.Drawing.Point point6 = c.Location;
                point3.Y += point6.Y;
                if (((Oranikle.Studio.Controls.StyledPanel)c).BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle)
                {
                    point3.X += ((Oranikle.Studio.Controls.StyledPanel)c).BorderWidth;
                    point3.Y += ((Oranikle.Studio.Controls.StyledPanel)c).BorderWidth;
                }
                return point3;
            }
            System.Drawing.Point point4 = GetScreenLocation(c.Parent);
            System.Drawing.Point point7 = c.Location;
            point4.X += point7.X;
            System.Drawing.Point point8 = c.Location;
            point4.Y += point8.Y;
            return point4;
        }

        public System.Drawing.Point GetScreenLocation()
        {
            System.Drawing.Point point = GetScreenLocation(this);
            System.Windows.Forms.Padding padding1 = Padding;
            point.X += padding1.Left;
            System.Windows.Forms.Padding padding2 = Padding;
            point.Y += padding2.Top;
            if (BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle)
            {
                point.X += BorderWidth;
                point.Y += BorderWidth;
            }
            return point;
        }

        public void HideAllStickies()
        {
            stickyManager.IsShowingStickies = false;
            RefreshStickyDisplay();
        }

        private bool IsNormalPanel()
        {
            if ((_CurveMode != Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.None) && (_Curvature > 0))
                return false;
            if ((_BorderStyle == System.Windows.Forms.BorderStyle.FixedSingle) && (_BorderWidth > 0))
                return false;
            if (BackColor != _BackColour2 && (_GradientMode != Oranikle.Studio.Controls.StyledPanel_LinearGradientMode.None))
                return false;
            return true;
        }

        public void MoveTop(int targetTop)
        {
            int i = targetTop - Top;
            Top = targetTop;
            Height -= i;
        }

        public void RefreshStickyDisplay()
        {
            stickyParent.RefreshStickyDisplay();
        }

        public void ScrollPanel(System.Windows.Forms.MouseEventArgs e)
        {
            OnMouseWheel(e);
        }

        private void SetDefaultControlStyles()
        {
            SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            SetStyle(System.Windows.Forms.ControlStyles.UserMouse, true);
            SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
            SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(System.Windows.Forms.ControlStyles.ContainerControl, true);
            UpdateStyles();
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            if (IsNormalPanel())
            {
                base.OnPaintBackground(pevent);
                return;
            }
            if ((_CurveMode == Oranikle.Studio.Controls.StyledPanel_CornerCurveMode.None) || (_Curvature == 0))
            {
                DrawRectPanel(pevent);
                return;
            }
            DrawCurvedPanel(pevent);
        }

        public static int DoubleToInt(double value)
        {
            return System.Decimal.ToInt32(System.Decimal.Floor(System.Decimal.Parse(value.ToString())));
        }

    } 
}
