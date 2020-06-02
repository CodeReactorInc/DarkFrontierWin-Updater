using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;

namespace CodeReactor.DarkFrontier.Core
{
    public class Downloader
    {
        TextBlock Txtblock;
        public WebClient Webclient;
        string Product;
        Stopwatch Sw;

        public Downloader(TextBlock txtblock, string product)
        {
            Txtblock = txtblock;
            Product = product;
            Webclient = new WebClient();
            Sw = new Stopwatch();
            Webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChangedEvent);
            Webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompleteChangedEvent);
        }

        public Task DownloadFile(string filename, string url)
        {
            StaticMemory.Errored = false;
            MainWindow.Instance.TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
            Sw.Start();
            return Webclient.DownloadFileTaskAsync(url, TempController.GenTempDir().FullName + "/" + filename);
        }

        void ProgressChangedEvent(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                Debug.WriteLine("Downloading " + Product + ": " + e.BytesReceived + "/" + e.TotalBytesToReceive + " (" + e.ProgressPercentage + "%)");
                Txtblock.Text = "Downloading " + Product + "\n" +
                    e.BytesReceived / 1024 / 1024 + "MB/" + e.TotalBytesToReceive / 1024 / 1024 + "MB (" + e.ProgressPercentage + "%)\n" +
                    (int) Math.Round(e.BytesReceived / 1024 / Sw.Elapsed.TotalSeconds) + "kbps";
                MainWindow.Instance.TaskbarItemInfo.ProgressValue = (double)e.ProgressPercentage / 100;
            } catch (DivideByZeroException) { }
        }

        void CompleteChangedEvent(object sender, AsyncCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                Txtblock.Text = "Unable to download";
                StaticMemory.Errored = true;
                return;
            }
            Debug.WriteLine("Successfully downloaded " + Product);
            Txtblock.Text = "Successfully downloaded";
        }
    }
}
