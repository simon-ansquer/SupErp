using SupErp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupErp.DAL.FacturationDAL
{
    public class BillQuotationDAL
    {
        #region Read

        public List<BILL_BillQuotation> GetBillQuotation()
        {
            var result = new List<BILL_BillQuotation>();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result = context.BILL_BillQuotation.Include("Company").Include("BILL_Transmitter")
                        .Include("BILL_BillQuotationStatus").Include("BILL_BillQuotationStatus.BILL_Status").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public List<BILL_BillQuotation> GetBillQuotationCompleted()
        {
            var result = new List<BILL_BillQuotation>();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result = context.BILL_BillQuotation.Include("Company").Include("BILL_Transmitter").Include("BILL_LineBillQuotation")
                        .Include("BILL_LineBillQuotation.BILL_Product").Include("BILL_LineBillQuotation.BILL_Product.BILL_Vat")
                        .Include("BILL_BillQuotationStatus").Include("BILL_BillQuotationStatus.BILL_Status")
                        .Include("BILL_LineBillQuotation.BILL_Product.BILL_Category").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public List<BILL_BillQuotation> GetBills()
        {
            var result = new List<BILL_BillQuotation>();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result = context.BILL_BillQuotation.Include("Company").Include("BILL_Transmitter").Include("BILL_LineBillQuotation")
                        .Include("BILL_LineBillQuotation.BILL_Product").Include("BILL_LineBillQuotation.BILL_Product.BILL_Vat")
                        .Include("BILL_BillQuotationStatus").Include("BILL_BillQuotationStatus.BILL_Status")
                        .Include("BILL_LineBillQuotation.BILL_Product.BILL_Category").Where(b => b.NBill != null).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public List<BILL_BillQuotation> GetQuotations()
        {
            var result = new List<BILL_BillQuotation>();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result = context.BILL_BillQuotation.Include("Company").Include("BILL_Transmitter").Include("BILL_LineBillQuotation")
                        .Include("BILL_LineBillQuotation.BILL_Product").Include("BILL_LineBillQuotation.BILL_Product.BILL_Vat")
                        .Include("BILL_BillQuotationStatus").Include("BILL_BillQuotationStatus.BILL_Status")
                        .Include("BILL_LineBillQuotation.BILL_Product.BILL_Category").Where(b => b.NBill == null).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public BILL_BillQuotation GetBillQuotationsById(long id)
        {
            var result = new BILL_BillQuotation();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result = context.BILL_BillQuotation.Include("Company").Include("BILL_Transmitter").Include("BILL_LineBillQuotation")
                        .Include("BILL_BillQuotationStatus").Include("BILL_BillQuotationStatus.BILL_Status")
                        .Include("BILL_LineBillQuotation.BILL_Product").Include("BILL_LineBillQuotation.BILL_Product.BILL_Vat")
                        .Include("BILL_LineBillQuotation.BILL_Product.BILL_Category")
                        .SingleOrDefault(b => b.BillQuotation_Id == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public BILL_BillQuotation GetBillByNum(string numBill)
        {
            var result = new BILL_BillQuotation();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result = context.BILL_BillQuotation.Include("Company").Include("BILL_Transmitter").Include("BILL_LineBillQuotation")
                        .Include("BILL_LineBillQuotation.BILL_Product").Include("BILL_LineBillQuotation.BILL_Product.BILL_Vat")
                        .Include("BILL_LineBillQuotation.BILL_Product.BILL_Category").SingleOrDefault(b => b.NBill == numBill);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public BILL_BillQuotation GetBillQuotation(DateTime dateBillQuotation)
        {
            var result = new BILL_BillQuotation();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    result = context.BILL_BillQuotation.Include("Company").Include("BILL_Transmitter").Include("BILL_LineBillQuotation")
                        .Include("BILL_LineBillQuotation.BILL_Product").Include("BILL_LineBillQuotation.BILL_Product.BILL_Vat")
                        .Include("BILL_LineBillQuotation.BILL_Product.BILL_Category").SingleOrDefault(b => b.DateBillQuotation == dateBillQuotation);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public static string getMaxNum()
        {
            var numStr = "000000001";

            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    var num = Convert.ToInt32(numStr);
                    var billQuotation = context.BILL_BillQuotation.OrderByDescending(b => b.NBill);
                    if (billQuotation != null && billQuotation.Count() > 0)
                    {
                        var nbill = billQuotation.First().NBill;
                        if (nbill != null)
                        {
                            var intNum = Convert.ToInt32(nbill) + 1;
                            numStr = intNum.ToString();
                        }
                    }

                    while (numStr.Length < 9)
                    {
                        numStr = "0" + numStr;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return numStr;
        }

        #endregion Read

        #region Create

        public BILL_BillQuotation CreateBillQutotation(BILL_BillQuotation billQuotationToAdd)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var b = context.BILL_BillQuotation.Add(billQuotationToAdd);
                context.SaveChanges();
                return b;
            }
        }

        #endregion Create

        #region Edit

        public BILL_BillQuotation EditBillQuotation(BILL_BillQuotation billQuotationToEdit)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                var b = context.BILL_BillQuotation.Find(billQuotationToEdit.BillQuotation_Id);
                if (b != null)
                {
                    b.AmountDF = billQuotationToEdit.AmountDF;
                    b.Company_Id = billQuotationToEdit.Company_Id;
                    b.Transmitter_Id = billQuotationToEdit.Transmitter_Id;
                    b.Vat = billQuotationToEdit.Vat;
                    context.SaveChanges();
                }
                return b;
            }
        }

        #endregion Edit

        #region Delete

        public bool DeleteBillQuotation(long id)
        {
            using (SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    var b = context.BILL_BillQuotation.Find(id);
                    context.BILL_BillQuotation.Remove(b);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion Delete

        public void SetNumFacture()
        {
            var result = new BILL_BillQuotation();
            try
            {
                using (SUPERPEntities context = new SUPERPEntities(false))
                {
                    var results = context.BILL_BillQuotation.OrderByDescending(b => b.BillQuotation_Id);
                    if (results != null)
                    {
                        result = results.First();
                        if (result != null && result.NBill.Equals("1"))
                        {
                            var numStr = "000000001";

                            var num = Convert.ToInt32(numStr);
                            var billQuotation = context.BILL_BillQuotation.OrderByDescending(b => b.NBill).Where(b => !b.NBill.Equals("1"));
                            if (billQuotation != null && billQuotation.Count() > 0)
                            {
                                var nbill = billQuotation.First().NBill;
                                if (nbill != null)
                                {
                                    var intNum = Convert.ToInt32(nbill) + 1;
                                    numStr = intNum.ToString();
                                }
                            }

                            while (numStr.Length < 9)
                            {
                                numStr = "0" + numStr;
                            }

                            //Set NbBill
                            result.NBill = numStr;
                        }
                        else if (result != null && result.NBill.Equals("0"))
                            result.NBill = null;
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}