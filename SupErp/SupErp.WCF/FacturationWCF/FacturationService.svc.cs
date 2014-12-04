using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using SupErp.Entities;
using SupErp.BLL.FacturationBLL;
using SupErp.DAL.FacturationModele;

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

        public List<BillQuotationLight> SearchBillQuotation(string nomClient, string numFact, DateTime? dateDocument, long? status,int? MontantHTMin, int? MontantHTMax, int? MontantTTCMin, int? MontantTTCMax, bool? isBill)
        {
            var list = new List<BillQuotationLight>();

            /*** Filtre client ***/
            if (nomClient != null)
                list.AddRange(billQuotationBLL.GetBillQuotation().Where(b => b.Company.name == nomClient).ToList());

            /*** Filtre numero ***/
            if (numFact != null)
                if (list.Count == 0)
                    list.Add(billQuotationBLL.GetBillByNum(numFact));
                else
                    list.Where(b => b.NBill == numFact);

            /*** Filtre date du document ***/
            if (dateDocument != null)
            {
                if (list.Count == 0)
                    list.AddRange(billQuotationBLL.GetBillQuotation().Where(b => b.DateBillQuotation.Date == dateDocument.Value.Date).ToList());
                else
                    list.Where(b => b.DateBillQuotation.Date == dateDocument.Value.Date);
            }
                

            /*** Filtre MontantHTMin ***/
            if (MontantHTMin != null)
                if (list.Count == 0)
                    list.AddRange(billQuotationBLL.GetBillQuotation().Where(b => b.AmountDF > MontantHTMin).ToList());
                else
                    list.Where(b => b.AmountDF > MontantHTMin);

            /*** Filtre MontantHTMax ***/
            if (MontantHTMax != null)
                if (list.Count == 0)
                    list.AddRange(billQuotationBLL.GetBillQuotation().Where(b => b.AmountDF < MontantHTMax).ToList());
                else
                    list.Where(b => b.AmountDF < MontantHTMax);

            /*** Filtre MontantTTCMin ***/
            if (MontantTTCMin != null)
                if (list.Count == 0)
                    list.AddRange(billQuotationBLL.GetBillQuotation().Where(b => b.AmountTTC > MontantTTCMin).ToList());
                else
                    list.Where(b => b.AmountTTC > MontantTTCMin);

            /*** Filtre MontantTTCMax ***/
            if (MontantTTCMax != null)
                if (list.Count == 0)
                    list.AddRange(billQuotationBLL.GetBillQuotation().Where(b => b.AmountTTC < MontantTTCMax).ToList());
                else
                    list.Where(b => b.AmountTTC < MontantTTCMax);

            /*** Filtre isbill ***/
            if (isBill != null)
                if(Convert.ToBoolean(isBill))
                    if (list.Count == 0)
                        list.AddRange(billQuotationBLL.GetBills().ToList());
                    else
                        list.Where(b => b.NBill == null);
                else
                    if (list.Count == 0)
                        list.AddRange(billQuotationBLL.GetQuotations().ToList());
                    else
                        list.Where(b => b.NBill != null);
            
            return list;
        }

    }
}
