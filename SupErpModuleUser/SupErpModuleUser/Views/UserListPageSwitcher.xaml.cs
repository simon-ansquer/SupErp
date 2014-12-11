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
    /// Interaction logic for UserListPageSwitcher.xaml
    /// </summary>
    public partial class UserListPageSwitcher : UserControl, ISubMenu
    {
        public static Frame UserListSwitcherFrame ;
        public UserListPageSwitcher()
        {
            InitializeComponent();

            UserListSwitcherFrame = new Frame();
            MainGrid.Children.Add(UserListSwitcherFrame);
            UserListSwitcherFrame.Navigate(new ListUserUserControl());
            UserListSwitcherFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        public string SubMenuName
        {
            get { return "Afficher les utilisateurs"; }
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
