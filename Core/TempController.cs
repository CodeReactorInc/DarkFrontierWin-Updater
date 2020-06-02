using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeReactor.DarkFrontier.Core
{
    public static class TempController
    {
        public static int GenInstallId
        {
            get { return new Random().Next(1000000, 9999999);  }
        }

        public static int InstallId { get; set; }

        public static DirectoryInfo GeneratedDir { get; set; }

        public static DirectoryInfo GenTempDir()
        {
            if (GeneratedDir == null)
            {
                InstallId = GenInstallId;
                string path = "DarkFrontier-" + InstallId;
                if (Directory.Exists(Path.GetTempPath() + "/" + path))
                {
                    return GenTempDir();
                }
                else
                {
                    GeneratedDir = Directory.CreateDirectory(Path.GetTempPath() + "/" + path);
                    return GeneratedDir;
                }
            } else
            {
                return GeneratedDir;
            }
        }
        public static DirectoryInfo GenTempDir(int id)
        {
            InstallId = id;
            string path = "DarkFrontier-" + InstallId;GeneratedDir = Directory.CreateDirectory(Path.GetTempPath() + "/" + path);
            return GeneratedDir;
        }

        public static void ClearDirectory()
        {
            string path = "DarkFrontier-" + InstallId;
            if (Directory.Exists(Path.GetTempPath() + "/" + path))
            {
                DestroyDirectory(Path.GetTempPath() + "/" + path);
                Directory.Delete(Path.GetTempPath() + "/" + path);
            }
        }

        public static void DestroyDirectory(string path)
        {
            foreach (string dir in Directory.GetDirectories(path))
            {
                DestroyDirectory(dir);
            }
            foreach (string file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }
        }
    }
}
