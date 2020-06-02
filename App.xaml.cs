using CodeReactor.DarkFrontier.Core;
using CodeReactor.DarkFrontier.Core.Xml;
using CodeReactor.DarkFrontier.Test;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace CodeReactor.DarkFrontier
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                MainWindow mw = new MainWindow();
                base.OnStartup(e);
                if (e.Args.Length == 2)
                {
                    if (e.Args[0] == "test")
                    {
                        Debug.Listeners.Add(new TextWriterTraceListener(TempController.GenTempDir().FullName + "/debug.log"));
                        StaticMemory.Operation = Operation.Test;
                        StaticMemory.Args = e.Args;
                        Debug.WriteLine("Using DFTestXml: " + e.Args[1]);
                        new MainTest();
                    }
                    else if (e.Args[0] == "install")
                    {
                        Debug.Listeners.Add(new TextWriterTraceListener(TempController.GenTempDir().FullName + "/debug.log"));
                        StaticMemory.Operation = Operation.Test;
                        StaticMemory.Operation = Operation.Install;
                        StaticMemory.Args = e.Args;
                        Debug.WriteLine("Using DFInstallXml: " + e.Args[1]);
                        mw.Show();
                    }
                    else if (e.Args[0] == "finalize")
                    {
                        TempController.GenTempDir(int.Parse(e.Args[1]));
                        TempController.ClearDirectory();
                    }
                    else if (e.Args[0] == "unpack")
                    {
                        StaticMemory.Operation = Operation.Unpack;
                        StaticMemory.Args = e.Args;
                        StaticMemory.DFU = DFUnpack.ParseXml(e.Args[1]);
                        Debug.Listeners.Add(new TextWriterTraceListener(TempController.GenTempDir(StaticMemory.DFU.InstallId).FullName + "/debug-installer.log"));
                        Debug.WriteLine("Using DFUnpackXml: " + e.Args[1]);
                        Debug.WriteLine("Temp path: " + TempController.GeneratedDir.FullName);
                        mw.Show();
                    }
                    else
                    {
                        Current.Shutdown(ExitCode.UnknownArgs);
                    }
                }
                else
                {
                    Current.Shutdown(ExitCode.UnknownArgs);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
                Current.Shutdown(ExitCode.InternalError);
                return;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            try
            {
                Debug.Close();
                if (e.ApplicationExitCode != ExitCode.Unpacking && e.ApplicationExitCode != ExitCode.InternalError && e.ApplicationExitCode != ExitCode.InvalidXml) TempController.ClearDirectory();
                Current.Shutdown(e.ApplicationExitCode);
            } catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
