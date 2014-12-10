using SupErpModuleUser.UserService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErpModuleUser.Helpers;

namespace SupErpModuleUser.Models
{
    public class IHMRole : IHMModel
    {

        private long id;
        private string label;
        private IEnumerable<IHMModule> modules;
        private bool isNew;
        private string listModules;

        public IHMRole()
        {
            isNew = true;
        }

        public IHMRole(Role role)
        {
            Id = role.Id;
            Label = role.Label;
            Modules = GetModules(role.RoleModules);
            isNew = false;

            ModulesDictionnary dictionnary = new ModulesDictionnary();
            StringBuilder sb = new StringBuilder();
            foreach (var m in Modules)
            {
                sb.Append(dictionnary.Modules[m.Name] + " / ");
            }
            ListModules = sb.ToString();
        }

        private IEnumerable<IHMModule> GetModules(IEnumerable<RoleModule> roleModules)
        {
            foreach (RoleModule roleModule in roleModules)
            {
                yield return roleModule.ToIHMModule();
            }
        }

        public long Id
        {
            get { return id; }
            set { 
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Label
        {
            get { return label; }
            set { 
                label = value;
                OnPropertyChanged("Label");
            }
        }

        public IEnumerable<IHMModule> Modules
        {
            get { return modules; }
            set { 
                modules = value;

                string listModule = string.Empty;
                foreach (IHMModule module in Modules)
                {
                    listModule += module.Name;
                }

                ListModules = listModule;

                OnPropertyChanged("Modules");
            }
        }

        public bool IsNew
        {
            get { return isNew; }
            set { 
                isNew = value;
                OnPropertyChanged("IsNew");
            }
        }

        public string ListModules
        {
            get { return listModules; }
            set
            {
                listModules = value;
                OnPropertyChanged("ListModules");
            }
        }

    }
}
