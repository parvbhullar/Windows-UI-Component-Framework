using System;
using System.Drawing;

namespace Oranikle.Studio.Controls.ProgressBar
{
    public interface IProgressBar
    {
        /// <summary>
        /// Gets or Sets, the current progress value of the control.
        /// </summary>
        int Value { get; set; }

        /// <summary>
        /// Gets or Sets, the minimum progress value of the control.
        /// </summary>
        int Minimum { get; set; }

        /// <summary>
        /// Gets or Sets, the maximum progress value of the control.
        /// </summary>
        int Maximum { get; set; }

        /// <summary>
        /// Determines whether the control's border is draw or not.
        /// </summary>
        bool IsPaintBorder { get; set; }

        /// <summary>
        /// Determines whether the digital number drawing is enabled or not.
        /// </summary>
        bool IsDigitDrawEnabled { get; set; }

        /// <summary>
        /// Determines whether the percentage text is show or hide.
        /// </summary>
        bool ShowPercentage { get; set; }

        /// <summary>
        /// Display text formatting for progressbar value.
        /// </summary>
        string DisplayFormat { get; set; }

        /// <summary>
        /// Gets or Sets, the control's border color from here.
        /// </summary>
        Color BorderColor { get; set; }
        
        /// <summary>
        /// Gets or Sets, the current border style of the ProgressBar control.
        /// </summary>
        ProgressBarBorderStyle ControlBorderStyle { get; set; }

        /// <summary>
        /// Occurs when the progress value changed of the control.
        /// </summary>
        event EventHandler ValueChanged;
    }
}