using System.Windows;
using System.Windows.Controls;
using WpfControlLibrarySalaire.Views;

namespace WpfApplicationSalaire
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Frame uneFrame = new Frame();
            MainGrid.Children.Add(uneFrame);
            uneFrame.Navigate(new EmployeeHistory());
        }
    }
}
