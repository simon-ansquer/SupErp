using System.Windows.Controls;
using WpfControlLibrarySalaire.Helpers;

namespace WpfControlLibrarySalaire.Views
{
    /// <summary>
    /// Logique d'interaction pour Switcher.xaml
    /// </summary>
    public partial class PageSwitcher : UserControl
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
    }
}
