using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UserControl_GestionClient.ClientServiceGestionClient;
using UserControl_GestionClient.Helpers;
using UserControl_GestionClient.Models;
using UserControl_GestionClient.Views;
using System.ServiceModel;

namespace UserControl_GestionClient.ViewModels
{
    class AccueilGestionClientViewModel : NotificationObject
    {
        public List<Models.Company> Companies { get; set; }

        public AccueilGestionClientViewModel()
        {
            //DisplayData();
            //RandomizeData();

            using (var ws = new ClientServiceGestionClient.ServiceGestionClientClient())
            {
                var companies = ws.GetListCompany();
                if (companies != null)
                    Companies = companies.ToCompanies().ToList();
                else
                    Companies = new List<Models.Company>();
                CompanyCollection = new ObservableCollection<Models.Company>(Companies);
            }
        }

        private void DisplayData()
        {
            throw new NotImplementedException();
        }
        private ObservableCollection<Models.Company> _companyCollection;

        public ObservableCollection<Models.Company> CompanyCollection
        {
            get { return _companyCollection; }
            set
            {
                if (_companyCollection != value)
                {
                    _companyCollection = value;
                    RaisePropertyChanged(() => CompanyCollection);
                }
            }
        }

        private void RandomizeData()
        {
            CompanyCollection = new ObservableCollection<Models.Company>();

            for (var i = 0; i < 10; i++)
            {
                //CompanyCollection.Add(new Company { id = i, address = "4" + i + " allée d'orléan", city = "Bordeaux", name = "neo", postalcode = 33000, siret = "00000000" + i });
            }
        }

        public ICommand AddCustomer { get { return new DelegateCommand(AddNewCustomer); } }
        private void AddNewCustomer(object parameter)
        {
            
        }
    }
}