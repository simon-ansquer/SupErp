using SupErpModuleUser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SupErpModuleUser.ViewModels
{
    public class AddRoleViewModel
    {

        #region Commands

        public ICommand AddCommand { get { return new DelegateCommand(OnAdd); } }
        public ICommand CancelCommand { get { return new DelegateCommand(OnCancel); } }
        public ICommand UpdateCommand { get { return new DelegateCommand(OnUpdate); } }

        #endregion

        #region Command Handlers
        private void OnAdd()
        { }

        private void OnCancel() 
        { }

        private void OnUpdate()
        { }

        #endregion
    }
}
