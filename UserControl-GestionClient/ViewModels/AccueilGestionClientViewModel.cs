using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UserControl_GestionClient.Helpers;
using UserControl_GestionClient.Models;
using UserControl_GestionClient.Views;

namespace UserControl_GestionClient.ViewModels
{
    class AccueilGestionClientViewModel : NotificationObject
    {
        public AccueilGestionClientViewModel()
        {
            //DisplayData();
            RandomizeData();
        }

        private void DisplayData()
        {
            throw new NotImplementedException();
        }
        private ObservableCollection<Company> _companyCollection;

        public ObservableCollection<Company> CompanyCollection
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
            CompanyCollection = new ObservableCollection<Company>();

            for (var i = 0; i < 10; i++)
            {
                CompanyCollection.Add(new Company { id = i, address = "4" + i + " allée d'orléan", city = "Bordeaux", name = "neo", postalcode = 33000, siret = "00000000" + i });
            }
        }

        public ICommand AddCustomer { get { return new DelegateCommand(AddNewCustomer); } }
        private void AddNewCustomer(object parameter)
        {
            
        }
    }
}