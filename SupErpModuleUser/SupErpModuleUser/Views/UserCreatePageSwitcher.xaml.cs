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
    /// Interaction logic for UserCreatePageSwitcher.xaml
    /// </summary>
    public partial class UserCreatePageSwitcher : UserControl, ISubMenu
    {
        public static Frame UserCreateSwitcherFrame;
        public UserCreatePageSwitcher()
        {
            InitializeComponent();

            UserCreateSwitcherFrame = new Frame();
            MainGrid.Children.Add(UserCreateSwitcherFrame);
            UserCreateSwitcherFrame.Navigate(new AddUpdateUserUserControl());
            UserCreateSwitcherFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        public string SubMenuName
        {
            get { return "Ajouter utilisateur"; }
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
