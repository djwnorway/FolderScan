using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderScanLib
{
    interface IFolderScan
    {
        Tuple<List<string>, List<string>> GetMissingFolders(string folder1, string folder2);
    }
}
