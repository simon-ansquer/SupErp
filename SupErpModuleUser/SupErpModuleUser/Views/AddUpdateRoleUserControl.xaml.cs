using SupErp.Shared;
using SupErpModuleUser.ViewModels;
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
    /// Logique d'interaction pour AddRoleUserControl.xaml
    /// </summary>
    public partial class AddUpdateRoleUserControl : UserControl, ISubMenu
    {
        public AddUpdateRoleUserControl()
        {
            InitializeComponent();
        }

        public AddUpdateRoleUserControl(AddUpdateRoleViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
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
