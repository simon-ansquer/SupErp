using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using SupErp.Entities;
using SupErp.BLL.FacturationBLL;

namespace SupErp.WCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "FacturationService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez FacturationService.svc ou FacturationService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class FacturationService : IFacturationService
    {
        //Conservation de l'instance au sein de la BLL, qui sera chargée à sa première utilisation
        private static readonly Lazy<BillQuotationBLL> lazyBillQuotationBLL = new Lazy<BillQuotationBLL>(() => new BillQuotationBLL());
        private static BillQuotationBLL billQuotationBLL { get { return lazyBillQuotationBLL.Value; } }

        public List<BILL_BillQuotation> GetListQuotation()
        {
            return billQuotationBLL.GetBillQuotation().ToList<BILL_BillQuotation>();
        }
    }
}
