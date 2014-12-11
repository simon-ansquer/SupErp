using SupErp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SupErpModuleUser.Views;

namespace SupErpModuleUser
{
    /// <summary>
    /// Logique d'interaction pour UserUserControl.xaml
    /// </summary>
    public partial class UserUserControl : UserControl, ISubMenu
    {
        private List<ISubMenu> subMenus;

        public UserUserControl()
        {
            InitializeComponent();

            subMenus = new List<ISubMenu>();
            subMenus.Add(new UserCreatePageSwitcher());
            subMenus.Add(new UserListPageSwitcher());
        }

        public string SubMenuName
        {
            get { return "Utilisateurs"; }
        }

        public List<ISubMenu> SubMenus
        {
            get
            {
                return subMenus;
            }
        }

        public bool CanWrite
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
