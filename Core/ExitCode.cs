using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeReactor.DarkFrontier.Core
{
    public static class ExitCode
    {
        public const int Updated = 0;
        public const int Success = 0;
        public const int UnknownArgs = -1;
        public const int InvalidXml = -2;
        public const int CantConnectWithServer = -3;
        public const int CantDownloadInstaller = -4;
        public const int InvalidInstallId = -5;
        public const int Outdated = -6;
        public const int InternalError = -7;
        public const int InvalidDownloadFile = -8;
        public const int Cancelled = -9;
        public const int Unpacking = -10;
    }
}
