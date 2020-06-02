using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using CodeReactor.DarkFrontier.Core;
using CodeReactor.DarkFrontier.Core.Xml;

namespace CodeReactor.DarkFrontier.Test
{
    public class MainTest
    {
        public MainTest()
        {
            DFTest dft = ParseXml(StaticMemory.Args[1]);
            if (dft == null) return;
            Core.Xml.DarkFrontier dfr = ServerRequest.GetDarkFrontier(dft.ServerUrl);
            if (dfr == null) return;
            if (Versioner.Test(dfr.Version, dft.Version))
            {
                Application.Current.Shutdown(ExitCode.Updated);
            } else
            {
                Application.Current.Shutdown(ExitCode.Outdated);
            }
        }

        public static DFTest ParseXml(string xml)
        {
            try
            {
                XmlSerializer xr = new XmlSerializer(typeof(DFTest));
                TextReader tr = new StringReader(xml);
                return (DFTest)xr.Deserialize(tr);
            } catch (Exception)
            {
                Application.Current.Shutdown(ExitCode.InvalidXml);
                return null;
            }
        }
    }
}
