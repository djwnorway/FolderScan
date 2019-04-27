using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;


namespace FolderScanLib
{
    public class OneDriveFolderScan : IFolderScan
    {
        private const string ExpandString = "thumbnails,children($expand=thumbnails)";
        private GraphServiceClient GraphClient { get; set; }
        private static Dictionary<string, string> OneDriveFolderIds { get; set; }

        public OneDriveFolderScan()
        {
            GraphClient = AuthenticationHelper.GetAuthenticatedClient();

            if (OneDriveFolderScan.OneDriveFolderIds == null)
            {
                OneDriveFolderIds = new Dictionary<string, string>();
                OneDriveFolderIds.Add("A-B", "3E227198EB03C31D!84277");
                OneDriveFolderIds.Add("C-D", "3E227198EB03C31D!84365");
                OneDriveFolderIds.Add("E-G", "3E227198EB03C31D!84366");
                OneDriveFolderIds.Add("H-I", "3E227198EB03C31D!84463");
                OneDriveFolderIds.Add("J-L", "3E227198EB03C31D!84464");
                OneDriveFolderIds.Add("M-N", "3E227198EB03C31D!84473");
                OneDriveFolderIds.Add("O-R", "3E227198EB03C31D!84474");
                OneDriveFolderIds.Add("S", "3E227198EB03C31D!84475");
                OneDriveFolderIds.Add("T", "3E227198EB03C31D!84476");
                OneDriveFolderIds.Add("U-V", "3E227198EB03C31D!84477");
                OneDriveFolderIds.Add("W-Z", "3E227198EB03C31D!84478");
            }
        }


        /// <summary>
        /// Returns two lists. 
        ///  Item1 list contains items found in folder1 missing from folder2.
        ///  Item2 lists contains items found in folder2 missing from folder1.
        /// </summary>
        /// <remarks>
        ///   Folder1 Parameter - This needs to be on the current hard drive
        ///   Folder2 Parameter - This is empty, since we are getting one drive folder names
        /// </remarks>
        /// <param name="folder1"></param>
        /// <param name="folder2"></param>
        /// <returns></returns>
        public Tuple<List<string>, List<string>> GetMissingFolders(string folder1, string folder2)
        {
            var folder1SubFolders = GetListOfSubFolders(folder1);
            var folder2SubFolders = GetListOfOneDriveFolderNames();

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

        /// <summary>
        /// Returns a list of all One Drive folder names using ID's in dictionary lookup, OneDriveFolderIds
        /// </summary>        
        /// <returns>List<string></returns>
        private List<string> GetListOfOneDriveFolderNames()
        {
            List<string> oneDriveFolders = new List<string>();

            //loop through folder ids to generate entire list A-Z
            foreach (var item in OneDriveFolderIds)
            {
                //get the music sub folder,   ex.  'A-B', 'C-D', etc...
                var musicSubFolder = GraphClient.Drive.Items[item.Value].Request().Expand(ExpandString).GetAsync().Result;
                Console.WriteLine($"Processing music sub folder: {musicSubFolder.Name}");
                foreach (var band in musicSubFolder.Children)
                {
                    oneDriveFolders.Add(band.Name);
                }
            }

            return oneDriveFolders;
        }
    }
}
