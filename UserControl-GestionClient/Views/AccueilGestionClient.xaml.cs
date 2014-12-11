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
using UserControl_GestionClient.Models;

namespace UserControl_GestionClient.Views
{
    /// <summary>
    /// Logique d'interaction pour AccueilGestionClient.xaml
    /// </summary>
    public partial class AccueilGestionClient : UserControl
    {
        public AccueilGestionClient()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.GetNavigationService(this).Navigate(new CreateCustomer());
            Switcher.Switch(new CreateCustomer());
        }

        private void CompanyDetails(object sender, RoutedEventArgs e)
        {
            var obj = (Company)dataGrid1.SelectedItem;
            NavigationService.GetNavigationService(this).Navigate(new CreateCustomer());
        }

        private void CompanyDelete(object sender, RoutedEventArgs e)
        {
            var obj = (Company)dataGrid1.SelectedItem;

            using (var ws = new ClientServiceGestionClient.ServiceGestionClientClient())
            {
                ws.DeleteCompany((int)obj.id);
            }
        }
    }
}