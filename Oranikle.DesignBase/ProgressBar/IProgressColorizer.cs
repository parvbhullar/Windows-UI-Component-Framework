using System;

namespace Oranikle.Studio.Controls.ProgressBar
{
    public interface IProgressColorizer : IDisposable
    {
        /// <summary>
        /// Determines whether the colorizer effect is enable or not for progress bitmap.
        /// </summary>
        bool IsColorizerEnabled { get; set; }

        /// <summary>
        /// Determines whether the transparency effect is visible or not for progress bitmap.
        /// </summary>
        bool IsTransparencyEnabled { get; set; }

        /// <summary>
        /// Gets or Sets, the red color component value of the progress bitmap.
        /// </summary>
        byte Red { get; set; }

        /// <summary>
        /// Gets or Sets, the green color component value of the progress bitmap.
        /// </summary>
        byte Green { get; set; }

        /// <summary>
        /// Gets or Sets, the blue color component value of the progress bitmap.
        /// </summary>
        byte Blue { get; set; }

        /// <summary>
        /// Gets or Sets, the alpha color component value of the progress bitmap.
        /// </summary>
        byte Alpha { get; set; }
    }
}
