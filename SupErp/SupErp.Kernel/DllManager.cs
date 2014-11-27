using SupErp.Entities;
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
        public IEnumerable<string> GetDllsPaths()
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules");
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

        /// <summary>
        /// Permet de récuperer tous les menus et sous-menus des modules auxquels l'utilisateur a accès
        /// </summary>
        /// <param name="userRole">Role de l'utilsateur courant</param>
        /// <returns>Liste de IMainMenu</returns>
        public List<IMainMenu> GetMainMenus(Role userRole)
        {
            List<IMainMenu> mainMenus = new List<IMainMenu>(); 
            IEnumerable<string> paths = GetDllsPaths();

            foreach (string p in paths)
            {
                if (userRole.RoleModules.Any(x => x.Module.Name == Path.GetFileNameWithoutExtension(p)))
                {
                    mainMenus.Add(GetMainMenu(p));
                }
            }

            return mainMenus;
        }
    }
}
