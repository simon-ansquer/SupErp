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
        public IHMRole()
        { }

        public IHMRole(Role role)
        {
            Id = role.Id;
            Label = role.Label;
            Modules = GetModules(role.RoleModules);
        }

        private IEnumerable<IHMModule> GetModules(IEnumerable<RoleModule> roleModules)
        {
            foreach (RoleModule roleModule in roleModules)
            {
                yield return roleModule.ToIHMModule();
            }
        }

        private long id;
        private string label;
        private IEnumerable<IHMModule> modules;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public IEnumerable<IHMModule> Modules
        {
            get { return modules; }
            set { modules = value; }
        }

    }
}
