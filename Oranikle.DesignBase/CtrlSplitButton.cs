using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Oranikle.Studio.Controls.Properties;

namespace Oranikle.Studio.Controls
{

    public class CtrlSplitButton : Oranikle.Studio.Controls.StyledButton
    {

        private bool _AlwaysDropDown;
        private bool _AlwaysHoverChange;
        private bool _CalculateSplitRect;
        private string _ClickedImage;
        private System.Windows.Forms.ImageList _DefaultSplitImages;
        private string _DisabledImage;
        private bool _DoubleClickedEnabled;
        private bool _FillSplitHeight;
        private string _FocusedImage;
        private string _HoverImage;
        private string _NormalImage;
        private bool _OnlyShowTriangle;
        private int _SplitHeight;
        private int _SplitWidth;

        private System.ComponentModel.IContainer components;

        [System.ComponentModel.Category("Action")]
        [System.ComponentModel.Description("Occurs when the button part of the SplitButton is clicked.")]
        [System.ComponentModel.Browsable(true)]
        public event System.EventHandler ButtonClick;
        [System.ComponentModel.Description("Occurs when the button part of the SplitButton is clicked.")]
        [System.ComponentModel.Category("Action")]
        [System.ComponentModel.Browsable(true)]
        public event System.EventHandler ButtonDoubleClick;

        [System.ComponentModel.Category("Split Button")]
        [System.ComponentModel.Description("Indicates whether the SplitButton always shows the drop down menu even if the button part of the SplitButton is clicked.")]
        [System.ComponentModel.DefaultValue(false)]
        public bool AlwaysDropDown
        {
            get
            {
                return _AlwaysDropDown;
            }
            set
            {
                _AlwaysDropDown = value;
            }
        }

        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Split Button")]
        [System.ComponentModel.Description("Indicates whether the SplitButton always shows the Hover image status in the split part even if the button part of the SplitButton is hovered.")]
        public bool AlwaysHoverChange
        {
            get
            {
                return _AlwaysHoverChange;
            }
            set
            {
                _AlwaysHoverChange = value;
            }
        }

        [System.ComponentModel.Category("Split Button")]
        [System.ComponentModel.DefaultValue(true)]
        [System.ComponentModel.Description("Indicates whether the split rectange must be calculated (basing on Split image size)")]
        public bool CalculateSplitRect
        {
            get
            {
                return _CalculateSplitRect;
            }
            set
            {
                bool flag = _CalculateSplitRect;
                _CalculateSplitRect = value;
                if ((flag != _CalculateSplitRect) && (_SplitWidth > 0) && (_SplitHeight > 0))
                    InitDefaultSplitImages(true);
            }
        }

        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.TypeConverter(typeof(System.Windows.Forms.ImageKeyConverter))]
        [System.ComponentModel.Category("Split Button Images")]
        [System.ComponentModel.Description("The Clicked status image name in the ImageList.")]
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        public string ClickedImage
        {
            get
            {
                return _ClickedImage;
            }
            set
            {
                _ClickedImage = value;
            }
        }

