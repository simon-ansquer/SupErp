using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.Kernel.Exceptions
{
    public class MainMenuNotImplementedException : Exception
    {
        public MainMenuNotImplementedException(string dllName) : base (dllName + " ne comporte aucune classe implémentant IMainMenu")
        {
        }
    }
}
