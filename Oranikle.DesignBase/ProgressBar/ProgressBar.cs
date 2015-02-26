using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;  
using System.Drawing.Design;        // For UITypeEditor
using System.Drawing.Imaging;  
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;  // For IWindowsFormsEditorService
using Oranikle.Studio.Controls.ProgressBar.Win32;  

namespace Oranikle.Studio.Controls.ProgressBar
{
    #region Enum

    public enum ProgressBarBorderStyle
    {
        Flat, Sunken, Raised
    };

    #endregion

    [Designer(typeof(ProgressBarDesigner))]
    [DefaultEvent("ValueChanged"), DefaultProperty("Value")]
    public partial class ProgressBar : Control, IProgressBar, IFloatWindowBase
    {
        #region Struct

        public struct ColorManipulation
        {
            #region Instance Members

            public Color Dark;
            public Color Light;
            public Color Darker;
            public Color Lighter;
            public Color BaseColor;

            #endregion

            #region Constructor

            public ColorManipulation(Color baseColor)
            {
                this.Dark = Shade(0.8f, baseColor);
                this.Light = Smooth(0.6f, baseColor);
                this.Darker = Shade(0.6f, baseColor);
                this.Lighter = Smooth(0.3f, baseColor);
                this.BaseColor = baseColor;
            }

            #endregion

            #region Helper Methods

            private static Color Shade(float ratio, Color baseColor)
            {
                return Manipule(ratio, baseColor, Color.Black);
            }

            private static Color Smooth(float ratio, Color baseColor)
            {
                return Manipule(ratio, baseColor, Color.White);
            }

            private static Color Manipule(float ratio, Color baseColor, Color manipuleColor)
            {
                int r = (int)(manipuleColor.R + ratio * (baseColor.R - manipuleColor.R));       // Red
                int g = (int)(manipuleColor.G + ratio * (baseColor.G - manipuleColor.G));     // Green
                int b = (int)(manipuleColor.B + ratio * (baseColor.B - manipuleColor.B));      // Blue

                return Color.FromArgb(r, g, b);
            }

            #endregion
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the currently docking style of the Oranikle.Studio.Controls.ProgressBar is changed.
        /// </summary>
        [Description("Occurs when the currently docking style of the Oranikle.Studio.Controls.ProgressBar is changed")]
        public event EventHandler DockingModeChanged;

        /// <summary>
        /// Event raised when the Oranikle.Studio.Controls.ProgressBar control is closed by the user, it's docking state should be float window for this change to take effect.
        /// </summary>
        [Description("Event raised when the Oranikle.Studio.Controls.ProgressBar control is closed by the user, it's docking state should be float window for this change to take effect")]
        public event EventHandler ProgressBarClosed;

        public delegate void ProgressBarClosingEventHandler(object sender, out bool cancel);
        /// <summary>
        /// Occurs when the docking state of the Oranikle.Studio.Controls.ProgressBar control is in the float mode and Oranikle.Studio.Controls.ProgressBar control is being closed by the user.
        /// </summary>
        [Description("Occurs when the docking state of the Oranikle.Studio.Controls.ProgressBar control is in the float mode and Oranikle.Studio.Controls.ProgressBar control is being closed by the user")]
        public event ProgressBarClosingEventHandler ProgressBarClosing;

        #endregion

        #region System Menu Constants

        // LEFTSIDE, RIGHTSIDE, DOCKIT, IS_OPACITY_ENABLED, SEEK_TO_OPACITY
        private static readonly uint[] SYSTEM_MENUITEMS = { 0x100, 0x101, 0x102, 0x103, 0x104 };

        #endregion

        #region Instance Members

        private int minimum = 0;                            // Initializer
        private int maximum = 100;                          // Initializer
        private int progress = 20;                          // Initializer
        private bool isDocked = true;                       // Initializer
        private bool isPaintBorder = true;                  // Initializer
        private bool isDigitDrawEnabled = false;            // Initializer
        private bool isUserInteraction = true;              // Initializer
        private bool showPercentage = true;                 // Initializer
        private string displayFormat = "done";              // Initializer
        private Color borderColor = Color.DarkGray;         // Initializer
        private ProgressBarBorderStyle controlBorderStyle = ProgressBarBorderStyle.Flat;  // Initializer
        private Components.IAlphaMaker alphaMaker = null;   // Initializer
        private Control parentContainer = null;             // Initializer
        private Point oldLocation = Point.Empty;            // Initializer

        private GradientProgress progressGradient;
        private GradientBackground backgroundGradient;
        private ColorizerProgress progressColorizer;
        private GradientDigitBox digitBoxGradient;

        private Rectangle progressRectangle;
        private Rectangle backgroundRectangle;
        private RectangleF infoBoxRectangle;

        #region IFloatWindowAlpha Members

        private bool isLayered = false;
        private byte targetTransparency = 200;
        private byte currentTransparency = 255;

        #endregion

        #endregion

        #region Constructor

        public ProgressBar()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

            // Object Initializer
            this.Size = new Size()
            {
                Width = 150,
                Height = 40
            };

            progressGradient = new GradientProgress();
            progressGradient.GradientChanged += new EventHandler(CONTROL_INVALIDATE_UPDATE);

            backgroundGradient = new GradientBackground();
            backgroundGradient.GradientChanged += new EventHandler(CONTROL_INVALIDATE_UPDATE);

            progressColorizer = new ColorizerProgress();
            progressColorizer.ProgressColorizerChanged += new EventHandler(CONTROL_INVALIDATE_UPDATE);

            digitBoxGradient = new GradientDigitBox();
            digitBoxGradient.GradientChanged += new EventHandler(CONTROL_INVALIDATE_UPDATE);
        }

        #endregion

        #region Destructor

        ~ProgressBar()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Property

