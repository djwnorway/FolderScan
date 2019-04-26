using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FolderScanLib;

namespace FolderScanApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var folderScan = new FolderScanConsole();
            folderScan.Run(Logging.File);

            Console.WriteLine("\n\nResults have been successfully logged. Thanks!\n");
        }        
    }
}
