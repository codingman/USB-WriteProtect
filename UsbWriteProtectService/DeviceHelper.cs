﻿using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace UsbWriteProtectService
{
    [Flags]
    internal enum SetupDiGetClassDevsFlags
    {
        Default = 1,
        Present = 2,
        AllClasses = 4,
        Profile = 8,
        DeviceInterface = 0x10
    }

    internal enum DiFunction
    {
        SelectDevice = 1,
        InstallDevice = 2,
        AssignResources = 3,
        Properties = 4,
        Remove = 5,
        FirstTimeSetup = 6,
        FoundDevice = 7,
        SelectClassDrivers = 8,
        ValidateClassDrivers = 9,
        InstallClassDrivers = 0xa,
        CalcDiskSpace = 0xb,
        DestroyPrivateData = 0xc,
        ValidateDriver = 0xd,
        Detect = 0xf,
        InstallWizard = 0x10,
        DestroyWizardData = 0x11,
        PropertyChange = 0x12,
        EnableClass = 0x13,
        DetectVerify = 0x14,
        InstallDeviceFiles = 0x15,
        UnRemove = 0x16,
        SelectBestCompatDrv = 0x17,
        AllowInstall = 0x18,
        RegisterDevice = 0x19,
        NewDeviceWizardPreSelect = 0x1a,
        NewDeviceWizardSelect = 0x1b,
        NewDeviceWizardPreAnalyze = 0x1c,
        NewDeviceWizardPostAnalyze = 0x1d,
        NewDeviceWizardFinishInstall = 0x1e,
        Unused1 = 0x1f,
        InstallInterfaces = 0x20,
        DetectCancel = 0x21,
        RegisterCoInstallers = 0x22,
        AddPropertyPageAdvanced = 0x23,
        AddPropertyPageBasic = 0x24,
        Reserved1 = 0x25,
        Troubleshooter = 0x26,
        PowerMessageWake = 0x27,
        AddRemotePropertyPageAdvanced = 0x28,
        UpdateDriverUI = 0x29,
        Reserved2 = 0x30
    }

    internal enum StateChangeAction
    {
        Enable = 1,
        Disable = 2,
        PropChange = 3,
        Start = 4,
        Stop = 5
    }

    [Flags]
    internal enum Scopes
    {
        Global = 1,
        ConfigSpecific = 2,
        ConfigGeneral = 4
    }

    internal enum SetupApiError
    {
        NoAssociatedClass = unchecked((int)0xe0000200),
        ClassMismatch = unchecked((int)0xe0000201),
        DuplicateFound = unchecked((int)0xe0000202),
        NoDriverSelected = unchecked((int)0xe0000203),
        KeyDoesNotExist = unchecked((int)0xe0000204),
        InvalidDevinstName = unchecked((int)0xe0000205),
        InvalidClass = unchecked((int)0xe0000206),
        DevinstAlreadyExists = unchecked((int)0xe0000207),
        DevinfoNotRegistered = unchecked((int)0xe0000208),
        InvalidRegProperty = unchecked((int)0xe0000209),
        NoInf = unchecked((int)0xe000020a),
        NoSuchHDevinst = unchecked((int)0xe000020b),
        CantLoadClassIcon = unchecked((int)0xe000020c),
        InvalidClassInstaller = unchecked((int)0xe000020d),
        DiDoDefault = unchecked((int)0xe000020e),
        DiNoFileCopy = unchecked((int)0xe000020f),
        InvalidHwProfile = unchecked((int)0xe0000210),
        NoDeviceSelected = unchecked((int)0xe0000211),
        DevinfolistLocked = unchecked((int)0xe0000212),
        DevinfodataLocked = unchecked((int)0xe0000213),
        DiBadPath = unchecked((int)0xe0000214),
        NoClassInstallParams = unchecked((int)0xe0000215),
        FileQueueLocked = unchecked((int)0xe0000216),
        BadServiceInstallSect = unchecked((int)0xe0000217),
        NoClassDriverList = unchecked((int)0xe0000218),
        NoAssociatedService = unchecked((int)0xe0000219),
        NoDefaultDeviceInterface = unchecked((int)0xe000021a),
        DeviceInterfaceActive = unchecked((int)0xe000021b),
        DeviceInterfaceRemoved = unchecked((int)0xe000021c),
        BadInterfaceInstallSect = unchecked((int)0xe000021d),
        NoSuchInterfaceClass = unchecked((int)0xe000021e),
        InvalidReferenceString = unchecked((int)0xe000021f),
        InvalidMachineName = unchecked((int)0xe0000220),
        RemoteCommFailure = unchecked((int)0xe0000221),
        MachineUnavailable = unchecked((int)0xe0000222),
        NoConfigMgrServices = unchecked((int)0xe0000223),
        InvalidPropPageProvider = unchecked((int)0xe0000224),
        NoSuchDeviceInterface = unchecked((int)0xe0000225),
        DiPostProcessingRequired = unchecked((int)0xe0000226),
        InvalidCOInstaller = unchecked((int)0xe0000227),
        NoCompatDrivers = unchecked((int)0xe0000228),
        NoDeviceIcon = unchecked((int)0xe0000229),
        InvalidInfLogConfig = unchecked((int)0xe000022a),
        DiDontInstall = unchecked((int)0xe000022b),
        InvalidFilterDriver = unchecked((int)0xe000022c),
        NonWindowsNTDriver = unchecked((int)0xe000022d),
        NonWindowsDriver = unchecked((int)0xe000022e),
        NoCatalogForOemInf = unchecked((int)0xe000022f),
        DevInstallQueueNonNative = unchecked((int)0xe0000230),
        NotDisableable = unchecked((int)0xe0000231),
        CantRemoveDevinst = unchecked((int)0xe0000232),
        InvalidTarget = unchecked((int)0xe0000233),
        DriverNonNative = unchecked((int)0xe0000234),
        InWow64 = unchecked((int)0xe0000235),
        SetSystemRestorePoint = unchecked((int)0xe0000236),
        IncorrectlyCopiedInf = unchecked((int)0xe0000237),
        SceDisabled = unchecked((int)0xe0000238),
        UnknownException = unchecked((int)0xe0000239),
        PnpRegistryError = unchecked((int)0xe000023a),
        RemoteRequestUnsupported = unchecked((int)0xe000023b),
        NotAnInstalledOemInf = unchecked((int)0xe000023c),
        InfInUseByDevices = unchecked((int)0xe000023d),
        DiFunctionObsolete = unchecked((int)0xe000023e),
        NoAuthenticodeCatalog = unchecked((int)0xe000023f),
        AuthenticodeDisallowed = unchecked((int)0xe0000240),
        AuthenticodeTrustedPublisher = unchecked((int)0xe0000241),
        AuthenticodeTrustNotEstablished = unchecked((int)0xe0000242),
        AuthenticodePublisherNotTrusted = unchecked((int)0xe0000243),
        SignatureOSAttributeMismatch = unchecked((int)0xe0000244),
        OnlyValidateViaAuthenticode = unchecked((int)0xe0000245)
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DeviceInfoData
    {
        public int Size;
        public Guid ClassGuid;
        public int DevInst;
        public IntPtr Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PropertyChangeParameters
    {
        public int Size;
        // part of header. It's flattened out into 1 structure.
        public DiFunction DiFunction;
        public StateChangeAction StateChange;
        public Scopes Scope;
        public int HwProfile;
    }

    internal class NativeMethods
    {
        private const string setupapi = "setupapi.dll";

        private NativeMethods()
        {
        }

        [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiCallClassInstaller(DiFunction installFunction,
                                                            SafeDeviceInfoSetHandle deviceInfoSet,
                                                            [In] ref DeviceInfoData deviceInfoData);

        [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiEnumDeviceInfo(SafeDeviceInfoSetHandle deviceInfoSet, int memberIndex,
                                                        ref DeviceInfoData deviceInfoData);

        [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode,
            SetLastError = true)]
        public static extern SafeDeviceInfoSetHandle SetupDiGetClassDevs([In] ref Guid classGuid,
                                                                            [MarshalAs(UnmanagedType.LPWStr)] string
                                                                                enumerator, IntPtr hwndParent,
                                                                            SetupDiGetClassDevsFlags flags);

        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiGetDeviceInstanceId(
            IntPtr DeviceInfoSet,
            ref DeviceInfoData did,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder DeviceInstanceId,
            int DeviceInstanceIdSize,
            out int RequiredSize
            );

        [SuppressUnmanagedCodeSecurity]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        [DllImport(setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetupDiSetClassInstallParams(SafeDeviceInfoSetHandle deviceInfoSet,
                                                                [In] ref DeviceInfoData deviceInfoData,
                                                                [In] ref PropertyChangeParameters classInstallParams,
                                                                int classInstallParamsSize);
    }

    internal class SafeDeviceInfoSetHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeDeviceInfoSetHandle()
            : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.SetupDiDestroyDeviceInfoList(handle);
        }
    }

    public sealed class DeviceHelper
    {
        private DeviceHelper()
        {
        }

        /// <summary>
        /// Enable or disable a device.
        /// </summary>
        /// <param name="classGuid">The class guid of the device. Available in the device manager.</param>
        /// <param name="instanceId">The device instance id of the device. Available in the device manager.</param>
        /// <param name="enable">True to enable, False to disable.</param>
        /// <remarks>Will throw an exception if the device is not Disableable.</remarks>
        public static void SetDeviceEnabled(Guid classGuid, string instanceId, bool enable)
        {
            SafeDeviceInfoSetHandle diSetHandle = null;
            try
            {
                // Get the handle to a device information set for all devices matching classGuid that are present on the 
                // system.
                diSetHandle = NativeMethods.SetupDiGetClassDevs(ref classGuid, null, IntPtr.Zero,
                                                                SetupDiGetClassDevsFlags.Present);
                // Get the device information data for each matching device.
                var diData = GetDeviceInfoData(diSetHandle);
                // Find the index of our instance. i.e. the touchpad mouse - I have 3 mice attached...
                var index = GetIndexOfInstance(diSetHandle, diData, instanceId);
                // Disable...
                EnableDevice(diSetHandle, diData[index], enable);
            }
            finally
            {
                if (diSetHandle != null)
                {
                    if (diSetHandle.IsClosed == false)
                    {
                        diSetHandle.Close();
                    }
                    diSetHandle.Dispose();
                }
            }
        }

        private static DeviceInfoData[] GetDeviceInfoData(SafeDeviceInfoSetHandle handle)
        {
            var data = new List<DeviceInfoData>();
            var did = new DeviceInfoData();
            var didSize = Marshal.SizeOf(did);
            did.Size = didSize;
            var index = 0;
            while (NativeMethods.SetupDiEnumDeviceInfo(handle, index, ref did))
            {
                data.Add(did);
                index += 1;
                did = new DeviceInfoData();
                did.Size = didSize;
            }
            return data.ToArray();
        }

        // Find the index of the particular DeviceInfoData for the instanceId.
        private static int GetIndexOfInstance(SafeDeviceInfoSetHandle handle, DeviceInfoData[] diData, string instanceId)
        {
            const int ERROR_INSUFFICIENT_BUFFER = 122;
            for (var index = 0; index <= diData.Length - 1; index++)
            {
                var sb = new StringBuilder(1);
                var requiredSize = 0;
                var result = NativeMethods.SetupDiGetDeviceInstanceId(handle.DangerousGetHandle(), ref diData[index],
                                                                        sb, sb.Capacity, out requiredSize);
                if (result == false && Marshal.GetLastWin32Error() == ERROR_INSUFFICIENT_BUFFER)
                {
                    sb.Capacity = requiredSize;
                    result = NativeMethods.SetupDiGetDeviceInstanceId(handle.DangerousGetHandle(), ref diData[index], sb,
                                                                        sb.Capacity, out requiredSize);
                }
                if (result == false)
                    throw new Win32Exception();
                if (instanceId.Equals(sb.ToString()))
                {
                    return index;
                }
            }
            // not found
            return -1;
        }

        // enable/disable...
        private static void EnableDevice(SafeDeviceInfoSetHandle handle, DeviceInfoData diData, bool enable)
        {
            var @params = new PropertyChangeParameters();
            // The size is just the size of the header, but we've flattened the structure.
            // The header comprises the first two fields, both integer.
            @params.Size = 8;
            @params.DiFunction = DiFunction.PropertyChange;
            @params.Scope = Scopes.Global;
            @params.StateChange = enable ? StateChangeAction.Enable : StateChangeAction.Disable;

            var result = NativeMethods.SetupDiSetClassInstallParams(handle, ref diData, ref @params,
                                                                        Marshal.SizeOf(@params));
            if (result == false) throw new Win32Exception();
            result = NativeMethods.SetupDiCallClassInstaller(DiFunction.PropertyChange, handle, ref diData);
            if (result == false)
            {
                var err = Marshal.GetLastWin32Error();
                if (err == (int)SetupApiError.NotDisableable)
                    throw new ArgumentException("Device can't be disabled (programmatically or in Device Manager).");
                else if (err >= (int)SetupApiError.NoAssociatedClass &&
                    err <= (int)SetupApiError.OnlyValidateViaAuthenticode)
                    throw new Win32Exception("SetupAPI error: " + ((SetupApiError)err).ToString());
                else
                    throw new Win32Exception();
            }
        }
    }
}
