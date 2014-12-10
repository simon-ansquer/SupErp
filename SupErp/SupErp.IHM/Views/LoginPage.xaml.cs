using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
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
using SupErp.IHM.Views;

namespace SupErp.IHM.ViewModels
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public double ScreenWidth { get; set; }
        public double ScreenHeight { get; set; }

        public LoginPage()
        {
            InitializeComponent();

            RedefineScreenSize();
            
            SetTextSize();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            RedefineScreenSize();
            SetTextSize();
        }

        private void SetTextSize()
        {
            Logo.FontSize = ScreenHeight / 20;

            Connexion.FontSize = ScreenHeight / 35;

            LoginTbl.FontSize = ScreenHeight / 45;
            LoginTbx.Height = ScreenHeight / 30;
            LoginTbx.FontSize = ScreenHeight / 50;
            LoginTbx.Focus();

            PassTbl.FontSize = ScreenHeight / 45;
            PassTbx.Height = ScreenHeight / 30;
            PassTbx.FontSize = ScreenHeight / 50;

            Connect.Height = ScreenHeight / 25;
            Connect.Width = (ScreenWidth * 0.4) * 0.3;
            Connect.FontSize = ScreenHeight / 50;
        }

        private void LeftTape_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Connect_Clicked(sender, e);
                ButtonAutomationPeer peer = new ButtonAutomationPeer(Connect);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                if(invokeProv != null)
                    invokeProv.Invoke();
            }
        }

        private void Connect_Clicked(object sender, RoutedEventArgs e)
        {
            ProgressRing.IsActive = true;
        }

        public void RedefineScreenSize()
        {
            ScreenHeight = StaticParams.ScreenHeight;
            ScreenWidth = StaticParams.ScreenWidth;
        }
        
    }
}
