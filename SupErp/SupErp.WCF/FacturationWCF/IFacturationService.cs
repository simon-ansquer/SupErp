using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SupErp.Entities;
using SupErp.DAL.FacturationModele;

namespace SupErp.WCF.FacturationWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IFacturationService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IFacturationService
    {
        [OperationContract]
        List<BillQuotationLight> GetListQuotation();

        [OperationContract]
        List<BillQuotationLight> SearchBillQuotation(string nomClient, string numFact, DateTime? dateDocument, long? status, int? MontantHTMin, int? MontantHTMax, int? MontantTTCMin, int? MontantTTCMax, bool? isBill);

        [OperationContract]
        BillQuotationComplete GetBillQuotation(long billQuotation_id);

        [OperationContract]
        List<LineExtended> GetAllLines(long billQuotation_id);

        [OperationContract]
        bool CreateBillQuotation(BillQuotationComplete billQuotation);

        [OperationContract]
        bool ModifyBillQuotation(BillQuotationComplete billQuotation);
    }
}
