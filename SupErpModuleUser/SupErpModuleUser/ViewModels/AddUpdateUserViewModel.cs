﻿using SupErpModuleUser.Helpers;
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
            using (UserServiceClient wcf = new UserServiceClient())
            {
                var roles = wcf.GetRoles();
                if (roles != null)
                {
                    Roles = roles.ToRoles().ToList();
                    SelectedRole = Roles[0];
                }
                else
                    Roles = new List<IHMRole>();
            }
        }

        public AddUpdateUserViewModel(int userId)
        {
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

                    SelectUserRole();
                }
            }
        }

        private void SelectUserRole()
        {
            if (User.Role == null)
                return;

            foreach(IHMRole role in Roles)
            {
                if (role.Id == User.Role.Id)
                    SelectedRole = role;
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
            User.Role = SelectedRole;
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