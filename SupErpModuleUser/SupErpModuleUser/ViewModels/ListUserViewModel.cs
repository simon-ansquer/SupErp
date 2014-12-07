using SupErpModuleUser.Helpers;
using SupErpModuleUser.Models;
using SupErpModuleUser.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SupErpModuleUser.ViewModels
{
    public class ListUserViewModel
    {
        public List<IHMUser> Users { get; set; }

        public ListUserViewModel()
        {
            //Users = new UserService.UserServiceClient().GetUsers().ToIHMUser().ToList();

            IHMRole role1 = new IHMRole();
            role1.Label = "Role1";

            IHMUser user1 = new IHMUser();
            user1.Id = 1;
            user1.Role = role1;
            user1.Firstname = "Eliott";
            user1.Lastname = "Lujan";
            user1.Email = "eliott.lujan@gmail.com";
            user1.Address = "179 rue Camille Godard";

            IHMUser user2 = new IHMUser();
            user2.Id = 2;
            user1.Role = role1;
            user2.Firstname = "Brice";
            user2.Lastname = "Jantieu";
            user2.Email = "brice.jantieu@gmail.com";
            user2.Address = "rue du Jardin Public";

            Users = new List<IHMUser>();
            Users.Add(user1);
            Users.Add(user2);
        
        }

        #region Commands

        public ICommand ImageCommand
        {
            get
            {
                return new ActionCommand(param =>
                {
                    OnImageClicked(param);
                });
            }
        }

        #endregion

        #region Command Handlers

        private void OnImageClicked(object id)
        {
            Switcher.Switch(new AddUpdateUserUserControl(new AddUpdateUserViewModel(int.Parse(id.ToString()))));
        }

        #endregion
    }
}
