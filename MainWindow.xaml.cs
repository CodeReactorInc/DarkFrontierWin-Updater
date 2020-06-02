using CodeReactor.DarkFrontier.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace CodeReactor.DarkFrontier
{
    /// <summary>
    /// Lógica interna para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; set; }
        public bool Iniatilized = false;

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            Debug.WriteLine("MainWindow ready");
        }

        protected override void OnContentRendered(EventArgs e)
        {
            if (!Iniatilized)
            {
                Iniatilized = true;
                base.OnContentRendered(e);
                Debug.WriteLine("MainWindow rendered");
                Content = new Views.SplashScreen();
            }
            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }
    }
}
