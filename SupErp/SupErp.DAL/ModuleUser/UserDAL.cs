using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (email == null || password == null)
                return null;

            using (var context = new SUPERPEntities(false))
            {
                return context.Users.Include("Role").Include("Role.RoleModules").Include("Role.RoleModules.Module").FirstOrDefault(x => x.Email == email && x.Passwordhash == password);
            }
        }

        #endregion

        #region Read

        public User GetUserById(int id)
        {
            if (id < 0)
                return null;

            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.Users.Include("Role").FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.Users.Include("Role");
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.Roles.Include("RoleModules");
            }
        }

        public Role GetRoleById(int roleId)
        {
            if (roleId < 0)
                return null;

            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.Roles.Include("RoleModules").FirstOrDefault(x => x.Id == roleId);
            }
        }

        public IEnumerable<Module> GetModules()
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.Modules;
            }
        }

        public Role GetRoleByUserId(int userId)
        {
            if (userId < 0)
                return null;

            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var r = context.Users.Find(userId);
                if(r == null)
                    return null;

                return r.Role;
            }
        }

        #endregion

        #region Create

        public User CreateUser(User userToAdd)
        {
            if (userToAdd == null)
                return null;

            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                userToAdd.Passwordhash = Encrypt.hashSHA256(userToAdd.Passwordhash);
                var u = context.Users.Add(userToAdd);
                context.SaveChanges();
                return u;
            }
        }

        public Role CreateRole(Role roleToAdd)
        {
            if (roleToAdd == null)
                return null;

            using (SUPERPEntities context = new SUPERPEntities(false))
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
            if (userToEdit == null)
                return null;

            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var u = context.Users.Find(userToEdit.Id);
                if (u == null)
                    return null;
                if (u.Passwordhash != userToEdit.Passwordhash)
                    userToEdit.Passwordhash = Encrypt.hashSHA256(userToEdit.Passwordhash);
                u = userToEdit;
                context.SaveChanges();
                return u;
            }
        }

        public Role EditRole(Role roleToEdit)
        {
            if (roleToEdit == null)
                return null;

            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var r = context.Roles.Find(roleToEdit.Id);
                if (r == null)
                    return null;
                r = roleToEdit;
                context.SaveChanges();
                return r;
            }
        }

        #endregion

        #region Delete

        public bool DeleteUser(int userId)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    context.Users.Remove(context.Users.Find(userId));
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e )
                {
                    Debug.WriteLine("Echec de suppression de l'utilisateur. Message : " + e.Message);
                    return false;
                }
            }
        }

        public bool DeleteRole(int roleId)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    context.Roles.Remove(context.Roles.Find(roleId));
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Echec de suppression des roles. Message : " + e.Message);
                    return false;
                }
            }
        }

        #endregion
    }
}
