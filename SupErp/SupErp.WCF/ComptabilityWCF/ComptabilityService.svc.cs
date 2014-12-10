using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SupErp.BLL.ComptabilityBLL;
using SupErp.BLL.ComptabilityBLL.BllObject;

namespace SupErp.WCF.ComptabilityWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Comptability" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Comptability.svc ou Comptability.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class ComptabilityService : IComptabilityService
    {

        private static readonly Lazy<PlanComptableBLL> LazyComptabiliteBLL = new Lazy<PlanComptableBLL>(() => new PlanComptableBLL());
        private static PlanComptableBLL comptabiliteBLL { get { return LazyComptabiliteBLL.Value; } }

        IEnumerable<ClassOfAccount> IComptabilityService.GetPlanComptable ()
        {
            return comptabiliteBLL.GetPlanComptable();
        }

        Entities.COMPTA_ExchangeRate IComptabilityService.GetExhangeRate ()
        {
            return comptabiliteBLL.GetLastExchangeRate();
        }

        IEnumerable<Entries> IComptabilityService.GetEntries ( string type, bool? paye, bool? impaye, DateTime? Debut, DateTime? Fin )
        {
            EntriesTypeEnum result;
            if(Enum.TryParse(type, out result))
                return comptabiliteBLL.GetEntries(result, paye, impaye, Debut, Fin);
            return comptabiliteBLL.GetEntries(null, paye, impaye, Debut, Fin);
        }

        IEnumerable<BilanComptable> IComptabilityService.GetBilanComptable ( string Mode, DateTime? Debut, DateTime? Fin )
        {
            BilanComptableModeEnum result;
            if ( Enum.TryParse(Mode, out result) )
                return comptabiliteBLL.GetBilanComptable(result, Debut.Value, Fin.Value);

            return comptabiliteBLL.GetBilanComptable(BilanComptableModeEnum.Month, Debut.Value, Fin.Value);
        }
    }
}
