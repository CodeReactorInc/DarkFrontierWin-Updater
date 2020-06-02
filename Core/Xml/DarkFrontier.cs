using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Windows;
using System.Diagnostics;

namespace CodeReactor.DarkFrontier.Core.Xml
{
    public class DarkFrontier
    {
        public Version Version;
        public string DownloadUrl;
        public string DFPath;
        public Version InstallerInfo;
        public string Filename;

        public static DarkFrontier ParseXml(string xml)
        {
            try
            {
                XmlSerializer xr = new XmlSerializer(typeof(DarkFrontier));
                TextReader tr = new StringReader(xml);
                DarkFrontier darkxml = (DarkFrontier) xr.Deserialize(tr);
                if (darkxml.DFPath == null) throw new XmlException("Invalid DarkFrontier.DFPath");
                if (darkxml.Filename == null) throw new XmlException("Invalid DarkFrontier.Filename");
                if (darkxml.DownloadUrl == null) throw new XmlException("Invalid DarkFrontier.DownloadUrl");
                if (darkxml.Version == null) throw new XmlException("Invalid DarkFrontier.Version");
                if (darkxml.Version.Major < 0) throw new XmlException("Invalid DarkFrontier.Version.Major");
                if (darkxml.Version.Minor < 0) throw new XmlException("Invalid DarkFrontier.Version.Minor");
                if (darkxml.Version.Patch < 0) throw new XmlException("Invalid DarkFrontier.Version.Patch");
                if (darkxml.Version.Revision < 0) throw new XmlException("Invalid DarkFrontier.Version.Revision");
                if (darkxml.InstallerInfo == null) throw new XmlException("Invalid DarkFrontier.InstallerInfo");
                if (darkxml.InstallerInfo.Major < 0) throw new XmlException("Invalid DarkFrontier.InstallerInfojor");
                if (darkxml.InstallerInfo.Minor < 0) throw new XmlException("Invalid DarkFrontier.InstallerInfonor");
                if (darkxml.InstallerInfo.Patch < 0) throw new XmlException("Invalid DarkFrontier.InstallerInfotch");
                if (darkxml.InstallerInfo.Revision < 0) throw new XmlException("Invalid DarkFrontier.InstallerInfo.Revision");
                return darkxml;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
                Application.Current.Shutdown(ExitCode.InvalidXml);
                return null;
            }
        }
    }
}
