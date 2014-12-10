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

            if ( result == null || DateTime.Now - result.updatedDate > new TimeSpan(1, 0, 0, 0, 0) )
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

                if ( noFilter )
                    resultBank = new List<COMPTA_BankJournalLine>(bankResult);

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

                    //finalEntries.Add(finalEntry);
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

                if ( noFilter )
                    resultCustomer = new List<COMPTA_CustomerJournalLine>(customerResult);

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

                    //finalEntries.Add(finalEntry);
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

                if ( noFilter )
                    resultSupplier = new List<COMPTA_SupplierJournalLine>(supplierResult);

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

                    //finalEntries.Add(finalEntry);
                });
            }

            #endregion

            #region Accounting Journal

            if ( accountingResult != null )
        {
                List<COMPTA_AccountingEntries> tempList = new List<COMPTA_AccountingEntries>(accountingResult);

                tempList.ForEach(delegate( COMPTA_AccountingEntries entry )
        {
                    var resultTemp = comptabilityDal.GetAccountingEntriesPeriodicityById(entry.id);

                    if ( resultTemp != null )
        {
                        // on recupere la periodicite
                        Periodicity periode = new Periodicity()
            {
                            startDate = resultTemp.startDate.Value,
                            endDate = resultTemp.endDate.Value,
                            Libelle = resultTemp.COMPTA_Periodicity.Libelle
                        };

                        int iteratorMonth = 0;

                        if ( periode.Libelle == "Mensuel" )
                            iteratorMonth = 1;
                        if ( periode.Libelle == "Annuel" )
                            iteratorMonth = 12;
                        if ( periode.Libelle == "Trimestriel" )
                            iteratorMonth = 3;

                        DateTime refTime = new DateTime(entry.postingDate.Value.Year, entry.postingDate.Value.Month, entry.postingDate.Value.Day);
                        DateTime refEndTime = new DateTime(periode.endDate.Year, periode.endDate.Month, periode.endDate.Day);

                        int month = entry.postingDate.Value.Month;
                        //tant que refTime n'est pas supérieure ou égal a EndTime
                        // Je crée des nouvelles entrée tout les X Mois
                        //j'incrémente refTime de X mois 
                        while ( refTime.Month < refEndTime.Month || refTime.Month == refEndTime.Month && refTime.Year < refTime.Year || refEndTime.Year == refEndTime.Year )
        {
                            month += iteratorMonth;
                            int addYear = 0;
                            while ( month > 13 )
            {
                                month = -12;
                                addYear++;
            }

                            DateTime newDateTime = new DateTime(entry.postingDate.Value.Year + addYear, month, entry.postingDate.Value.Day);

                            if ( newDateTime < periode.endDate )
        {
                                COMPTA_AccountingEntries newEntry = new COMPTA_AccountingEntries()
            {
                                    amount = entry.amount,
                                    chartOfAccount_id = entry.chartOfAccount_id,
                                    direction = entry.direction,
                                    postingDate = newDateTime
                                };

                                tempList.Add(newEntry);

                                refTime = newDateTime;
            }
        }


            }

                });

                accountingResult = tempList;

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

                if ( noFilter )
                    resultAccounting = new List<COMPTA_AccountingEntries>(accountingResult);

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
                CreateBilanComptableModeYear(accountLines, Debut.Value, Fin.Value, out listBilan);
                return listBilan;
            }
            else if ( Mode == BilanComptableModeEnum.Year )
            {
                CreateBilanComptableModeMonth(accountLines, Debut.Value, Fin.Value, out listBilan);
                return listBilan;
            }
                    return null;
        }

        private void CreateBilanComptableModeMonth(IEnumerable<COMPTA_AccountingEntries> accountLines, DateTime Debut, DateTime Fin, out List<BilanComptable> bilan)
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

                List<COMPTA_AccountingEntries> tempList = new List<COMPTA_AccountingEntries>(result);

                tempList.ForEach(delegate( COMPTA_AccountingEntries entry )
                {
                    var resultTemp = comptabilityDal.GetAccountingEntriesPeriodicityById(entry.id);

                    if ( resultTemp != null )
        {
                        // on recupere la periodicite
                        Periodicity periode = new Periodicity()
            {
                            startDate = resultTemp.startDate.Value,
                            endDate = resultTemp.endDate.Value,
                            Libelle = resultTemp.COMPTA_Periodicity.Libelle
                        };

                        int iteratorMonth = 0;

                        if ( periode.Libelle == "Mensuel" )
                            iteratorMonth = 1;
                        if ( periode.Libelle == "Annuel" )
                            iteratorMonth = 12;
                        if ( periode.Libelle == "Trimestriel" )
                            iteratorMonth = 3;



                        for ( int i = iteratorMonth ; i < Capacity - iteratorMonth ; i = +iteratorMonth )
                {
                            int month2 = entry.postingDate.Value.Month + i;
                            int addYear = 0;
                            while ( month2 > 13 )
                {
                                month2 = -12;
                                addYear++;
        }

                            DateTime newDateTime = new DateTime(entry.postingDate.Value.Year + addYear, month2, entry.postingDate.Value.Day);

                            if ( newDateTime < periode.endDate )
                {
                                COMPTA_AccountingEntries newEntry = new COMPTA_AccountingEntries()
                {
                                    amount = entry.amount,
                                    chartOfAccount_id = entry.chartOfAccount_id,
                                    direction = entry.direction,
                                    postingDate = newDateTime
                                };

                                tempList.Add(newEntry);
            }
        }

        }
                });

                accountLines = tempList;

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

        private void CreateBilanComptableModeYear ( IEnumerable<COMPTA_AccountingEntries> accountLines, DateTime Debut, DateTime Fin, out List<BilanComptable> bilan )
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

                List<COMPTA_AccountingEntries> tempList = new List<COMPTA_AccountingEntries>(result);

                tempList.ForEach(delegate( COMPTA_AccountingEntries entry )
                {
                    var resultTemp = comptabilityDal.GetAccountingEntriesPeriodicityById(entry.id);

                    if ( resultTemp != null )
        {
                        // on recupere la periodicite
                        Periodicity periode = new Periodicity()
            {
                            startDate = resultTemp.startDate.Value,
                            endDate = resultTemp.endDate.Value,
                            Libelle = resultTemp.COMPTA_Periodicity.Libelle
                        };

                        int iteratorMonth = 0;

                        if ( periode.Libelle == "Mensuel" )
                            iteratorMonth = 1;
                        if ( periode.Libelle == "Annuel" )
                            iteratorMonth = 12;
                        if ( periode.Libelle == "Trimestriel" )
                            iteratorMonth = 3;

                       

                        for ( int i = iteratorMonth ; i < Capacity - iteratorMonth ; i=+iteratorMonth )
                {
                            int month = entry.postingDate.Value.Month + i;
                            int addYear = 0;
                            while(month > 13)
                {
                                month =- 12;
                                addYear++;
        }

                            DateTime newDateTime = new DateTime(entry.postingDate.Value.Year + addYear, month, entry.postingDate.Value.Day);

                            if ( newDateTime < periode.endDate )
                {
                                COMPTA_AccountingEntries newEntry = new COMPTA_AccountingEntries()
                {
                                    amount = entry.amount,
                                    chartOfAccount_id = entry.chartOfAccount_id,
                                    direction = entry.direction,
                                    postingDate = newDateTime
                                };

                                tempList.Add(newEntry);
                }
            }
                        
        }
                });

                accountLines = tempList;

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
    }
}
