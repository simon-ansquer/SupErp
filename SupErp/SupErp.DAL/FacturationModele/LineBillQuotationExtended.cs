using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationModele
{
    public class LineExtended : BILL_LineBillQuotation
    {
        public bool Included { get; private set; }

        public LineExtended(BILL_LineBillQuotation line, bool included)
        {
            base.BILL_BillQuotation = line.BILL_BillQuotation;
            base.BILL_Product = line.BILL_Product;
            base.BillQuotation_Id = line.BillQuotation_Id;
            base.DateLine = line.DateLine;
            base.LineBillQuotation_Id = line.LineBillQuotation_Id;
            base.Product_Id = line.Product_Id;
            base.Quantite = line.Quantite;

            Included = included;
        }

    }
}
