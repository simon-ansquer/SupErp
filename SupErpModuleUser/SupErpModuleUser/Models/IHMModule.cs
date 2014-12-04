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
        public IHMModule()
        { }

        public IHMModule(RoleModule roleModule)
        {
            Id = roleModule.Module.Id;
            Name = roleModule.Module.Name;
            IdRoleModule = roleModule.Id;
        }

        public long Id { get{ return Id; } set { Id = value; OnPropertyChanged("Id"); } }
        public string Name { get; set; }
        public long IdRoleModule { get; set; }
    }
}
