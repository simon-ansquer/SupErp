using SupErpModuleUser.Models;
using SupErpModuleUser.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErpModuleUser.Helpers
{
    public static class ModuleAdapter
    {
        public static IHMModule ToIHMModule(this RoleModule module)
        {
            return new IHMModule(module);
        }

        public static IEnumerable<RoleModule> GetRoleModules(this IHMRole role)
        {
            foreach (var module in role.Modules)
            {
                yield return new RoleModule()
                {
                    Id = module.IdRoleModule,
                    Role = role.ToRole(),
                    Role_id = role.Id,
                    Module_id = module.IdRoleModule,
                    Module = new Module()
                    {
                        Id = module.Id,
                        Name = module.Name
                    }
                };
            }
        }

        public static IEnumerable<IHMModule> ToIHMModules(this IEnumerable<Module> modules)
        {
            foreach (var m in modules)
            {
                yield return new IHMModule(m);
            }
        }
    }
}
