using SupErpModuleUser.Models;
using SupErpModuleUser.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErpModuleUser.Helpers
{
    public static class UserAdapter
    {
        public static IHMUser ToIHMUser(this User user)
        {
            return new IHMUser(user);
        }

        public static User ToUser(this IHMUser user)
        {
            return new User()
            {
                Id = user.Id,
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Passwordhash = user.Password,
                Address = user.Address,
                Role = user.Role.ToRole(),
                Role_id = user.Role.Id,
                City = user.City,
                Zip_code = user.Zipcode
            };
        }

        public static IEnumerable<IHMUser> ToIHMUser(this IEnumerable<User> users)
        {
            foreach(User user in users)
            {
                yield return user.ToIHMUser();
            }
        }
    }
}
