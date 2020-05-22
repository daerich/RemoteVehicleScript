using System.Windows.Forms;
using Rage;
using Rage.Attributes;
using DaErich.Core;
using Remote_Vehicle_Locker.Functions;
using System;

[assembly: Plugin("Remote Vehicle Locker", Description = "Remotely Locks your Vehicle", Author = "DaErich")]


namespace Remote_Vehicle_Locker
{
    public class EntryPoint
    {
        
        internal static void Main()
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
                if (myVehicle != null){ 
   
                    Game.LogTrivial("Key Pressed");
                    Plugin.BlipSiren(myVehicle);
                    Plugin.FlashIndy(myVehicle);
                    Plugin.LockCar(myVehicle);
                    Plugin.CloseVehicleDoors(myVehicle);
                    GameFiber.Sleep(2000); //implemented cooldown
                }

            }


        }
    }
    
    
}


