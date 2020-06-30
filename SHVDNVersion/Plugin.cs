/* ------------------------------------------
			COPYRIGHT © DAERICH 2020
ALL RIGHTS RESERVED EXCEPT OTHERWISE STATED IN COPYRIGHT.TXT
   ------------------------------------------ */
using GTA;
using GTA.UI;

namespace RMVL_Scripthookv.Functions
{
    internal class Plugin
    {
        internal Vehicle Vehicle { get; set; } //do not declare public instance fields!

        public Plugin()
        {
            Vehicle = Game.Player.Character.LastVehicle;
        }

        internal void FlashIndy()
        {

            Vehicle.IsLeftIndicatorLightOn = true;
            Vehicle.IsRightIndicatorLightOn = true;
            Script.Wait(500);
            Vehicle.IsLeftIndicatorLightOn = false;
            Vehicle.IsRightIndicatorLightOn = false;
        }
        internal void BlipSiren()
        {
            Vehicle.SoundHorn(300);
            /*if (Vehicle.HasSiren)
            {
                if (Vehicle.IsSirenActive)
                {
                    Vehicle.IsSirenSilent = false;
                    Script.Wait(500);
                    Vehicle.IsSirenSilent = true;
                }


                else
                {
                   
                }

            }*/


        }
        internal void LockCar()
        {
            // if (Vehicle.Driver == null)
            //{                                     Funny little artifact
            if (Vehicle.LockStatus == VehicleLockStatus.Locked)
            {
                int PINSUP = Notification.Show("*Pins UP*", true);
                Vehicle.LockStatus = VehicleLockStatus.Unlocked;
                Script.Wait(1000);
                Notification.Hide(PINSUP);

            }



            else
            {
                Vehicle.LockStatus = VehicleLockStatus.Locked;
                int PINSD = Notification.Show("*Pins DOWN*", true);
                Script.Wait(1000);
                Notification.Hide(PINSD);
            }
            AmbientLightning();
        }
        internal void CloseVehicleDoors()
        {
            if (Vehicle.LockStatus == VehicleLockStatus.Locked)
            {
                VehicleDoorCollection vDoor = Vehicle.Doors;

                foreach (VehicleDoor door in vDoor.ToArray())
                {
                    if (door.IsOpen)
                    {
                        door.Close(false);
                    }
                }
            }

        }

        internal void AmbientLightning()
        {
            if (World.CurrentTimeOfDay.TotalHours >= 18.00 || World.CurrentTimeOfDay.TotalHours < 8.00)
            {
                if (Vehicle.LockStatus == VehicleLockStatus.Unlocked)
                {
                    Vehicle.IsInteriorLightOn = true;
                }
                if (Vehicle.LockStatus == VehicleLockStatus.Locked)
                {
                    Vehicle.IsInteriorLightOn = false;
                }
            }
        }

        internal void RollDown()
        {
            for (int i = 0; i <= 1; i++)
            {
                VehicleWindowIndex window = (VehicleWindowIndex)i;
                if (Vehicle.Windows[window].IsIntact)
                {
                    Vehicle.Windows[window].RollDown();               
                }
                else
                {
                    Vehicle.Windows[window].RollUp();
                }

            }

        }
    }

}