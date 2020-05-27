/* ------------------------------------------
			COPYRIGHT © DAERICH 2020
ALL RIGHTS RESERVED EXCEPT OTHERWISE STATED IN COPYRIGHT.TXT
   ------------------------------------------ */
using Rage;
using System;
using System.IO;
using System.Linq;

namespace DaErich.Core
{
   public class FileReader : IFileWriter
    {
        public string filedir { get; set; }
        public string commandvars { get; set; }
        private string[] content;

        public FileReader(string fdir, string cvars)
        {
            commandvars = cvars;
            filedir = fdir;
            try
            {
                content = File.ReadAllText(filedir).Split('=');
            }
            catch (Exception ex)
            {
                Game.LogTrivial(ex.Message);
            }


        }

        public string GetCurrentValue()
        {
            try
            {
                foreach (string element in content)
                {
                    if (element.Contains(commandvars))
                    {
                        return content[Array.IndexOf(content, element) + 1];
                    }
                    else
                    {
                        throw new Exception("No such values!");
                        
                    }
                }
            }
            catch (Exception e)
            {
                Game.LogTrivial(e.Message);
            }
            throw new Exception("No such values code2");
        }




    }
}
