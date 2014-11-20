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
    /// <summary>
    /// Logique d'interaction pour RoleUserControl.xaml
    /// </summary>
    public partial class RoleUserControl : UserControl, ISubMenu
    {
        private List<ISubMenu> subMenus;

        public RoleUserControl()
        {
            InitializeComponent();

            subMenus = new List<ISubMenu>();
            subMenus.Add(new AddRoleUserControl());
            subMenus.Add(new ListRoleUserControl());
        }

        public string SubMenuName
        {
            get { return "Rôles"; }
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
