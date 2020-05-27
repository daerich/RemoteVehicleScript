/* ------------------------------------------
			COPYRIGHT © DAERICH 2020
ALL RIGHTS RESERVED EXCEPT OTHERWISE STATED IN COPYRIGHT.TXT
   ------------------------------------------ */
using Rage;

namespace Remote_Vehicle_Locker.Functions
{
    internal class Plugin
    {
        internal Vehicle Vehicle { get; set; } = Game.LocalPlayer.LastVehicle; //do not declare public instance fields!

        internal void FlashIndy()
        {

            Vehicle.IndicatorLightsStatus = VehicleIndicatorLightsStatus.Both;
            GameFiber.Sleep(500);
            Vehicle.IndicatorLightsStatus = VehicleIndicatorLightsStatus.Off;
        }
        internal void BlipSiren()
        {
            if (Vehicle.HasSiren)
            {
                Vehicle.BlipSiren(true);
            }


        }
        internal void LockCar()
        {
            // if (Vehicle.Driver == null)
            //{                                     Funny little artifact
            if (Vehicle.LockStatus == VehicleLockStatus.Locked)
            {
                Vehicle.LockStatus = VehicleLockStatus.Unlocked;

                Game.DisplaySubtitle("*Pins UP");
            }



            else
            {
                Vehicle.LockStatus = VehicleLockStatus.Locked;
                Game.DisplaySubtitle("*Pins DOWN");
            }
        }
        internal void CloseVehicleDoors()
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
