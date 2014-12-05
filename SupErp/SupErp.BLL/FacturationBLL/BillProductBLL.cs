﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;
using SupErp.DAL.FacturationDAL;

namespace SupErp.BLL.FacturationBLL
{
    class BillProductBLL
    {
        private static BillProductDAL billProdDAL { get; set; }

        #region Read
        public List<BILL_Product> GetBillProduct()
        {
            return billProdDAL.GetBillProduct();
        }

        public List<BILL_Product> GetProducts()
        {
            return billProdDAL.GetProducts();
        }

        public BILL_Product GetProductByName(string nameProduct)
        {
            return billProdDAL.GetProductByName(nameProduct);
        }

        public BILL_Product GetProductDescription(string descriptionProduct)
        {
            return billProdDAL.GetProductDescription(descriptionProduct);
        }

        public BILL_Product GetProductPrice(Double priceProduct)
        {
            return billProdDAL.GetProductPrice(priceProduct);
        }

        #endregion

        #region Create
        public BILL_Product CreateBillProduct(BILL_Product billProductToAdd)
        {
            return billProdDAL.CreateBillProduct(billProductToAdd);
        }
        #endregion

        #region Edit
        public BILL_Product EditBillProduct(BILL_Product billProductToEdit)
        {
            return billProdDAL.EditBillProduct(billProductToEdit);
        }
        #endregion

        #region Delete
        public bool DeleteBillProduct(BILL_Product billProductToDelete)
        {
            return billProdDAL.DeleteBillProduct(billProductToDelete);
        }
        #endregion
    }
}