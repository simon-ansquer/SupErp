using SupErpModuleUser.Helpers;
using SupErpModuleUser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SupErpModuleUser.ViewModels
{
    public class ListRoleViewModel
    {
        public List<IHMRole> Roles { get; set; }

        public ListRoleViewModel()
        {
            IHMRole role1 = new IHMRole();
            role1.Id = 1;
            role1.Label = "Role 1";
            role1.ListModules = "zaeui nazieu nazuieb iuazbeiu baziue baizbe iabzuei baziue aiu";
            IHMRole role2 = new IHMRole();
            role2.Id = 2;
            role2.Label = "Role 2";
            role2.ListModules = "zaeui nazieu nazuieb iuazbeiu baziue baizbe iabzuei baziue aiu";

            Roles = new List<IHMRole>();
            Roles.Add(role1);
            Roles.Add(role2);
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
        { }

        #endregion

    }
}
