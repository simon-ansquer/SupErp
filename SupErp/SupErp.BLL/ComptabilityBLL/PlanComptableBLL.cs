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
                CurrencyConvertor.CurrencyConvertorSoapClient client = new CurrencyConvertor.CurrencyConvertorSoapClient("CurrencyConvertorSoap",new System.ServiceModel.EndpointAddress("http://www.webservicex.net/CurrencyConvertor.asmx"));

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

        /// <summary>
        /// Récupère les entrées et sorties des comptes comptables
        /// </summary>
        /// <param name="type">Type d'entrée : Crédit ou Débit</param>
        /// <param name="paye"></param>
        /// <param name="impaye"></param>
        /// <param name="Debut"></param>
        /// <param name="Fin"></param>
        /// <returns></returns>
        public IEnumerable<Entries> GetEntries ( EntriesTypeEnum? type, bool? paye, bool? impaye, DateTime? Debut, DateTime? Fin )
        {
            var bankResult = comptabilityDal.GetBankJournalLines();
            var supplierResult = comptabilityDal.GetSupplierJournalLines();
            var customerResult = comptabilityDal.GetCustomerJournalLines();
            var accountingResult = comptabilityDal.GetAccountingEntries();

            List<COMPTA_BankJournalLine> resultBank = new List<COMPTA_BankJournalLine>();
            List<COMPTA_CustomerJournalLine> resultCustomer = new List<COMPTA_CustomerJournalLine>();
            List<COMPTA_SupplierJournalLine> resultSupplier = new List<COMPTA_SupplierJournalLine>();
            List<COMPTA_AccountingEntries> resultAccounting = new List<COMPTA_AccountingEntries>();

            List<Entries> finalEntries = new List<Entries>();

            bool noFilter = false;

            if ( !type.HasValue && !paye.HasValue && !impaye.HasValue && !Debut.HasValue && !Fin.HasValue )
                noFilter = true;

            #region Bank Journal

            if ( resultBank != null )
            {
                if(type.HasValue)
                {
                    switch(type)
                    {
                        case EntriesTypeEnum.Credit:
                            resultBank = new List<COMPTA_BankJournalLine>(bankResult.Where(x => x.direction.Value == true));
                            break;
                        case EntriesTypeEnum.Debit:
                            resultBank = new List<COMPTA_BankJournalLine>(bankResult.Where(x => x.direction.Value == false));
                            break;
                    }
                }

                if ( Debut.HasValue && Fin.HasValue )
                    resultBank = new List<COMPTA_BankJournalLine>(resultBank.Where(x => Debut.Value < x.postingDate.Value && x.postingDate.Value < Fin.Value));
                else if ( Debut.HasValue )
                    resultBank = new List<COMPTA_BankJournalLine>(resultBank.Where(x => Debut.Value < x.postingDate.Value));
                else if ( Fin.HasValue )
                    resultBank = new List<COMPTA_BankJournalLine>(resultBank.Where(x => x.postingDate.Value < Fin.Value));

                resultBank.ForEach(delegate( COMPTA_BankJournalLine entry )
                {
                    Entries finalEntry = new Entries()
                    {
                        amount = entry.amount,
                        EntryType = entry.direction == false ? EntriesTypeEnum.Debit : EntriesTypeEnum.Credit,
                        Foreign_id = entry.bankAccount_id,
                        id = entry.id,
                        postingDate = entry.postingDate,
                        SourceType = SourceEntriesEnum.Bank
                    };

                    finalEntries.Add(finalEntry);
                });
            }

            #endregion

            #region Customer Journal

            if ( customerResult != null )
            {
                if ( type.HasValue )
                {
                    switch ( type )
                    {
                        case EntriesTypeEnum.Credit:
                            resultCustomer = new List<COMPTA_CustomerJournalLine>(customerResult.Where(x => x.direction.Value == true));
                            break;
                        case EntriesTypeEnum.Debit:
                            resultCustomer = new List<COMPTA_CustomerJournalLine>(customerResult.Where(x => x.direction.Value == false));
                            break;
                    }
                }

                if ( Debut.HasValue && Fin.HasValue )
                    resultCustomer = new List<COMPTA_CustomerJournalLine>(resultCustomer.Where(x => Debut.Value < x.postingDate.Value && x.postingDate.Value < Fin.Value));
                else if ( Debut.HasValue )
                    resultCustomer = new List<COMPTA_CustomerJournalLine>(resultCustomer.Where(x => Debut.Value < x.postingDate.Value));
                else if ( Fin.HasValue )
                    resultCustomer = new List<COMPTA_CustomerJournalLine>(resultCustomer.Where(x => x.postingDate.Value < Fin.Value));

                resultCustomer.ForEach(delegate( COMPTA_CustomerJournalLine entry )
                {
                    Entries finalEntry = new Entries()
                    {
                        amount = entry.amount,
                        EntryType = entry.direction == false ? EntriesTypeEnum.Debit : EntriesTypeEnum.Credit,
                        Foreign_id = entry.clientAccount_id,
                        id = entry.id,
                        postingDate = entry.postingDate,
                        SourceType = SourceEntriesEnum.Customer
                    };

                    finalEntries.Add(finalEntry);
                });
            }

            #endregion

            #region  Supplier Journal

            if ( supplierResult != null )
            {
                if ( type.HasValue )
                {
                    switch ( type )
                    {
                        case EntriesTypeEnum.Credit:
                            resultSupplier = new List<COMPTA_SupplierJournalLine>(supplierResult.Where(x => x.direction.Value == true));
                            break;
                        case EntriesTypeEnum.Debit:
                            resultSupplier = new List<COMPTA_SupplierJournalLine>(supplierResult.Where(x => x.direction.Value == false));
                            break;
                    }
                }

                if ( Debut.HasValue && Fin.HasValue )
                    resultSupplier = new List<COMPTA_SupplierJournalLine>(resultSupplier.Where(x => Debut.Value < x.postingDate.Value && x.postingDate.Value < Fin.Value));
                else if ( Debut.HasValue )
                    resultSupplier = new List<COMPTA_SupplierJournalLine>(resultSupplier.Where(x => Debut.Value < x.postingDate.Value));
                else if ( Fin.HasValue )
                    resultSupplier = new List<COMPTA_SupplierJournalLine>(resultSupplier.Where(x => x.postingDate.Value < Fin.Value));

                resultSupplier.ForEach(delegate( COMPTA_SupplierJournalLine entry )
                {
                    Entries finalEntry = new Entries()
                    {
                        amount = entry.amount,
                        EntryType = entry.direction == false ? EntriesTypeEnum.Debit : EntriesTypeEnum.Credit,
                        Foreign_id = entry.SupplierAccount_id,
                        id = entry.id,
                        postingDate = entry.postingDate,
                        SourceType = SourceEntriesEnum.Supplier
                    };

                    finalEntries.Add(finalEntry);
                });
            }

            #endregion

            #region Accounting Journal

            if ( accountingResult != null )
            {
                if ( type.HasValue )
                {
                    switch ( type )
                    {
                        case EntriesTypeEnum.Credit:
                            resultAccounting = new List<COMPTA_AccountingEntries>(accountingResult.Where(x => x.direction.Value == true));
                            break;
                        case EntriesTypeEnum.Debit:
                            resultAccounting = new List<COMPTA_AccountingEntries>(accountingResult.Where(x => x.direction.Value == false));
                            break;
                    }
                }

                if ( Debut.HasValue && Fin.HasValue )
                    resultAccounting = new List<COMPTA_AccountingEntries>(resultAccounting.Where(x => Debut.Value < x.postingDate.Value && x.postingDate.Value < Fin.Value));
                else if ( Debut.HasValue )
                    resultAccounting = new List<COMPTA_AccountingEntries>(resultAccounting.Where(x => Debut.Value < x.postingDate.Value));
                else if ( Fin.HasValue )
                    resultAccounting = new List<COMPTA_AccountingEntries>(resultAccounting.Where(x => x.postingDate.Value < Fin.Value));

                resultAccounting.ForEach(delegate( COMPTA_AccountingEntries entry )
                {
                    Entries finalEntry = new Entries()
                    {
                        amount = entry.amount,
                        EntryType = entry.direction == false ? EntriesTypeEnum.Debit : EntriesTypeEnum.Credit,
                        Foreign_id = entry.chartOfAccount_id,
                        id = entry.id,
                        postingDate = entry.postingDate,
                        SourceType = SourceEntriesEnum.Accounting
                    };

                    finalEntries.Add(finalEntry);
                });
            }

            #endregion

            return finalEntries;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Debut"></param>
        /// <param name="Fin"></param>
        /// <returns></returns>
        public IEnumerable<BilanComptable> GetBilanComptable ( BilanComptableModeEnum Mode, DateTime? Debut, DateTime? Fin )
        {
            if ( !Debut.HasValue )
                Debut = DateTime.Now;
            if ( !Fin.HasValue )
                Fin = DateTime.Now;

            List<BilanComptable> listBilan;

            IEnumerable<COMPTA_AccountingEntries> accountLines = comptabilityDal.GetAccountingEntries();

            if (Mode == BilanComptableModeEnum.Month)
            {
                CreateBilanComptableModeMonth(accountLines, Debut.Value, Fin.Value, out listBilan);
                return listBilan;
            }
            else if ( Mode == BilanComptableModeEnum.Year )
            {
                CreateBilanComptableModeYear(accountLines, Debut.Value, Fin.Value, out listBilan);
                return listBilan;
            }
            return null;
        }

        private void CreateBilanComptableModeYear(IEnumerable<COMPTA_AccountingEntries> accountLines, DateTime Debut, DateTime Fin, out List<BilanComptable> bilan)
        {
            DateTime refTime = Debut;
            DateTime refEndTime = Fin;

            int yearDifference, month;

            yearDifference = Debut.Year - Fin.Year;

            int Capacity = ( 12 - ( Debut.Month - 1 ) + 12 * yearDifference + Fin.Month );

            bilan = new List<BilanComptable>(Capacity);

            int count = 0;

            while ( refTime.Year != refEndTime.Year + 1 )
            {
                // On récupère les lignes correspondante à notre année de réfèrence
                var result = accountLines.Where(x => x.postingDate.HasValue && x.postingDate.Value.Year == refTime.Year);

                if ( result != null )
                {
                    // On instancie un nouveau bilanComptable
                    BilanComptable tempBilan = new BilanComptable();
                    tempBilan.TimePoint = new DateTime(refTime.Year, 1, 1);

                    // Pour chaque ligne récupère on affecte ça valeur positive ou négative selon sa direction
                    foreach ( var lineAccount in result )
                    {
                        if ( lineAccount.amount.HasValue && lineAccount.direction.HasValue )
                            tempBilan.Amount += lineAccount.direction == false ? -lineAccount.amount.Value : lineAccount.amount.Value;
                    }

                    // On assigne la valeur du Bilan temporaire à la listeFinale au niveau de l'index courant
                    bilan[count] = tempBilan;
                    // On incrémente l'index
                    count++;

                    // On recalcule la date de réfèrence et on ré-assigne la valeur au temps de réfèrence
                    int refYear = refTime.Year + 1;

                    refTime = new DateTime(refYear, 1, 1);
                }

            }
        }

        private void CreateBilanComptableModeMonth ( IEnumerable<COMPTA_AccountingEntries> accountLines, DateTime Debut, DateTime Fin, out List<BilanComptable> bilan )
        {
            DateTime refTime = Debut;
            DateTime refEndTime = Fin;

            int Capacity = (Debut.Year - Fin.Year) + 1;
            int count = 0;

            bilan = new List<BilanComptable>(Capacity);

            while ( refTime.Month != refEndTime.Month + 1 && refTime.Year != refEndTime.Year )
            {
                // On récupère les lignes correspondante à notre mois et notre année de réfèrence
                var result = accountLines.Where(x => x.postingDate.HasValue && x.postingDate.Value.Month == refTime.Month && x.postingDate.Value.Year == refTime.Year);

                if ( result != null )
                {
                    // On instancie un nouveau bilanComptable
                    BilanComptable tempBilan = new BilanComptable();
                    tempBilan.TimePoint = new DateTime(refTime.Year, refTime.Month, 1);

                    // Pour chaque ligne récupère on affecte ça valeur positive ou négative selon sa direction
                    foreach ( var lineAccount in result )
                    {
                        if ( lineAccount.amount.HasValue && lineAccount.direction.HasValue )
                            tempBilan.Amount += lineAccount.direction == false ? -lineAccount.amount.Value : lineAccount.amount.Value;
                    }

                    // On assigne la valeur du Bilan temporaire à la listeFinale au niveau de l'index courant
                    bilan[count] = tempBilan;
                    // On incrémente l'index
                    count++;

                    // On recalcule la date de réfèrence et on ré-assigne la valeur au temps de réfèrence
                    int refMonth = refTime.Month + 1;
                    int refYear = refTime.Year;

                    if ( refMonth == 13 )
                    {
                        refMonth = 1;
                        refYear += 1;
                    }

                    refTime = new DateTime(refYear, refMonth, 1);
                }

            }
        }

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
