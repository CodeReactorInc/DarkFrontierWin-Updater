using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using System.Diagnostics;

namespace CodeReactor.DarkFrontier.Core.Xml
{
    public class DFInstall
    {
        public string ProductName;
        public string ServerUrl;
        public string InstallPath;

        public static DFInstall ParseXml(string xml)
        {
            try
            {
                XmlSerializer xr = new XmlSerializer(typeof(DFInstall));
                TextReader tr = new StringReader(xml);
                DFInstall darkxml = (DFInstall) xr.Deserialize(tr);
                if (darkxml.ProductName == null) throw new XmlException("Invalid DFInstall.ProductName");
                if (darkxml.ServerUrl == null) throw new XmlException("Invalid DFInstall.ServerUrl");
                if (darkxml.InstallPath == null) throw new XmlException("Invalid DFInstall.InstallPath");
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
