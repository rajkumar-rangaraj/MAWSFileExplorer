using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAWSFileExplorer
{
    public class DirectoryTree
    {
        public string title { get; set; }
        public double size { get; set; }
        public string fullPath { get; set; }
        public string date { get; set; }
        public bool? lazy { get; set; }
        public bool? folder { get; set; }
        public int? files { get; set; }
        public int? folders { get; set; }
        //public IList<Directory> children { get; set; }
    }
}