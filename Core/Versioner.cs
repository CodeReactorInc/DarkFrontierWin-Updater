using CodeReactor.DarkFrontier.Core.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeReactor.DarkFrontier.Core
{
    public static class Versioner
    {
        public static bool Test(Xml.Version server, Xml.Version client)
        {
            if (!TestMajor(server, client)) return false;
            if (!TestMinor(server, client)) return false;
            if (!TestPatch(server, client)) return false;
            return TestRevision(server, client);
        }

        public static bool TestMajor(Xml.Version server, Xml.Version client)
        {
            return server.Major <= client.Major;
        }

        public static bool TestMinor(Xml.Version server, Xml.Version client)
        {
            return server.Minor <= client.Minor;
        }

        public static bool TestRevision(Xml.Version server, Xml.Version client)
        {
            return server.Revision <= client.Revision;
        }

        public static bool TestPatch(Xml.Version server, Xml.Version client)
        {
            return server.Patch <= client.Patch;
        }
    }
}
