using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls
{
    [System.Drawing.ToolboxBitmap(typeof(Oranikle.Studio.Controls.GradientPanel), "GradientPanel")]
    [System.ComponentModel.ToolboxItem(true)]
    [System.ComponentModel.Description("Enables you to group control in a panel with a gradient background.")]
    public class GradientPanel : System.Windows.Forms.Panel
    {

        private System.Drawing.Color _endColor;
        private System.Drawing.Drawing2D.LinearGradientMode _gradientMode;
        private System.Drawing.Color _startColor;

        [System.ComponentModel.Description("The right/lower color for the gradient panel.")]
        [System.ComponentModel.Category("Appearance")]
        public System.Drawing.Color EndColor
        {
            get
            {
                return _endColor;
            }
            set
            {
                _endColor = value;
                Refresh();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("The direction of the linear gradient.")]
        public System.Drawing.Drawing2D.LinearGradientMode GradientMode
        {
            get
            {
                return _gradientMode;
            }
            set
            {
                _gradientMode = value;
                Refresh();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        [System.ComponentModel.Description("The left/upper color for the gradient panel.")]
        public System.Drawing.Color StartColor
        {
            get
            {
                return _startColor;
            }
            set
            {
                _startColor = value;
                Refresh();
            }
        }

        public GradientPanel()
        {
            //if (!DesignMode)
            LP.Validate();
            _startColor = System.Drawing.Color.Black;
            _endColor = System.Drawing.Color.White;
            _gradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            System.Drawing.Drawing2D.LinearGradientBrush linearGradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(pevent.ClipRectangle, _startColor, _endColor, _gradientMode);
            pevent.Graphics.FillRectangle(linearGradientBrush, pevent.ClipRectangle);
            linearGradientBrush.Dispose();
        }

    } 
}
