using System.Windows.Forms;
using Rage;
using Rage.Attributes;
using Remote_Vehicle_Locker.Functions;

[assembly: Plugin("Remote Vehicle Locker", Description = "Remotely Locks your Vehicle", Author = "DaErich")]


namespace Remote_Vehicle_Locker
{
    public static class EntryPoint
    {
        
        private static void Main()
        {
            while (true)
            {
                // Game.LogTrivial("RMVL initialized");
                KeyEvent();
                GameFiber.Yield();

            }
        }
        private static void KeyEvent()
        {
            KeyboardState Keyboard = Game.GetKeyboardState();
            Plugin Plg = new Plugin();
            Vehicle myVehicle = Plg.Vehicle;
            //Game.LogTrivial("KeyEvent init");
            if (Keyboard.IsDown(Keys.L) && myVehicle != null)
            {
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


