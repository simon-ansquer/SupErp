using SupErp.Kernel.Exceptions;
using SupErp.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.Kernel
{
    public class DllManager
    {
        private IEnumerable<string> GetDllsPaths()
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            return Directory.GetFiles(directoryPath, "*.dll");
        }

        private IMainMenu GetMainMenu(string path)
        {
            Assembly assembly = Assembly.LoadFile(path);

            Type[] types = assembly.GetTypes();

            IEnumerable<Type> mainMenus = from t in types
                                     where t.IsClass && t.GetInterfaces().Contains(typeof(IMainMenu))
                                     select t;

            if (mainMenus.Count() == 0)
            {
                throw new MainMenuNotImplementedException(assembly.FullName);
            }
            else if (mainMenus.Count() > 1)
            {
                throw new MainMenuImplementationException(assembly.FullName);
            }

            return (IMainMenu)Activator.CreateInstance(mainMenus.First());
        }

        public List<IMainMenu> GetMainMenus()
        {
            List<IMainMenu> mainMenus = new List<IMainMenu>();
 
            IEnumerable<string> paths = GetDllsPaths();

            foreach (string p in paths)
            {
                mainMenus.Add(GetMainMenu(p));
            }

            return mainMenus;
        }
    }
}
