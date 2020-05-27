/* ------------------------------------------
			COPYRIGHT © DAERICH 2020
ALL RIGHTS RESERVED EXCEPT OTHERWISE STATED IN COPYRIGHT.TXT
   ------------------------------------------ */
using GTA;
using GTA.UI;
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
                    Plg.BlipSiren();
                    Plg.FlashIndy();
                    Plg.LockCar();
                    Plg.CloseVehicleDoors();
                    //Wait(2000);

                }
                else if (myVehicle != null)
                {
                    int FAR = Notification.Show("*Too far away from Vehicle", true);
                    Wait(1000);
                    Notification.Hide(FAR);
                   // Wait(2000);
                }
            }
        }
    }
}
