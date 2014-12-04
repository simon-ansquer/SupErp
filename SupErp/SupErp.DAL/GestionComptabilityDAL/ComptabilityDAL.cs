using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;

namespace SupErp.DAL.GestionComptabilityDAL
{
    public class ComptabilityDAL
    {
        #region Read

        public COMPTA_AccountingEntries GetAccountingEntriesById ( int id )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_AccountingEntries.Include("COMPTA_ChartOfAccounts").FirstOrDefault(x => x.id == id);
            }
        }

        public IEnumerable<COMPTA_AccountingEntries> GetAccountingEntries ()
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_AccountingEntries.Include("COMPTA_ChartOfAccounts");
            }
        }

        public IEnumerable<COMPTA_ChartOfAccounts> GetChartOfAccounts ()
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_ChartOfAccounts.Include("COMPTA_ClassOfAccounts");
            }
        }

        public IEnumerable<COMPTA_ClassOfAccounts> GetClassOfAccounts ()
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_ClassOfAccounts;
            }
        }

        public COMPTA_ExchangeRate GetLastExchangeRate ()
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_ExchangeRate.OrderByDescending(x => x.updatedDate).FirstOrDefault();
            }
        }

        public COMPTA_Bank GetBankById ( int id)
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_Bank.FirstOrDefault(x => x.id == id);
            }
        }

        public IEnumerable<COMPTA_Bank> GetBank ()
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_Bank;
            }
        }

        public COMPTA_BankAccount GetBankAccountById ( int id )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_BankAccount
                    .Include("COMPTA_Bank")
                    .Include("COMPTA_BankJournalLine")
                    .Include("COMPTA_Currency")
                    .FirstOrDefault(x => x.id == id);
            }
        }

        public IEnumerable<COMPTA_BankAccount> GetBankAccount ()
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_BankAccount
                    .Include("COMPTA_Bank")
                    .Include("COMPTA_BankJournalLine")
                    .Include("COMPTA_Currency");
            }
        }

        public IEnumerable<COMPTA_BankJournalLine> GetBankJournalLine ()
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_BankJournalLine.Include("COMPTA_BankAccount");
            }
        }

        public COMPTA_Currency GetCurrencyById ( int id )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_Currency.Include("COMPTA_BankAccount").FirstOrDefault(x => x.id == id);
            }
        }

        public IEnumerable<COMPTA_Currency> GetCurrency ()
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                return context.COMPTA_Currency.Include("COMPTA_BankAccount");
            }
        }

        #endregion

        #region Create

        public COMPTA_Bank CreateBank ( COMPTA_Bank bankToAdd )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {              
                var bank = context.COMPTA_Bank.Add(bankToAdd);
                context.SaveChanges();
                return bank;
            }
        }

        public COMPTA_BankAccount CreateBankAccount ( COMPTA_BankAccount bankAccountToAdd )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                var bankAccount = context.COMPTA_BankAccount.Add(bankAccountToAdd);
                context.SaveChanges();
                return bankAccount;
            }
        }

        public COMPTA_Currency CreateCurrency ( COMPTA_Currency currencyToAdd )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                var currency = context.COMPTA_Currency.Add(currencyToAdd);
                context.SaveChanges();
                return currency;
            }
        }

        public COMPTA_BankJournalLine CreateBankJournalLine ( COMPTA_BankJournalLine bankJournalLineToAdd )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                var bankJournalLine = context.COMPTA_BankJournalLine.Add(bankJournalLineToAdd);
                context.SaveChanges();
                return bankJournalLine;
            }
        }

        public COMPTA_ExchangeRate CreateExchangeRate ( COMPTA_ExchangeRate exchangeRateToAdd )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                var exchangeRate = context.COMPTA_ExchangeRate.Add(exchangeRateToAdd);
                context.SaveChanges();
                return exchangeRate;
            }
        }

        public COMPTA_ChartOfAccounts CreateChartOfAccounts ( COMPTA_ChartOfAccounts chartOfAcountsToAdd )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                var chartOfAccounts = context.COMPTA_ChartOfAccounts.Add(chartOfAcountsToAdd);
                context.SaveChanges();
                return chartOfAccounts;
            }
        }

        public COMPTA_ClassOfAccounts CreateClassOfAccounts ( COMPTA_ClassOfAccounts classOfAccountsToAdd )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                var classOfAccounts = context.COMPTA_ClassOfAccounts.Add(classOfAccountsToAdd);
                context.SaveChanges();
                return classOfAccounts;
            }
        }

        public COMPTA_SupplierJournalLine CreateSupplierJournalLine ( COMPTA_SupplierJournalLine supplierJournalLineToAdd )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                var supplierJournalLine = context.COMPTA_SupplierJournalLine.Add(supplierJournalLineToAdd);
                context.SaveChanges();
                return supplierJournalLine;
            }
        }

        public COMPTA_CustomerJournalLine CreateCustomerJournalLine ( COMPTA_CustomerJournalLine customerJournalLineToAdd )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                var customerJournalLine = context.COMPTA_CustomerJournalLine.Add(customerJournalLineToAdd);
                context.SaveChanges();
                return customerJournalLine;
            }
        }

        public COMPTA_AccountingEntries CreateAccountingEntries ( COMPTA_AccountingEntries accountingEntriesToAdd )
        {
            using ( SUPERPEntities context = new SUPERPEntities(false) )
            {
                var accountingEntries = context.COMPTA_AccountingEntries.Add(accountingEntriesToAdd);
                context.SaveChanges();
                return accountingEntries;
            }
        }

        #endregion       
    }
}
