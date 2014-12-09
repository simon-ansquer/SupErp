using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;
using System.Runtime.Serialization;

namespace SupErp.DAL.FacturationModele
{

    [DataContract(IsReference = true)]
    [KnownType(typeof(BILL_LineBillQuotation))]
    public class LineCompleted: BILL_LineBillQuotation
    {
        [DataMember]
        public double AmountHT { get; set; }

        [DataMember]
        public double AmountTTC{get; set;}

        public LineCompleted(BILL_LineBillQuotation line)
        {
            base.BILL_BillQuotation = line.BILL_BillQuotation;
            base.BILL_Product = line.BILL_Product;
            base.BillQuotation_Id = line.BillQuotation_Id;
            base.DateLine = line.DateLine;
            base.LineBillQuotation_Id = line.LineBillQuotation_Id;
            base.Product_Id = line.Product_Id;
            base.Quantite = line.Quantite;

            var tva = line.BILL_Product.BILL_Vat;
            var unitPrice = line.BILL_Product.Price;

            AmountHT = unitPrice * Quantite;

            if(tva != null)
                AmountTTC = Convert.ToDouble((AmountHT * tva.Rate) + AmountHT);
            else
                AmountTTC = AmountHT;
        }
    }
}
