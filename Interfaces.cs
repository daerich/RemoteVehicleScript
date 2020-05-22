using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaErich.Core
{
    internal interface IFileWriter
    {
     
        string filedir { get; set; }
        string commandvars { get; set; }

    }
}