        /// <summary>
        /// You can change the progress appearance from here.
        /// </summary>
        [Description("You can change the progress appearance from here")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [Category("Gradient")]
        public GradientProgress ProgressGradient
        {
            get { return progressGradient; }
            set
            {
                try
                {
                    if (!value.Equals(progressGradient))
                    {
                        progressGradient.GradientChanged -= CONTROL_INVALIDATE_UPDATE;
                        progressGradient = value;
                        progressGradient.GradientChanged += new EventHandler(CONTROL_INVALIDATE_UPDATE);

                        Invalidate();
                        Update();
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Value cannot be null!, please enter a valid value.");
                }
            }
        }

        /// <summary>
        /// You can change the background appearance from here.
        /// </summary>
        [Description("You can change the background appearance from here")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [Category("Gradient")]
        public GradientBackground BackgroundGradient
        {
            get { return backgroundGradient; }
            set
            {
                try
                {
                    if (!value.Equals(backgroundGradient))
                    {
                        backgroundGradient.GradientChanged -= CONTROL_INVALIDATE_UPDATE;
                        backgroundGradient = value;
                        backgroundGradient.GradientChanged += new EventHandler(CONTROL_INVALIDATE_UPDATE);

                        Invalidate();
                        Update();
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Value cannot be null!, please enter a valid value.");
                }
            }
        }

        /// <summary>
        /// You can change the color components of the progress bitmap[RGBA Colorizer for progress indicator].
        /// </summary>
        [Description("You can change the color components of the progress bitmap[RGBA Colorizer for progress indicator]")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [Category("Gradient")]
        public ColorizerProgress ProgressColorizer
        {
            get { return progressColorizer; }
            set
            {
                try
                {
                    if (!value.Equals(progressColorizer))
                    {
                        progressColorizer.ProgressColorizerChanged -= CONTROL_INVALIDATE_UPDATE;
                        progressColorizer = value;
                        progressColorizer.ProgressColorizerChanged += new EventHandler(CONTROL_INVALIDATE_UPDATE);

                        Invalidate();
                        Update();
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Value cannot be null!, please enter a valid value.");
                }
            }
        }

        /// <summary>
        /// You can change the background appearance of the DigitBox rectangle from here.
        /// </summary>
        [Description("You can change the background appearance of the DigitBox rectangle from here")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [Category("Gradient")]
        public GradientDigitBox DigitBoxGradient
        {
            get { return digitBoxGradient; }
            set
            {
                try
                {
                    if (!value.Equals(backgroundGradient))
                    {
                        digitBoxGradient.GradientChanged -= CONTROL_INVALIDATE_UPDATE;
                        digitBoxGradient = value;
                        digitBoxGradient.GradientChanged += new EventHandler(CONTROL_INVALIDATE_UPDATE);

                        Invalidate();
                        Update();
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Value cannot be null!, please enter a valid value.");
                }
            }
        }

        /// <summary>
        /// Provides keyboard support to the user for Oranikle.Studio.Controls.ProgressBar operations[Home(Min value), End(Max value), Left(Decrease current value), Right(Increase current value), Enter(ReDocking), F1 Keys].
        /// </summary>
        [Description("Provides keyboard support to the user for Oranikle.Studio.Controls.ProgressBar operations[Home(Min value), End(Max value), Left(Decrease current value), Right(Increase current value), Enter(ReDocking), F1 Keys]")]
        [DefaultValue(true)]
        [Browsable(true)]
        public bool IsUserInteraction
        {
            get { return isUserInteraction; }
            set
            {
                if (!value.Equals(isUserInteraction))
                    isUserInteraction = value;
            }
        }

        /// <summary>
        /// Gets or sets, this control's alpha maker component whose window has a alpha support or to be.
        /// </summary>
        [Description("Gets or sets, this control's alpha maker component whose window has a alpha support or to be")]
        [Browsable(true)]
        public Components.IAlphaMaker AlphaMaker
        {
            get { return alphaMaker; }
            set
            {
                try
                {
                    if (!value.Equals(alphaMaker))
                    {
                        alphaMaker = value;

                        if (!isDocked)
                            alphaMaker.IFloatWindowControl = this;
                    }
                }
                catch (NullReferenceException)
                {
                    alphaMaker = null;
                }
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!isDocked)
                {
                    cp.Style &= (~0x00800000);              // WS_BORDER
                    cp.Style &= (~0x00C00000);              // WS_CAPTION
                    cp.Style |= 0x00080000 | 0x00020000;    // WS_SYSMENU | WS_MINIMIZEBOX
                }

                return cp;
            }
        }

        #endregion

        #region Helper Methods

        private void CONTROL_INVALIDATE_UPDATE(object sender, EventArgs e)
        {
            Invalidate();
            Update();
        }

        private void GenerateProgressBar()
        {
            // Draws percent text value on the control and generates the width of the progress background and its progress indicator. 
            int percent = (int)(progress * 100 / maximum);
            this.Text = String.Format("{0}{1} {2}", percent >= 100 ? 100 : percent, "%", displayFormat);

            backgroundRectangle = this.ClientRectangle;

            if (isPaintBorder)
                backgroundRectangle.Inflate(-1, -1);

            progressRectangle = backgroundRectangle;

            int progressWidth, currentProgress = Value;

            if (Value >= Maximum)
                progressWidth = backgroundRectangle.Width;
            else
                progressWidth = backgroundRectangle.Width * currentProgress / Maximum;

            progressRectangle.Width = progressWidth;
            progressRectangle.Height--;
        }

        // Resets the system menu to it's default.
        private void ResetSystemMenu()
        {
            User32.GetSystemMenu(this.Handle, true);
        }

        // Creates extra menu items for docking and alpha operations.
        private void CreateSystemMenu()
        {
            IntPtr menuHandle = User32.GetSystemMenu(this.Handle, false);

            // Insert a separator item on top.
            User32.InsertMenu(menuHandle, 0, User32.MenuFlags.MF_SEPARATOR | User32.MenuFlags.MF_BYPOSITION, 0, null);
            // Create and insert a popup menu for layout operations.
            IntPtr subMenu = User32.CreatePopupMenu();
            User32.AppendMenu(subMenu, digitBoxGradient.DigitalNumberSide != GradientDigitBox.DigitalNumberLayout.LeftSide ? User32.MenuFlags.MF_STRING : User32.MenuFlags.MF_STRING | User32.MenuFlags.MF_GRAYED | User32.MenuFlags.MF_DISABLED | User32.MenuFlags.MF_CHECKED, SYSTEM_MENUITEMS[0], "Left Side");
            User32.AppendMenu(subMenu, digitBoxGradient.DigitalNumberSide != GradientDigitBox.DigitalNumberLayout.RightSide ? User32.MenuFlags.MF_STRING : User32.MenuFlags.MF_STRING | User32.MenuFlags.MF_GRAYED | User32.MenuFlags.MF_DISABLED | User32.MenuFlags.MF_CHECKED, SYSTEM_MENUITEMS[1], "Right Side");
            User32.InsertMenu(menuHandle, 0, isDigitDrawEnabled ? User32.MenuFlags.MF_POPUP | User32.MenuFlags.MF_BYPOSITION : User32.MenuFlags.MF_POPUP | User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_GRAYED | User32.MenuFlags.MF_DISABLED, (uint)subMenu, "DigitBox Layouts");

            // Insert a separator item by specified position.
            User32.InsertMenu(menuHandle, 2, User32.MenuFlags.MF_SEPARATOR | User32.MenuFlags.MF_BYPOSITION, 0, null);
            User32.InsertMenu(menuHandle, 2, User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_STRING, SYSTEM_MENUITEMS[2], "Dock It!");

            // If our alpha maker component is not null, we adds extra a separator and alpha menu items.
            if (alphaMaker != null)
            {
                User32.InsertMenu(menuHandle, 2, User32.MenuFlags.MF_SEPARATOR | User32.MenuFlags.MF_BYPOSITION, 0, null);
                User32.InsertMenu(menuHandle, 2, isLayered ? User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_STRING | User32.MenuFlags.MF_CHECKED : User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_STRING, SYSTEM_MENUITEMS[3],
                    String.Format("Opacity {0}[{1}]", isLayered ? "Enabled" : "Disabled", currentTransparency));
                User32.InsertMenu(menuHandle, 2, User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_STRING, SYSTEM_MENUITEMS[4], "Seek To Opacity");
            }
        }

        #endregion

        #region Virtual Methods

        protected virtual void DrawText(Graphics gr)
        {
            if (!showPercentage)
                return;

            RectangleF rct; bool wrap = false;
            if (!isDigitDrawEnabled || backgroundRectangle.Height < 32)
                rct = backgroundRectangle;
            else
            {
                wrap = true;
                rct = infoBoxRectangle;
            }

            rct.Inflate(-1, -1);
            if (rct.Width > 2)
            {
                if (wrap)
                {
                    SmoothingMode mode = gr.SmoothingMode;
                    gr.SmoothingMode = SmoothingMode.AntiAlias;
                    DrawDigitalHelper.RctWidth = rct.Width;
                    DrawDigitalHelper.DrawNumbers(gr, progress, maximum, this.ForeColor, infoBoxRectangle);
                    gr.SmoothingMode = mode;
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(this.ForeColor))
                    using (StringFormat textFormat = new StringFormat(StringFormatFlags.LineLimit))
                    {
                        textFormat.Alignment = StringAlignment.Center;
                        textFormat.LineAlignment = StringAlignment.Center;

                        textFormat.Trimming = StringTrimming.EllipsisCharacter;
                        textFormat.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide;

                        gr.DrawString(this.Text, this.Font, brush, rct, textFormat);
                    }
                }
            }
        }

        protected virtual void DrawBorder(Graphics gr)
        {
            if (!isPaintBorder)
                return;

            Pen topLeft;
            Pen bottomRight;

            switch (controlBorderStyle)
            {
                default:
                    topLeft = new Pen(borderColor);
                    bottomRight = topLeft;
                    break;
                case ProgressBarBorderStyle.Raised:
                    topLeft = new Pen(new SolidBrush(SystemColors.ControlLightLight));
                    bottomRight = new Pen(new SolidBrush(SystemColors.ControlDark));
                    break;
                case ProgressBarBorderStyle.Sunken:
                    topLeft = new Pen(new SolidBrush(SystemColors.ControlDark));
                    bottomRight = new Pen(new SolidBrush(SystemColors.ControlLightLight));
                    break;
            }

            gr.DrawLine(topLeft, 0, 0, this.Width - 1, 0);  // Top
            gr.DrawLine(topLeft, 0, 0, 0, this.Height - 1); // Left
            gr.DrawLine(bottomRight, 0, this.Height - 1, this.Width - 1, this.Height - 1);  // Bottom
            gr.DrawLine(bottomRight, this.Width - 1, 0, this.Width - 1, this.Height - 1);   // Right

            topLeft.Dispose();
            bottomRight.Dispose();
        }

        protected virtual void DrawProgress(Graphics gr)
        {
            if (progressRectangle.Width < 2)
                return;

            Point left = new Point(progressRectangle.X, progressRectangle.Y);
            Point right = new Point(progressRectangle.Right, progressRectangle.Y);

            // Create a new empty image for manipulations. If you use this constructor, you get a new Bitmap object that represents a bitmap in memory with a PixelFormat of Format32bppARGB.
            using (Bitmap overlay = new Bitmap(progressRectangle.Width + 2, progressRectangle.Height + 2))
            {
                // Make an associated Graphics object.
                using (Graphics bmpGraphics = Graphics.FromImage(overlay))
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(Point.Empty, new Point(0, progressRectangle.Height), progressGradient.ManipuleStart.BaseColor, progressGradient.ManipuleEnd.BaseColor))
                    {
                        if (!progressGradient.IsBlendedForProgress)
                            bmpGraphics.FillRectangle(brush, progressRectangle);
                        else
                        {
                            Blend bl = new Blend(2);
                            bl.Factors = new float[] { 0.3F, 1.0F };
                            bl.Positions = new float[] { 0.0F, 1.0F };
                            brush.Blend = bl;
                            bmpGraphics.FillRectangle(brush, progressRectangle);
                        }
                    }

                    LinearGradientBrush topInner = new LinearGradientBrush(left, right, progressGradient.ManipuleStart.Light, progressGradient.ManipuleEnd.Light);
                    LinearGradientBrush topOuter = new LinearGradientBrush(left, right, progressGradient.ManipuleStart.Lighter, progressGradient.ManipuleEnd.Lighter);
                    LinearGradientBrush bottomInner = new LinearGradientBrush(left, right, progressGradient.ManipuleStart.Dark, progressGradient.ManipuleEnd.Dark);
                    LinearGradientBrush bottomOuter = new LinearGradientBrush(left, right, progressGradient.ManipuleStart.Darker, progressGradient.ManipuleEnd.Darker);

                    // Inner Top
                    using (Pen pen = new Pen(topInner))
                    {
                        bmpGraphics.DrawLine(pen, progressRectangle.X + 1, progressRectangle.Y + 1, progressRectangle.Right - 1, progressRectangle.Y + 1);
                    }

                    // Inner Left
                    using (Pen pen = new Pen(progressGradient.ManipuleStart.Light))
                    {
                        bmpGraphics.DrawLine(pen, progressRectangle.X + 1, progressRectangle.Y + 1, progressRectangle.X + 1, progressRectangle.Bottom - 1);
                    }

                    // Outer Top
                    using (Pen pen = new Pen(topOuter))
                    {
                        bmpGraphics.DrawLine(pen, progressRectangle.X, progressRectangle.Y, progressRectangle.Right, progressRectangle.Y);
                    }

                    // Outer Left
                    using (Pen pen = new Pen(progressGradient.ManipuleStart.Lighter))
                    {
                        bmpGraphics.DrawLine(pen, progressRectangle.X, progressRectangle.Y, progressRectangle.X, progressRectangle.Bottom);
                    }

                    // Inner Bottom
                    using (Pen pen = new Pen(bottomInner))
                    {
                        bmpGraphics.DrawLine(pen, progressRectangle.X + 1, progressRectangle.Bottom - 1, progressRectangle.Right - 1, progressRectangle.Bottom - 1);
                    }

                    // Inner Right
                    using (Pen pen = new Pen(progressGradient.ManipuleEnd.Dark))
                    {
                        bmpGraphics.DrawLine(pen, progressRectangle.Right - 1, progressRectangle.Y + 1, progressRectangle.Right - 1, progressRectangle.Bottom - 1);
                    }

                    // Outer Bottom
                    using (Pen pen = new Pen(bottomOuter))
                    {
                        bmpGraphics.DrawLine(pen, progressRectangle.X, progressRectangle.Bottom, progressRectangle.Right, progressRectangle.Bottom);
                    }

                    // Outer Right
                    using (Pen pen = new Pen(progressGradient.ManipuleEnd.Darker))
                    {
                        bmpGraphics.DrawLine(pen, progressRectangle.Right, progressRectangle.Y, progressRectangle.Right, progressRectangle.Bottom);
                    }

                    topInner.Dispose();
                    topOuter.Dispose();
                    bottomInner.Dispose();
                    bottomOuter.Dispose();
                }

                /* Create a new color matrix,
                   The value Alpha in row 4, column 4 specifies the alpha value */
                float[][] jaggedMatrix = new float[][]
                {
                    // Red component   [from 0.0 to 1.0 increase red color component.]
                    new float[]{ progressColorizer.IsColorizerEnabled ? progressColorizer.Red / 255f : 1.0f , 0.0f , 0.0f , 0.0f , 0.0f },                  
                    // Green component [from 0.0 to 1.0 increase green color component.]
                    new float[]{ 0.0f , progressColorizer.IsColorizerEnabled ? progressColorizer.Green / 255f : 1.0f , 0.0f , 0.0f , 0.0f },                
                    // Blue component  [from 0.0 to 1.0 increase blue color component.]
                    new float[]{ 0.0f , 0.0f , progressColorizer.IsColorizerEnabled ? progressColorizer.Blue / 255f : 1.0f , 0.0f , 0.0f },                 
                    // Alpha component [from 1.0 to 0.0 increase transparency bitmap.]
                    new float[]{ 0.0f , 0.0f , 0.0f , progressColorizer.IsTransparencyEnabled ? progressColorizer.Alpha / 255f : 1.0f , 0.0f },       
                    // White component [0.0: goes to Original color, 1.0: goes to white for all color component(Red, Green, Blue.)]
                    new float[]{ progressColorizer.IsColorizerEnabled ? 0.2f : 0.0f , progressColorizer.IsColorizerEnabled ? 0.2f : 0.0f , progressColorizer.IsColorizerEnabled ? 0.2f : 0.0f , 0.0f , 1.0f }                                                                                           
                };

                ColorMatrix colorMatrix = new ColorMatrix(jaggedMatrix);

                // Create an ImageAttributes object and set its color matrix
                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(
                        colorMatrix,
                        ColorMatrixFlag.Default,
                        ColorAdjustType.Bitmap);

                    gr.DrawImage(overlay, new Rectangle(Point.Empty, new Size(overlay.Width, overlay.Height)), 0, 0, overlay.Width, overlay.Height, GraphicsUnit.Pixel, attributes);
                }
            }
        }

        protected virtual void DrawBackground(Graphics gr)
        {
            if (backgroundRectangle.Width < 2)
                return;

            using (LinearGradientBrush brush = new LinearGradientBrush(Point.Empty, new Point(0, backgroundRectangle.Height), backgroundGradient.ColorStart, backgroundGradient.ColorEnd))
            {
                if (!backgroundGradient.IsBlendedForBackground)
                    gr.FillRectangle(brush, backgroundRectangle);
                else
                {
                    Blend bl = new Blend(2);
                    bl.Factors = new float[] { 0.3F, 1.0F };
                    bl.Positions = new float[] { 0.0F, 1.0F };
                    brush.Blend = bl;
                    gr.FillRectangle(brush, backgroundRectangle);
                }
            }
        }

        protected virtual void DrawDigitBox(Graphics gr)
        {
            if (!isDigitDrawEnabled || !showPercentage)
                return;

            string num = maximum.ToString();
            float startingWide = 0;
            switch (num.Length - 1)
            {
                case 0:
                    startingWide = 0.8f;
                    break;
                case 1:
                    startingWide = 1.5f;
                    break;
                case 2:
                    startingWide = 2;
                    break;
                default:
                    startingWide = num.Length - 1.42f;
                    break;
            }

            switch (digitBoxGradient.DigitalNumberSide)
            {
                case GradientDigitBox.DigitalNumberLayout.LeftSide:
                    infoBoxRectangle = backgroundRectangle;
                    infoBoxRectangle.Width = backgroundRectangle.Height * startingWide;
                    break;
                case GradientDigitBox.DigitalNumberLayout.RightSide:
                    infoBoxRectangle = backgroundRectangle;
                    infoBoxRectangle.X = backgroundRectangle.Right - backgroundRectangle.Height * startingWide;
                    infoBoxRectangle.Width = backgroundRectangle.Height * startingWide;
                    break;
            }

            if (infoBoxRectangle.Height >= 32)
            {
                infoBoxRectangle.Inflate(-5, -5);
                using (Pen pen = new Pen(digitBoxGradient.BorderColor))
                {
                    gr.DrawRectangle(pen, infoBoxRectangle.X, infoBoxRectangle.Y, infoBoxRectangle.Width - 1, infoBoxRectangle.Height - 1);
                }

                infoBoxRectangle.Inflate(-1, -1);
                using (LinearGradientBrush brush = new LinearGradientBrush(Point.Empty, new Point(0, backgroundRectangle.Height), digitBoxGradient.ColorStart, digitBoxGradient.ColorEnd))
                {
                    if (!digitBoxGradient.IsBlendedForBackground)
                        gr.FillRectangle(brush, infoBoxRectangle);
                    else
                    {
                        Blend bl = new Blend(2);
                        bl.Factors = new float[] { 0.3F, 1.0F };
                        bl.Positions = new float[] { 0.0F, 1.0F };
                        brush.Blend = bl;
                        gr.FillRectangle(brush, infoBoxRectangle);
                    }
                }
            }
        }

        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
        }

        protected virtual void OnDockingModeChanged(EventArgs e)
        {
            if (DockingModeChanged != null)
                DockingModeChanged(this, e);
        }

        protected virtual void OnProgressBarClosed(EventArgs e)
        {
            if (ProgressBarClosed != null)
                ProgressBarClosed(this, e);
        }

        protected virtual bool OnProgressBarClosing()
        {
            bool result = false;
            if (ProgressBarClosing != null)
                ProgressBarClosing(this, out result);

            return result;
        }

        #endregion

        #region Override Methods

        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawBackground(pe.Graphics);
            DrawProgress(pe.Graphics);
            DrawDigitBox(pe.Graphics);
            DrawText(pe.Graphics);
            DrawBorder(pe.Graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GenerateProgressBar();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (!DesignMode && !isDocked)
                {
                    User32.ReleaseCapture();
                    User32.SendMessage(this.Handle, (int)User32.Msgs.WM_NCLBUTTONDOWN, (int)User32._HT_CAPTION, 0);
                }
            }
        }

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);

