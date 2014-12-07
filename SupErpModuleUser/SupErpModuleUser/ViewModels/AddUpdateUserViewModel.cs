using SupErpModuleUser.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SupErpModuleUser.UserService;
using SupErpModuleUser.Helpers;
using SupErpModuleUser.Models;

namespace SupErpModuleUser.ViewModels
{
    public class AddUpdateUserViewModel
    {
        public IHMUser User { get; set; }
        public List<IHMRole> Roles { get; set; }
        public IHMRole SelectedRole { get; set; }

        public AddUpdateUserViewModel()
        {
            User = new IHMUser();
            User.IsNew = true;
            //using (UserServiceClient wcf = new UserServiceClient())
            //{
            //    Roles = wcf.GetRoles().ToRoles();
            //}
        }

        public AddUpdateUserViewModel(int userId)
        {
            //User = new UserServiceClient().GetUserById(userId).ToIHMUser();
            //Roles = new UserServiceClient().GetRoles();

            //IHMRole role1 = new IHMRole();
            //role1.Label = "Role1";
            //role1.Id = 1;
            //IHMRole role2 = new IHMRole();
            //role2.Label = "Role2";
            //role2.Id = 2;
            //IHMRole role3 = new IHMRole();
            //role3.Label = "Role3";
            //role3.Id = 3;

            //User = new IHMUser();
            //User.Id = 1;
            //User.Role = role2;
            //User.Firstname = "Eliott";
            //User.Lastname = "Lujan";
            //User.Email = "eliott.lujan@gmail.com";
            //User.Address = "179 rue Camille Godard";
            //User.IsNew = false;

            //Roles = new List<IHMRole>();
            //Roles.Add(role1);
            //Roles.Add(role2);
            //Roles.Add(role3);


            ////A conserver après les appels en place du WS
            //foreach (IHMRole role in Roles)
            //{
            //    if (role.Id == User.Role.Id)
            //        SelectedRole = role;
            //}

            using (UserServiceClient ws = new UserServiceClient())
            {
                var rep = ws.GetUserById(userId);
                if(rep != null)
                {
                    User = rep.ToIHMUser();
                    var roles = ws.GetRoles();
                    if (roles != null)
                        Roles = roles.ToRoles().ToList();
                    else
                        Roles = new List<IHMRole>();
                    SelectedRole = User.Role;
                }
            }
        }

        #region Commands

        public ICommand AddOrUpdateCommand { get { return new DelegateCommand(OnAddOrUpdate); } }
        public ICommand CancelCommand { get { return new DelegateCommand(OnCancel); } }

        #endregion

        #region Command Handlers
        private void OnAddOrUpdate()
        {
            //TODO Terminer l'ajout du user
            if (User.IsNew)
                new UserService.UserServiceClient().CreateUser(User.ToUser());
            else
                new UserService.UserServiceClient().EditUser(User.ToUser());

            Switcher.Switch(new ListUserUserControl());
        }

        private void OnCancel()
        {
            Switcher.Switch(new ListUserUserControl());
        }

        #endregion
    }
}
