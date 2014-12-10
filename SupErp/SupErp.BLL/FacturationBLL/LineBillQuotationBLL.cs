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
    public class LineBillQuotationBLL
    {
        private static readonly Lazy<LineBillQuotationDAL> LazyLineDAL = new Lazy<LineBillQuotationDAL>(() => new LineBillQuotationDAL());
        private static LineBillQuotationDAL DAL { get { return LazyLineDAL.Value; } }


        #region Read

        public List<BILL_LineBillQuotation> GetLineBillQuotation(long billQuotation_id)
        {
            return DAL.GetLineBillQuotation(billQuotation_id).ToList();
        }
        
        #endregion

        #region Create

        public List<BILL_LineBillQuotation> CreateLineBillQuotation(List<BILL_LineBillQuotation> billLineToAdd)
        {
            return DAL.CreateLineBillQuotation(billLineToAdd);
        }

        #endregion

        #region Edit

        public List<BILL_LineBillQuotation> EditLineBillQuotation(List<BILL_LineBillQuotation> LineBillQuotationToEdit)
        {
           return DAL.EditLineBillQuotation(LineBillQuotationToEdit);
        }

        #endregion

        #region Delete

        public bool DeleteLineBillQuotation(List<BILL_LineBillQuotation> listQuotation)
        {
            var listID = new List<long>();

            foreach (var b in listQuotation)
                listID.Add(b.LineBillQuotation_Id);

            return DAL.DeleteLineBillQuotation(listID);
        }

        #endregion
    }
}
