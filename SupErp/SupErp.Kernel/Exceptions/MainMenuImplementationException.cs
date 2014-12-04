using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.Kernel.Exceptions
{
    public class MainMenuImplementationException : Exception
    {
        public MainMenuImplementationException(string dllName) : base(dllName + " ne doit comporter qu'une seule classe implémentant IMainMenu")
        {

        }
    }
}
