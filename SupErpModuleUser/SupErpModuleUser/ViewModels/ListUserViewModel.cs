using SupErpModuleUser.Helpers;
using SupErpModuleUser.Models;
using SupErpModuleUser.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupErpModuleUser.Views;

namespace SupErpModuleUser.ViewModels
{
    public class ListUserViewModel
    {
        public List<IHMUser> Users { get; set; }

        public ListUserViewModel()
        {
            using (UserServiceClient ws = new UserServiceClient())
            {
                var users = ws.GetUsers();
                if (users != null)
                    Users = users.ToIHMUser().ToList();
                else
                    Users = new List<IHMUser>();
            }
        
        }

        #region Commands

        public ICommand ImageCommand
        {
            get
            {
                return new ActionCommand(param =>
                {
                    OnImageClicked(param);
                });
            }
        }

        #endregion

        #region Command Handlers

        private void OnImageClicked(object id)
        {
            if (UserListPageSwitcher.UserListSwitcherFrame != null)
                UserListPageSwitcher.UserListSwitcherFrame.Navigate(new AddUpdateUserUserControl(new AddUpdateUserViewModel(int.Parse(id.ToString()))));
            else
                UserCreatePageSwitcher.UserCreateSwitcherFrame.Navigate(new AddUpdateUserUserControl(new AddUpdateUserViewModel(int.Parse(id.ToString()))));
            //Switcher.Switch(new AddUpdateUserUserControl(new AddUpdateUserViewModel(int.Parse(id.ToString()))));
        }

        #endregion
    }
}
