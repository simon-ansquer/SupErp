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
using UserControl_GestionClient.ViewModels;

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

        private void CreateCustomer(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CreateCustomer());
        }
        private void CreateContact(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new CreateContact());
        }

        //private void CompanyDetails(object sender, RoutedEventArgs e)
        //{
        //    Company obj = (Company)dataGrid1.SelectedItem;
        //    var employeeDetails = new DetailCustomer(new DetailCustomerViewModel(obj));
        //    Switcher.Switch(employeeDetails);
        //}

        private void CompanyDelete(object sender, RoutedEventArgs e)
        {
            var obj = (Company)dataGrid1.SelectedItem;

            using (var ws = new ClientServiceGestionClient.ServiceGestionClientClient())
            {
                ws.DeleteCompany((int)obj.id);

                Switcher.Switch(new AccueilGestionClient());
            }
        }
    }
}