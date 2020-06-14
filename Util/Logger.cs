/* ------------------------------------------
			COPYRIGHT © DAERICH 2020
ALL RIGHTS RESERVED EXCEPT OTHERWISE STATED IN COPYRIGHT.TXT
   ------------------------------------------ */
using System;
using System.IO;

namespace RMVL_Scripthookv.Util
{
    internal static class Logger
    {
        
            internal static void Write(string path, string values)
            {
            string date = DateTime.Now.ToShortTimeString();
            if (!File.Exists(path))
            {
                File.Create(path);
                using (TextWriter fw = new StreamWriter(path))
                {
                    fw.WriteLine(System.String.Format("[{0:G}]: {1}", date, values));
                }
            }
            else if (File.Exists(path))
            {
                using (TextWriter fw = new StreamWriter(path, true))
                {
                    fw.WriteLine(System.String.Format("[{0:G}]: {1}", date, values));
                }
            }
        }
    }

}