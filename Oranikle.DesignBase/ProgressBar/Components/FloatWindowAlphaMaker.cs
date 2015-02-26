using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using Oranikle.Studio.Controls.ProgressBar.Win32;

namespace Oranikle.Studio.Controls.ProgressBar.Components
{
    public class FloatWindowAlphaMaker : Component, IAlphaMaker
    {
        #region Instance Members

        private int stepCount = 8;
        private int stepInterval = 20;
        private int initialValue = 0;
        private int incrementValue = 0;
        private IFloatWindowAlphaMembers iFloatWindowControl = null;
        private bool direction = false;
        private Timer controlTimer;

        #endregion
                
        #region Constructor

        public FloatWindowAlphaMaker()
        {
            controlTimer = new Timer();
            controlTimer.Interval = stepInterval;
            controlTimer.Tick += new EventHandler(controlTimer_Tick);
        }

        public FloatWindowAlphaMaker(IContainer container)
            : this()
        {
            container.Add(this);
        }

        #endregion
                
        #region Destructor

        ~FloatWindowAlphaMaker()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Virtual Methods

        protected virtual void OnFadingOperationCompleted(EventArgs e)
        {
            if (FadingOperationCompleted != null)
                FadingOperationCompleted(this, e);
        }

        #endregion

        #region Override Methods

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                controlTimer.Tick -= controlTimer_Tick;
                controlTimer.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Helper Methods

        private void RefreshWindow()
        {
            User32.RedrawWindow(iFloatWindowControl.FloatWindowHandle, IntPtr.Zero, IntPtr.Zero,
                (uint)(User32.RedrawWindowFlags.Frame | User32.RedrawWindowFlags.AllChildren | User32.RedrawWindowFlags.Invalidate | User32.RedrawWindowFlags.UpdateNow));
        }

        private void controlTimer_Tick(object sender, EventArgs e)
        {
            if (direction ? ((initialValue + incrementValue) < (int)iFloatWindowControl.TargetTransparency) : ((initialValue + incrementValue) > (int)iFloatWindowControl.TargetTransparency))
                initialValue += incrementValue;
            else
                initialValue = (int)iFloatWindowControl.TargetTransparency;

            UpdateOpacity((byte)initialValue, true);

            if (iFloatWindowControl.CurrentTransparency == iFloatWindowControl.TargetTransparency)
            {
                // If operation is completed, stops the timer.
                controlTimer.Stop();

                // Trigger a notification event.
                OnFadingOperationCompleted(EventArgs.Empty);
            }
        }

        #endregion

        #region IAlphaMaker Members

