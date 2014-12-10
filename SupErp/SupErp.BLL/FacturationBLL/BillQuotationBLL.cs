using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.DAL.FacturationDAL;
using SupErp.Entities;
using SupErp.DAL.FacturationModele;

namespace SupErp.BLL.FacturationBLL
{
    public class BillQuotationBLL
    {
        private static readonly Lazy<BillQuotationDAL> LazyBillQuotationDAL = new Lazy<BillQuotationDAL>(() => new BillQuotationDAL());
        private static BillQuotationDAL billQuotationDAL { get { return LazyBillQuotationDAL.Value; } }

        #region Read

        public IEnumerable<BillQuotationLight> GetBillQuotation()
        {
            return billQuotationDAL.GetBillQuotation().Select(b => new BillQuotationLight(b));
        }

        public BillQuotationLight GetBillByNum(string numBill)
        {
            var tmp = billQuotationDAL.GetBillByNum(numBill);

            return new BillQuotationLight(tmp);
        }

        public IEnumerable<BillQuotationLight> GetBills()
        {
            return billQuotationDAL.GetBills().Select(b => new BillQuotationLight(b));
        }

        public IEnumerable<BillQuotationLight> GetQuotations()
        {
            return billQuotationDAL.GetQuotations().Select(b => new BillQuotationLight(b));
        }

        public IEnumerable<BillQuotationLight> GetBillQuotationByStatus(BILL_Status status)
        {
            var res = new List<BillQuotationLight>();
            try
            {
                var list = billQuotationDAL.GetBillQuotation().Select(b => new BillQuotationLight(b)).Where(bq => bq.BillStatus.Status_Id == status.Status_Id);
                
            }
            catch(Exception ex)
            {
                throw new Exception(" BillQuotationBLL >> GetBillQuotationByStatus :" + ex.Message);
            }
            return res;
        }

        public BillQuotationLight GetBillQuotation(DateTime dateBillQuotation)
        {
            return new BillQuotationLight(billQuotationDAL.GetBillQuotation(dateBillQuotation));
        }

        public BillQuotationComplete GetBillQuotationsById(long id)
        {
            return new BillQuotationComplete(billQuotationDAL.GetBillQuotationsById(id));
        }

        #endregion

        #region Create

        public BILL_BillQuotation CreateBillQutotation(BILL_BillQuotation billQuotationToAdd)
        {
            return billQuotationDAL.CreateBillQutotation(billQuotationToAdd);
        }

        #endregion

        #region Edit

        public BILL_BillQuotation EditBillQuotation(BILL_BillQuotation billQuotationToEdit)
        {
            return billQuotationDAL.EditBillQuotation(billQuotationToEdit);
        }

        #endregion

        #region Delete

        public bool DeleteBillQuotation(long id)
        {
            return billQuotationDAL.DeleteBillQuotation(id);
        }

        #endregion

    }
}
