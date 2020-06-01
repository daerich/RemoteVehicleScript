/* ------------------------------------------
			COPYRIGHT © DAERICH 2020
ALL RIGHTS RESERVED EXCEPT OTHERWISE STATED IN COPYRIGHT.TXT
   ------------------------------------------ */
using System;
using System.Runtime.InteropServices;
using DaErich.Core.External;
using GTA;
using GTA.UI;

namespace RMVL_Scripthookv.Dashboard
{
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    public struct VehicleDashboardData
    {
        public float RPM; // 0x00
        public float speed; // 0x04
        public float fuel; // 0x08
        public float temp; // 0x0C
        public float vacuum; // 0x10
        public float boost; // 0x14
        public float waterTemp; // 0x18
        public float oilTemperature; // 0x1C
        public float oilPressure; // 0x20
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x3F)]
        public byte[] _pad; // 0x24 aircraft data
        [MarshalAs(UnmanagedType.I1)]
        public bool indicator_left; // 0x63
        [MarshalAs(UnmanagedType.I1)]
        public bool indicator_right; // 0x64
        [MarshalAs(UnmanagedType.I1)]
        public bool handbrakeLight; // 0x65
        [MarshalAs(UnmanagedType.I1)]
        public bool engineLight; // 0x66
        [MarshalAs(UnmanagedType.I1)]
        public bool ABSLight; // 0x67
        [MarshalAs(UnmanagedType.I1)]
        public bool petrolLight; // 0x68
        [MarshalAs(UnmanagedType.I1)]
        public bool oilLight; // 0x69
        [MarshalAs(UnmanagedType.I1)]
        public bool headlights; // 0x6A
        [MarshalAs(UnmanagedType.I1)]
        public bool fullBeam; // 0x6B
        [MarshalAs(UnmanagedType.I1)]
        public bool batteryLight; // 0x6C
    }

    internal static class Dll
    {
        [DllImport("kernel32")]
        internal static extern IntPtr GetModuleHandle(string lpFileName);
    }
    internal static class DashHookDll
    {
        [DllImport("DashHook.dll")]
        public static extern void DashHook_GetData(out VehicleDashboardData data);

        [DllImport("DashHook.dll")]
        public static extern void DashHook_SetData(VehicleDashboardData data);
    }
    internal class DashboardWarning : Script
    {
        private readonly bool isCompatible;
        private IniFile dashIni = new IniFile("scripts/VehicleLocker.ini");
        private static Vehicle PlayerVeh()
        {
           
            Vehicle[] AllVehicles = World.GetAllVehicles();
            foreach(Vehicle vehicle in AllVehicles)
            {
                if(vehicle.Driver == Game.Player.Character){

                    return vehicle;
                }
            }

            return null;
        }
   
        public DashboardWarning()
        {
            Tick += OnTick;

            IntPtr DashLib = Dll.GetModuleHandle(@"DashHook.dll");
            if (DashLib == IntPtr.Zero)
            {
                isCompatible = false;

                int Note = Notification.Show("DashHook not found!");
                Wait(1000);
                Notification.Hide(Note);
            }

            else
            {
                isCompatible = true;
            }

        }

        private void OnTick(object sender, EventArgs e)
        {
            Crash += OnCrash;

            if (isCompatible && Game.Player.Character.IsInVehicle())
            {
                CheckStatus();
            }

        }

        private void OnCrash(object sender, CEventArgs e)
        {
            if (e.isRepaired)
            {
                DashControl(dashIni);
            }

            if (!e.isRepaired)
            {
                DashControl(dashIni,true);
            }
        }

        private void CheckStatus()
        {
            CEventArgs e = new CEventArgs();
            Vehicle myVehicle = PlayerVeh();
            if (myVehicle != null)
            {
                if (myVehicle.BodyHealth <= 950f)
                {
                    e.isRepaired = false;
                    Crash(this, e);
                }
                else
                {
                    e.isRepaired = true;
                    Crash(this, e);
                }
            }
        }

        private static void DashControl(IniFile file, bool on = false)
        {
            VehicleDashboardData data = new VehicleDashboardData();
            DashHookDll.DashHook_GetData(out data);
            data.batteryLight = on;
            data.engineLight = on;
            data.oilLight = on;
            if (bool.Parse(file.Read("enableHandBrakeLight", "DashBoard")) && on)
            {
                data.handbrakeLight = true; //most common denominator
            }
            else
            {
                data.handbrakeLight = false;
            }
            
            DashHookDll.DashHook_SetData(data);
        }

            
        internal event EventHandler<CEventArgs> Crash;
    }

   
    internal class CEventArgs : EventArgs
    {
        internal bool isRepaired { get; set; }
    }

}

