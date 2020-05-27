/* ------------------------------------------
			COPYRIGHT © DAERICH 2020
ALL RIGHTS RESERVED EXCEPT OTHERWISE STATED IN COPYRIGHT.TXT
   ------------------------------------------ */
using System.Windows.Forms;
using Rage;
using Rage.Attributes;
using DaErich.Core;
using Remote_Vehicle_Locker.Functions;
using System;

[assembly: Plugin("Remote Vehicle Locker", Description = "Remotely Locks your Vehicle", Author = "DaErich", PrefersSingleInstance = true)]


namespace Remote_Vehicle_Locker
{
    public static class EntryPoint
    {
        

        public static void Main()
        {

            while (true)
            {

                KeyEvent();
                
                GameFiber.Yield();

            }
        }
        private static void KeyEvent()
        {
            const string cvars = "LockKey";
            FileReader FileRD = new FileReader("Plugins/VehicleLocker.ini", cvars);
        // Game.LogTrivial(FileRD.GetCurrentValue());
        KeyboardState Keyboard = Game.GetKeyboardState();
            //Game.LogTrivial("KeyEvent init");
            if (Enum.TryParse(FileRD.GetCurrentValue(), out Keys kresult) && Keyboard.IsDown(kresult))
            {
                Plugin Plg = new Plugin();
                Vehicle myVehicle = Plg.Vehicle;
                if (myVehicle != null && Game.LocalPlayer.Character.DistanceTo(myVehicle) <= 100f) { 
                    Game.LogTrivial("Key Pressed");
                    Plg.BlipSiren();
                    Plg.FlashIndy();
                    Plg.LockCar();
                    Plg.CloseVehicleDoors();
                    GameFiber.Sleep(2000); //implemented cooldown
                }

            }


        }
    }
    
    
}


