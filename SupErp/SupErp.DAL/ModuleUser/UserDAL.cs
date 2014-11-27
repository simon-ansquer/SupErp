using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.DAL.ModuleUser
{
    public class UserDAL
    {

        #region Authentication

        public User Login(string email, string password)
        {

            using (var context = new SUPERPEntities(false))
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                return context.Users.Include("Role").Include("Role.RoleModules").Include("Role.RoleModules.Module").FirstOrDefault(x => x.Email == email && x.Passwordhash == password);
            }
        }

        #endregion

        #region Read

        public User GetUserById(int id)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.Users.Include("Role").FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.Users.Include("Role");
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.Roles.Include("RoleModules");
            }
        }

        public Role GetRoleByUserId(int userId)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.Users.Find(userId).Role;
            }
        }

        #endregion

        #region Create

        public User CreateUser(User userToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var u = context.Users.Add(userToAdd);
                context.SaveChanges();
                return u;
            }
        }

        public Role CreateRole(Role roleToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var r = context.Roles.Add(roleToAdd);
                context.SaveChanges();
                return r;
            }
        }

        #endregion

        #region Edit

        public User EditUser(User userToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var u = context.Users.Find(userToEdit.Id);
                u = userToEdit;
                context.SaveChanges();
                return u;
            }
        }

        public Role EditRole(Role roleToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var r = context.Roles.Find(roleToEdit.Id);
                r = roleToEdit;
                context.SaveChanges();
                return r;
            }
        }

        #endregion

        #region Delete

        public bool DeleteUser(int userId)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                try
                {
                    context.Users.Remove(context.Users.Find(userId));
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool DeleteRole(int roleId)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                try
                {
                    context.Roles.Remove(context.Roles.Find(roleId));
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion
    }
}
