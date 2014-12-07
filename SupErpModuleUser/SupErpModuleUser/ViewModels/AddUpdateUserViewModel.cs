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
    public class AddUpdateUserViewModel
    {
        public IHMUser User { get; set; }
        public List<IHMRole> Roles { get; set; }

        public AddUpdateUserViewModel()
        {
            User = new IHMUser();
            User.IsNew = true;
            //using (UserServiceClient wcf = new UserServiceClient())
            //{
            //    Roles = wcf.GetRoles().ToRoles();
            //}
        }

        public AddUpdateUserViewModel(int userId)
        {
            //User = new UserServiceClient().GetUserById(userId).ToIHMUser();

            IHMRole role1 = new IHMRole();
            role1.Label = "Role1";

            User = new IHMUser();
            User.Id = 1;
            User.Role = role1;
            User.Firstname = "Eliott";
            User.Lastname = "Lujan";
            User.Email = "eliott.lujan@gmail.com";
            User.Address = "179 rue Camille Godard";
            User.IsNew = false;

            Roles = new List<IHMRole>();
            Roles.Add(role1);
            Roles.Add(role1);
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
            Switcher.Switch(new ListUserUserControl());
        }

        #endregion
    }
}
