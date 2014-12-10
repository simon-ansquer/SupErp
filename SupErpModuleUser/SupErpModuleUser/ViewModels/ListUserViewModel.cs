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
            using (UserServiceClient ws = new UserServiceClient())
            {
                var users = ws.GetUsers();
                if (users != null)
                    Users = users.ToIHMUser().ToList();
                else
                    Users = new List<IHMUser>();
            }
        
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
