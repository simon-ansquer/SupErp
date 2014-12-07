using SupErp.Shared;
using System.Collections.Generic;
using System.Windows.Controls;
using WpfControlLibrarySalaire.Helpers;

namespace WpfControlLibrarySalaire.Views
{
    /// <summary>
    /// Logique d'interaction pour Switcher.xaml
    /// </summary>
    public partial class PageSwitcher : UserControl, IMainMenu
    {
        public PageSwitcher()
        {
            InitializeComponent();
            Switcher.PageSwitcher = this;
            Switcher.Switch(new EmployeesList());
        }

        internal void Navigate(UserControl newPage)
        {
            this.Content = newPage;
        }

        public string MenuName
        {
            get { return "Salaire"; }
        }

        public List<ISubMenu> SubMenus
        {
            get { return null; }
        }
    }
}
