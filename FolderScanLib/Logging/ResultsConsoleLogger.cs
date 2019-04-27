using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderScanLib
{
    public class ResultsConsoleLogger : IResultsLogger
    {
        public void LogResults(string results)
        {
            Console.WriteLine(results);
        }
    }
}
