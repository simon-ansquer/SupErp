using SupErpModuleUser.Models;
using SupErpModuleUser.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErpModuleUser.Helpers
{
    public static class RoleAdapter
    {
        public static IHMRole ToIHMRole (this Role role)
        {
            return new IHMRole(role);
        }

        public static Role ToRole (this IHMRole role)
        {
            return new Role()
            {
                Id = role.Id,
                Label = role.Label,
                RoleModules = role.GetRoleModules().ToList()
            };
        }

        public static IEnumerable<IHMRole> ToRoles(this IEnumerable<Role> roles)
        {
            foreach (var r in roles)
            {
                yield return r.ToIHMRole();
            }
        }

        public static IEnumerable<Role> ToRoles(this IEnumerable<IHMRole> roles)
        {
            foreach (var r in roles)
            {
                yield return r.ToRole();
            }
        }
    }
}
