using Rage;

namespace Remote_Vehicle_Locker.Functions
{
    internal class Plugin
    {
        internal Vehicle Vehicle { get; set; } = Game.LocalPlayer.LastVehicle; //do not declare public instance fields!

        internal static void FlashIndy(Vehicle Vehicle)
        {
            
            Vehicle.IndicatorLightsStatus = VehicleIndicatorLightsStatus.Both;
            GameFiber.Sleep(500);
            Vehicle.IndicatorLightsStatus = VehicleIndicatorLightsStatus.Off;
        }
        internal static void BlipSiren(Vehicle Vehicle)
        {
            if (Vehicle.HasSiren)
            {
                Vehicle.BlipSiren(true);
            }


        }
        internal static void LockCar(Vehicle Vehicle)
        {
            // if (Vehicle.Driver == null)
            //{                                     Funny little artifact
            if (Vehicle.LockStatus == VehicleLockStatus.Locked)
            {
                Vehicle.LockStatus = VehicleLockStatus.Unlocked;
            }



            else
            {
                Vehicle.LockStatus = VehicleLockStatus.Locked;
            }
        }    
        internal static void CloseVehicleDoors(Vehicle Vehicle)
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