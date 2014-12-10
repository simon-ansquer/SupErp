using SupErpModuleUser.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErpModuleUser.Models
{
    public class IHMModule : IHMModel
    {
        private long id;
        private string name;
        private long idRoleModule;
        private bool isSelected;
        private bool isWritingSelected;

        public IHMModule()
        { }

        public IHMModule(RoleModule roleModule)
        {
            Id = roleModule.Module.Id;
            Name = roleModule.Module.Name;
            IdRoleModule = roleModule.Id;
        }

        public IHMModule(Module module)
        {
            Id = module.Id;
            Name = module.Name;
        }

        public long Id { 
            get{ return id; } 
            set { id = value; OnPropertyChanged("Id"); } 
        }
        public string Name { 
            get { return name; } 
            set { name = value; OnPropertyChanged("Name"); } 
        }
        public long IdRoleModule { 
            get {return idRoleModule; } 
            set {idRoleModule = value; OnPropertyChanged("IdRoleModule"); } 
        }
        public bool IsSelected { 
            get { return isSelected; } 
            set { isSelected = value; OnPropertyChanged("IsSelected"); } 
        }
        public bool IsWritingSelected { 
            get { return isWritingSelected; } 
            set { isWritingSelected = value; OnPropertyChanged("IsWritingSelected"); } 
        }
    }
}
