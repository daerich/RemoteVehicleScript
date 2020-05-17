using System;
using System.Windows.Forms;
using Rage;
using Remote_Vehicle_Locker;

namespace Remote_Vehicle_Locker.Functions
{
    internal class Plugin
    {
        internal Vehicle myVehicle = Game.LocalPlayer.LastVehicle;

        internal static void FlashIndy(ref Vehicle Vehicle)
        {
            
            Vehicle.IndicatorLightsStatus = VehicleIndicatorLightsStatus.Both;
            GameFiber.Sleep(500);
            Vehicle.IndicatorLightsStatus = VehicleIndicatorLightsStatus.Off;
        }
        internal static void BlipSiren(ref Vehicle Vehicle)
        {
            if (Vehicle.HasSiren)
            {
                Vehicle.BlipSiren(true);
            }


        }
        internal static void LockCar(ref Vehicle Vehicle)
        {
            // if (Vehicle.Driver == null)
            //{
            if (Vehicle.LockStatus == VehicleLockStatus.Locked)
            {
                Vehicle.LockStatus = VehicleLockStatus.Unlocked;
            }



            else
            {
                Vehicle.LockStatus = VehicleLockStatus.Locked;
            }
        }    
        internal static void CloseVehicleDoors(ref Vehicle Vehicle)
        {
            VehicleDoor[] vDoor = Vehicle.GetDoors();

            foreach (VehicleDoor door in vDoor)
            {
                if (door.IsOpen && door.IsValid())
                {
                    door.Close(true);
                }
            }

        }
    }

}