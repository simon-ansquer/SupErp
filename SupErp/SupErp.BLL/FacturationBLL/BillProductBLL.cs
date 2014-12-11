using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;
using SupErp.DAL.FacturationDAL;
using SupErp.DAL.FacturationModele;

namespace SupErp.BLL.FacturationBLL
{
    public class BillProductBLL
    {
        private static readonly Lazy<BillProductDAL> LazyBillProdDAL = new Lazy<BillProductDAL>(() => new BillProductDAL());
        private static BillProductDAL billProdDAL { get { return LazyBillProdDAL.Value; } }

        #region Read
        public List<BILL_Product> GetBillProduct()
        {
            return billProdDAL.GetBillProduct();
        }

        public List<ProductExtended> getListProductIncludedOrNot(long billquotation_id)
        {
            return GetBillProduct().Select(p => new ProductExtended(p, billquotation_id)).ToList();
        }


        public BILL_Product GetProductByID(long id)
        {
            return billProdDAL.GetProductByID(id);
        }

        #endregion

    }
}
