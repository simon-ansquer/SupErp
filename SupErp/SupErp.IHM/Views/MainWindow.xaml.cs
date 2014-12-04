using System;
using System.Collections.Generic;
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
using SupErp.IHM.Helpers;
using SupErp.IHM.Models;
using SupErp.IHM.ViewModels;

namespace SupErp.IHM
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }
        public static Frame MainFrame;

        public MainWindow()
        {
            InitializeComponent();

            StaticParams.ScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            StaticParams.ScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            Main.Width = ScreenWidth;
            Main.Height = ScreenHeight;
            MainFrame = new Frame();
            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden; 

            Main.AddChild(MainFrame);

            MainFrame.Navigate(new LoginPage());
        }
    }
}
