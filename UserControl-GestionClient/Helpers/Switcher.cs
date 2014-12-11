using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using UserControl_GestionClient.Views;

namespace UserControl_GestionClient.Helpers
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
