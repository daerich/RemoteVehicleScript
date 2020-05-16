using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rage;
using Remote_Vehicle_Locker;

namespace Remote_Vehicle_Locker.Functions
{
    internal class Plugin
    {
        private static Player player = Game.LocalPlayer;
        private Vehicle myVehicle = player.LastVehicle;
        internal void KeyEvent()
        {
            KeyboardState Keyboard = new KeyboardState();

            if (Keyboard.PressedKeys.Contains(Keys.L))
            {
                CloseVehicleDoors(myVehicle);
                LockCar(myVehicle);
                BlipSiren(myVehicle);
                GameFiber.ExecuteFor(FlashIndy, 3000);

            }
        }

        private void FlashIndy()
        {
            myVehicle.IndicatorLightsStatus = VehicleIndicatorLightsStatus.Both;
        }
        private static void BlipSiren(Vehicle Vehicle)
        {
            if (Vehicle.HasSiren)
            {
                Vehicle.BlipSiren(false);
            }


        }
        private static void LockCar(Vehicle Vehicle)
        {
            Vehicle.LockStatus = VehicleLockStatus.Locked;
        }
        private static void CloseVehicleDoors(Vehicle Vehicle)
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