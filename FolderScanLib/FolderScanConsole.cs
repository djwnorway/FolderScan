using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderScanLib
{
    /// <summary>
    /// Retrieves user input and writes results via the console. 
    /// Additional processing logic occurs here to get results from FolderScan objects
    /// </summary>
    public class FolderScanConsole
    {
        private string Folder1 { get; set; }
        private string Folder2 { get; set; }
        private List<string> Folder1Only { get; set; }
        private List<string> Folder2Only { get; set; }

        public FolderScanConsole()
        {
            Folder1 = string.Empty;
            Folder2 = string.Empty;
        }

        public void Run()
        {
            GetUserInput();
            DoFolderScan();
            ShowResults();
        }

        private void GetUserInput()
        {
            Console.WriteLine("Please enter the file path for folder 1: ");
            Folder1 = Console.ReadLine();

            Console.WriteLine("Please enter the file path folr folder 2: ");
            Folder2 = Console.ReadLine();
        }

        private void DoFolderScan()
        {
            var missingFolders = FolderScan.GetMissingFolders(Folder1, Folder2);
            Folder1Only = missingFolders.Item1;
            Folder2Only = missingFolders.Item2;
        }

        private void ShowResults()
        {
            InsertNewLines(5);

            //DIRECTORY(aka FOLDER) 1
            Console.WriteLine($"DIRECTORY 1 RESULTS '{Folder1}'");
            Console.WriteLine($"All folders in directory 1 that are NOT in directory 2:");
            PrintSubFolderResults(Folder1Only);
            InsertNewLines(4);

            //DIRECTORY(aka FOLDER) 2
            Console.WriteLine($"DIRECTORY 2 RESULTS '{Folder2}'");
            Console.WriteLine($"All folders in directory 2 that are NOT in directory 1:");
            PrintSubFolderResults(Folder2Only);
            InsertNewLines(2);
        }

        private void PrintSubFolderResults(List<string> subFolders)
        {
            for (int i = 0; i < subFolders.Count(); i++)
            {
                Console.WriteLine($" {i + 1}) {subFolders[i]}");
            }
        }

        private void InsertNewLines(int numberOfNewLines)
        {
            if (numberOfNewLines < 0) return;
            for (int i = 0; i < numberOfNewLines; i++)
            {
                Console.WriteLine();
            }
        }
    }
}
