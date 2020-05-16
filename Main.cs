using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rage;
using Remote_Vehicle_Locker.Functions;

[assembly: Rage.Attributes.Plugin("Remote Vehicle Locker", Description = "Remotely Locks your Vehicle", Author = "DaErich")]


namespace Remote_Vehicle_Locker
{
    public static class EntryPoint
    {
        
        private static void Main()
        {
            Plugin Plg = new Plugin();
            
            GameFiber.StartNew(Plg.KeyEvent);
        }
      
    }
    
    
}


