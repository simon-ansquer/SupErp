using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.Shared
{
    /// <summary>
    /// Pemrmet de définir le menu principal de votre module
    /// Ne doit être implémenté que par une seule classe par module
    /// </summary>
    public interface IMainMenu
    {
        /// <summary>
        /// Nom du menu principal qui sera affiché dans l'application
        /// </summary>
        string MenuName { get; set; }

        /// <summary>
        /// Liste des sous-menus du menu principal
        /// </summary>
        List<ISubMenu> SubMenus { get; set; }
    }
}
