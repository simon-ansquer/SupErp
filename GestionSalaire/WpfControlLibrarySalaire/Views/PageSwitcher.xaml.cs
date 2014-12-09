using SupErp.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Markup;
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
            Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
            Switcher.PageSwitcher = this;
            Switcher.Switch(new EmployeesList());
        }

        internal void Navigate(UserControl newPage)
        {
            Content = newPage;
        }

        public string MenuName
        {
            get { return "Salaires"; }
        }

        public List<ISubMenu> SubMenus
        {
            get { return null; }
        }
    }
}
