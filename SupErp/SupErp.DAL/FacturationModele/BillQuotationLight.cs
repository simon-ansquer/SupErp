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
    [KnownType(typeof(BILL_BillQuotation))]
    public class BillQuotationLight : BILL_BillQuotation
    {
        public BillQuotationLight(BILL_BillQuotation bill_billQuotation)
        {
            base.AmountDF = bill_billQuotation.AmountDF;
            base.BILL_BillQuotationStatus = bill_billQuotation.BILL_BillQuotationStatus;
            base.BILL_LineBillQuotation = bill_billQuotation.BILL_LineBillQuotation;
            base.BILL_Transmitter = bill_billQuotation.BILL_Transmitter;
            base.BillQuotation_Id = bill_billQuotation.BillQuotation_Id;
            base.Company = bill_billQuotation.Company;
            base.Company_Id = bill_billQuotation.Company_Id;
            base.DateBillQuotation = bill_billQuotation.DateBillQuotation;
            base.NBill = bill_billQuotation.NBill;
            base.Transmitter_Id = bill_billQuotation.Transmitter_Id;
            base.Vat = bill_billQuotation.Vat;

            CalculateTTC();
        }

        [DataMember]
        public double AmountTTC{ get; private set;}

        private void CalculateTTC()
        {
            var htRef = 0.00;    
            var context = new SUPERPEntities();
            var lstLine = context.BILL_LineBillQuotation.Where(l => l.BillQuotation_Id == BillQuotation_Id);

            foreach(var line in lstLine)
            {
                var tvaRate = (line.BILL_Product.BILL_Vat.Rate == null) ? 0.00 : Convert.ToDouble(line.BILL_Product.BILL_Vat.Rate);

                double priceProductTTC = (tvaRate * line.BILL_Product.Price) + line.BILL_Product.Price;
                htRef += line.BILL_Product.Price * line.Quantite;
                AmountTTC += priceProductTTC * line.Quantite;
            }


            /* Check if amountHT is valid */
            if(htRef != AmountDF)
            {
                var b = context.BILL_BillQuotation.Find(BillQuotation_Id);
                b.AmountDF = htRef;
                context.SaveChanges();
            }

            /* Check if VAT is affected */
            if (!Vat) AmountTTC = AmountDF;

        }

    }
}
