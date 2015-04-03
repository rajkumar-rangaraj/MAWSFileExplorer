using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MAWSFileExplorer
{
    public class FileExplorer
    {
        public List<DirectoryTree> GetDirectories(string directoryName)
        {
            List<DirectoryTree> listofDirectories = new List<DirectoryTree>();
            DirectoryInfo dirInfo;
            IEnumerable<System.IO.DirectoryInfo> rootDirectories;

            try
            {
                dirInfo = new DirectoryInfo(directoryName);
                rootDirectories = dirInfo.GetDirectories("*.*", System.IO.SearchOption.TopDirectoryOnly);
                //Add file information to json
                GetFileInfo(dirInfo, listofDirectories);
                //Add Directory information to json
                GetDirectoryInfo(rootDirectories, listofDirectories);
            }
            catch (Exception ex)
            {
                var resp = Utility.ExceptionMessage(ex);
                throw new HttpResponseException(resp);
            }

            if (listofDirectories.Count > 0)
            {
                return listofDirectories.OrderByDescending(l => l.size).OrderByDescending(l => l.folder).ToList();
            }
            else
                return null;
        }

        private void GetDirectoryInfo(IEnumerable<System.IO.DirectoryInfo> rootDirectories, List<DirectoryTree> listofDirectories)
        {
            double totalFileSize;
            DirectoryInfo dirInfo;
            DirectoryTree dir;
            IEnumerable<FileInfo> fileInfo;

            var directories = from r in rootDirectories
                              select new
                              {
                                  Fullname = r.FullName,
                                  name = r.Name,
                                  date = r.LastWriteTime,
                              };

            foreach (var directory in directories)
            {
                try
                {
                    dirInfo = new DirectoryInfo(directory.Fullname);
                    //Get Size of directory 
                    fileInfo = dirInfo.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
                    totalFileSize = fileInfo.Sum(p => p.Length);
                    //Adding directory to list
                    dir = new DirectoryTree();
                    dir.title = directory.name;
                    dir.fullPath = directory.Fullname;
                    dir.size = totalFileSize;
                    dir.date = directory.date.ToShortDateString();
                    dir.files = fileInfo.Count();
                    dir.folders = dirInfo.GetDirectories("*.*", System.IO.SearchOption.AllDirectories).Count();
                    if (dir.files > 0 || dir.folders > 0)
                        dir.lazy = true;
                    dir.folder = true;
                    //recursive call to generate complete tree
                    // dir.children = GetDirectories(directory.Fullname);
                    listofDirectories.Add(dir);
                }
                catch (Exception ex)
                {
                    var resp = Utility.ExceptionMessage(ex);
                    throw new HttpResponseException(resp);
                }
            }
        }

        private void GetFileInfo(DirectoryInfo dirInfo, List<DirectoryTree> listofDirectories)
        {
            IEnumerable<FileInfo> fileInfo = dirInfo.GetFiles("*.*", System.IO.SearchOption.TopDirectoryOnly);

            if (fileInfo.Count() > 0)
            {
                var files = from file in fileInfo
                            select new
                            {
                                name = file.Name,
                                fullName = file.FullName,
                                date = file.LastWriteTime,
                                size = file.Length
                            };

                foreach (var file in files)
                {
                    listofDirectories.Add(new DirectoryTree
                    {
                        title = file.name,
                        fullPath = file.fullName,
                        size = file.size,
                        date = file.date.ToShortDateString()
                    });
                }
            }
        }
    }
}