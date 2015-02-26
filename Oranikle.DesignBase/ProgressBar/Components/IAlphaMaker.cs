using System;

namespace Oranikle.Studio.Controls.ProgressBar.Components
{
    public interface IAlphaMaker
    {
        /// <summary>
        /// How many steps to take effect for destination opacity, the StepCount property must be in the range of 5 to 15.
        /// </summary>
        int StepCount { get; set; }

        /// <summary>
        /// Gets or sets, the fade interval, the StepInterval property must be in the range of 10 to 300.
        /// </summary>
        int StepInterval { get; set; }

        /// <summary>
        /// Sets the current float window to layered window or clear its flag.
        /// </summary>
        /// <param name="isLayered">Currently float window layered flag</param>
        void SetLayered(ref bool isLayered);

        /// <summary>
        /// Starts a new fading operation for specified opacity.
        /// </summary>
        /// <param name="opacity">Alpha value</param>
        void SeekToOpacity(byte opacity);

        /// <summary>
        /// Updates the float window's opacity.
        /// </summary>
        /// <param name="opacity">Alpha value</param>
        /// <param name="isItRefreshed">If parameter is true, the currently float window will be refreshed; otherwise, does not perform an redraw operation.</param>
        void UpdateOpacity(byte opacity, bool isItRefreshed);

        /// <summary>
        /// Gets or sets, the active float window's IFloatWindowAlphaMembers interface.
        /// </summary>
        IFloatWindowAlphaMembers IFloatWindowControl { get; set; }

        /// <summary>
        /// Occurs when the fading operation is successfully completed.
        /// </summary>
        event EventHandler FadingOperationCompleted;
    }
}