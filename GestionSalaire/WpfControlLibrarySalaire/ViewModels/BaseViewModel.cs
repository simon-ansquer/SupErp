using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using WpfControlLibrarySalaire.ServiceSalaire;

namespace WpfControlLibrarySalaire.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        internal readonly ServiceSalaireClient ServiceSalaire;
        ObservableCollection<User> _employees;
        public ObservableCollection<User> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                if (_employees != value)
                {
                    _employees = value;
                    RaisePropertyChanged(() => Employees);
                }
            }
        }

        protected BaseViewModel()
        {
            if(ServiceSalaire == null)
                ServiceSalaire = new ServiceSalaireClient();
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            RaisePropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }  
    }
}
