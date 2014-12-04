using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationModele
{
    public class ProductExtended:  BILL_Product
    {
        private bool included { get; set; }
        public ProductExtended (BILL_Product product, long billQuotation_id)
        {
            base.BILL_Category = product.BILL_Category;
            base.BILL_LineBillQuotation = product.BILL_LineBillQuotation;
            base.BILL_Vat = product.BILL_Vat;
            base.Category_Id = product.Category_Id;
            base.DescriptionPro = product.DescriptionPro;
            base.Name = product.Name;
            base.Price = product.Price;
            base.Product_Id = product.Product_Id;
            base.Vat_Id = product.Vat_Id;

            var DAL = new FacturationDAL.LineBillQuotationDAL();
            var listProduct = DAL.productsIncludedInBill(billQuotation_id);
            included = listProduct.Contains(product);
        }



    }
}
