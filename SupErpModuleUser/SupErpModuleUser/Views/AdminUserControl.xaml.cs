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

namespace SupErpModuleUser
{
    public partial class AdminUserControl : UserControl, IMainMenu
    {
        private List<ISubMenu> subMenus;

        public AdminUserControl()
        {
            InitializeComponent();

            subMenus = new List<ISubMenu>();
            subMenus.Add(new UserUserControl());
            subMenus.Add(new RoleUserControl());

        }

        public string MenuName
        {
            get
            {
                return "Administration";
            }
        }

        public List<ISubMenu> SubMenus
        {
            get
            {
                return subMenus;
            }
        }
    }
}
