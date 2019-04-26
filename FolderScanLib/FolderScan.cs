using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderScanLib
{

    internal class FolderScan : IFolderScan
    {
        /// <summary>
        /// Returns two lists. 
        ///  Item1 list contains items found in folder1 missing from folder2.
        ///  Item2 lists contains items found in folder2 missing from folder1.
        /// </summary>
        /// <param name="folder1"></param>
        /// <param name="folder2"></param>
        /// <returns></returns>
        public Tuple<List<string>, List<string>> GetMissingFolders(string folder1, string folder2)
        {
            var folder1SubFolders = GetListOfSubFolders(folder1);
            var folder2SubFolders = GetListOfSubFolders(folder2);

            var folder1Only = folder1SubFolders.Except(folder2SubFolders).ToList();
            var folder2Only = folder2SubFolders.Except(folder1SubFolders).ToList();

            return new Tuple<List<string>, List<string>>(folder1Only, folder2Only);
        }

        /// <summary>
        /// Returns a list of sub folders names. These do not include the file paths. 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>List<string></returns>
        private List<string> GetListOfSubFolders(string path)
        {
            var directories = new DirectoryInfo(path).GetDirectories();
            List<string> subFolders = new List<string>();

            foreach (var directory in directories)
            {
                subFolders.Add(directory.Name);
            }

            return subFolders;
        }
    }
}
