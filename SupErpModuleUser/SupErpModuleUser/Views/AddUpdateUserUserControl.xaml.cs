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
    /// Logique d'interaction pour AddUserUserControl.xaml
    /// </summary>
    public partial class AddUpdateUserUserControl : UserControl, ISubMenu
    {

        public AddUpdateUserUserControl()
        {
            InitializeComponent();
        }

        public AddUpdateUserUserControl(AddUpdateUserViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        public string SubMenuName
        {
            get { return "Ajouter utilisateur"; }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            var g = maingrid.Children;
            var newSize = (sizeInfo.NewSize.Height * sizeInfo.NewSize.Width);
            foreach (UIElement el in g)
            {
                if (el.GetType() == typeof(TextBlock))
                    ((TextBlock)el).FontSize =  newSize/ 50000;
                if (el.GetType() == typeof(TextBox))
                    ((TextBox)el).FontSize = newSize / 50000;
            }
            base.OnRenderSizeChanged(sizeInfo);
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
