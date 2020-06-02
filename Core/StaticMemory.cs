using CodeReactor.DarkFrontier.Core.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeReactor.DarkFrontier.Core
{
    public static class StaticMemory
    {
        public static DFUnpack DFU { get; set; }
        public static int Operation { get; set; }
        public static string[] Args { get; set; }
        public static Task DownloadFile { get; set; }
        public static bool Errored { get; set; }
    }
}
