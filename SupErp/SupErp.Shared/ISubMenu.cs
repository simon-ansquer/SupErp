using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.Shared
{
    /// <summary>
    /// ¨Permet de définir qu'un élément est un sous-menu
    /// </summary>
    public interface ISubMenu
    {
        /// <summary>
        /// Nom du sous-menu qui sera affiché dans l'application.
        /// </summary>
        string SubMenuName { get; }

        /// <summary>
        /// Liste des sous-menus de ce sous-menu.
        /// </summary>
        List<ISubMenu> SubMenus { get; set; }

        /// <summary>
        /// Booléen qui indique si ce sous-menu necessite les droits en écriture pour être affiché dans le menu.
        /// </summary>
        bool CanWrite { get; set; }
    }
}