        /// <summary>
        /// How many steps to take effect for destination opacity, the StepCount property must be in the range of 5 to 15.
        /// </summary>
        [Description("How many steps to take effect for destination opacity, the StepCount property must be in the range of 5 to 15")]
        [DefaultValue(8)]
        [Browsable(true)]
        public int StepCount
        {
            get
            {
                return stepCount;
            }
            set
            {
                if (!value.Equals(stepCount))
                {
                    if (value < 5)
                        value = 5;
                    else if (value > 15)
                        value = 15;

                    stepCount = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets, the fade interval, the StepInterval property must be in the range of 10 to 300.
        /// </summary>
        [Description("Gets or sets, the fade interval, the StepInterval property must be in the range of 10 to 300")]
        [DefaultValue(20)]
        [Browsable(true)]
        public int StepInterval
        {
            get
            {
                return stepInterval;
            }
            set
            {
                if (!value.Equals(stepInterval))
                {
                    if (value < 10)
                        value = 10;
                    else if (value > 300)
                        value = 300;

                    stepInterval = value;
                    controlTimer.Interval = stepInterval;
                }
            }
        }

        /// <summary>
        /// Sets the current float window to layered window or clear its flag.
        /// </summary>
        /// <param name="isLayered">Currently float window layered flag</param>
        public void SetLayered(ref bool isLayered)
        {
            if (iFloatWindowControl == null)
                return;

            if (!isLayered)
            {
                User32.SetWindowLong(iFloatWindowControl.FloatWindowHandle, (int)User32.WindowExStyles.GWL_EXSTYLE, User32.GetWindowLong(iFloatWindowControl.FloatWindowHandle, (int)User32.WindowExStyles.GWL_EXSTYLE) ^ ((int)User32.WindowExStyles.WS_EX_LAYERED));
                User32.SetLayeredWindowAttributes(iFloatWindowControl.FloatWindowHandle, 0, iFloatWindowControl.TargetTransparency, User32._LWA_ALPHA);
                iFloatWindowControl.CurrentTransparency = iFloatWindowControl.TargetTransparency;
                isLayered = true;
            }
            else
            {
                User32.SetWindowLong(iFloatWindowControl.FloatWindowHandle, (int)User32.WindowExStyles.GWL_EXSTYLE, User32.GetWindowLong(iFloatWindowControl.FloatWindowHandle, (int)User32.WindowExStyles.GWL_EXSTYLE) ^ ((int)User32.WindowExStyles.WS_EX_LAYERED));
                iFloatWindowControl.CurrentTransparency = 255;
                isLayered = false;
            }
        }

        /// <summary>
        /// Starts a new fading operation for specified opacity.
        /// </summary>
        /// <param name="opacity">Alpha value</param>
        public void SeekToOpacity(byte opacity)
        {
            if (iFloatWindowControl == null || (iFloatWindowControl.CurrentTransparency == opacity))
                return;

            if (iFloatWindowControl.IsLayered)
            {
                // Keeps new target opacity value.
                iFloatWindowControl.TargetTransparency = opacity;

                if (iFloatWindowControl.CurrentTransparency < opacity)
                    direction = true;
                else
                    direction = false;

                initialValue = iFloatWindowControl.CurrentTransparency;
                incrementValue = (opacity - initialValue) / stepCount;
                controlTimer.Interval = stepInterval;
                controlTimer.Start();
            }
        }

        /// <summary>
        /// Updates the float window's opacity.
        /// </summary>
        /// <param name="opacity">Alpha value</param>
        /// <param name="isItRefreshed">If parameter is true, the currently float window will be refreshed; otherwise, does not perform an redraw operation.</param>
        public void UpdateOpacity(byte opacity, bool isItRefreshed)
        {
            if (iFloatWindowControl == null || (iFloatWindowControl.CurrentTransparency == opacity))
                return;

            if (iFloatWindowControl.IsLayered)
            {
                // Sets new attributes for float window.
                User32.SetLayeredWindowAttributes(iFloatWindowControl.FloatWindowHandle, 0, opacity, User32._LWA_ALPHA);
                // Sets the currently opacity value of the float window.
                iFloatWindowControl.CurrentTransparency = opacity;

                if (isItRefreshed)
                    RefreshWindow();
            }
        }

        /// <summary>
        /// Gets or sets, the active float window's IFloatWindowAlphaMembers interface.
        /// </summary>
        [Browsable(false)]
        public IFloatWindowAlphaMembers IFloatWindowControl
        {
            get
            {
                return iFloatWindowControl;
            }
            set
            {
                try
                {
                    if (value is ProgressBar)
                    {
                        if (((ProgressBar)value).DockUndockProgressBar == false)
                        {
                            if (!value.Equals(iFloatWindowControl))
                            {
                                if (iFloatWindowControl != null && controlTimer.Enabled)
                                {
                                    // Stops the timer and updates window for destination opacity.
                                    controlTimer.Stop();
                                    UpdateOpacity(iFloatWindowControl.TargetTransparency, true);
                                }

                                iFloatWindowControl = value;
                            }
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    if (iFloatWindowControl != null && controlTimer.Enabled)
                    {
                        // Stops the timer and updates window for destination opacity.
                        controlTimer.Stop();
                        UpdateOpacity(iFloatWindowControl.TargetTransparency, true);
                    }

                    iFloatWindowControl = null;
                }
            }
        }

        private bool ShouldSerializeIFloatWindowControl()
        {
            return false;
        }

        /// <summary>
        /// Occurs when the fading operation is successfully completed.
        /// </summary>
        [Description("Occurs when the fading operation is successfully completed")]
        public event EventHandler FadingOperationCompleted;

        #endregion
    }
}