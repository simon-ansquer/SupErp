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

namespace SupErp.IHM
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            ScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            ScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            Main.Width = ScreenWidth;
            Main.Height = ScreenHeight;

            SetTextSize();
        }

        private void Main_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetTextSize();                                //Si taille de la fenêtre change, on appelle la méthode qui redimentionne les éléments.
        }

        private void Main_StateChanged(object sender, EventArgs e)
        {
            if(Main.WindowState == WindowState.Maximized)
            {
                Main.Width = ScreenWidth;
                Main.Height = ScreenHeight;
            }
            SetTextSize();
        }

        private void SetTextSize()
        {
            Logo.FontSize = Main.Height / 20;

            Connexion.FontSize = Main.Height / 35;

            LoginTbl.FontSize = Main.Height / 45;
            LoginTbx.Height = Main.Height / 30;
            LoginTbx.FontSize = Main.Height / 50;
            LoginTbx.Focus();

            PassTbl.FontSize = Main.Height / 45;
            PassTbx.Height = Main.Height / 30;
            PassTbx.FontSize = Main.Height / 50;

            Connect.Height = Main.Height / 25;
            Connect.Width = (Main.Width * 0.4) * 0.3;
            Connect.FontSize = Main.Height / 50;
        }
    }
}
