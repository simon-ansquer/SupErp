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
    public class ListRoleViewModel
    {
        public List<IHMRole> Roles { get; set; }

        public ListRoleViewModel()
        {
            using (var ws = new UserServiceClient())
            {
                var roles = ws.GetRoles();
                if (roles != null)
                    Roles = roles.ToRoles().ToList();
                else
                    Roles = new List<IHMRole>();
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

            if (RoleListPageSwitcher.RoleListSwitcherFrame != null)
                RoleListPageSwitcher.RoleListSwitcherFrame.Navigate(new AddUpdateRoleUserControl(new AddUpdateRoleViewModel(int.Parse(id.ToString()))));
            else
                RoleCreatePageSwitcher.RoleCreateSwitcherFrame.Navigate(new AddUpdateRoleUserControl(new AddUpdateRoleViewModel(int.Parse(id.ToString()))));
          //  Switcher.Switch(new AddUpdateRoleUserControl(new AddUpdateRoleViewModel(int.Parse(id.ToString())))); 
        }

        #endregion

    }
}
