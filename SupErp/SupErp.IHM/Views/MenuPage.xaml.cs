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
using SupErp.Shared;

namespace SupErp.IHM.Views
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }

        public MenuPage(IEnumerable<IMainMenu> mainMenus)
        {           
            InitializeComponent();

            ScreenHeight = StaticParams.ScreenHeight;
            ScreenWidth = StaticParams.ScreenWidth;
            SetTextSize();
        }

        private void SetTextSize()
        {
            Logo.FontSize = ScreenHeight / 20;

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
    }
}
