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
                    {
                        var modules = ws.GetModules();
                        if(modules != null)
                            Modules = modules.ToIHMModules().ToList();
                        foreach (var m in Role.Modules)
	                    {
                            IHMModule module = null;
		                    if((module = Modules.FirstOrDefault(x=>x.Id == m.Id)) !=null)
                                module.IsSelected = true;
	                    }
                    }
                    else
                        Modules = new List<IHMModule>();
                }else
                    Role = new IHMRole();
            }
        }

        #region Commands

        public ICommand AddOrUpdateCommand { get { return new DelegateCommand(OnAddOrUpdate); } }
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
                    if (Role.Modules == null)
                        Role.Modules = new List<IHMModule>();
                    IHMModule m = null;
                    if((m = Role.Modules.FirstOrDefault(x=>x.Id == module.Id)) == null)
                    {
                        Role.Modules = Role.Modules.ToList();
                        ((List<IHMModule>)Role.Modules).Add(module);
                    }
                }else
                {
                    IHMModule m = null;
                    if ((m = Role.Modules.FirstOrDefault(x => x.Id == module.Id)) != null)
                    {
                        Role.Modules = Role.Modules.ToList();
                        ((List<IHMModule>)Role.Modules).Remove(Role.Modules.FirstOrDefault(x=>x.Id == m.Id));
                    }
                }
            }

            if (Role.IsNew)
            {
                using (UserServiceClient ws = new UserServiceClient())
                {
                    var newRole = Role.ToRole();
                    if (newRole != null)
                        ws.CreateRole(newRole);
                }
            }
            else
            {
                using (UserServiceClient ws = new UserServiceClient())
                {
                    var newRole = Role.ToRole();
                    if (newRole != null)
                        ws.EditRole(newRole);
                }
            }

            Switcher.Switch(new ListRoleUserControl());
        }

        private void OnCancel() 
        {
            Switcher.Switch(new ListRoleUserControl());
        }

        #endregion
    }
}
