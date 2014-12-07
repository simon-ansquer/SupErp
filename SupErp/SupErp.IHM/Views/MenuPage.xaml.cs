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
using SupErp.Shared;
using System.Linq;

namespace SupErp.IHM.Views
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }
        public IEnumerable<IMainMenu> MainMenus { get; set; }

        public MenuPage(IEnumerable<IMainMenu> mainMenus)
        {           
            InitializeComponent();

            MainMenus = mainMenus;
            ScreenHeight = StaticParams.ScreenHeight;
            ScreenWidth = StaticParams.ScreenWidth;
            Menus.ItemsSource = MainMenus;

            SetTextSize();
        }

        private void SetTextSize()
        {
            Logo.FontSize = ScreenHeight / 20;
            LogOut.FontSize = ScreenHeight/40;
            LogOutImage.Width = ScreenHeight/25;
            LogOutImage.Height = ScreenHeight/25;

            //Connexion.FontSize = ScreenHeight / 35;

            //LoginTbl.FontSize = ScreenHeight / 45;
            //LoginTbx.Height = ScreenHeight / 30;
            //LoginTbx.FontSize = ScreenHeight / 50;
            //LoginTbx.Focus();

            //PassTbl.FontSize = ScreenHeight / 45;
            //PassTbx.Height = ScreenHeight / 30;
            //PassTbx.FontSize = ScreenHeight / 50;

            //Connect.Height = ScreenHeight / 25;
            //Connect.Width = (ScreenWidth * 0.4) * 0.3;
            //Connect.FontSize = ScreenHeight / 50;
        }

        private void LogOutPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            LogOut.Foreground = new SolidColorBrush(Color.FromArgb(255, 127, 127, 127));
        }
        private void LogOutPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            LogOut.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        private void LogOutClicked(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MainFrame.Navigate(new LoginPage());
        }

        private void MenuItem_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock t = (sender as TextBlock);

            if (t != null)
            {
                t.FontSize = ScreenHeight / 38;
                t.Height = ScreenHeight / 35;
            }
        }

        
    }
}