            if (!isDocked)
            {
                User32.SetParent(Handle, IntPtr.Zero);
                User32.SetWindowLong(Handle, (int)User32.WindowExStyles.GWL_STYLE, User32.GetWindowLong(Handle, (int)User32.WindowExStyles.GWL_STYLE) ^ 0x40000000);
                if (alphaMaker != null)
                {
                    isLayered = !isLayered;
                    alphaMaker.IFloatWindowControl = this;
                    alphaMaker.SetLayered(ref isLayered);
                }
            }
        }

        /// <summary>
        /// If IsUserInteraction property is enabled, executes this method operations.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (isUserInteraction)
            {
                try
                {
                    switch (keyData)
                    {
                        // Changes to maximum value.
                        case Keys.End:
                            this.Value = this.Maximum;
                            break;
                        // Changes to minimum value.
                        case Keys.Home:
                            this.Value = this.Minimum;
                            break;
                        // Decrease the current value on the control.
                        case Keys.Left:
                            this.Value--;
                            break;
                        // Increase the current value on the control.
                        case Keys.Right:
                            this.Value++;
                            break;
                        case Keys.Return:
                            if (MessageBox.Show("Do you want to re-docking this control?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                DockUndockProgressBar = !DockUndockProgressBar;
                            }
                            break;
                        case Keys.F1:
                            break;
                    }
                }
                catch { }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)User32.Msgs.WM_CLOSE)
            {
                // Fire a ProgressBarClosing Event.
                if (OnProgressBarClosing())
                {
                    m.Result = new IntPtr(1);
                    return;
                }

                // Fire a ProgressBarClosed Event.
                OnProgressBarClosed(EventArgs.Empty);
            }

