using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErpModuleUser.Helpers
{
    public class ModulesDictionnary
    {
        public Dictionary<string, string> Modules { get; set; }

        public ModulesDictionnary()
        {
            Modules = new Dictionary<string, string>
            {
                {"SupErpModuleUser", "Utilisateurs"},
                {"WpfControlLibrarySalaire", "Salaires"}
            };
        }
    }
}
