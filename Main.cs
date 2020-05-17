using System.Threading.Tasks;
using System.Windows.Forms;
using Rage;
using Rage.Attributes;
using Remote_Vehicle_Locker.Functions;

[assembly: Rage.Attributes.Plugin("Remote Vehicle Locker", Description = "Remotely Locks your Vehicle", Author = "DaErich")]


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
            Vehicle myVehicle = Plg.myVehicle;
            //Game.LogTrivial("KeyEvent init");
            if (Keyboard.IsDown(Keys.L) && myVehicle != null)
            {
                Game.LogTrivial("Key Pressed");
                Plugin.BlipSiren(ref myVehicle);
                Plugin.FlashIndy(ref myVehicle);
                Plugin.LockCar(ref myVehicle);
                Plugin.CloseVehicleDoors(ref myVehicle);
                GameFiber.Sleep(2000); //implemented cooldown

            }

          
        }
    }
    
    
}


