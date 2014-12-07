using System.Windows.Controls;
using WpfControlLibrarySalaire.Views;

namespace WpfApplicationSalaire
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var uneFrame = new Frame();
            MainGrid.Children.Add(uneFrame);
            uneFrame.Navigate(new PageSwitcher());
        }
    }
}
