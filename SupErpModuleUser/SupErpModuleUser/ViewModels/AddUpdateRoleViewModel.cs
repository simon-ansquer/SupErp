using SupErpModuleUser.Helpers;
using SupErpModuleUser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupErpModuleUser.Helpers;
using SupErpModuleUser.UserService;

namespace SupErpModuleUser.ViewModels
{
    public class AddUpdateRoleViewModel
    {
        public IHMRole Role { get; set; }
        public List<IHMModule> Modules { get; set; }

        public AddUpdateRoleViewModel()
        {
            Role = new IHMRole();

            using (UserServiceClient ws = new UserServiceClient())
            {
                var rep = ws.GetModules();
                if (rep != null)
                    Modules = rep.ToIHMModules().ToList();
            }
        }

        public AddUpdateRoleViewModel(int roleId)
        {
            using(UserServiceClient ws = new UserServiceClient())
            {
                var rep = ws.GetRoleById(roleId);
                if (rep != null)
                {
                    Role = rep.ToIHMRole();
                    if (Role.Modules != null)
                        Modules = Role.Modules.ToList();
                    else
                        Modules = new List<IHMModule>();
                }
                Role = new IHMRole();
            }
        }

        #region Commands

        public ICommand AddCommand { get { return new DelegateCommand(OnAddOrUpdate); } }
        public ICommand CancelCommand { get { return new DelegateCommand(OnCancel); } }

        #endregion

        #region Command Handlers
        private void OnAddOrUpdate()
        {
            //TODO Terminer l'ajout des modules dans le role
            foreach(IHMModule module in Modules)
            {
                if(module.IsSelected)
                {
                    Role.Modules.ToList().Add(module);
                }
            }

            if (Role.IsNew)
                new UserService.UserServiceClient().CreateRole(Role.ToRole());
            else
                new UserService.UserServiceClient().EditRole(Role.ToRole());

            Switcher.Switch(new ListRoleUserControl());
        }

        private void OnCancel() 
        {
            Switcher.Switch(new ListRoleUserControl());
        }

        #endregion
    }
}
