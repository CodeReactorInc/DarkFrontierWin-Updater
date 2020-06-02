using System;
using System.Net;
using System.Windows;
using System.Diagnostics;

namespace CodeReactor.DarkFrontier.Core
{
    public class ServerRequest
    {
        public static string GetRaw(string url)
        {
            return GetRaw(new Uri(url));
        }

        public static string GetRaw(Uri url)
        {
            try
            {
                Debug.WriteLine("Link url: " + url);
                WebClient wclient = new WebClient();
                string xml = wclient.DownloadString(url);
                Debug.WriteLine("Found DarkFrontierXml: " + xml);
                return xml;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
                Application.Current.Shutdown(ExitCode.CantConnectWithServer);
                return null;
            }
        }

        public static Xml.DarkFrontier GetDarkFrontier(string url)
        {
            return GetDarkFrontier(new Uri(url));
        }

        public static Xml.DarkFrontier GetDarkFrontier(Uri url)
        {
            return Xml.DarkFrontier.ParseXml(GetRaw(url));
        }
    }
}
