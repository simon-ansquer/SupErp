using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.BLL.ComptabilityBLL.BllObject;
using SupErp.DAL.GestionComptabilityDAL;
using SupErp.BLL.ComptabilityBLL.Assembleur;
using SupErp.Entities;
using SupErp.BLL.CurrencyConvertor;


namespace SupErp.BLL.ComptabilityBLL
{
    public class PlanComptableBLL
    {
        private static readonly Lazy<ComptabilityDAL> lazyComptabilityDal = new Lazy<ComptabilityDAL>(() => new ComptabilityDAL());
        private static ComptabilityDAL comptabilityDal { get { return lazyComptabilityDal.Value; } }

        /// <summary>
        /// Récupère et re-structure le plan Comptable
        /// </summary>
        /// <returns>Plan Comptable sous forme de Liste</returns>
        public IEnumerable<ClassOfAccount> GetPlanComptable()
        {
            List<ClassOfAccount> classAccount = new List<ClassOfAccount>(comptabilityDal.GetAccountingClasses().ToClassOfAccount());

            List<ChartsOfAccount> chartsAccount = new List<ChartsOfAccount>(comptabilityDal.GetAccountingAccounts().ToChartsOfAccount());

            foreach ( var _class in classAccount )
            {
                List<ChartsOfAccount> _listCharts = new List<ChartsOfAccount>();
                
                if ( _class.ChartsOfAccount == null )
                    _class.ChartsOfAccount = new List<ChartsOfAccount>();

                _listCharts.AddRange(chartsAccount.Where(x => x.class_id == _class.id && x.account_number.HasValue && x.account_number.Value.ToString().Length == 2));

                foreach (var chartLv2 in _listCharts)
	            {
                    List<ChartsOfAccount> _listCharts3 = new List<ChartsOfAccount>();

                    // Tronquer les 2 premier et Comparé avec X
                    _listCharts3.AddRange(chartsAccount.Where(x => x.class_id == _class.id && x.account_number.HasValue && x.account_number.Value.ToString().Length == 3 && x.account_number.Value.ToString().Substring(0,2).Equals(chartLv2.account_number.Value.ToString().Substring(0,2))));

                    foreach ( var chartLv3 in _listCharts3 )
                    {
                        List<ChartsOfAccount> _listCharts4 = new List<ChartsOfAccount>();

                        // Tronquer les 3 premiers et COmparé avec X
                        _listCharts4.AddRange(chartsAccount.Where(x => x.class_id == _class.id && x.account_number.HasValue && x.account_number.Value.ToString().Length == 4 && x.account_number.Value.ToString().Substring(0, 3).Equals(chartLv3.account_number.Value.ToString().Substring(0, 3))));

                        chartLv3.chartsOfAccount = _listCharts4;
                    }

                    chartLv2.chartsOfAccount = _listCharts3;
	            }

                _class.ChartsOfAccount = _listCharts;
            }

            return classAccount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public COMPTA_ExchangeRate GetLastExchangeRate ()
        {
            var result = comptabilityDal.GetLastExchangeRate();

            if ( result == null || DateTime.Now - result.updatedDate > new TimeSpan(2, 0, 0, 0, 0) )
            {
                // Call Update WebService for ExchangeRate Update =)
                //comptabilityDal.
                // ReGet the value updated
                CurrencyConvertorSoapClient client = new CurrencyConvertorSoapClient();

                try
                {
                    COMPTA_ExchangeRate exchangeRate = new COMPTA_ExchangeRate();

                    exchangeRate.EURO_AUD = client.ConversionRate(Currency.EUR, Currency.AUD);
                    exchangeRate.EURO_GBP = client.ConversionRate(Currency.EUR, Currency.GBP);
                    exchangeRate.EURO_USD = client.ConversionRate(Currency.EUR, Currency.USD);
                    exchangeRate.EURO_ZAR = client.ConversionRate(Currency.EUR, Currency.ZAR);
                    exchangeRate.USD_EURO = client.ConversionRate(Currency.USD, Currency.EUR);
                    exchangeRate.updatedDate = DateTime.Now;
                    
                    comptabilityDal.CreateExchangeRate(exchangeRate);

                    result = comptabilityDal.GetLastExchangeRate();
                }
                catch (Exception e)
                {
                   
                }


            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<COMPTA_Currency> GetCurrency ()
        {
            var result = comptabilityDal.GetCurrencies();

            if ( result == null )
                return null;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<COMPTA_Bank> GetBank ()
        {
            var result = comptabilityDal.GetBanks();

            if ( result == null )
                return null;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<COMPTA_BankAccount> GetBankAccount ()
        {
            var result = comptabilityDal.GetBankAccounts();

            if ( result == null )
                return null;

            return result;
        }
        
        public IEnumerable<>

        /*
        public IEnumerable<COMPTA_AccountingEntries> GetAccountingEntries()
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.COMPTA_AccountingEntries.Include("COMPTA_ChartOfAccounts");
            }
        }

        public IEnumerable<COMPTA_BankJournalLine> GetBankJournalLine()
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.COMPTA_BankJournalLine.Include("COMPTA_BankAccount");
            }
        }

        public IEnumerable<COMPTA_CustomerJournalLine> GetCustomerJournalLine()
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.COMPTA_CustomerJournalLine.Include("Company");
            }
        }

        public IEnumerable<COMPTA_SupplierJournalLine> GetSupplierJournalLine()
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                return context.COMPTA_SupplierJournalLine.Include("Company");
            }
        }

        #endregion

        #region Create

        public COMPTA_ClassOfAccounts CreateClassOfAccounts(COMPTA_ClassOfAccounts classOfAccountsToAdd)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var classOfAccounts = context.COMPTA_ClassOfAccounts.Add(classOfAccountsToAdd);
                context.SaveChanges();
                return classOfAccounts;
            }
        }

        public COMPTA_ChartOfAccounts CreateChartOfAccounts(COMPTA_ChartOfAccounts chartOfAcountsToAdd)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var chartOfAccounts = context.COMPTA_ChartOfAccounts.Add(chartOfAcountsToAdd);
                context.SaveChanges();
                return chartOfAccounts;
            }
        }

        public COMPTA_ExchangeRate CreateExchangeRate(COMPTA_ExchangeRate exchangeRateToAdd)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var exchangeRate = context.COMPTA_ExchangeRate.Add(exchangeRateToAdd);
                context.SaveChanges();
                return exchangeRate;
            }
        }

        public COMPTA_Currency CreateCurrency(COMPTA_Currency currencyToAdd)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var currency = context.COMPTA_Currency.Add(currencyToAdd);
                context.SaveChanges();
                return currency;
            }
        }

        public COMPTA_Bank CreateBank(COMPTA_Bank bankToAdd)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {              
                var bank = context.COMPTA_Bank.Add(bankToAdd);
                context.SaveChanges();
                return bank;
            }
        }

        public COMPTA_BankAccount CreateBankAccount(COMPTA_BankAccount bankAccountToAdd)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var bankAccount = context.COMPTA_BankAccount.Add(bankAccountToAdd);
                context.SaveChanges();
                return bankAccount;
            }
        }

        public COMPTA_AccountingEntries CreateAccountingEntries(COMPTA_AccountingEntries accountingEntriesToAdd)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var accountingEntries = context.COMPTA_AccountingEntries.Add(accountingEntriesToAdd);
                context.SaveChanges();
                return accountingEntries;
            }
        }

        public COMPTA_BankJournalLine CreateBankJournalLine(COMPTA_BankJournalLine bankJournalLineToAdd)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var bankJournalLine = context.COMPTA_BankJournalLine.Add(bankJournalLineToAdd);
                context.SaveChanges();
                return bankJournalLine;
            }
        }

        public COMPTA_CustomerJournalLine CreateCustomerJournalLine(COMPTA_CustomerJournalLine customerJournalLineToAdd)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var customerJournalLine = context.COMPTA_CustomerJournalLine.Add(customerJournalLineToAdd);
                context.SaveChanges();
                return customerJournalLine;
            }
        }

        public COMPTA_SupplierJournalLine CreateSupplierJournalLine(COMPTA_SupplierJournalLine supplierJournalLineToAdd)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var supplierJournalLine = context.COMPTA_SupplierJournalLine.Add(supplierJournalLineToAdd);
                context.SaveChanges();
                return supplierJournalLine;
            }
        }

        #endregion

        #region Edit

        public COMPTA_ClassOfAccounts EditClassOfAccounts(COMPTA_ClassOfAccounts classOfAccountsToEdit)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var classOfAccounts = context.COMPTA_ClassOfAccounts.Find(classOfAccountsToEdit.id);

                if (classOfAccounts == null)
                    return null;

                classOfAccounts = classOfAccountsToEdit;
                context.SaveChanges();
                return classOfAccounts;
            }
        }

        public COMPTA_ChartOfAccounts EditChartOfAccounts(COMPTA_ChartOfAccounts chartOfAccountsToEdit)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var chartOfAccounts = context.COMPTA_ChartOfAccounts.Find(chartOfAccountsToEdit.id);

                if (chartOfAccounts == null)
                    return null;

                chartOfAccounts = chartOfAccountsToEdit;
                context.SaveChanges();
                return chartOfAccounts;
            }
        }

        public COMPTA_ExchangeRate EditExchangeRate(COMPTA_ExchangeRate exchangeRateToEdit)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var exchangeRate = context.COMPTA_ExchangeRate.Find(exchangeRateToEdit.id);

                if (exchangeRate == null)
                    return null;

                exchangeRate = exchangeRateToEdit;
                context.SaveChanges();
                return exchangeRate;
            }
        }

        public COMPTA_Currency EditCurrency(COMPTA_Currency currencyToEdit)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var currency = context.COMPTA_Currency.Find(currencyToEdit.id);

                if (currency == null)
                    return null;

                currency = currencyToEdit;
                context.SaveChanges();
                return currency;
            }
        }

        public COMPTA_Bank EditBank(COMPTA_Bank bankToEdit)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var bank = context.COMPTA_Bank.Find(bankToEdit.id);

                if (bank == null)
                    return null;

                bank = bankToEdit;
                context.SaveChanges();
                return bank;
            }
        }

        public COMPTA_BankAccount EditBankAccount(COMPTA_BankAccount bankAccountToEdit)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var bankAccount = context.COMPTA_BankAccount.Find(bankAccountToEdit.id);

                if (bankAccount == null)
                    return null;

                bankAccount = bankAccountToEdit;
                context.SaveChanges();
                return bankAccount;
            }
        }

        public COMPTA_AccountingEntries EditAccountingEntry(COMPTA_AccountingEntries accountingEntryToEdit)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var accountingEntry = context.COMPTA_AccountingEntries.Find(accountingEntryToEdit.id);

                if (accountingEntry == null)
                    return null;

                accountingEntry = accountingEntryToEdit;
                context.SaveChanges();
                return accountingEntry;
            }
        }

        public COMPTA_BankJournalLine EditBankJournalLine(COMPTA_BankJournalLine bankJournalLineToEdit)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var bankJournalLine = context.COMPTA_BankJournalLine.Find(bankJournalLineToEdit.id);

                if (bankJournalLine == null)
                    return null;

                bankJournalLine = bankJournalLineToEdit;
                context.SaveChanges();
                return bankJournalLine;
            }
        }

        public COMPTA_CustomerJournalLine EditCustomerJournalLine(COMPTA_CustomerJournalLine customerJournalLineToEdit)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var customerJournalLine = context.COMPTA_CustomerJournalLine.Find(customerJournalLineToEdit.id);

                if (customerJournalLine == null)
                    return null;

                customerJournalLine = customerJournalLineToEdit;
                context.SaveChanges();
                return customerJournalLine;
            }
        }

        public COMPTA_SupplierJournalLine EditSupplierJournalLine(COMPTA_SupplierJournalLine supplierJournalLineToEdit)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                var supplierJournalLine = context.COMPTA_SupplierJournalLine.Find(supplierJournalLineToEdit.id);

                if (supplierJournalLine == null)
                    return null;

                supplierJournalLine = supplierJournalLineToEdit;
                context.SaveChanges();
                return supplierJournalLine;
            }
        }

        #endregion

        #region Delete

        public bool DeleteClassOfAccounts(int id)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    context.COMPTA_ClassOfAccounts.Remove(context.COMPTA_ClassOfAccounts.Find(id));
                    context.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Echec de suppression de la classe comptable. Message : " + e.Message);
                    return false;
                }
            }
        }

        public bool DeleteChartOfAccounts(int id)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    context.COMPTA_ChartOfAccounts.Remove(context.COMPTA_ChartOfAccounts.Find(id));
                    context.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Echec de suppression du compte comptable : " + e.Message);
                    return false;
                }
            }
        }

        public bool DeleteExchangeRate(int id)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    context.COMPTA_ExchangeRate.Remove(context.COMPTA_ExchangeRate.Find(id));
                    context.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Echec de suppression du taux de change : " + e.Message);
                    return false;
                }
            }
        }

        public bool DeleteCurrency(int id)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    context.COMPTA_Currency.Remove(context.COMPTA_Currency.Find(id));
                    context.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Echec de suppression de la devise : " + e.Message);
                    return false;
                }
            }
        }

        public bool DeleteBank(int id)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    context.COMPTA_Bank.Remove(context.COMPTA_Bank.Find(id));
                    context.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Echec de suppression de la banque : " + e.Message);
                    return false;
                }
            }
        }

        public bool DeleteBankAccount(int id)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    context.COMPTA_BankAccount.Remove(context.COMPTA_BankAccount.Find(id));
                    context.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Echec de suppression du compte bancaire : " + e.Message);
                    return false;
                }
            }
        }

        public bool DeleteAccountingEntry(int id)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    context.COMPTA_AccountingEntries.Remove(context.COMPTA_AccountingEntries.Find(id));
                    context.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Echec de suppression de la ligne du journal général : " + e.Message);
                    return false;
                }
            }
        }

        public bool DeleteBankJournalLine(int id)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
            {
                try
                {
                    context.COMPTA_BankJournalLine.Remove(context.COMPTA_BankJournalLine.Find(id));
                    context.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Echec de suppression de la ligne du journal de banque " + e.Message);
                    return false;
                }
            }
        }

        public bool DeleteCustomerJournalLine(int id)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
        {
                try
            {
                    context.COMPTA_CustomerJournalLine.Remove(context.COMPTA_CustomerJournalLine.Find(id));
                context.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Echec de suppression de la ligne du journal client : " + e.Message);                    
                    return false;
                }
            }
        }

        public bool DeleteSupplierJournalLine(int id)
        {
            using(SUPERPEntities context = new SUPERPEntities(false))
        {
                try
            {
                    context.COMPTA_SupplierJournalLine.Remove(context.COMPTA_SupplierJournalLine.Find(id));
                context.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    Debug.WriteLine("Echec de suppression de la ligne du journal fournisseur : " + e.Message);
                    return false;
                }
            }
        }*/
    }
}
