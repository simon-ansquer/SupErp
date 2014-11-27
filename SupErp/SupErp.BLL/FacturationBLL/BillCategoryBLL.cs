﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;
using SupErp.DAL.FacturationDAL;

namespace SupErp.BLL.FacturationBLL
{
    public class BillCategoryBLL
    {
        private static BillCategoryDAL billCategoryDAL;

        #region Read
        public List<BILL_Category> GetBillCategory()
        {
            return billCategoryDAL.GetBillCategory();
        }

        public BILL_Category GetBillCategory(String nameBillCategory)
        {
            return billCategoryDAL.GetBillCategory(nameBillCategory);
        }

        public BILL_Category GetBillCategoryByDescription(string descriptionBillCategory)
        {
            return billCategoryDAL.GetBillCategoryByDescription(descriptionBillCategory);
        }
        #endregion

        #region Create
        public BILL_Category CreateBillCategory(BILL_Category billCategoryToAdd)
        {
            return billCategoryDAL.CreateBillCategory(billCategoryToAdd);
        }
        #endregion

        #region Edit
        public BILL_Category EditBillCategory(BILL_Category billCategoryToEdit)
        {
            return billCategoryDAL.EditBillCategory(billCategoryToEdit);
        }
        #endregion

        #region Delete
        public bool DeleteBillCategory(BILL_Category billCategoryToDelete)
        {
            return billCategoryDAL.DeleteBillCategory(billCategoryToDelete);
        }
        #endregion
    }
}
