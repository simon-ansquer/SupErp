using SupErp.BLL.FacturationBLL;
using SupErp.DAL.FacturationModele;
using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<BillQuotationLight> SearchBillQuotation(string nomClient, string numFact, DateTime? dateDocument, BILL_Status status, int? MontantHTMin, int? MontantHTMax, bool? isBill)
        {
            var list = new List<BillQuotationLight>();
            var noFilter = string.IsNullOrEmpty(nomClient) & string.IsNullOrEmpty(numFact) & dateDocument == null
                & status == null & MontantHTMin == null & MontantHTMax == null & isBill == null;

            if (noFilter)
                list = billQuotationBLL.GetBillQuotation().ToList();

            /*** Filtre client ***/
            if (nomClient != null)
                list.AddRange(billQuotationBLL.GetBillQuotation().Where(b => b.Company.name == nomClient).ToList());

            /*** Filtre numero ***/
            if (numFact != null)
                if (list.Count == 0)
                {
                    var bill = billQuotationBLL.GetBillByNum(numFact);
                    if (bill != null)
                        list.Add(bill);
                }
                else
                    list.Where(b => b.NBill == numFact);

            /*** Filtre status ***/
            if (status != null)
                if (list.Count == 0)
                    list.AddRange(billQuotationBLL.GetBillQuotationByStatus(status));
                else
                    list.Where(b => b.BillStatus.Status_Id == status.Status_Id);

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

            ///*** Filtre MontantTTCMin ***/
            //if (MontantTTCMin != null)
            //    if (list.Count == 0)
            //        list.AddRange(billQuotationBLL.GetBillQuotation().Where(b => b.AmountTTC > MontantTTCMin).ToList());
            //    else
            //        list.Where(b => b.AmountTTC > MontantTTCMin);

            ///*** Filtre MontantTTCMax ***/
            //if (MontantTTCMax != null)
            //    if (list.Count == 0)
            //        list.AddRange(billQuotationBLL.GetBillQuotation().Where(b => b.AmountTTC < MontantTTCMax).ToList());
            //    else
            //        list.Where(b => b.AmountTTC < MontantTTCMax);

            /*** Filtre isbill ***/
            if (isBill != null)
                if (Convert.ToBoolean(isBill))
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

        public BillQuotationComplete GetBillQuotation(long billQuotation_id)
        {
            return billQuotationBLL.GetBillQuotationsById(billQuotation_id);
        }

        /**************************/
        /* LIGNE FACTURE / DEVIS  */
        /**************************/

        private static readonly Lazy<LineBillQuotationBLL> lazyLineBLL = new Lazy<LineBillQuotationBLL>(() => new LineBillQuotationBLL());

        private static LineBillQuotationBLL lineBLL { get { return lazyLineBLL.Value; } }

        private static readonly Lazy<BillProductBLL> lazyProductBLL = new Lazy<BillProductBLL>(() => new BillProductBLL());

        private static BillProductBLL productBLL { get { return lazyProductBLL.Value; } }

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
        /*    CREATION FACTURE    */
        /**************************/

        private static readonly Lazy<BillQuotationStatusBLL> lazybqsBLL = new Lazy<BillQuotationStatusBLL>(() => new BillQuotationStatusBLL());

        private static BillQuotationStatusBLL bqsBLL { get { return lazybqsBLL.Value; } }

        public bool CreateBillQuotation(BillQuotationComplete billQuotationComplete)
        {
            var res = true;
            try
            {
                BILL_BillQuotation bill = new BILL_BillQuotation
                {
                    AmountDF = billQuotationComplete.AmountDF,
                    BILL_Transmitter = billQuotationComplete.BILL_Transmitter,
                    Company = billQuotationComplete.Company,
                    DateBillQuotation = billQuotationComplete.DateBillQuotation,
                    NBill = billQuotationComplete.NBill,
                    Vat = billQuotationComplete.Vat,
                    Company_Id = billQuotationComplete.Company_Id,
                    Transmitter_Id = billQuotationComplete.Transmitter_Id
                };

                //bill = billQuotationBLL.CreateBillQutotation(bill);

                var status = new BILL_BillQuotationStatus { BILL_BillQuotation = bill, BillQuotation_Id = bill.BillQuotation_Id, DateAdvancement = DateTime.Now, Status_Id = billQuotationComplete.BillStatus.Status_Id };
                bqsBLL.CreateBillQuotationStatus(status);

                billQuotationBLL.SetNumFacture();

                if (billQuotationComplete.lines == null) return res;

                var listLine = new List<BILL_LineBillQuotation>();
                foreach (var line in billQuotationComplete.lines)
                {
                    var l = new BILL_LineBillQuotation
                    {
                        BILL_BillQuotation = bill,
                        BILL_Product = line.BILL_Product,
                        BillQuotation_Id = bill.BillQuotation_Id,
                        DateLine = line.DateLine,
                        LineBillQuotation_Id = line.LineBillQuotation_Id,
                        Product_Id = line.Product_Id,
                        Quantite = line.Quantite
                    };

                    listLine.Add(l);
                }

                lineBLL.CreateLineBillQuotation(listLine);
            }
            catch (Exception)
            {
                res = false;
            }
            return res;
        }

        /******************************/
        /*    MODIFICATION FACTURE    */
        /******************************/

        public bool ModifyBillQuotation(BillQuotationComplete billQuotationComplete)
        {
            var res = true;
            try
            {
                /*** Modification de la facture/devis ***/
                BILL_BillQuotation bill = new BILL_BillQuotation
                {
                    BillQuotation_Id = billQuotationComplete.BillQuotation_Id,
                    AmountDF = billQuotationComplete.AmountDF,
                    BILL_Transmitter = billQuotationComplete.BILL_Transmitter,
                    Company = billQuotationComplete.Company,
                    DateBillQuotation = billQuotationComplete.DateBillQuotation,
                    NBill = billQuotationComplete.NBill,
                    Vat = billQuotationComplete.Vat,
                    Company_Id = billQuotationComplete.Company_Id,
                    Transmitter_Id = billQuotationComplete.Transmitter_Id
                };

                var billEdit = billQuotationBLL.EditBillQuotation(bill);

                /*** Modification du status ***/
                var status = new BILL_BillQuotationStatus { BillQuotation_Id = bill.BillQuotation_Id, DateAdvancement = DateTime.Now, Status_Id = billQuotationComplete.BillStatus.Status_Id };
                //bqsBLL.CreateBillQuotationStatus(status);

                /*** Modification des lignes de facture ***/
                var lineBDD = lineBLL.GetLineBillQuotation(billQuotationComplete.BillQuotation_Id);
                var lineModif = billQuotationComplete.lines;

                var listLineAModifie = new List<BILL_LineBillQuotation>();
                var listLineAAjoute = new List<BILL_LineBillQuotation>();
                /* TODO: METTRE A JOUR LES LIGNES FACTURES */
                foreach (var line in lineModif)
                {
                    var l = new BILL_LineBillQuotation
                   {
                       BILL_BillQuotation = bill,
                       BILL_Product = line.BILL_Product,
                       BillQuotation_Id = bill.BillQuotation_Id,
                       DateLine = line.DateLine,
                       LineBillQuotation_Id = line.LineBillQuotation_Id,
                       Product_Id = line.Product_Id,
                       Quantite = line.Quantite
                   };

                    if (lineBDD.Contains(l))
                    {
                        listLineAModifie.Add(l);
                        lineBDD.Remove(l);
                    }
                    else
                    {
                        listLineAAjoute.Add(l);
                        lineBDD.Remove(l);
                    }
                }

                lineBLL.CreateLineBillQuotation(listLineAAjoute);
                lineBLL.EditLineBillQuotation(listLineAModifie);
                lineBLL.DeleteLineBillQuotation(lineBDD);
            }
            catch (Exception)
            {
                res = false;
            }
            return res;
        }

        /***********************/
        /*    TRANSMITTER      */
        /**********************/

        private static readonly Lazy<BillTransmitterBLL> lazyTransmitterBLL = new Lazy<BillTransmitterBLL>(() => new BillTransmitterBLL());

        private static BillTransmitterBLL transmitterBLL { get { return lazyTransmitterBLL.Value; } }

        public List<BILL_Transmitter> GetTransmitter()
        {
            try
            {
                return transmitterBLL.GetBillTrans();
            }
            catch (Exception ex)
            {
                throw new Exception("FacturationService >> GetTransmitter :" + ex.Message);
            }
        }

        /***********************/
        /*       STATUS       */
        /**********************/

        private static readonly Lazy<StatusBLL> lazyStatusBLL = new Lazy<StatusBLL>(() => new StatusBLL());

        private static StatusBLL statusBLL { get { return lazyStatusBLL.Value; } }

        public List<BILL_Status> GetStatus()
        {
            try
            {
                return statusBLL.GetStatus();
            }
            catch (Exception ex)
            {
                throw new Exception("FacturationService >> GetStatus :" + ex.Message);
            }
        }
    }
}