            base.WndProc(ref m);
            if (m.Msg == (int)User32.Msgs.WM_SYSCOMMAND)
            {
                int wParam = m.WParam.ToInt32();
                if (wParam == SYSTEM_MENUITEMS[0])
                {
                    digitBoxGradient.DigitalNumberSide = GradientDigitBox.DigitalNumberLayout.LeftSide;

                    IntPtr menuHandle = User32.GetSystemMenu(this.Handle, false);
                    User32.RemoveMenu(menuHandle, 0, User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_REMOVE);

                    IntPtr subMenu = User32.CreatePopupMenu();
                    User32.AppendMenu(subMenu, digitBoxGradient.DigitalNumberSide != GradientDigitBox.DigitalNumberLayout.LeftSide ? User32.MenuFlags.MF_STRING : User32.MenuFlags.MF_STRING | User32.MenuFlags.MF_GRAYED | User32.MenuFlags.MF_DISABLED | User32.MenuFlags.MF_CHECKED, SYSTEM_MENUITEMS[0], "Left Side");
                    User32.AppendMenu(subMenu, digitBoxGradient.DigitalNumberSide != GradientDigitBox.DigitalNumberLayout.RightSide ? User32.MenuFlags.MF_STRING : User32.MenuFlags.MF_STRING | User32.MenuFlags.MF_GRAYED | User32.MenuFlags.MF_DISABLED | User32.MenuFlags.MF_CHECKED, SYSTEM_MENUITEMS[1], "Right Side");
                    User32.InsertMenu(menuHandle, 0, isDigitDrawEnabled ? User32.MenuFlags.MF_POPUP | User32.MenuFlags.MF_BYPOSITION : User32.MenuFlags.MF_POPUP | User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_GRAYED | User32.MenuFlags.MF_DISABLED, (uint)subMenu, "DigitBox Layouts");
                }
                else if (wParam == SYSTEM_MENUITEMS[1])
                {
                    digitBoxGradient.DigitalNumberSide = GradientDigitBox.DigitalNumberLayout.RightSide;

                    IntPtr menuHandle = User32.GetSystemMenu(this.Handle, false);
                    User32.RemoveMenu(menuHandle, 0, User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_REMOVE);

                    IntPtr subMenu = User32.CreatePopupMenu();
                    User32.AppendMenu(subMenu, digitBoxGradient.DigitalNumberSide != GradientDigitBox.DigitalNumberLayout.LeftSide ? User32.MenuFlags.MF_STRING : User32.MenuFlags.MF_STRING | User32.MenuFlags.MF_GRAYED | User32.MenuFlags.MF_DISABLED | User32.MenuFlags.MF_CHECKED, SYSTEM_MENUITEMS[0], "Left Side");
                    User32.AppendMenu(subMenu, digitBoxGradient.DigitalNumberSide != GradientDigitBox.DigitalNumberLayout.RightSide ? User32.MenuFlags.MF_STRING : User32.MenuFlags.MF_STRING | User32.MenuFlags.MF_GRAYED | User32.MenuFlags.MF_DISABLED | User32.MenuFlags.MF_CHECKED, SYSTEM_MENUITEMS[1], "Right Side");
                    User32.InsertMenu(menuHandle, 0, isDigitDrawEnabled ? User32.MenuFlags.MF_POPUP | User32.MenuFlags.MF_BYPOSITION : User32.MenuFlags.MF_POPUP | User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_GRAYED | User32.MenuFlags.MF_DISABLED, (uint)subMenu, "DigitBox Layouts");
                }
                else if (wParam == SYSTEM_MENUITEMS[2])
                    DockUndockProgressBar = !DockUndockProgressBar;
                else if (wParam == SYSTEM_MENUITEMS[3])
                {
                    if (alphaMaker != null)
                    {
                        alphaMaker.IFloatWindowControl = this;
                        alphaMaker.SetLayered(ref isLayered);

                        IntPtr menuHandle = User32.GetSystemMenu(this.Handle, false);
                        User32.RemoveMenu(menuHandle, 3, User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_REMOVE);
                        User32.InsertMenu(menuHandle, 3, isLayered ? User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_STRING | User32.MenuFlags.MF_CHECKED : User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_STRING, SYSTEM_MENUITEMS[3],
                            String.Format("Opacity {0}[{1}]", isLayered ? "Enabled" : "Disabled", currentTransparency));
                    }
                }
                else if (wParam == SYSTEM_MENUITEMS[4])
                {
                    if (alphaMaker != null)
                    {
                        string error = "";
                        alphaMaker.IFloatWindowControl = this;
                        for (; ; )
                        {
                            string result = Microsoft.VisualBasic.Interaction.InputBox(String.Format("Please enter a {0} opacity value here, {1}", error, "the value must be in the range of 0 to 255."), "Seek To Opacity", targetTransparency.ToString(), -1, -1);

                            if (result.Equals(String.Empty))
                                break;

                            try
                            {
                                byte value = Convert.ToByte(result);

                                if (!isLayered)
                                    alphaMaker.SetLayered(ref isLayered);

                                alphaMaker.SeekToOpacity(value);

                                IntPtr menuHandle = User32.GetSystemMenu(this.Handle, false);
                                User32.RemoveMenu(menuHandle, 3, User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_REMOVE);
                                User32.InsertMenu(menuHandle, 3, isLayered ? User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_STRING | User32.MenuFlags.MF_CHECKED : User32.MenuFlags.MF_BYPOSITION | User32.MenuFlags.MF_STRING, SYSTEM_MENUITEMS[3],
                                    String.Format("Opacity {0}[{1}]", isLayered ? "Enabled" : "Disabled", targetTransparency));

                                break;
                            }
                            catch (Exception)
                            {
                                error = "valid";
                            }
                        }
                    }
                }
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                progressGradient.GradientChanged -= CONTROL_INVALIDATE_UPDATE;
                progressGradient.Dispose();

                backgroundGradient.GradientChanged -= CONTROL_INVALIDATE_UPDATE;
                backgroundGradient.Dispose();

                progressColorizer.ProgressColorizerChanged -= CONTROL_INVALIDATE_UPDATE;
                progressColorizer.Dispose();

                digitBoxGradient.GradientChanged -= CONTROL_INVALIDATE_UPDATE;
                digitBoxGradient.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        #region IProgressBar Members

        /// <summary>
        /// Gets or Sets, the current progress value of the control.
        /// </summary>
        [Description("Gets or Sets, the current progress value of the control")]
        [DefaultValue(20)]
        [Browsable(true)]
        [Category("Progress")]
        public int Value
        {
            get
            {
                return progress;
            }
            set
            {
                if (!value.Equals(progress))
                {
                    if (value > maximum)
                        throw new ArgumentException("Value must be smaller than or equal to Maximum.");
                    if (value < minimum)
                        throw new ArgumentException("Value must be greater than or equal to Minimum.");

                    progress = value;

                    GenerateProgressBar();

                    // Trigger a ValueChanged event.
                    OnValueChanged();

                    Invalidate();
                    Update();
                }
            }
        }

        /// <summary>
        /// Gets or Sets, the minimum progress value of the control.
        /// </summary>
        [Description("Gets or Sets, the minimum progress value of the control")]
        [DefaultValue(0)]
        [Browsable(true)]
        [Category("Progress")]
        public int Minimum
        {
            get
            {
                return minimum;
            }
            set
            {
                if (!value.Equals(minimum))
                {
                    if (value >= maximum)
                        throw new ArgumentException("Minimum must be smaller than Maximum.");
                    else if (value < 0)
                        throw new ArgumentException("Minimum must be positive integer.");

                    minimum = value;

                    GenerateProgressBar();

                    Invalidate();
                    Update();
                }
            }
        }

        /// <summary>
        /// Gets or Sets, the maximum progress value of the control.
        /// </summary>
        [Description("Gets or Sets, the maximum progress value of the control")]
        [DefaultValue(100)]
        [Browsable(true)]
        [Category("Progress")]
        public int Maximum
        {
            get
            {
                return maximum;
            }
            set
            {
                if (!value.Equals(maximum))
                {
                    if (value <= minimum)
                        throw new ArgumentException("Maximum must be greater than Minimum.");

                    maximum = value;

                    GenerateProgressBar();

                    Invalidate();
                    Update();
                }
            }
        }

        /// <summary>
        /// Determines whether the control's border is draw or not.
        /// </summary>
        [Description("Determines whether the control's border is draw or not")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(true)]
        [Category("Appearance")]
        public bool IsPaintBorder
        {
            get
            {
                return isPaintBorder;
            }
            set
            {
                if (!value.Equals(isPaintBorder))
                {
                    isPaintBorder = value;

                    Invalidate();
                    Update();
                }
            }
        }

        /// <summary>
        /// Determines whether the digital number drawing is enabled or not.
        /// </summary>
        [Description("Determines whether the digital number drawing is enabled or not")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(false)]
        [Browsable(true)]
        [Category("Appearance")]
        public bool IsDigitDrawEnabled
        {
            get
            {
                return isDigitDrawEnabled;
            }
            set
            {
                if (!value.Equals(isDigitDrawEnabled))
                {
                    isDigitDrawEnabled = value;

                    Invalidate();
                    Update();
                }
            }
        }

        /// <summary>
        /// Determines whether the percentage text is show or hide.
        /// </summary>
        [Description("Determines whether the percentage text is show or hide")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(true)]
        [Browsable(true)]
        [Category("Appearance")]
        public bool ShowPercentage
        {
            get
            {
                return showPercentage;
            }
            set
            {
                if (!value.Equals(showPercentage))
                {
                    showPercentage = value;

                    Invalidate();
                    Update();
                }
            }
        }

        /// <summary>
        /// Display text formatting for progressbar value.
        /// </summary>
        [Description("Display text formatting for progressbar value")]
        [DefaultValue("done")]
        [Browsable(true)]
        [Category("Appearance")]
        public string DisplayFormat
        {
            get { return displayFormat; }
            set
            {
                try
                {
                    if (!value.Equals(displayFormat))
                    {
                        displayFormat = value;

                        // Update text on the control.
                        string[] result = this.Text.Split(new char[] { '%', ' ' }, 2);
                        this.Text = String.Format("{0}% {1}", result[0], displayFormat);

                        if (showPercentage)
                        {
                            Invalidate();
                            Update();
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Value cannot be null!, please enter a valid value or empty string.");
                }
            }
        }

        /// <summary>
        /// Gets or Sets, the control's border color from here.
        /// </summary>
        [Description("Gets or Sets, the control's border color from here")]
        [DefaultValue(typeof(Color), "DarkGray")]
        [Browsable(true)]
        [Category("Appearance")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                if (!value.Equals(borderColor))
                {
                    borderColor = value;

                    if (isPaintBorder)
                    {
                        Invalidate();
                        Update();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or Sets, the current border style of the ProgressBar control.
        /// </summary>
        [Description("Gets or Sets, the current border style of the ProgressBar control")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(typeof(ProgressBarBorderStyle), "Flat")]
        [Browsable(true)]
        [Category("Appearance")]
        public ProgressBarBorderStyle ControlBorderStyle
        {
            get
            {
                return controlBorderStyle;
            }
            set
            {
                if (!value.Equals(controlBorderStyle))
                {
                    controlBorderStyle = value;

                    if (isPaintBorder)
                    {
                        Invalidate();
                        Update();
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when the progress value changed of the control.
        /// </summary>
        [Description("Occurs when the progress value changed of the control")]
        public event EventHandler ValueChanged;

        #endregion

        #region IFloatWindowBase Members

        public bool SetFloatWindowTaskbarText(IntPtr controlHandle, string text)
        {
            return User32.SetWindowText(controlHandle, text);
        }

        [Browsable(false)]
        public bool DockUndockProgressBar
        {
            get
            {
                return isDocked;
            }
            set
            {
                if (!value.Equals(isDocked))
                {
                    isDocked = value;
                    if (!isDocked)
                    {
                        // Save the current location.
                        oldLocation = this.Location;

                        // Top of the control container.
                        parentContainer = this.Parent;
                        UpdateStyles();
                        Parent = null;
                        User32.SetParent(Handle, IntPtr.Zero);
                        User32.SetWindowLong(Handle, (int)User32.WindowExStyles.GWL_STYLE, User32.GetWindowLong(Handle, (int)User32.WindowExStyles.GWL_STYLE) ^ 0x40000000);

                        if (alphaMaker != null && !isLayered)
                        {
                            alphaMaker.IFloatWindowControl = this;
                            alphaMaker.SetLayered(ref isLayered);
                        }

                        // Create System Menu Items
                        CreateSystemMenu();
                    }
                    else
                    {
                        // Clear System Menu Items
                        ResetSystemMenu();

                        // If our alpha component is not null, we clear transparency attributes.
                        if (alphaMaker != null && isLayered)
                        {
                            alphaMaker.IFloatWindowControl = this;
                            alphaMaker.SetLayered(ref isLayered);
                        }

                        UpdateStyles();
                        User32.SetWindowLong(Handle, (int)User32.WindowExStyles.GWL_STYLE, User32.GetWindowLong(Handle, (int)User32.WindowExStyles.GWL_STYLE) ^ 0x40000000);
                        Parent = parentContainer;

                        //Re-Set old location to the control.
                        this.Location = oldLocation;
                    }

                    // Fire a DockingModeChanged Event.
                    OnDockingModeChanged(EventArgs.Empty);
                }
            }
        }

        private bool ShouldSerializeDockUndockProgressBar()
        {
            return false;
        }

        [Browsable(false)]
        public Control DockParentContainer
        {
            get { return parentContainer; }
        }

        #endregion

        #region IFloatWindowAlpha Members

        /// <summary>
        /// Gets, the handle of the float window, whose window has a alpha support or to be.
        /// </summary>
        [Browsable(false)]
        public IntPtr FloatWindowHandle
        {
            get { return Handle; }
        }

        /// <summary>
        /// Gets, the float window control is marked to layered flag or not.
        /// </summary>
        [Browsable(false)]
        public bool IsLayered
        {
            get { return isLayered; }
        }

        /// <summary>
        /// Gets or sets, the target opacity value of the float window.
        /// </summary>
        [Browsable(false)]
        public byte TargetTransparency
        {
            get
            {
                return targetTransparency;
            }
            set
            {
                if (!value.Equals(targetTransparency))
                    targetTransparency = value;
            }
        }

        private bool ShouldSerializeTargetTransparency()
        {
            return false;
        }

        /// <summary>
        /// Gets or sets, the currently opacity value of the float window.
        /// </summary>
        [Browsable(false)]
        public byte CurrentTransparency
        {
            get { return currentTransparency; }
            set
            {
                if (!value.Equals(currentTransparency))
                    currentTransparency = value;
            }
        }

        private bool ShouldSerializeCurrentTransparency()
        {
            return false;
        }

        #endregion

        [Editor(typeof(GradientProgressEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(GradientProgressConverter))]
        public class GradientProgress : IDisposable
        {
            #region Event

            /// <summary>
            /// Occurs when the sub properties changed of the ProgressGradient property.
            /// </summary>
            [Description("Occurs when the sub properties changed of the ProgressGradient property")]
            public event EventHandler GradientChanged;

            #endregion

            #region Instance Members

            private bool isBlended = false;

            private ColorManipulation[] manipuleArray = new ColorManipulation[] 
            { 
                new ColorManipulation(Color.LightBlue), new ColorManipulation(Color.IndianRed) 
            };

            #endregion

            #region Constructor

            public GradientProgress() { }

            public GradientProgress(Color first, Color second)
            {
                this.ColorStart = first;
                this.ColorEnd = second;
            }

            public GradientProgress(Color first, Color second, bool isBlended)
            {
                this.ColorStart = first;
                this.ColorEnd = second;
                this.isBlended = isBlended;
            }

            #endregion

            #region Property

            /// <summary>
            /// Determines whether the blended effect enable or not for progress indicator.
            /// </summary>
            [Description("Determines whether the blended effect enable or not for progress indicator")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(false)]
            [Browsable(true)]
            public bool IsBlendedForProgress
            {
                get { return isBlended; }
                set
                {
                    if (!value.Equals(isBlended))
                    {
                        isBlended = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets, the first progress color.
            /// </summary>
            [Description("Gets or Sets, the first progress color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "LightBlue")]
            [Browsable(true)]
            public Color ColorStart
            {
                get { return manipuleArray[0].BaseColor; }
                set
                {
                    if (!value.Equals(manipuleArray[0].BaseColor))
                    {
                        manipuleArray[0] = new ColorManipulation(value);
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets, the second progress color.
            /// </summary>
            [Description("Gets or Sets, the second progress color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "IndianRed")]
            [Browsable(true)]
            public Color ColorEnd
            {
                get { return manipuleArray[1].BaseColor; }
                set
                {
                    if (!value.Equals(manipuleArray[1].BaseColor))
                    {
                        manipuleArray[1] = new ColorManipulation(value);
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets, the first manipulation parameters.
            /// </summary>
            [Description("Gets, the first manipulation parameters")]
            [Browsable(false)]
            public ColorManipulation ManipuleStart
            {
                get { return manipuleArray[0]; }
            }

            /// <summary>
            /// Gets, the second manipulation parameters.
            /// </summary>
            [Description("Gets, the second manipulation parameters")]
            [Browsable(false)]
            public ColorManipulation ManipuleEnd
            {
                get { return manipuleArray[1]; }
            }

            #endregion

            #region  Virtual Methods

            protected virtual void OnGradientChanged(EventArgs e)
            {
                if (GradientChanged != null)
                    GradientChanged(this, e);
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        class GradientProgressConverter : ExpandableObjectConverter
        {
            #region Destructor

            ~GradientProgressConverter()
            {
                GC.SuppressFinalize(this);
            }

            #endregion

            #region Override Methods

            //All the CanConvertTo() method needs to is check that the target type is a string.
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                else
                    return base.CanConvertTo(context, destinationType);
            }

            //ConvertTo() simply checks that it can indeed convert to the desired type.
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return ToString(value);
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }

            /* The exact same process occurs in reverse when converting a GradientProgress object to a string.
            First the Properties window calls CanConvertFrom(). If it returns true, the next step is to call
            the ConvertFrom() method. */
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                else
                    return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                    return FromString(value);
                else
                    return base.ConvertFrom(context, culture, value);
            }

            #endregion

            #region Helper Methods

            private string ToString(object value)
            {
                GradientProgress gradient = value as GradientProgress;    // Gelen object tipimizi GradientProgress tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
                ColorConverter converter = new ColorConverter();
                return String.Format("{0}, {1}, {2}",
                    converter.ConvertToString(gradient.ColorStart), converter.ConvertToString(gradient.ColorEnd), gradient.IsBlendedForProgress);
            }

            private GradientProgress FromString(object value)
            {
                string[] result = ((string)value).Split(',');
                if (result.Length != 3)
                    throw new ArgumentException("Could not convert to value");

                try
                {
                    GradientProgress gradient = new GradientProgress();

                    // Retrieve the colors
                    ColorConverter converter = new ColorConverter();
                    gradient.ColorStart = (Color)converter.ConvertFromString(result[0]);
                    gradient.ColorEnd = (Color)converter.ConvertFromString(result[1]);
                    // Retrieve boolean value
                    BooleanConverter booleanConverter = new BooleanConverter();
                    gradient.IsBlendedForProgress = (bool)booleanConverter.ConvertFromString(result[2]);

                    return gradient;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Could not convert to value");
                }
            }

            #endregion
        }

        class GradientProgressEditor : UITypeEditor
        {
            #region Destructor

            ~GradientProgressEditor()
            {
                GC.SuppressFinalize(this);
            }

            #endregion

            #region Override Methods

            public override bool GetPaintValueSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override void PaintValue(PaintValueEventArgs e)
            {
                GradientProgress gradient = e.Value as GradientProgress;
                using (LinearGradientBrush brush = new LinearGradientBrush(Point.Empty, new Point(0, e.Bounds.Height), gradient.ColorStart, gradient.ColorEnd))
                {
                    if (!gradient.IsBlendedForProgress)
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    else
                    {
                        Blend bl = new Blend(2);
                        bl.Factors = new float[] { 0.3F, 1.0F };
                        bl.Positions = new float[] { 0.0F, 1.0F };
                        brush.Blend = bl;
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                }
            }

            #endregion
        }

        [Editor(typeof(GradientBackgroundEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(GradientBackgroundConverter))]
        public class GradientBackground : IDisposable
        {
            #region Event

            /// <summary>
            /// Occurs when the sub properties changed of the BackgroundGradient property.
            /// </summary>
            [Description("Occurs when the sub properties changed of the BackgroundGradient property")]
            public event EventHandler GradientChanged;

            #endregion

            #region Instance Members

            private bool isBlended = false;
            private Color[] colorArray = { Color.WhiteSmoke, Color.Gainsboro };

            #endregion

            #region Constructor

            public GradientBackground() { }

            public GradientBackground(Color first, Color second)
            {
                this.colorArray[0] = first;
                this.colorArray[1] = second;
            }

            public GradientBackground(Color first, Color second, bool isBlended)
            {
                this.colorArray[0] = first;
                this.colorArray[1] = second;
                this.isBlended = isBlended;
            }

            #endregion

            #region Property

            /// <summary>
            /// Determines whether the blended effect enable or not for progress background.
            /// </summary>
            [Description("Determines whether the blended effect enable or not for progress background")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(false)]
            [Browsable(true)]
            public bool IsBlendedForBackground
            {
                get { return isBlended; }
                set
                {
                    if (!value.Equals(isBlended))
                    {
                        isBlended = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets, the first background color.
            /// </summary>
            [Description("Gets or Sets, the first background color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "WhiteSmoke")]
            [Browsable(true)]
            public Color ColorStart
            {
                get { return colorArray[0]; }
                set
                {
                    if (!value.Equals(colorArray[0]))
                    {
                        colorArray[0] = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets, the second background color.
            /// </summary>
            [Description("Gets or Sets, the second background color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Gainsboro")]
            [Browsable(true)]
            public Color ColorEnd
            {
                get { return colorArray[1]; }
                set
                {
                    if (!value.Equals(colorArray[1]))
                    {
                        colorArray[1] = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            #endregion

            #region Virtual Methods

            protected virtual void OnGradientChanged(EventArgs e)
            {
                if (GradientChanged != null)
                    GradientChanged(this, e);
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        class GradientBackgroundConverter : ExpandableObjectConverter
        {
            #region Destructor

            ~GradientBackgroundConverter()
            {
                GC.SuppressFinalize(this);
            }

            #endregion

            #region Override Methods

            //All the CanConvertTo() method needs to is check that the target type is a string.
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                else
                    return base.CanConvertTo(context, destinationType);
            }

            //ConvertTo() simply checks that it can indeed convert to the desired type.
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return ToString(value);
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }

            /* The exact same process occurs in reverse when converting a GradientBackground object to a string.
            First the Properties window calls CanConvertFrom(). If it returns true, the next step is to call
            the ConvertFrom() method. */
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                else
                    return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                    return FromString(value);
                else
                    return base.ConvertFrom(context, culture, value);
            }

            #endregion

            #region Helper Methods

            private string ToString(object value)
            {
                GradientBackground gradient = value as GradientBackground;    // Gelen object tipimizi GradientBackground tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
                ColorConverter converter = new ColorConverter();
                return String.Format("{0}, {1}, {2}",
                    converter.ConvertToString(gradient.ColorStart), converter.ConvertToString(gradient.ColorEnd), gradient.IsBlendedForBackground);
            }

            private GradientBackground FromString(object value)
            {
                string[] result = ((string)value).Split(',');
                if (result.Length != 3)
                    throw new ArgumentException("Could not convert to value");

                try
                {
                    GradientBackground gradient = new GradientBackground();

                    // Retrieve the colors
                    ColorConverter converter = new ColorConverter();
                    gradient.ColorStart = (Color)converter.ConvertFromString(result[0]);
                    gradient.ColorEnd = (Color)converter.ConvertFromString(result[1]);
                    // Retrieve the boolean value
                    BooleanConverter booleanConverter = new BooleanConverter();
                    gradient.IsBlendedForBackground = (bool)booleanConverter.ConvertFromString(result[2]);

                    return gradient;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Could not convert to value");
                }
            }

            #endregion
        }

        class GradientBackgroundEditor : UITypeEditor
        {
            #region Destructor

            ~GradientBackgroundEditor()
            {
                GC.SuppressFinalize(this);
            }

            #endregion

            #region Override Methods

            public override bool GetPaintValueSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override void PaintValue(PaintValueEventArgs e)
            {
                GradientBackground gradient = e.Value as GradientBackground;
                using (LinearGradientBrush brush = new LinearGradientBrush(Point.Empty, new Point(0, e.Bounds.Height), gradient.ColorStart, gradient.ColorEnd))
                {
                    if (!gradient.IsBlendedForBackground)
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    else
                    {
                        Blend bl = new Blend(2);
                        bl.Factors = new float[] { 0.3F, 1.0F };
                        bl.Positions = new float[] { 0.0F, 1.0F };
                        brush.Blend = bl;
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                }
            }

            #endregion
        }

        [Editor(typeof(ColorizerProgressEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ColorizerProgressConverter))]
        public class ColorizerProgress : IProgressColorizer
        {
            #region Event

            /// <summary>
            /// Occurs when the sub properties changed of the ProgressColorizer property.
            /// </summary>
            [Description("Occurs when the sub properties changed of the ProgressColorizer property")]
            public event EventHandler ProgressColorizerChanged;

            #endregion

            #region Instance Members

            // Red, Green, Blue, Alpha
            private byte[] rgba = { 180, 180, 180, 180 };
            // IsColorizerEnabled, IsTransparencyEnabled
            private bool[] options = { false, false };

            #endregion

            #region Constructor

            public ColorizerProgress() { }

            public ColorizerProgress(byte red, byte green, byte blue, byte alpha,
                bool isColorizerEnabled, bool isTransparencyEnabled)
            {
                // Sets RGBA
                rgba[0] = red;
                rgba[1] = green;
                rgba[2] = blue;
                rgba[3] = alpha;

                // Sets Options
                options[0] = isColorizerEnabled;
                options[1] = isTransparencyEnabled;
            }

            #endregion

            #region Virtual Methods

            protected virtual void OnProgressColorizerChanged(EventArgs e)
            {
                if (ProgressColorizerChanged != null)
                    ProgressColorizerChanged(this, e);
            }

            #endregion

            #region IProgressColorizer Members

            /// <summary>
            /// Determines whether the colorizer effect is enable or not for progress bitmap.
            /// </summary>
            [Description("Determines whether the colorizer effect is enable or not for progress bitmap")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(false)]
            [Browsable(true)]
            public bool IsColorizerEnabled
            {
                get { return options[0]; }
                set
                {
                    if (!value.Equals(options[0]))
                    {
                        options[0] = value;
                        OnProgressColorizerChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Determines whether the transparency effect is visible or not for progress bitmap.
            /// </summary>
            [Description("Determines whether the transparency effect is visible or not for progress bitmap")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(false)]
            [Browsable(true)]
            public bool IsTransparencyEnabled
            {
                get { return options[1]; }
                set
                {
                    if (!value.Equals(options[1]))
                    {
                        options[1] = value;
                        OnProgressColorizerChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// The red color component property must be in the range of 0 to 255.
            /// </summary>
            [Description("The red color component property must be in the range of 0 to 255")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Byte), "180")]
            [Browsable(true)]
            public byte Red
            {
                get { return rgba[0]; }
                set
                {
                    if (!value.Equals(rgba[0]))
                    {
                        rgba[0] = value;

                        if (IsColorizerEnabled)
                            OnProgressColorizerChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// The green color component property must be in the range of 0 to 255.
            /// </summary>
            [Description("The green color component property must be in the range of 0 to 255")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Byte), "180")]
            [Browsable(true)]
            public byte Green
            {
                get { return rgba[1]; }
                set
                {
                    if (!value.Equals(rgba[1]))
                    {
                        rgba[1] = value;

                        if (IsColorizerEnabled)
                            OnProgressColorizerChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// The blue color component property must be in the range of 0 to 255.
            /// </summary>
            [Description("The blue color component property must be in the range of 0 to 255")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Byte), "180")]
            [Browsable(true)]
            public byte Blue
            {
                get { return rgba[2]; }
                set
                {
                    if (!value.Equals(rgba[2]))
                    {
                        rgba[2] = value;

                        if (IsColorizerEnabled)
                            OnProgressColorizerChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// This property must be in the range of 50 to 255.
            /// </summary>
            [Description("This property must be in the range of 50 to 255")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Byte), "180")]
            [Browsable(true)]
            public byte Alpha
            {
                get { return rgba[3]; }
                set
                {
                    if (!value.Equals(rgba[3]))
                    {
                        if (value < 50)
                            value = 50;

                        rgba[3] = value;

                        if (IsTransparencyEnabled)
                            OnProgressColorizerChanged(EventArgs.Empty);
                    }
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        class ColorizerProgressConverter : ExpandableObjectConverter
        {
            #region Destructor

            ~ColorizerProgressConverter()
            {
                GC.SuppressFinalize(this);
            }

            #endregion

            #region Override Methods

            //All the CanConvertTo() method needs to is check that the target type is a string.
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                else
                    return base.CanConvertTo(context, destinationType);
            }

            //ConvertTo() simply checks that it can indeed convert to the desired type.
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return ToString(value);
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }

            /* The exact same process occurs in reverse when converting a ColorizerProgress object to a string.
            First the Properties window calls CanConvertFrom(). If it returns true, the next step is to call
            the ConvertFrom() method. */
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                else
                    return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                    return FromString(value);
                else
                    return base.ConvertFrom(context, culture, value);
            }

            #endregion

            #region Helper Methods

            private string ToString(object value)
            {
                // Gelen object tipimizi ColorizerProgress tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
                ColorizerProgress colorizer = value as ColorizerProgress;

                return String.Format("{0}, {1}, {2}, {3}, {4}, {5}",
                    colorizer.Red, colorizer.Green, colorizer.Blue, colorizer.Alpha,
                    colorizer.IsColorizerEnabled, colorizer.IsTransparencyEnabled);
            }

            private ColorizerProgress FromString(object value)
            {
                string[] result = ((string)value).Split(',');
                if (result.Length != 6)
                    throw new ArgumentException("Could not convert to value");

                try
                {
                    ColorizerProgress colorizer = new ColorizerProgress();

                    ByteConverter byteConverter = new ByteConverter();
                    BooleanConverter booleanConverter = new BooleanConverter();

                    // Retrieve the values of the object.
                    colorizer.Red = (byte)byteConverter.ConvertFromString(result[0]);
                    colorizer.Green = (byte)byteConverter.ConvertFromString(result[1]);
                    colorizer.Blue = (byte)byteConverter.ConvertFromString(result[2]);
                    colorizer.Alpha = (byte)byteConverter.ConvertFromString(result[3]);
                    colorizer.IsColorizerEnabled = (bool)booleanConverter.ConvertFromString(result[4]);
                    colorizer.IsTransparencyEnabled = (bool)booleanConverter.ConvertFromString(result[5]);

                    return colorizer;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Could not convert to value");
                }
            }

            #endregion
        }

        class ColorizerProgressEditor : UITypeEditor
        {
            #region Constructor

            public ColorizerProgressEditor() : base() { }

            #endregion

            #region Destructor

            ~ColorizerProgressEditor()
            {
                GC.SuppressFinalize(this);
            }

            #endregion

            #region Override Methods

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                if (provider != null)
                {
                    IWindowsFormsEditorService editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
                    if (editorService != null)
                    {
                        using (DropDownProgress dropDownProgress = new DropDownProgress(value, editorService))
                        {
                            editorService.DropDownControl(dropDownProgress);
                            value = dropDownProgress.Colorizer;
                        }
                    }
                }

                return value;
            }

            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.DropDown;  // We choose the drop-down style.
            }

            /// <summary>
            /// Indicates whether the specified context supports painting a representation of an object's value within the specified context.
            /// </summary>
            /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
            /// <returns>Normally, true if PaintValue is implemented; otherwise, false.But, in this scope returns false.</returns>
            public override bool GetPaintValueSupported(ITypeDescriptorContext context)
            {
                return false;   // We turn down thumbnails.
            }

            #endregion
        }

        [Editor(typeof(GradientDigitBoxEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(GradientDigitBoxConverter))]
        public class GradientDigitBox : IDisposable
        {
            #region Enum

            public enum DigitalNumberLayout
            {
                LeftSide, RightSide
            };

            #endregion

            #region Event

            /// <summary>
            /// Occurs when the sub properties changed of the DigitBoxGradient property.
            /// </summary>
            [Description("Occurs when the sub properties changed of the DigitBoxGradient property")]
            public event EventHandler GradientChanged;

            #endregion

            #region Instance Members

            private bool isBlended = false;
            private Color[] colorArray = { Color.WhiteSmoke, Color.Aquamarine, Color.LightGray };
            private DigitalNumberLayout digitalNumberSide = DigitalNumberLayout.RightSide;

            #endregion

            #region Constructor

            public GradientDigitBox() { }

            public GradientDigitBox(Color first, Color second)
            {
                this.colorArray[0] = first;
                this.colorArray[1] = second;
            }

            public GradientDigitBox(Color first, Color second, bool isBlended)
            {
                this.colorArray[0] = first;
                this.colorArray[1] = second;
                this.isBlended = isBlended;
            }

            public GradientDigitBox(Color first, Color second, Color borderColor, bool isBlended)
            {
                this.colorArray[0] = first;
                this.colorArray[1] = second;
                this.colorArray[2] = borderColor;
                this.isBlended = isBlended;
            }

            public GradientDigitBox(Color first, Color second, Color borderColor, bool isBlended, DigitalNumberLayout side)
            {
                this.colorArray[0] = first;
                this.colorArray[1] = second;
                this.colorArray[2] = borderColor;
                this.isBlended = isBlended;
                this.digitalNumberSide = side;
            }

            #endregion

            #region Property

            /// <summary>
            /// Determines whether the blended effect enable or not for DigitBox background.
            /// </summary>
            [Description("Determines whether the blended effect enable or not for DigitBox background")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(false)]
            [Browsable(true)]
            public bool IsBlendedForBackground
            {
                get { return isBlended; }
                set
                {
                    if (!value.Equals(isBlended))
                    {
                        isBlended = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets, the first background color of the DigitBox rectangle.
            /// </summary>
            [Description("Gets or Sets, the first background color of the DigitBox rectangle")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "WhiteSmoke")]
            [Browsable(true)]
            public Color ColorStart
            {
                get { return colorArray[0]; }
                set
                {
                    if (!value.Equals(colorArray[0]))
                    {
                        colorArray[0] = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets, the second background color of the DigitBox rectangle.
            /// </summary>
            [Description("Gets or Sets, the second background color of the DigitBox rectangle")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Aquamarine")]
            [Browsable(true)]
            public Color ColorEnd
            {
                get { return colorArray[1]; }
                set
                {
                    if (!value.Equals(colorArray[1]))
                    {
                        colorArray[1] = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets, the border color of the DigitBox rectangle.
            /// </summary>
            [Description("Gets or Sets, the border color of the DigitBox rectangle")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "LightGray")]
            [Browsable(true)]
            public Color BorderColor
            {
                get { return colorArray[2]; }
                set
                {
                    if (!value.Equals(colorArray[2]))
                    {
                        colorArray[2] = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets, the layouts of the DigitBox rectangle on the control.
            /// </summary>
            [Description("Gets or Sets, the layouts of the DigitBox rectangle on the control")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(DigitalNumberLayout), "RightSide")]
            [Browsable(true)]
            public DigitalNumberLayout DigitalNumberSide
            {
                get { return digitalNumberSide; }
                set
                {
                    if (!value.Equals(digitalNumberSide))
                    {
                        digitalNumberSide = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            #endregion

            #region Virtual Methods

            protected virtual void OnGradientChanged(EventArgs e)
            {
                if (GradientChanged != null)
                    GradientChanged(this, e);
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        class GradientDigitBoxConverter : ExpandableObjectConverter
        {
            #region Destructor

            ~GradientDigitBoxConverter()
            {
                GC.SuppressFinalize(this);
            }

            #endregion

            #region Override Methods

            //All the CanConvertTo() method needs to is check that the target type is a string.
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                else
                    return base.CanConvertTo(context, destinationType);
            }

            //ConvertTo() simply checks that it can indeed convert to the desired type.
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return ToString(value);
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }

            /* The exact same process occurs in reverse when converting a GradientDigitBox object to a string.
            First the Properties window calls CanConvertFrom(). If it returns true, the next step is to call
            the ConvertFrom() method. */
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                else
                    return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                    return FromString(value);
                else
                    return base.ConvertFrom(context, culture, value);
            }

            #endregion

            #region Helper Methods

            private string ToString(object value)
            {
                GradientDigitBox gradient = value as GradientDigitBox;    // Gelen object tipimizi GradientDigitBox tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
                ColorConverter converter = new ColorConverter();
                return String.Format("{0}, {1}, {2}, {3}, {4}",
                    converter.ConvertToString(gradient.ColorStart), converter.ConvertToString(gradient.ColorEnd), converter.ConvertToString(gradient.BorderColor), gradient.IsBlendedForBackground, gradient.DigitalNumberSide);
            }

            private GradientDigitBox FromString(object value)
            {
                string[] result = ((string)value).Split(',');
                if (result.Length != 5)
                    throw new ArgumentException("Could not convert to value");

                try
                {
                    GradientDigitBox gradient = new GradientDigitBox();

                    // Retrieve the colors
                    ColorConverter converter = new ColorConverter();
                    gradient.ColorStart = (Color)converter.ConvertFromString(result[0]);
                    gradient.ColorEnd = (Color)converter.ConvertFromString(result[1]);
                    gradient.BorderColor = (Color)converter.ConvertFromString(result[2]);
                    // Retrieve the boolean value
                    BooleanConverter booleanConverter = new BooleanConverter();
                    gradient.IsBlendedForBackground = (bool)booleanConverter.ConvertFromString(result[3]);
                    gradient.DigitalNumberSide = (GradientDigitBox.DigitalNumberLayout)Enum.Parse(typeof(GradientDigitBox.DigitalNumberLayout), result[4], true);

                    return gradient;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Could not convert to value");
                }
            }

            #endregion
        }

        class GradientDigitBoxEditor : UITypeEditor
        {
            #region Destructor

            ~GradientDigitBoxEditor()
            {
                GC.SuppressFinalize(this);
            }

            #endregion

            #region Override Methods

            public override bool GetPaintValueSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override void PaintValue(PaintValueEventArgs e)
            {
                GradientDigitBox gradient = e.Value as GradientDigitBox;
                using (LinearGradientBrush brush = new LinearGradientBrush(Point.Empty, new Point(0, e.Bounds.Height), gradient.ColorStart, gradient.ColorEnd))
                {
                    if (!gradient.IsBlendedForBackground)
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    else
                    {
                        Blend bl = new Blend(2);
                        bl.Factors = new float[] { 0.3F, 1.0F };
                        bl.Positions = new float[] { 0.0F, 1.0F };
                        brush.Blend = bl;
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                }
            }

            #endregion
        }
    }

    public class ProgressBarDesigner : ControlDesigner
    {
        #region Instance Members

        private DesignerActionListCollection actionLists;

        #endregion

        #region Constructor

        protected ProgressBarDesigner()
            : base() { }

        #endregion

        #region Destructor

        ~ProgressBarDesigner()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Property

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (actionLists == null)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new ProgressBarActionList((ProgressBar)Control));
                }

                return actionLists;
            }
        }

        #endregion

        #region Override Methods

        /*  As a general rule, always call the base method first in the PreFilterXxx() methods and last in the 
            PostFilterXxx() methods. This way, all designer classes are given the proper opportunity to apply their 
            changes. The ControlDesigner and ComponentDesigner use these methods to add properties like Visible, 
            Enabled, Name, and Locked. */

        /// <summary>
        /// Override this method to remove unused or inappropriate events.
        /// </summary>
        /// <param name="events">Events collection of the control.</param>
        protected override void PostFilterEvents(System.Collections.IDictionary events)
        {
            events.Remove("EnabledChanged");
            events.Remove("PaddingChanged");
            events.Remove("RightToLeftChanged");
            events.Remove("BackgroundImageChanged");
            events.Remove("BackgroundImageLayoutChanged");

            base.PostFilterEvents(events);
        }

        /// <summary>
        /// Override this method to add some properties to the control or change the properties attributes for a dynamic user interface.
        /// </summary>
        /// <param name="properties">Properties collection of the control before than add a new property to the collection by user.</param>
        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);

            // We don't want to show the "Text" property to the end-users at the design-time.
            properties["Text"] = TypeDescriptor.CreateProperty(typeof(ProgressBar),
              (PropertyDescriptor)properties["Text"], BrowsableAttribute.No);

            /* After than, we don't want to see some properties at the design-time for general reasons(Dynamic property attributes). */
            ProgressBar progressBar = Control as ProgressBar;

            if (progressBar != null)
            {
                if (progressBar.ControlBorderStyle != ProgressBarBorderStyle.Flat)
                {
                    properties["BorderColor"] = TypeDescriptor.CreateProperty(typeof(ProgressBar),
                      (PropertyDescriptor)properties["BorderColor"], BrowsableAttribute.No);
                }

                if (!progressBar.IsPaintBorder)
                {
                    properties["BorderColor"] = TypeDescriptor.CreateProperty(typeof(ProgressBar),
                      (PropertyDescriptor)properties["BorderColor"], BrowsableAttribute.No);
                    properties["ControlBorderStyle"] = TypeDescriptor.CreateProperty(typeof(ProgressBar),
                      (PropertyDescriptor)properties["ControlBorderStyle"], BrowsableAttribute.No);
                }

                if (!progressBar.ShowPercentage)
                {
                    properties["Font"] = TypeDescriptor.CreateProperty(typeof(ProgressBar),
                      (PropertyDescriptor)properties["Font"], BrowsableAttribute.No);
                    properties["ForeColor"] = TypeDescriptor.CreateProperty(typeof(ProgressBar),
                      (PropertyDescriptor)properties["ForeColor"], BrowsableAttribute.No);
                    properties["DisplayFormat"] = TypeDescriptor.CreateProperty(typeof(ProgressBar),
                      (PropertyDescriptor)properties["DisplayFormat"], BrowsableAttribute.No);
                }

                if (!progressBar.IsDigitDrawEnabled)
                {
                    properties["DigitBoxGradient"] = TypeDescriptor.CreateProperty(typeof(ProgressBar),
                      (PropertyDescriptor)properties["DigitBoxGradient"], BrowsableAttribute.No);
                }
            }
        }

        /// <summary>
        /// Override this method to remove unused or inappropriate properties.
        /// </summary>
        /// <param name="properties">Properties collection of the control.</param>
        protected override void PostFilterProperties(System.Collections.IDictionary properties)
        {
            properties.Remove("Enabled");
            properties.Remove("Padding");
            properties.Remove("RightToLeft");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");

            base.PostFilterProperties(properties);
        }

        #endregion
    }

    public class ProgressBarActionList : DesignerActionList
    {
        #region Instance Members

        private ProgressBar linkedControl;
        private DesignerActionUIService designerService;

        #endregion

        #region Constructor

        // The constructor associates the control to the smart tag action list.
        public ProgressBarActionList(ProgressBar control)
            : base(control)
        {
            linkedControl = control;
            designerService = (DesignerActionUIService)GetService(typeof(DesignerActionUIService));

            this.AutoShow = true;   // When this control will be added to the design area, the smart tag panel will open automatically.
        }

        #endregion

        #region Destructor

        ~ProgressBarActionList()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Property - DesignerActionPropertyItem

        /* Property that is target of a DesignerActionPropertyItem. */

        public int Value
        {
            get { return linkedControl.Value; }
            set
            {
                GetPropertyByName("Value", false).SetValue(linkedControl, value);
            }
        }

        #endregion

        #region Override Methods

        /* Implementation of this abstract method creates smart tag 
           items, associates their targets, and collects into list. */

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();
            try
            {
                // Creating the action list static headers.
                items.Add(new DesignerActionHeaderItem("Progress"));
                items.Add(new DesignerActionHeaderItem("Appearance"));

                items.Add(new DesignerActionPropertyItem("Value", "Value", "Progress",
                    "Sets, the current progress value of the control."));

                items.Add(new DesignerActionMethodItem(this,
                    "IsColorizerEnabled", "Is Colorizer Enabled: " + (linkedControl.ProgressColorizer.IsColorizerEnabled ? "ON" : "OFF"), "Appearance",
                    "Determines whether the colorizer effect is enable or not for progress bitmap.", false));

                items.Add(new DesignerActionMethodItem(this,
                    "IsTransparencyEnabled", "Is Transparency Enabled: " + (linkedControl.ProgressColorizer.IsTransparencyEnabled ? "ON" : "OFF"), "Appearance",
                    "Determines whether the transparency effect is visible or not for progress bitmap.", false));

                // Add a new static header and its items.
                items.Add(new DesignerActionHeaderItem("Information"));
                items.Add(new DesignerActionTextItem("X: " + linkedControl.Location.X + ", " + "Y: " + linkedControl.Location.Y, "Information"));
                items.Add(new DesignerActionTextItem("Width: " + linkedControl.Size.Width + ", " + "Height: " + linkedControl.Size.Height, "Information"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception while generating the action list panel for this Oranikle.Studio.Controls.ProgressBar, " + ex.Message);
            }

            return items;
        }

        #endregion

        #region Helper Methods

        /* This helper method to retrieve control properties.
           GetProperties ensures undo and menu updates to work properly. */

        private PropertyDescriptor GetPropertyByName(String propName, bool isChildren)
        {
            if (propName != null)
            {
                PropertyDescriptor prop = isChildren ? TypeDescriptor.GetProperties(linkedControl.ProgressColorizer)[propName] : TypeDescriptor.GetProperties(linkedControl)[propName];

                if (prop != null)
                    return prop;
                else
                    throw new ArgumentException("Property name not found!", propName);
            }
            else
                throw new ArgumentNullException("Property name cannot be null!");
        }

        #endregion

        #region General Methods - DesignerActionMethodItem

        /* Methods that are targets of DesignerActionMethodItem entries. */

        public void IsColorizerEnabled()
        {
            try
            {
                GetPropertyByName("IsColorizerEnabled", true).SetValue(linkedControl.ProgressColorizer, !linkedControl.ProgressColorizer.IsColorizerEnabled);
                designerService.Refresh(Component);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception while changing the value of the IsColorizerEnabled property, " + ex.Message);
            }
        }

        public void IsTransparencyEnabled()
        {
            try
            {
                GetPropertyByName("IsTransparencyEnabled", true).SetValue(linkedControl.ProgressColorizer, !linkedControl.ProgressColorizer.IsTransparencyEnabled);
                designerService.Refresh(Component);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception while changing the value of the IsTransparencyEnabled property, " + ex.Message);
            }
        }

        #endregion
    }
}