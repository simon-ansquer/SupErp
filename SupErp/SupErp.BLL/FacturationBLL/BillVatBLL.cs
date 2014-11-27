using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;
using SupErp.DAL.FacturationDAL;

namespace SupErp.BLL.FacturationBLL
{
    class BillVatBLL
    {
        private static readonly Lazy<BillVatDAL> LazyBillVatDAL = new Lazy<BillVatDAL>(() => new BillVatDAL());
        private static BillVatDAL billVatDAL { get { return LazyBillVatDAL.Value; } }

        #region Read

        public IEnumerable<BILL_Vat> GetBillVat()
        {
            return billVatDAL.GetBillVat();
        }
        #endregion

        #region Read

        public BILL_Vat GetBillVat(Double rateBillVat)
        {
            return billVatDAL.GetBillVat(rateBillVat);
        }

        public BILL_Vat GetBillVat(DateTime dateBillVat)
        {
            return billVatDAL.GetBillVat(dateBillVat);
        }
        #endregion

        #region Create
        public BILL_Vat CreateBillVat(BILL_Vat billVatToAdd)
        {
            return billVatDAL.CreateBillVat(billVatToAdd);
        }
        #endregion

        #region Edit
        public BILL_Vat EditBillVat(BILL_Vat billVatToEdit)
        {
            return billVatDAL.EditBillVat(billVatToEdit);
        }
        #endregion

        #region Delete
        public bool DeleteBillVat(long id)
        {
            return billVatDAL.DeleteBillVat(id);
        }
        #endregion
    }
}
