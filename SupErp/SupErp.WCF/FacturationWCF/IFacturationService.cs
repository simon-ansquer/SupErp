using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SupErp.Entities;

namespace SupErp.WCF.FacturationWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IFacturationService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IFacturationService
    {
        [OperationContract]
        List<BILL_BillQuotation> GetListQuotation();
    }
}
