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
using SupErp.Shared;

namespace SupErpModuleUser.Views
{
    /// <summary>
    /// Interaction logic for RoleCreatePageSwitcher.xaml
    /// </summary>
    public partial class RoleCreatePageSwitcher : UserControl, ISubMenu
    {
        public static Frame RoleCreateSwitcherFrame;
        public RoleCreatePageSwitcher()
        {
            InitializeComponent();

            RoleCreateSwitcherFrame = new Frame();
            MainGrid.Children.Add(RoleCreateSwitcherFrame);
            RoleCreateSwitcherFrame.Navigate(new AddUpdateRoleUserControl());
            RoleCreateSwitcherFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        public string SubMenuName
        {
            get { return "Ajouter un rôle"; }
        }

        public List<ISubMenu> SubMenus
        {
            get
            {
                return null;
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
