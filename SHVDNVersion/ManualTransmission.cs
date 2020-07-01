/* ------------------------------------------
			COPYRIGHT © DAERICH 2020
ALL RIGHTS RESERVED EXCEPT OTHERWISE STATED IN COPYRIGHT.TXT
   ------------------------------------------ */
using DaErich.Core.External;
using GTA;
using RMVL_Scripthookv.Dashboard;
using RMVL_Scripthookv.Util;
using System;
using System.Runtime.InteropServices;

namespace RMVL_Scripthookv.MTL
{
    internal class ManualTransmission : Script
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private delegate bool FnBool();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U4)]
        private delegate int GetInt();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SetInt(int handle);

        FnBool IsActive;
        SetInt AddIgnoreVehicle;
        FnBool NeutralGear;
        GetInt ShiftMode;

        private readonly IntPtr mtlib;
        private readonly bool mtPresent;
        private readonly string logpath = "scripts/MTCompatibilty.log";
        private readonly string path = "scripts/VehicleLocker.ini";
        private readonly string animdict;
        private readonly string anim;
        private bool isEngaged = false;

        public ManualTransmission()
        {
            IniFile ini = new IniFile(path);
            animdict = ini.Read("animdict", "MTSupport");
            anim = ini.Read("anim", "MTSupport");

            Tick += OnTick;
            mtlib = Dll.GetModuleHandle(@"Gears.asi");
            if(mtlib == IntPtr.Zero)
            {
                Logger.Write(logpath, "Process not found!");
            }
            else
            {
                Logger.Write(logpath, "Process present!");
            }

            IsActive = CheckAddr<FnBool>(mtlib, "MT_IsActive");
            AddIgnoreVehicle = CheckAddr<SetInt>(mtlib, "MT_AddIgnoreVehicle");
            NeutralGear = CheckAddr<FnBool>(mtlib, "MT_NeutralGear");
            ShiftMode = CheckAddr<GetInt>(mtlib, "MT_GetShiftMode");

            if(IsActive == null || AddIgnoreVehicle == null || NeutralGear == null)
            {
                mtPresent = false;
                Logger.Write(logpath, "MTSupport disabled!");
            }
            else
            {
                mtPresent = true;
                Logger.Write(logpath, "MTSupport initialized!");
            }

        }

        private T CheckAddr<T>(IntPtr lib, string Func) where T : class
        {
            IntPtr mtproc = Dll.GetProcAddress(lib, Func);
            if(mtproc == IntPtr.Zero)
            {
                Logger.Write(logpath, $"Process {lib} not found!");
            }
            return Marshal.GetDelegateForFunctionPointer<T>(mtproc);
           
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (mtPresent)
            {
                PluginLogic();
            }
        }

        private void PluginLogic()
        {
            Vehicle playerVeh = Game.Player.Character.CurrentVehicle;

            if (!isEngaged && IsActive() && playerVeh != null && playerVeh.Exists() && Game.Player.Character.IsSittingInVehicle() && NeutralGear() && ShiftMode() == 3)
            {
                Game.Player.Character.Task.PlayAnimation(animdict, anim, 8f, -1, AnimationFlags.UpperBodyOnly | (AnimationFlags)32);
                isEngaged = true;
            }
            if (isEngaged)
            {
                if (!Game.Player.Character.IsSittingInVehicle() || !NeutralGear())
                {
                    isEngaged = false;
                }
            }
           
        }


    }
}