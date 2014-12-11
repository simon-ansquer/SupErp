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
using UserControl_GestionClient.Helpers;
using UserControl_GestionClient.ViewModels;

namespace UserControl_GestionClient.Views
{
    /// <summary>
    /// Logique d'interaction pour DetailCustomer.xaml
    /// </summary>
    public partial class DetailCustomer : UserControl
    {
        public DetailCustomer(DetailCustomerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Retour(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new AccueilGestionClient());
        }
    }
}
