using System.Windows.Controls;
using WpfControlLibrarySalaire.Views;

namespace WpfControlLibrarySalaire.Helpers
{
    public static class Switcher
    {
        public static PageSwitcher PageSwitcher;

        public static void Switch(UserControl newPage)
        {
            PageSwitcher.Navigate(newPage);
        }

    }
}
