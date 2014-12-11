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
            if(email != null && password != null)
            {
            using (var context = new SUPERPEntities(false))
            {
                    password = Encrypt.hashSHA256(password);
                return context.Users.Include("Role").Include("Role.RoleModules").Include("Role.RoleModules.Module").FirstOrDefault(x => x.Email == email && x.Passwordhash == password);
            }
        }
            return null;
        }

        #endregion

        #region Read

        public User GetUserById(int id)
        {
            if (id < 0)
                return null;

            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.Users.Include("Role").Include("Role.RoleModules").Include("Role.RoleModules.Module").FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.Users.Include("Role").Include("Role.RoleModules").Include("Role.RoleModules.Module").ToList();
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.Roles.Include("RoleModules").Include("RoleModules.Module").Include("RoleModules.Role").ToList();
            }
        }

        public Role GetRoleById(int roleId)
        {
            if (roleId < 0)
                return null;

            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.Roles.Include("RoleModules").Include("RoleModules.Module").Include("RoleModules.Role").FirstOrDefault(x => x.Id == roleId);
            }
        }

        public IEnumerable<Module> GetModules()
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.Modules.Include("RoleModules").Include("RoleModules.Module").Include("RoleModules.Role").ToList();
            }
        }

        public Role GetRoleByUserId(int userId)
        {
            if (userId < 0)
                return null;

            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var r = context.Users.Include("Role").FirstOrDefault(x => x.Id == userId);
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
                var r = context.Roles.Add(new Role()
                    {
                        Label = roleToAdd.Label,
                        Users = new List<User>(),
                        RoleModules = new List<RoleModule>()
                    });
                context.SaveChanges();

                foreach(RoleModule rm in roleToAdd.RoleModules)
                {
                    context.RoleModules.Add(new RoleModule() { 
                        Role_id = r.Id,
                        Module_id = rm.Module_id
                    });
                }

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

                u.Address = userToEdit.Address;
                u.Email = userToEdit.Email;
                u.Firstname = userToEdit.Firstname;
                u.Lastname = userToEdit.Lastname;

                if (userToEdit.Role != null)
                {
                    u.Role = context.Roles.Find(userToEdit.Role.Id);
                    u.Role_id = u.Role.Id;
                }

                u.Zip_code = userToEdit.Zip_code;
                u.City = userToEdit.City;
                context.SaveChanges();
                return u;
            }
        }

        public Role EditRole(Role roleToEdit)
        {
            if (roleToEdit == null)
                return null;

            var listToAdd = new List<RoleModule>();

            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var r = context.Roles.Include("RoleModules").Include("RoleModules.Module").Include("RoleModules.Role").FirstOrDefault(x => x.Id == roleToEdit.Id);
                if (r == null)
                    return null;
                r.RoleModules.Clear();
                foreach (var rm in roleToEdit.RoleModules)
                {
                    RoleModule rrm = null;
                    if ((rrm = r.RoleModules.FirstOrDefault(x => x.Id == rm.Id)) == null)
                    {
                        listToAdd.Add(new RoleModule()
                        {
                            Module = rm.Module,
                            Module_id = rm.Module_id,
                            Role = rm.Role,
                            Role_id = rm.Role_id
                        });
                    }
                }

                context.RoleModules.AddRange(listToAdd);

                r.Label = roleToEdit.Label;
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
