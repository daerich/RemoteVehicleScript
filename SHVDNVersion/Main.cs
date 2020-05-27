using GTA;
using RMVL_Scripthookv.Functions;
using DaErich.Core;
using System.Windows.Forms;
using System;

namespace RMVL_Scripthookv
{
    class RemoteVehicleLocker : Script
    {
        public RemoteVehicleLocker()
        {
            //Tick += OnTick;
            KeyDown += OnKeyDown;
        }

        /*  private void OnTick(object sender, EventArgs e)
          {

          } */

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            const string cvars = "LockKey";
            FileReader FileRD = new FileReader("scripts/VehicleLocker.ini", cvars);
            Enum.TryParse(FileRD.GetCurrentValue(), out Keys kresult);
            if (kresult == e.KeyCode)
            {
                Plugin Plg = new Plugin();
                Vehicle myVehicle = Plg.Vehicle;
                if (myVehicle != null && Plugin.Player.LastVehicle.Position.DistanceTo(Plugin.Player.Character.Position) <= 100f)
                {
                    //Game.LogTrivial("Key Pressed");
                    Plg.BlipSiren();
                    Plg.FlashIndy();
                    Plg.LockCar();
                    Plg.CloseVehicleDoors();
                    Wait(2000);
                }
            }
        }
    }
}
