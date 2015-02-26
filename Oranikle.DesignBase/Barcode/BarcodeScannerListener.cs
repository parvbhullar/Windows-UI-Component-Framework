// <copyright file="BarcodeScannerListener.cs" > 
//  
// </copyright>

namespace Oranikle.Studio.Controls.Barcode
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// This class uses Windows's native Raw Input API to listen for input from
    /// a certain set of barcode scanners and devices. This way, the application
    /// can receive input from a barcode scanner without the user having to
    /// worry about whether or not a certain text field has focus, which was a
    /// big problem
    /// </summary>
    public class BarcodeScannerListener : NativeWindow
    {
        /// <summary>
        /// A mapping of device handles to information about the barcode scanner
        /// devices.
        /// </summary>
        private Dictionary<IntPtr, BarcodeScannerDeviceInfo> devices;

        /// <summary>
        /// The barcode currently being read.
        /// </summary>
        private StringBuilder keystrokeBuffer;

        /// <summary>
        /// Initializes a new instance of the BarcodeScannerListener
        /// class. The raw input devices that this class will listen to are
        /// registered with the given window handle.
        /// </summary>
        /// <param name="form">the form that should listen for
        /// barcode scans</param>
        /// <exception cref="ArgumentNullException">if the form is null</exception>
        /// <exception cref="ApplicationException">if we are unable to register
        /// for raw input devices</exception>
        /// <exception cref="ConfigurationErrorsException">if an error occurs
        /// during configuration</exception>
        public BarcodeScannerListener(IntPtr hwndform)
        {
            IntPtr hwnd;

            //if (form == null)
            {
            //    throw new ArgumentNullException("form");
            }

            hwnd = hwndform;

            this.devices = new Dictionary<IntPtr, BarcodeScannerDeviceInfo>();
            this.keystrokeBuffer = new StringBuilder();

            this.InitializeBarcodeScannerDeviceHandles();
            HookRawInput(hwnd);
            //this.HookHandleEvents(form);

            this.AssignHandle(hwnd);
        }

        /// <summary>
        /// Event fired when a barcode is scanned.
        /// </summary>
        public event EventHandler BarcodeScanned;

        /// <summary>
        /// Hook into the form's WndProc message. We listen for WM_INPUT and do
        /// special processing on the raw data.
        /// </summary>
        /// <param name="m">the message</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_INPUT:
                    if (this.ProcessRawInputMessage(m.LParam))
                    {
                        NativeMethods.MSG message;
                        NativeMethods.PeekMessage(
                            out message,
                            IntPtr.Zero,
                            NativeMethods.WM_KEYDOWN,
                            NativeMethods.WM_KEYDOWN,
                            NativeMethods.PeekMessageRemoveFlag.PM_REMOVE);
                    }

                    break;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Converts a native raw input type into our version.
        /// </summary>
        /// <param name="rawInputType">the raw in put type</param>
        /// <returns>our version of the type</returns>
        private static BarcodeScannerDeviceType GetBarcodeScannerDeviceType(NativeMethods.RawInputType rawInputType)
        {
            BarcodeScannerDeviceType type;

            switch (rawInputType)
            {
                case NativeMethods.RawInputType.RIM_TYPEHID:
                    type = BarcodeScannerDeviceType.HumanInterfaceDevice;
                    break;
                case NativeMethods.RawInputType.RIM_TYPEKEYBOARD:
                    type = BarcodeScannerDeviceType.Keyboard;
                    break;
                default:
                    type = BarcodeScannerDeviceType.Unknown;
                    break;
            }

            return type;
        }

        /// <summary>
        /// Gets the friendly name or description for a device based on its dbccName.
        /// </summary>
        /// <param name="dbccName">the dbccName of the device</param>
        /// <returns>the friendly name or the description of the device</returns>
        private static string GetDeviceFriendlyName(string dbccName)
        {
            string[] dbccParts;
            string deviceInstanceId;
            string friendlyName;
            NativeMethods.SetupDiGetClassDevsFlags getClassDevsFlags;
            IntPtr devInfo;
            Guid classGuid;

            classGuid = new Guid(NativeMethods.GUID_DEVINTERFACE_HID.ToByteArray());
            friendlyName = string.Empty;

            dbccParts = dbccName.Split('#');
            deviceInstanceId = (dbccParts[0].Substring(dbccParts[0].LastIndexOf(@"\", StringComparison.Ordinal) + 1) +
                @"\" +
                dbccParts[1] +
                @"\" +
                dbccParts[2]).ToUpperInvariant();
            getClassDevsFlags = 
                NativeMethods.SetupDiGetClassDevsFlags.DIGCF_PRESENT | 
                NativeMethods.SetupDiGetClassDevsFlags.DIGCF_DEVICEINTERFACE;

           devInfo = NativeMethods.SetupDiGetClassDevs(
                ref classGuid,
                null,
                IntPtr.Zero,
                getClassDevsFlags);
            if (!devInfo.Equals(NativeMethods.INVALID_HANDLE_VALUE))
            {
                NativeMethods.SP_DEVINFO_DATA devInfoData;

                devInfoData = new NativeMethods.SP_DEVINFO_DATA();
                devInfoData.cbSize = (uint)Marshal.SizeOf(devInfoData);

                for (uint i = 0; NativeMethods.SetupDiEnumDeviceInfo(devInfo, i, ref devInfoData); ++i)
                {
                    IntPtr buffer;
                    int bufferSize;
                    string currentDeviceInstanceId;
                    int requiredSize;

                    bufferSize = 1024;
                    buffer = Marshal.AllocHGlobal(bufferSize);
                    requiredSize = 0;

                    if (NativeMethods.SetupDiGetDeviceInstanceId(
                        devInfo,
                        ref devInfoData,
                        buffer,
                        bufferSize,
                        out requiredSize))
                    {
                        currentDeviceInstanceId = Marshal.PtrToStringAnsi(buffer);

                        if (currentDeviceInstanceId == deviceInstanceId)
                        {
                            IntPtr propertyBuffer;
                            uint regType;
                            int propertyBufferSize;
                            uint propertyBufferSizeRequiredSize;

                            propertyBufferSize = 1024;
                            propertyBuffer = Marshal.AllocHGlobal(propertyBufferSize);

                            // We found our device!
                            if (NativeMethods.SetupDiGetDeviceRegistryProperty(
                                devInfo,
                                ref devInfoData,
                                NativeMethods.SetupDiGetDeviceRegistryPropertyType.SPDRP_FRIENDLYNAME,
                                out regType,
                                propertyBuffer,
                                (uint)propertyBufferSize,
                                out propertyBufferSizeRequiredSize) ||
                                NativeMethods.SetupDiGetDeviceRegistryProperty(
                                devInfo,
                                ref devInfoData,
                                NativeMethods.SetupDiGetDeviceRegistryPropertyType.SPDRP_DEVICEDESC,
                                out regType,
                                propertyBuffer,
                                (uint)propertyBufferSize,
                                out propertyBufferSizeRequiredSize))
                            {
                                friendlyName = Marshal.PtrToStringAnsi(propertyBuffer);
                            }

                            Marshal.FreeHGlobal(propertyBuffer);
                        }
                    }

                    Marshal.FreeHGlobal(buffer);
                }

                NativeMethods.SetupDiDestroyDeviceInfoList(devInfo);
            }

            return friendlyName;
        }

        /// <summary>
        /// Registers ourselves to listen to raw input from keyboard-like devices.
        /// </summary>
        /// <param name="hwnd">the handle of the form that will receive the raw
        /// input messages</param>
        /// <exception cref="InvalidOperationException">if the call to register with the
        /// raw input API fails for some reason</exception>
        private static void HookRawInput(IntPtr hwnd)
        {
            NativeMethods.RAWINPUTDEVICE[] rid;

            rid = new NativeMethods.RAWINPUTDEVICE[1];

            rid[0].usUsagePage = 0x01;      // USB HID Generid Desktop Page
            rid[0].usUsage = 0x06;          // Keyboard Usage ID
            rid[0].dwFlags = NativeMethods.RawInputDeviceFlags.RIDEV_INPUTSINK;
            rid[0].hwndTarget = hwnd;

            if (!NativeMethods.RegisterRawInputDevices(rid, (uint)rid.Length, (uint)Marshal.SizeOf(rid[0])))
            {
                InvalidOperationException e;

                e = new InvalidOperationException(
                    "The barcode scanner listener could not register for raw input devices.",
                    new Win32Exception());
                throw e;
            }
        }

        /// <summary>
        /// Fires the barcode scanned event.
        /// </summary>
        /// <param name="deviceInfo">information about the device that generated
        /// the barcode</param>
        private void FireBarcodeScanned(BarcodeScannerDeviceInfo deviceInfo)
        {
            string barcode;
            EventHandler handler;

            barcode = this.keystrokeBuffer.ToString();
            handler = this.BarcodeScanned;

            this.keystrokeBuffer = new StringBuilder();

            if (handler != null)
            {
                handler(this, new BarcodeScannedEventArgs(barcode, deviceInfo));
            }
        }

        /// <summary>
        /// Enumerates devices provided by GetRawInputDeviceList. We'll only listen
        /// to these devices.
        /// </summary>
        /// <exception cref="ConfigurationErrorsException">if an error occurs
        /// during configuration</exception>
        private void InitializeBarcodeScannerDeviceHandles()
        {
            BarcodeScannerListenerConfigurationSection config;
            BarcodeScannerListenerConfigurationElementCollection hardwareIdsConfig;
            List<string> hardwareIds;
            uint numDevices;
            uint size;

            config = BarcodeScannerListenerConfigurationSection.GetConfiguration();
            hardwareIdsConfig = config.HardwareIds;
            hardwareIds = new List<string>();
            numDevices = 0;
            size = (uint)Marshal.SizeOf(typeof(NativeMethods.RAWINPUTDEVICELIST));

            foreach (BarcodeScannerListenerConfigurationElement hardwareId in hardwareIdsConfig)
            {
                hardwareIds.Add(hardwareId.Id);
            }

            // First, we get the number of raw input devices in the list by passing
            // in IntPtr.Zero. Then we allocate sufficient memory and retrieve the
            // entire list.
            if (NativeMethods.GetRawInputDeviceList(IntPtr.Zero, ref numDevices, size) == 0)
            {
                IntPtr rawInputDeviceList;

                rawInputDeviceList = Marshal.AllocHGlobal((int)(size * numDevices));
                if (NativeMethods.GetRawInputDeviceList(
                    rawInputDeviceList,
                    ref numDevices,
                    size) != uint.MaxValue)
                {
                    // Next, we iterate through the list, discarding undesired items
                    // and retrieving further information on the barcode scanner devices
                    for (int i = 0; i < numDevices; ++i)
                    {
                        uint pcbSize;
                        NativeMethods.RAWINPUTDEVICELIST rid;

                        pcbSize = 0;
                        rid = (NativeMethods.RAWINPUTDEVICELIST)Marshal.PtrToStructure(
                            new IntPtr((rawInputDeviceList.ToInt32() + (size * i))),
                            typeof(NativeMethods.RAWINPUTDEVICELIST));

                        if (NativeMethods.GetRawInputDeviceInfo(
                            rid.hDevice,
                            NativeMethods.RawInputDeviceInfoCommand.RIDI_DEVICENAME,
                            IntPtr.Zero,
                            ref pcbSize) >= 0)
                        {
                            if (pcbSize > 0)
                            {
                                string deviceName;
                                string friendlyName;
                                BarcodeScannerDeviceInfo info;
                                IntPtr data;

                                data = Marshal.AllocHGlobal((int)pcbSize);
                                if (NativeMethods.GetRawInputDeviceInfo(
                                    rid.hDevice,
                                    NativeMethods.RawInputDeviceInfoCommand.RIDI_DEVICENAME,
                                    data,
                                    ref pcbSize) >= 0)
                                {
                                    deviceName = (string)Marshal.PtrToStringAnsi(data);

                                    if ((from hardwareId in hardwareIds
                                         where deviceName.Contains(hardwareId)
                                         select hardwareId).Count() > 0)
                                    {
                                        friendlyName = GetDeviceFriendlyName(deviceName);

                                        info = new BarcodeScannerDeviceInfo(
                                            deviceName,
                                            GetBarcodeScannerDeviceType(rid.dwType),
                                            rid.hDevice,
                                            friendlyName);

                                        this.devices.Add(rid.hDevice, info);
                                    }
                                }

                                Marshal.FreeHGlobal(data);
                            }
                        }
                    }
                }

                Marshal.FreeHGlobal(rawInputDeviceList);
            }
        }

        /// <summary>
        /// Hooks into the form's HandleCreated and HandleDestoryed events
        /// to ensure that we start and stop listening at appropriate times.
        /// </summary>
        /// <param name="form">the form to listen to</param>
        private void HookHandleEvents(Form form)
        {
            form.HandleCreated += this.OnHandleCreated;
            form.HandleDestroyed += this.OnHandleDestroyed;
        }

        /// <summary>
        /// When the form's handle is created, let's hook into it so we can see
        /// the WM_INPUT event.
        /// </summary>
        /// <param name="sender">the form whose handle was created</param>
        /// <param name="e">the event arguments</param>
        private void OnHandleCreated(object sender, EventArgs e)
        {
            this.AssignHandle(((Form)sender).Handle);
        }

        /// <summary>
        /// When the form's handle is destroyed, let's unhook from it so we stop
        /// listening and allow the OS to free up its resources.
        /// </summary>
        /// <param name="sender">the form whose handle was destroyed</param>
        /// <param name="e">the event arguments</param>
        private void OnHandleDestroyed(object sender, EventArgs e)
        {
            this.ReleaseHandle();
        }

        /// <summary>
        /// Process the given WM_INPUT message.
        /// </summary>
        /// <param name="rawInputHeader">the rawInputHeader of the message</param>
        /// <returns>whether or not the keystroke was handled</returns>
        private bool ProcessRawInputMessage(IntPtr rawInputHeader)
        {
            bool handled;
            uint size;

            handled = false;
            size = 0;

            // First we call GetRawInputData() to set the value of size, which
            // we will the nuse to allocate the appropriate amount of memory in
            // the buffer.
            if (NativeMethods.GetRawInputData(
                    rawInputHeader,
                    NativeMethods.RawInputCommandFlag.RID_INPUT,
                    IntPtr.Zero,
                    ref size,
                    (uint)Marshal.SizeOf(typeof(NativeMethods.RAWINPUTHEADER))) == 0)
            {
                IntPtr buffer;
                BarcodeScannerDeviceInfo deviceInfo;
                NativeMethods.RAWINPUT raw;

                buffer = Marshal.AllocHGlobal((int)size);
                
                try
                {
                    if (NativeMethods.GetRawInputData(
                            rawInputHeader,
                            NativeMethods.RawInputCommandFlag.RID_INPUT,
                            buffer,
                            ref size,
                            (uint)Marshal.SizeOf(typeof(NativeMethods.RAWINPUTHEADER))) == size)
                    {
                        raw = (NativeMethods.RAWINPUT)Marshal.PtrToStructure(buffer, typeof(NativeMethods.RAWINPUT));

                        if (this.devices.TryGetValue(raw.header.hDevice, out deviceInfo))
                        {
                            handled = true;

                            if (raw.header.dwType == NativeMethods.RawInputType.RIM_TYPEKEYBOARD)
                            {
                                if (raw.keyboard.Message == NativeMethods.WM_KEYDOWN)
                                {
                                    StringBuilder localBuffer;
                                    byte[] state;

                                    localBuffer = new StringBuilder();
                                    state = new byte[256];

                                    if (NativeMethods.GetKeyboardState(state))
                                    {
                                        if (NativeMethods.ToUnicode(
                                                raw.keyboard.VKey,
                                                raw.keyboard.MakeCode,
                                                state,
                                                localBuffer,
                                                64,
                                                0) > 0)
                                        {
                                            if (localBuffer.Length == 1 && localBuffer[0] == 0x4)
                                            {
                                                this.FireBarcodeScanned(deviceInfo);
                                            }
                                            else
                                            {
                                                this.keystrokeBuffer.Append(localBuffer.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            }

            return handled;
        }
    }
}
