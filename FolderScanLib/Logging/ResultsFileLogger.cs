using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderScanLib
{
    public class ResultsFileLogger : IResultsLogger
    {
        public void LogResults(string results)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            using (var writer = new StreamWriter($"{Directory.GetCurrentDirectory()}\\results.txt"))
            {
                writer.Write(results);
            }
        }
    }
}
