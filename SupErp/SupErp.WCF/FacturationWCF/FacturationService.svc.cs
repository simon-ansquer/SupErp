using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using SupErp.Entities;
using SupErp.BLL.FacturationBLL;
using SupErp.DAL.FacturationModele;

namespace SupErp.WCF.FacturationWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "FacturationService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez FacturationService.svc ou FacturationService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class FacturationService : IFacturationService
    {
        /**************************/
        /*    FACTURE / DEVIS     */
        /**************************/
        private static readonly Lazy<BillQuotationBLL> lazyBillQuotationBLL = new Lazy<BillQuotationBLL>(() => new BillQuotationBLL());
        private static BillQuotationBLL billQuotationBLL { get { return lazyBillQuotationBLL.Value; } }

        public List<BillQuotationLight> GetListQuotation()
        {
            return billQuotationBLL.GetBillQuotation().OrderBy(b => b.DateBillQuotation).ToList();
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


        /**************************/
        /* LIGNE FACTURE / DEVIS  */
        /**************************/

        private static readonly Lazy<LineBillQuotationBLL> lazyLineBLL = new Lazy<LineBillQuotationBLL>(() => new LineBillQuotationBLL());
        private static LineBillQuotationBLL lineBLL { get { return lazyLineBLL.Value; } }


        public List<LineExtended> GetAllLines(long billQuotation_id)
        {
            var listLineExtended = lineBLL.GetLineBillQuotation(billQuotation_id).Select(l => new LineExtended(l, true)).ToList();


            var listProductNotIncluded = productBLL.getListProductIncludedOrNot(billQuotation_id).Where(p => !p.included);
            foreach (var p in listProductNotIncluded)
            {
                var line = new BILL_LineBillQuotation { BILL_Product = p, Quantite = 0, BillQuotation_Id = billQuotation_id };
                listLineExtended.Add(new LineExtended(line, false));
            }

            return listLineExtended;
        }


        /**************************/
        /*        PRODUIT         */
        /**************************/

        private static readonly Lazy<BillProductBLL> lazyProductBLL = new Lazy<BillProductBLL>(() => new BillProductBLL());
        private static BillProductBLL productBLL { get { return lazyProductBLL.Value; } }

    }
}
