using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationDAL
{
    class BillProductDAL
    {
        #region Read
        public List<BILL_Product> GetBillProduct()
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_Product.ToList();
            }
        }

        public List<BILL_Product> GetProducts()
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_Product.Where(p => p.Name != null).ToList();
            }
        }

        public BILL_Product GetProductByName(string nameProduct)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_Product.SingleOrDefault(p => p.Name == nameProduct);
            }
        }

        public BILL_Product GetProductDescription(string descriptionProduct)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_Product.SingleOrDefault(p => p.DescriptionPro == descriptionProduct);
            }
        }

        public BILL_Product GetProductPrice(Double priceProduct)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_Product.SingleOrDefault(p => p.Price == priceProduct);
            }
        }

        #endregion

        #region Create
        public BILL_Product CreateBillProduct(BILL_Product billProductToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities())
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
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var p = context.BILL_Product.Find(billProductToEdit.Product_Id);
                p = billProductToEdit;
                context.SaveChanges();
                return p;
            }
        }
        #endregion

        #region Delete
        public bool DeleteBillProduct(BILL_Product billProductToDelete)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                try
                {
                    context.BILL_Product.Remove(billProductToDelete);
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