        [System.ComponentModel.Category("Split Button Images")]
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.TypeConverter(typeof(System.Windows.Forms.ImageKeyConverter))]
        [System.ComponentModel.Description("The Disabled status image name in the ImageList.")]
        public string DisabledImage
        {
            get
            {
                return _DisabledImage;
            }
            set
            {
                _DisabledImage = value;
            }
        }

        [System.ComponentModel.Description("Indicates whether the double click event is raised on the SplitButton")]
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behavior")]
        public bool DoubleClickedEnabled
        {
            get
            {
                return _DoubleClickedEnabled;
            }
            set
            {
                _DoubleClickedEnabled = value;
            }
        }

        [System.ComponentModel.Category("Split Button")]
        [System.ComponentModel.DefaultValue(true)]
        [System.ComponentModel.Description("Indicates whether the split height must be filled to the button height even if the split image height is lower.")]
        public bool FillSplitHeight
        {
            get
            {
                return _FillSplitHeight;
            }
            set
            {
                _FillSplitHeight = value;
            }
        }

        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.Description("The Focused status image name in the ImageList.")]
        [System.ComponentModel.Category("Split Button Images")]
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.TypeConverter(typeof(System.Windows.Forms.ImageKeyConverter))]
        [System.ComponentModel.Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        public string FocusedImage
        {
            get
            {
                return _FocusedImage;
            }
            set
            {
                _FocusedImage = value;
            }
        }

        [System.ComponentModel.Description("The Hover status image name in the ImageList.")]
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.TypeConverter(typeof(System.Windows.Forms.ImageKeyConverter))]
        [System.ComponentModel.Category("Split Button Images")]
        public string HoverImage
        {
            get
            {
                return _HoverImage;
            }
            set
            {
                _HoverImage = value;
            }
        }

        [System.ComponentModel.TypeConverter(typeof(System.Windows.Forms.ImageKeyConverter))]
        [System.ComponentModel.Description("The Normal status image name in the ImageList.")]
        [System.ComponentModel.Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        [System.ComponentModel.Localizable(true)]
        [System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        [System.ComponentModel.Category("Split Button Images")]
        [System.ComponentModel.DefaultValue("")]
        public string NormalImage
        {
            get
            {
                return _NormalImage;
            }
            set
            {
                _NormalImage = value;
            }
        }

        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Description("Whether it should only show the triangle")]
        [System.ComponentModel.Category("Split Button")]
        public bool OnlyShowTriangle
        {
            get
            {
                return _OnlyShowTriangle;
            }
            set
            {
                _OnlyShowTriangle = value;
            }
        }

        [System.ComponentModel.DefaultValue(0)]
        [System.ComponentModel.Description("The split height (ignored if CalculateSplitRect is setted to true).")]
        [System.ComponentModel.Category("Split Button")]
        public int SplitHeight
        {
            get
            {
                return _SplitHeight;
            }
            set
            {
                _SplitHeight = value;
                if (!_CalculateSplitRect && (_SplitWidth > 0) && (_SplitHeight > 0))
                    InitDefaultSplitImages(true);
            }
        }

        [System.ComponentModel.Category("Split Button")]
        [System.ComponentModel.Description("The split width (ignored if CalculateSplitRect is setted to true).")]
        [System.ComponentModel.DefaultValue(0)]
        public int SplitWidth
        {
            get
            {
                return _SplitWidth;
            }
            set
            {
                _SplitWidth = value;
                if (!_CalculateSplitRect && (_SplitWidth > 0) && (_SplitHeight > 0))
                    InitDefaultSplitImages(true);
            }
        }

        public CtrlSplitButton()
        {
            _CalculateSplitRect = true;
            _FillSplitHeight = true;
            InitializeComponent();
            ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            CalculateSplitRect = true;
            FillSplitHeight = true;
        }

        private System.Drawing.Image GetImage(string imageName)
        {
            if ((ImageList != null) && ImageList.Images.ContainsKey(imageName))
                return ImageList.Images[imageName];
            return null;
        }

        public System.Drawing.Rectangle GetImageRect(string imageKey)
        {
            System.Drawing.Image image = GetImage(imageKey);
            if (image == null)
                goto label_2;
            int i1 = 0, i2 = 0;
            int i3 = image.Width + 1;
            int i4 = image.Height + 1;
            if (i3 > Width)
                i3 = Width;
            if (i4 > Width)
                i4 = Width;
            switch (ImageAlign)
            {
                case System.Drawing.ContentAlignment.TopLeft:
                    i1 = i2 = 0;
                    break;

                case System.Drawing.ContentAlignment.TopCenter:
                    i1 = (Width - i3) / 2;
                    i2 = 0;
                    if (((Width - i3) % 2) <= 0)
                        goto label_1;
                    i1++;
                    break;

                case System.Drawing.ContentAlignment.TopRight:
                    i1 = Width - i3;
                    i2 = 0;
                    break;

                case System.Drawing.ContentAlignment.MiddleLeft:
                    i1 = 0;
                    i2 = (Height - i4) / 2;
                    if (((Height - i4) % 2) <= 0)
                        goto label_1;
                    i2++;
                    break;

                case System.Drawing.ContentAlignment.MiddleCenter:
                    i1 = (Width - i3) / 2;
                    i2 = (Height - i4) / 2;
                    if (((Width - i3) % 2) > 0)
                        i1++;
                    if (((Height - i4) % 2) <= 0)
                        goto label_1;//goto label_0;
                    i2++;
                    break;

                case System.Drawing.ContentAlignment.MiddleRight:
                    i1 = Width - i3;
                    i2 = (Height - i4) / 2;
                    if (((Height - i4) % 2) <= 0)
                        goto label_1;
                    i2++;
                    break;

                case System.Drawing.ContentAlignment.BottomLeft:
                    i1 = 0;
                    i2 = Height - i4;
                    if (((Height - i4) % 2) <= 0)
                        goto label_1;
                    i2++;
                    break;

                case System.Drawing.ContentAlignment.BottomCenter:
                    i1 = (Width - i3) / 2;
                    i2 = Height - i4;
                    if (((Width - i3) % 2) <= 0)
                        goto label_1;
                    i1++;
                    break;

                case System.Drawing.ContentAlignment.BottomRight:
                    i1 = Width - i3;
                    i2 = Height - i4;
                    break;

                case System.Drawing.ContentAlignment.TopLeft | System.Drawing.ContentAlignment.TopCenter:
                    goto label_1;
                    
               
            }
        label_1:
            if (_FillSplitHeight && (i4 < Height))
                i4 = Height;
            if (i1 > 0)
                i1--;
            if (i2 > 0)
                i2--;
            return new System.Drawing.Rectangle(i1, i2, i3, i4);
        label_2:
            return System.Drawing.Rectangle.Empty;
        }

        private void InitDefaultSplitImages()
        {
            InitDefaultSplitImages(false);
        }

        private void InitDefaultSplitImages(bool refresh)
        {
            if (System.String.IsNullOrEmpty(_NormalImage))
                _NormalImage = "Normal";
            if (System.String.IsNullOrEmpty(_HoverImage))
                _HoverImage = "Hover";
            if (System.String.IsNullOrEmpty(_ClickedImage))
                _ClickedImage = "Clicked";
            if (System.String.IsNullOrEmpty(_DisabledImage))
                _DisabledImage = "Disabled";
            if (System.String.IsNullOrEmpty(_FocusedImage))
                _FocusedImage = "Focused";
            if (_DefaultSplitImages == null)
            {
                _DefaultSplitImages = new System.Windows.Forms.ImageList();
                components.Add(_DefaultSplitImages);
            }
            if ((_DefaultSplitImages.Images.Count == 0) || refresh)
            {
                if (_DefaultSplitImages.Images.Count > 0)
                    _DefaultSplitImages.Images.Clear();
                try
                {
                    int i1 = 0, i2 = 0;
                    if (!_CalculateSplitRect && (_SplitWidth > 0))
                        i1 = _SplitWidth;
                    else
                        i1 = 18;
                    if (!CalculateSplitRect && (SplitHeight > 0))
                        i2 = SplitHeight;
                    else
                        i2 = Height;
                    i2 -= 8;
                    _DefaultSplitImages.ImageSize = new System.Drawing.Size(i1, i2);
                    int i3 = i1 / 2;
                    i3 += i3 % 2;
                    int i4 = i2 / 2;
                    System.Drawing.Pen pen = new System.Drawing.Pen(ForeColor, 1.0F);
                    System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(ForeColor);
                    System.Drawing.Bitmap bitmap1 = new System.Drawing.Bitmap(i1, i2);
                    System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap1);
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.DrawLine(System.Drawing.SystemPens.ButtonShadow, new System.Drawing.Point(1, 1), new System.Drawing.Point(1, i2 - 2));
                    graphics.DrawLine(System.Drawing.SystemPens.ButtonFace, new System.Drawing.Point(2, 1), new System.Drawing.Point(2, i2));
                    System.Drawing.Point[] pointArr1 = new System.Drawing.Point[3];
                    pointArr1[0] = new System.Drawing.Point(i3 - 2, i4 - 1);
                    pointArr1[1] = new System.Drawing.Point(i3 + 3, i4 - 1);
                    pointArr1[2] = new System.Drawing.Point(i3, i4 + 2);
                    graphics.FillPolygon(solidBrush, pointArr1);
                    graphics.Dispose();
                    System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(i1, i2);
                    graphics = System.Drawing.Graphics.FromImage(bitmap2);
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.DrawLine(System.Drawing.SystemPens.ButtonShadow, new System.Drawing.Point(1, 1), new System.Drawing.Point(1, i2 - 2));
                    graphics.DrawLine(System.Drawing.SystemPens.ButtonFace, new System.Drawing.Point(2, 1), new System.Drawing.Point(2, i2));
                    System.Drawing.Point[] pointArr2 = new System.Drawing.Point[3];
                    pointArr2[0] = new System.Drawing.Point(i3 - 3, i4 - 2);
                    pointArr2[1] = new System.Drawing.Point(i3 + 4, i4 - 2);
                    pointArr2[2] = new System.Drawing.Point(i3, i4 + 2);
                    graphics.FillPolygon(solidBrush, pointArr2);
                    graphics.Dispose();
                    System.Drawing.Bitmap bitmap3 = new System.Drawing.Bitmap(i1, i2);
                    graphics = System.Drawing.Graphics.FromImage(bitmap3);
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.DrawLine(System.Drawing.SystemPens.ButtonShadow, new System.Drawing.Point(1, 1), new System.Drawing.Point(1, i2 - 2));
                    graphics.DrawLine(System.Drawing.SystemPens.ButtonFace, new System.Drawing.Point(2, 1), new System.Drawing.Point(2, i2));
                    System.Drawing.Point[] pointArr3 = new System.Drawing.Point[3];
                    pointArr3[0] = new System.Drawing.Point(i3 - 2, i4 - 1);
                    pointArr3[1] = new System.Drawing.Point(i3 + 3, i4 - 1);
                    pointArr3[2] = new System.Drawing.Point(i3, i4 + 2);
                    graphics.FillPolygon(solidBrush, pointArr3);
                    graphics.Dispose();
                    System.Drawing.Bitmap bitmap4 = new System.Drawing.Bitmap(i1, i2);
                    graphics = System.Drawing.Graphics.FromImage(bitmap4);
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.DrawLine(System.Drawing.SystemPens.GrayText, new System.Drawing.Point(1, 1), new System.Drawing.Point(1, i2 - 2));
                    System.Drawing.Point[] pointArr4 = new System.Drawing.Point[3];
                    pointArr4[0] = new System.Drawing.Point(i3 - 2, i4 - 1);
                    pointArr4[1] = new System.Drawing.Point(i3 + 3, i4 - 1);
                    pointArr4[2] = new System.Drawing.Point(i3, i4 + 2);
                    graphics.FillPolygon(new System.Drawing.SolidBrush(System.Drawing.SystemColors.GrayText), pointArr4);
                    graphics.Dispose();
                    System.Drawing.Bitmap bitmap5 = new System.Drawing.Bitmap(i1, i2);
                    graphics = System.Drawing.Graphics.FromImage(bitmap5);
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.DrawLine(System.Drawing.SystemPens.ButtonShadow, new System.Drawing.Point(1, 1), new System.Drawing.Point(1, i2 - 2));
                    graphics.DrawLine(System.Drawing.SystemPens.ButtonFace, new System.Drawing.Point(2, 1), new System.Drawing.Point(2, i2));
                    System.Drawing.Point[] pointArr5 = new System.Drawing.Point[3];
                    pointArr5[0] = new System.Drawing.Point(i3 - 2, i4 - 1);
                    pointArr5[1] = new System.Drawing.Point(i3 + 3, i4 - 1);
                    pointArr5[2] = new System.Drawing.Point(i3, i4 + 2);
                    graphics.FillPolygon(solidBrush, pointArr5);
                    graphics.Dispose();
                    pen.Dispose();
                    solidBrush.Dispose();
                    _DefaultSplitImages.Images.Add(_NormalImage, bitmap1);
                    _DefaultSplitImages.Images.Add(_HoverImage, bitmap2);
                    _DefaultSplitImages.Images.Add(_ClickedImage, bitmap3);
                    _DefaultSplitImages.Images.Add(_DisabledImage, bitmap4);
                    _DefaultSplitImages.Images.Add(_FocusedImage, bitmap5);
                }
                catch 
                {
                }
            }
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        public bool MouseInSplit()
        {
            return PointInSplit(PointToClient(System.Windows.Forms.Control.MousePosition));
        }

        public bool PointInSplit(System.Drawing.Point pt)
        {
            System.Drawing.Rectangle rectangle = GetImageRect(_NormalImage);
            if (!_CalculateSplitRect)
            {
                rectangle.Width = _SplitWidth;
                rectangle.Height = _SplitHeight;
            }
            return rectangle.Contains(pt);
        }

        private void SetSplit(string imageName)
        {
            if ((imageName != null) && (ImageList != null) && ImageList.Images.ContainsKey(imageName))
                ImageKey = imageName;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnClick(System.EventArgs e)
        {
            if (!MouseInSplit() && !_AlwaysDropDown)
            {
                base.OnClick(e);
                if (ButtonClick == null)
                    return;
                ButtonClick(this, e);
                return;
            }
            if ((ContextMenuStrip != null) && (ContextMenuStrip.Items.Count > 0))
                ContextMenuStrip.Show(this, new System.Drawing.Point(0, Height));
        }

        protected override void OnCreateControl()
        {
            InitDefaultSplitImages();
            if (ImageList == null)
                ImageList = _DefaultSplitImages;
            if (Enabled)
                SetSplit(_NormalImage);
            else
                SetSplit(_DisabledImage);
            base.OnCreateControl();
        }

        protected override void OnDoubleClick(System.EventArgs e)
        {
            if (_DoubleClickedEnabled)
            {
                base.OnDoubleClick(e);
                if (!MouseInSplit() && !_AlwaysDropDown && (ButtonClick != null))
                    ButtonDoubleClick(this, e);
            }
        }

        protected override void OnEnabledChanged(System.EventArgs e)
        {
            if (!Enabled)
                SetSplit(_DisabledImage);
            else if (MouseInSplit())
                SetSplit(_HoverImage);
            else
                SetSplit(_NormalImage);
            base.OnEnabledChanged(e);
        }

        protected override void OnGotFocus(System.EventArgs e)
        {
            if (Enabled)
                SetSplit(_FocusedImage);
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            if (Enabled)
                SetSplit(_NormalImage);
            base.OnLostFocus(e);
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs mevent)
        {
            if (_AlwaysDropDown || MouseInSplit())
            {
                if (Enabled)
                {
                    SetSplit(_ClickedImage);
                    if ((ContextMenuStrip != null) && (ContextMenuStrip.Items.Count > 0))
                        ContextMenuStrip.Show(this, new System.Drawing.Point(0, Height));
                }
            }
            else if (Enabled)
            {
                SetSplit(_NormalImage);
            }
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            if (Enabled)
                SetSplit(_NormalImage);
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs mevent)
        {
            if (_AlwaysDropDown || _AlwaysHoverChange || MouseInSplit())
            {
                if (Enabled)
                    SetSplit(_HoverImage);
            }
            else if (Enabled)
            {
                SetSplit(_NormalImage);
            }
            base.OnMouseMove(mevent);
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs mevent)
        {
            if (_AlwaysDropDown || _AlwaysHoverChange || MouseInSplit())
            {
                if (Enabled)
                    SetSplit(_HoverImage);
            }
            else if (Enabled)
            {
                SetSplit(_NormalImage);
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
        {
            pevent.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Invalid;
            pevent.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            pevent.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            System.Drawing.Rectangle rectangle3 = ClientRectangle;
            int i = rectangle3.Width - 18;
            if (!_OnlyShowTriangle)
            {
                pevent.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                pevent.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                pevent.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                System.Drawing.Brush brush1 = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, BackColor, BackColor2, BackFillMode);
                pevent.Graphics.FillRectangle(brush1, ClientRectangle);
                brush1.Dispose();
                System.Drawing.Pen pen1 = new System.Drawing.Pen(BorderColor, 1.0F);
                System.Drawing.Rectangle rectangle4 = ClientRectangle;
                System.Drawing.Rectangle rectangle5 = ClientRectangle;
                System.Drawing.Rectangle rectangle6 = ClientRectangle;
                System.Drawing.Rectangle rectangle1 = new System.Drawing.Rectangle(rectangle4.Location, new System.Drawing.Size(rectangle5.Width, rectangle6.Height));
                pevent.Graphics.DrawRectangle(pen1, rectangle1);
                pen1.Dispose();
                System.Drawing.Pen pen2 = new System.Drawing.Pen(BorderColor, 1.0F);
                System.Drawing.Rectangle rectangle7 = ClientRectangle;
                pevent.Graphics.DrawLine(pen2, new System.Drawing.Point(i, 0), new System.Drawing.Point(i, rectangle7.Height));
                pen2.Dispose();
                System.Drawing.StringFormat stringFormat = new System.Drawing.StringFormat();
                System.Drawing.Rectangle rectangle8 = ClientRectangle;
                System.Drawing.Rectangle rectangle9 = ClientRectangle;
                System.Drawing.Rectangle rectangle2 = new System.Drawing.Rectangle(rectangle8.Location, new System.Drawing.Size(i, rectangle9.Height));
                System.Drawing.ContentAlignment contentAlignment = TextAlign;
                if ((contentAlignment == System.Drawing.ContentAlignment.TopLeft) || (contentAlignment == System.Drawing.ContentAlignment.MiddleLeft) || (contentAlignment == System.Drawing.ContentAlignment.BottomLeft))
                {
                    stringFormat.Alignment = System.Drawing.StringAlignment.Near;
                    rectangle2.X += 4;
                }
                else
                {
                    stringFormat.Alignment = System.Drawing.StringAlignment.Center;
                }
                stringFormat.LineAlignment = System.Drawing.StringAlignment.Center;
                stringFormat.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide;
                System.Drawing.Brush brush2 = new System.Drawing.SolidBrush(ForeColor);
                pevent.Graphics.DrawString(Text, Font, brush2, rectangle2, stringFormat);
            }
            else
            {
                System.Drawing.Color color = System.Drawing.Color.White;
                System.Windows.Forms.Control control = Parent;
                while ((control != null) && control.BackColor == System.Drawing.Color.Transparent)
                {
                    control = control.Parent;
                }
                if (control != null)
                    color = control.BackColor;
                System.Drawing.Brush brush3 = new System.Drawing.SolidBrush(color);
                pevent.Graphics.FillRectangle(brush3, ClientRectangle);
                brush3.Dispose();
            }
            System.Drawing.Image image = Oranikle.Studio.Controls.Properties.Resources.ComboBoxTransparent;
            pevent.Graphics.DrawImage(image, new System.Drawing.Point(i, 0));
        }

    } // class CtrlSplitButton

}

