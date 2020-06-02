using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace CodeReactor.DarkFrontier.Core.Xml
{
    public class DFTest
    {
        public Version Version;
        public string ServerUrl;

        public static DFTest ParseXml(string xml)
        {
            try
            {
                XmlSerializer xr = new XmlSerializer(typeof(DFInstall));
                TextReader tr = new StringReader(xml);
                DFTest darkxml = (DFTest)xr.Deserialize(tr);
                if (darkxml.Version == null) throw new XmlException("Invalid DFTest.Version");
                if (darkxml.Version.Major < 0) throw new XmlException("Invalid DFTest.Version.Major");
                if (darkxml.Version.Minor < 0) throw new XmlException("Invalid DFTest.Version.Minor");
                if (darkxml.Version.Patch < 0) throw new XmlException("Invalid DFTest.Version.Patch");
                if (darkxml.Version.Revision < 0) throw new XmlException("Invalid DFTest.Version.Revision");
                if (darkxml.ServerUrl == null) throw new XmlException("Invalid DFTest.ServerUrl");
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
