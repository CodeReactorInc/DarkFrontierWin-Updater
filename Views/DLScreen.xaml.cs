using CodeReactor.DarkFrontier.Core;
using CodeReactor.DarkFrontier.Core.Xml;
using System.IO.Compression;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Reflection;
using System;

namespace CodeReactor.DarkFrontier.Views
{
    /// <summary>
    /// Interação lógica para DLScreen.xam
    /// </summary>
    public partial class DLScreen : UserControl
    {
        public Downloader dl;
        public System.Windows.Forms.NotifyIcon Notify;

        public DLScreen()
        {
            try
            {
                InitializeComponent();
                Notify = new System.Windows.Forms.NotifyIcon();
                Notify.DoubleClick += (s, e) => ShowApp();
                Notify.Icon = new Icon(Application.GetResourceStream(new Uri("pack://application:,,,/Images/DarkFrontierIcon.ico")).Stream);
                Notify.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
                Notify.ContextMenuStrip.Items.Add("Show").Click += (s, e) => ShowApp();
                Notify.ContextMenuStrip.Items.Add("Close").Click += (s, e) => CloseAndCancel(s, new RoutedEventArgs());
                Debug.WriteLine("Started with success!");
                MoveLogo();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
                Application.Current.Shutdown(ExitCode.InternalError);
                return;
            }
        }

        private async void MoveLogo()
        {
            try
            {
                await Task.Delay(1000);
                while (DarkFrontierLogo.Height > 77)
                {
                    Thickness thc = DarkFrontierLogo.Margin;
                    thc.Bottom += 1;
                    Debug.WriteLine("DarkFrontierLogo Bottom: " + thc.Bottom);
                    DarkFrontierLogo.Margin = thc;
                    DarkFrontierLogo.Height -= 1;
                    Debug.WriteLine("DarkFrontierLogo Height: " + DarkFrontierLogo.Height);
                    await Task.Delay(5);
                }
                StatusText.Opacity = 1;
                if (StaticMemory.Operation == Operation.Unpack)
                {
                    Unpack();
                }
                else
                {
                    Download();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
                Application.Current.Shutdown(ExitCode.InternalError);
                return;
            }
        }

        private void Unpack()
        {
            try
            {
                StatusText.Text = "Starting unpacker...";
                StatusText.Text = "Unpacking...";
                DFUnpack dfu = StaticMemory.DFU;
                DirectoryInfo dir = Directory.CreateDirectory(TempController.GeneratedDir.FullName + "/update");
                ZipFile.ExtractToDirectory(TempController.GeneratedDir.FullName + "/" + dfu.Filename, dir.FullName);
                StatusText.Text = "Deleting old version...";
                if (Directory.Exists(dfu.InstallPath))
                {
                    TempController.DestroyDirectory(dfu.InstallPath);
                    Directory.Delete(dfu.InstallPath);
                }
                StatusText.Text = "Moving files...";
                Directory.Move(dir.FullName, dfu.InstallPath);
                StatusText.Text = "Finalizing update...";
                Application.Current.Shutdown(ExitCode.Success);
            } catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
                Application.Current.Shutdown(ExitCode.InternalError);
                return;
            }
        }

        private async void Download()
        {
            try
            {
                StatusText.Text = "Starting downloader...";
                DFInstall dfi = DFInstall.ParseXml(StaticMemory.Args[1]);
                if (dfi == null) return;
                Core.Xml.DarkFrontier darkfile = Core.ServerRequest.GetDarkFrontier(dfi.ServerUrl);
                if (darkfile == null) return;
                dl = new Downloader(StatusText, dfi.ProductName);
                await (StaticMemory.DownloadFile = dl.DownloadFile(darkfile.Filename, darkfile.DownloadUrl));
                if (StaticMemory.Errored) return;
                StatusText.Text = "Copying the DarkFrontier";
                File.Copy(Assembly.GetExecutingAssembly().Location, TempController.GeneratedDir.FullName + "/update.exe");
                StatusText.Text = "Executing DarkFrontier";
                MainWindow.Instance.TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
                Process proc = new Process();
                DFUnpack dfu = new DFUnpack(TempController.InstallId, darkfile.Filename, dfi.InstallPath, darkfile.DFPath);
                proc.StartInfo.Arguments = "unpack \"" + DFUnpack.ToString(dfu) + "\"";
                Debug.WriteLine("Using unpack: " + DFUnpack.ToString(dfu));
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = TempController.GeneratedDir.FullName + "/update.exe";
                Debug.WriteLine("Executable in " + TempController.GeneratedDir.FullName + "/update.exe");
                proc.StartInfo.Verb = "runas";
                Notify.Dispose();
                Debug.WriteLine("Process started? " + (proc.Start() ? "yes" : "no"));
                Application.Current.Shutdown(ExitCode.Unpacking);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
                Application.Current.Shutdown(ExitCode.InternalError);
                return;
            }
        }

        private void CloseAndCancel(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("User has cancelled download...");
            if (dl != null) dl.Webclient.CancelAsync();
            Application.Current.Shutdown(ExitCode.Cancelled);
            return;
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.WindowState = WindowState.Minimized;
        }

        private void RunOnBackground(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Hide();
            Notify.Visible = true;
        }

        private void ShowApp()
        {
            MainWindow.Instance.Show();
            Notify.Visible = false;
        }
    }
}
