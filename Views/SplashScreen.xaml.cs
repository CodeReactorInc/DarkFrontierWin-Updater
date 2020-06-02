using CodeReactor.DarkFrontier.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodeReactor.DarkFrontier.Views
{
    /// <summary>
    /// Interação lógica para SplashScreen.xam
    /// </summary>
    public partial class SplashScreen : UserControl
    {
        public SplashScreen()
        {
            try
            {
                InitializeComponent();
                Debug.WriteLine("Loading SplashScreen...");
                ChangeScreen();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
                Application.Current.Shutdown(ExitCode.InternalError);
                return;
            }
        }

        void CloseAndCancel(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(ExitCode.Cancelled);
            return;
        }

        public void Minimize(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.WindowState = WindowState.Minimized;
        }

        private async void ChangeScreen()
        {
            try
            {
                await Task.Delay(1800);
                while (CodeReactorLogo.Opacity > 0)
                {
                    CodeReactorLogo.Opacity -= 0.01;
                    Debug.WriteLine("CodeReactor.img Opacity: " + CodeReactorLogo.Opacity);
                    await Task.Delay(5);
                }
                Debug.WriteLine("Waiting a more time");
                await Task.Delay(700);
                Debug.WriteLine("Loading DLScreen...");
                MainWindow.Instance.Content = new DLScreen();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
                Application.Current.Shutdown(ExitCode.InternalError);
                return;
            }
        }
    }
}
