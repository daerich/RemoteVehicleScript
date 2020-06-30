/* ------------------------------------------
			COPYRIGHT © DAERICH 2020
ALL RIGHTS RESERVED EXCEPT OTHERWISE STATED IN COPYRIGHT.TXT
   ------------------------------------------ */
using GTA;
using GTA.UI;
using RMVL_Scripthookv.Functions;
using DaErich.Core.External;
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
            string path = "scripts/VehicleLocker.ini";
            IniFile FileRD1 = new IniFile(path);
            IniFile FileRD2 = new IniFile(path);
            Enum.TryParse(FileRD1.Read("LockKey", "General"), out Keys kresult1);
            Enum.TryParse(FileRD2.Read("WindowKey", "General"), out Keys kresult2);
           if (kresult1 == e.KeyCode)
            {
                Plugin Plg = new Plugin();
                Vehicle myVehicle = Plg.Vehicle;
                if (myVehicle != null && myVehicle.Position.DistanceTo(Game.Player.Character.Position) <= 100f)
                {
                    Plg.BlipSiren();
                    Plg.FlashIndy();
                    Plg.LockCar();
                    Plg.CloseVehicleDoors();
                    //Wait(2000);

                }
                else if (myVehicle != null)
                {
                    int FAR = Notification.Show("*Too far away from Vehicle*", true);
                    Wait(1000);
                    Notification.Hide(FAR);
                }
            }
          if (kresult2 == e.KeyCode)
            {
                Plugin Plg = new Plugin();
                Vehicle myVehicle = Plg.Vehicle;
                if (myVehicle != null && myVehicle.Position.DistanceTo(Game.Player.Character.Position) <= 100f)
                {
                    Plg.RollDown();
                }
                else if (myVehicle != null)
                {
                    int FAR = Notification.Show("*Too far away from Vehicle*", true);
                    Wait(1000);
                    Notification.Hide(FAR);
                } 
            }
        }
    }
}