using System;
using System.Windows.Forms;

namespace Oranikle.Studio.Controls.ProgressBar
{
    public interface IFloatWindowBase : IFloatWindowAlphaMembers
    {
        /// <summary>
        /// Changes the text of the specified window's title bar or its taskbar text (if it has one).
        /// </summary>
        /// <param name="controlHandle">Handle to the window or control whose text is to be changed</param>
        /// <param name="text">Pointer to a null-terminated string to be used as the new title or control text</param>
        /// <returns>If the function succeeds, the return value is true; otherwise, false</returns>
        bool SetFloatWindowTaskbarText(IntPtr controlHandle, String text);

        /// <summary>
        /// Gets or sets, the currently dock layout of the Oranikle.Studio.Controls.ProgressBar control.
        /// </summary>
        bool DockUndockProgressBar { get; set; }

        /// <summary>
        /// Gets, the parent container of the Oranikle.Studio.Controls.ProgressBar control.
        /// </summary>
        Control DockParentContainer { get; }
    }

    public interface IFloatWindowAlphaMembers
    {
        /// <summary>
        /// Gets, the handle of the float window, whose window has a alpha support or to be.
        /// </summary>
        IntPtr FloatWindowHandle { get; }

        /// <summary>
        /// Gets, the float window control is marked to layered flag or not.
        /// </summary>
        bool IsLayered { get; }

        /// <summary>
        /// Gets or sets, the target opacity value of the float window.
        /// </summary>
        byte TargetTransparency { get; set; }

        /// <summary>
        /// Gets or sets, the currently opacity value of the float window.
        /// </summary>
        byte CurrentTransparency { get; set; }
    }
}