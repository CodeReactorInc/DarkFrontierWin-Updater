using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace CodeReactor.DarkFrontier.Core.Xml
{
    public class DFUnpack
    {
        public int InstallId;
        public string Filename;
        public string InstallPath;
        public string DFPath;

        public DFUnpack(int installid, string filename, string installpath, string dfpath)
        {
            InstallId = installid;
            Filename = filename;
            InstallPath = installpath;
            DFPath = dfpath;
        }

        public DFUnpack() { }

        public static DFUnpack ParseXml(string xml)
        {
            try
            {
                XmlSerializer xr = new XmlSerializer(typeof(DFUnpack));
                TextReader tr = new StringReader(xml);
                DFUnpack darkxml = (DFUnpack) xr.Deserialize(tr);
                if (darkxml.InstallId < 1000000 || darkxml.InstallId > 9999999) throw new XmlException("Invalid DFUnpack.InstallId");
                if (darkxml.Filename == null) throw new XmlException("Invalid DFUnpack.Filename");
                if (darkxml.InstallPath == null) throw new XmlException("Invalid DFUnpack.InstallPath");
                if (darkxml.DFPath == null) throw new XmlException("Invalid DFUnpack.DFPath");
                return darkxml;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
                Application.Current.Shutdown(ExitCode.InvalidXml);
                return null;
            }
        }

        public static string ToString(DFUnpack xml)
        {
            try
            {
                XmlSerializer xr = new XmlSerializer(typeof(DFUnpack));
                TextWriter tw = new StringWriter();
                xr.Serialize(tw, xml);
                return tw.ToString().Replace("\"", "\\\"");
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
