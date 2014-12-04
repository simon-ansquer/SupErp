using SupErpModuleUser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupErpModuleUser.UserService;
using SupErpModuleUser.Helpers;
using SupErpModuleUser.Models;

namespace SupErpModuleUser.ViewModels
{
    public class AddUserViewModel
    {
        public IHMUser User { get; set; }
        public IEnumerable<IHMRole> Roles { get; set; }

        public AddUserViewModel()
        {
            User = new IHMUser();
            //using (UserServiceClient wcf = new UserServiceClient())
            //{
            //    Roles = wcf.GetRoles().ToRoles();
            //}
        }

        public AddUserViewModel(User user)
        {
            User = user.ToIHMUser();
        }

        #region Commands

        public ICommand AddOrUpdateCommand { get { return new DelegateCommand(OnAddOrUpdate); } }
        public ICommand CancelCommand { get { return new DelegateCommand(OnCancel); } }

        #endregion

        #region Command Handlers
        private void OnAddOrUpdate()
        { 
        
        }

        private void OnCancel()
        { 

        }

        #endregion
    }
}
