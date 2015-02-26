using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Runtime.InteropServices;

namespace Oranikle.Studio.Controls.ProgressBar
{
    partial class ProgressBar
    {
        [ToolboxItem(false)]
        partial class DropDownProgress : UserControl
        {
            #region API

            #region Symbolic Constants

            private const int WM_PAINT = 0x000F;
            private const int WM_NCPAINT = 0x0085;
            private const int WM_ERASEBKGND = 0x0014;

            #endregion

            #region UnmanagedMethods
            
            [DllImport("user32", CharSet = CharSet.Auto)]
            internal static extern IntPtr GetDC(
                IntPtr hwnd);

            [DllImport("user32", CharSet = CharSet.Auto)]
            internal static extern int ReleaseDC(
                IntPtr hwnd,
                IntPtr hdc);

            #endregion

            #endregion

            #region Instance Members

            private IWindowsFormsEditorService editorService = null;
            private ColorizerProgress colorizer = null;

            #endregion

            #region Constructor

            public DropDownProgress()
            {
                InitializeComponent();
            }

            public DropDownProgress(object value, IWindowsFormsEditorService editorService)
                : this()
            {
                this.editorService = editorService;
                if (value is ColorizerProgress)
                {
                    this.colorizer = value as ColorizerProgress;
                }
            }

            #endregion

            #region Destructor

            ~DropDownProgress()
            {
                GC.SuppressFinalize(this);
            }

            #endregion

            #region Property

            public ColorizerProgress Colorizer
            {
                get
                {
                    return colorizer = new ColorizerProgress((byte)redTrackBar.Value, (byte)greenTrackBar.Value,
                        (byte)blueTrackBar.Value, (byte)alphaTrackBar.Value, colorizer.IsColorizerEnabled, colorizer.IsTransparencyEnabled);
                }
            }

            #endregion

            #region Override Methods

            /// <summary>
            /// Uygulamaya gelecek windows mesajları burda işlenir.
            /// </summary>
            /// <param name="m">Windows mesaj parametresi</param>
            protected override void WndProc(ref Message m)
            {
                // Kendi process işlemlerini yapsın.
                base.WndProc(ref m);
                // Daha sonra bizim metodumuzuda çağırsın.
                DrawBorder(ref m, this.Width, this.Height);
            }

            #endregion

            #region Helper Methods

            /// <summary>
            /// Kontrolümüz için kenarlık çiziyoruz.
            /// </summary>
            /// <param name="message">Kontrolümüzün Win32 mesaj işleme komutu</param>
            /// <param name="width">Kontrolümüzün genişlik değeri</param>
            /// <param name="height">Kontrolümüzün yükseklik değeri</param>
            private void DrawBorder(ref Message message, int width, int height)
            {
                if (message.Msg == WM_NCPAINT || message.Msg == WM_ERASEBKGND ||
                    message.Msg == WM_PAINT)
                {
                    IntPtr hdc = GetDC(message.HWnd);

                    if (hdc != IntPtr.Zero)
                    {
                        Graphics graphics = Graphics.FromHdc(hdc);
                        Rectangle rectangle = new Rectangle(0, 0, width, height);
                        ControlPaint.DrawBorder(graphics, rectangle, SystemColors.ActiveBorder, this.Padding.Left, ButtonBorderStyle.Solid, SystemColors.ActiveBorder, this.Padding.Top, ButtonBorderStyle.Solid, SystemColors.ActiveBorder, this.Padding.Right, ButtonBorderStyle.Solid, SystemColors.ActiveBorder, this.Padding.Bottom, ButtonBorderStyle.Solid);

                        // Grafik nesnesinden kurtul.
                        ReleaseDC(message.HWnd, hdc);
                    }
                }
            }

            #endregion

            #region Event Handlers

            private void DropDownProgress_Load(object sender, EventArgs e)
            {
                redTrackBar.Value = colorizer.Red;
                greenTrackBar.Value = colorizer.Green;
                blueTrackBar.Value = colorizer.Blue;
                alphaTrackBar.Value = colorizer.Alpha;
            }

            private void closeDialogBtn_Click(object sender, EventArgs e)
            {
                editorService.CloseDropDown();
            }

            private void TRACKBAR_VALUE_CHANGED(object sender, EventArgs e)
            {
                if (sender is TrackBar)
                {
                    string result = null;
                    TrackBar ctrl = (TrackBar)sender;
                    if (ctrl.Value == 0)
                        result = "Min";
                    else if (ctrl.Value == 255)
                        result = "Max";

                    switch (ctrl.Name)
                    {
                        case "redTrackBar":
                            label1.Text = String.Format("Red: {0}", result ?? ctrl.Value.ToString());
                            break;
                        case "greenTrackBar":
                            label2.Text = String.Format("Green: {0}", result ?? ctrl.Value.ToString());
                            break;
                        case "blueTrackBar":
                            label3.Text = String.Format("Blue: {0}", result ?? ctrl.Value.ToString());
                            break;
                        default:
                            label4.Text = String.Format("Alpha: {0}", result ?? ctrl.Value.ToString());
                            break;
                    }
                }
            }

            #endregion
        }
    }
}