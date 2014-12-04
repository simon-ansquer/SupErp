using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationDAL
{
    public class BillProductDAL
    {
        #region Read
        public List<BILL_Product> GetBillProduct()
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Product.ToList();
            }
        }

        public BILL_Product GetBillProduct(long id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Product.Single(p => p.Product_Id == id);
            }
        }

        public List<BILL_Product> GetProducts()
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Product.Where(p => p.Name != null).ToList();
            }
        }

        public BILL_Product GetProductByID(long id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Product.SingleOrDefault(p => p.Product_Id == id);
            }
        }


        public BILL_Product GetProductByName(string nameProduct)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Product.SingleOrDefault(p => p.Name == nameProduct);
            }
        }

        public IEnumerable<BILL_Product> GetProductCategory(long idCategory)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Product.Where(p => p.Category_Id == idCategory);
            }
        }

        public BILL_Product GetProductPrice(Double priceProduct)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.BILL_Product.SingleOrDefault(p => p.Price == priceProduct);
            }
        }

        #endregion

        #region Create
        public BILL_Product CreateBillProduct(BILL_Product billProductToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var p = context.BILL_Product.Add(billProductToAdd);
                context.SaveChanges();
                return p;
            }
        }
        #endregion

        #region Edit
        public BILL_Product EditBillProduct(BILL_Product billProductToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var p = context.BILL_Product.Find(billProductToEdit.Product_Id);
                p = billProductToEdit;
                context.SaveChanges();
                return p;
            }
        }
        #endregion

        #region Delete
        public bool DeleteBillProduct(long billProduct_id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    var p = context.BILL_Product.Find(billProduct_id);
                    context.BILL_Product.Remove(p);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
