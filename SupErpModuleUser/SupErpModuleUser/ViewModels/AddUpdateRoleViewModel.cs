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

            //Modules = new UserService.UserServiceClient().GetModules();

            IHMModule module1 = new IHMModule();
            module1.Name = "Utilisateur";
            IHMModule module2 = new IHMModule();
            module2.Name = "Salaire";
            IHMModule module3 = new IHMModule();
            module3.Name = "LOL";

            Modules = new List<IHMModule>();
            Modules.Add(module1);
            Modules.Add(module2);
            Modules.Add(module3);
        }

        public AddUpdateRoleViewModel(int roleId)
        {
            //Role = role.ToIHMRole();
            Role = new IHMRole();
            Role.Label = "Test";
            //Modules = new UserService.UserServiceClient().GetModules();

            IHMModule module1 = new IHMModule();
            module1.Name = "Utilisateur";
            IHMModule module2 = new IHMModule();
            module2.Name = "Salaire";

            Modules = new List<IHMModule>();
            Modules.Add(module1);
            Modules.Add(module2);
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
        }

        private void OnCancel() 
        {
            Switcher.Switch(new ListRoleUserControl());
        }

        #endregion
    }
}
