using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.FacturationDAL
{
    public class BillCategoryDAL
    {
        #region Read
        public List<BILL_Category> GetBillCategory()
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_Category.ToList();
            }
        }

        public BILL_Category GetBillCategory(String nameBillCategory)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_Category.SingleOrDefault(c => c.Name == nameBillCategory);
            }
        }

        public BILL_Category GetBillCategoryByDescription(string descriptionBillCategory)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                return context.BILL_Category.SingleOrDefault(c => c.DescriptionCat == descriptionBillCategory);
            }
        }
        #endregion

        #region Create
        public BILL_Category CreateBillCategory(BILL_Category billCategoryToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var c = context.BILL_Category.Add(billCategoryToAdd);
                context.SaveChanges();
                return c;
            }
        }
        #endregion

        #region Edit
        public BILL_Category EditBillCategory(BILL_Category billCategoryToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                var c = context.BILL_Category.Find(billCategoryToEdit.Category_Id);
                c = billCategoryToEdit;
                context.SaveChanges();
                return c;
            }
        }
        #endregion

        #region Delete
        public bool DeleteBillCategory(BILL_Category billCategoryToDelete)
        {
            using (SUPERPEntities context = new SUPERPEntities())
            {
                try
                {
                    context.BILL_Category.Remove(billCategoryToDelete);
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
