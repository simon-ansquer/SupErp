using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.ComptabilityDAL
{
    public class ComptabilityDAL
    {
        #region READ

        public COMPTA_Bank CreateBank ( COMPTA_Bank _bank )
        {
            using ( var context = new SUPERPEntities(false) )
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                context.COMPTA_Bank.Add(_bank);
                int i = context.SaveChanges();
                if ( i == 0 )
                    return null;
                else return _bank;
            }
        }

        public COMPTA_BankAccount CreateBankAccount ( COMPTA_BankAccount _bankAccount )
        {
            using ( var context = new SUPERPEntities(false) )
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                context.COMPTA_BankAccount.Add(_bankAccount);
                int i = context.SaveChanges();
                if ( i == 0 )
                    return null;
                else return _bankAccount;
            }
        }

        public COMPTA_BankJournalLine CreateBankJournalLine ( COMPTA_BankJournalLine _bankJournalLine )
        {
            using ( var context = new SUPERPEntities(false) )
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                context.COMPTA_BankJournalLine.Add(_bankJournalLine);
                int i = context.SaveChanges();
                if ( i == 0 )
                    return null;
                else return _bankJournalLine;
            }
        }

        public COMPTA_ChartOfAccounts CreateChartOfAccounts ( COMPTA_ChartOfAccounts _chartsOfAccounts )
        {
            using ( var context = new SUPERPEntities(false) )
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                context.COMPTA_ChartOfAccounts.Add(_chartsOfAccounts);
                int i = context.SaveChanges();
                if ( i == 0 )
                    return null;
                else return _chartsOfAccounts;
            }
        }

        public COMPTA_ClassOfAccounts CreateClassOfAccounts ( COMPTA_ClassOfAccounts _classOfAccounts )
        {
            using ( var context = new SUPERPEntities(false) )
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                context.COMPTA_ClassOfAccounts.Add(_classOfAccounts);
                int i = context.SaveChanges();
                if ( i == 0 )
                    return null;
                else return _classOfAccounts;
            }
        }

        public COMPTA_Currency CreateCurrency ( COMPTA_Currency _currency )
        {
            using ( var context = new SUPERPEntities(false) )
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                context.COMPTA_Currency.Add(_currency);
                int i = context.SaveChanges();
                if ( i == 0 )
                    return null;
                else return _currency;
            }
        }

        public COMPTA_CustomerJournalLine CreateCurrency ( COMPTA_CustomerJournalLine _customerJournalLine )
        {
            using ( var context = new SUPERPEntities(false) )
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                context.COMPTA_CustomerJournalLine.Add(_customerJournalLine);
                int i = context.SaveChanges();
                if ( i == 0 )
                    return null;
                else return _customerJournalLine;
            }
        }

        public COMPTA_ExchangeRate CreateCurrency ( COMPTA_ExchangeRate _exchangeRate )
        {
            using ( var context = new SUPERPEntities(false) )
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                context.COMPTA_ExchangeRate.Add(_exchangeRate);
                int i = context.SaveChanges();
                if ( i == 0 )
                    return null;
                else return _exchangeRate;
            }
        }

        public COMPTA_SupplierJournalLine CreateCurrency ( COMPTA_SupplierJournalLine _supplierJournalLine )
        {
            using ( var context = new SUPERPEntities(false) )
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;

                context.COMPTA_SupplierJournalLine.Add(_supplierJournalLine);
                int i = context.SaveChanges();
                if ( i == 0 )
                    return null;
                else return _supplierJournalLine;
            }
        }

        #endregion

        #region CREATE

        #endregion

        #region DELETE

        #endregion

        #region UPDATE

        #endregion

    }
}
