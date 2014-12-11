using SupERP.WPF.Comptabiity.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SupERP.WPF.Comptabiity
{
    /// <summary>
    /// Logique d'interaction pour Journal.xaml
    /// </summary>
    public partial class Journal : Window
    {
        List<TransactionWPFJournal> listeTransactions;
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        public Journal()
        {
            InitializeComponent();
            listeTransactions = new List<TransactionWPFJournal>(getEntries());
            lvListeTransactions.ItemsSource = listeTransactions;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addTransactionToListView(TransactionWPFJournal transaction)
        {
            listeTransactions.Add(new TransactionWPFJournal(transaction.libelle, transaction.montant, transaction.date, transaction.paye, transaction.client,transaction.compte));
        }

        public IEnumerable<Model.TransactionWPFJournal> getEntries()
        {
            IEnumerable<ComptabilityWebServiceReference.Entries> entriesList;
            using (var ws = new ComptabilityWebServiceReference.ComptabilityServiceClient())
            {
                entriesList = ws.GetEntries(string.Empty, null, null, DateTime.Now, null).ToList();
            }

            List<Model.TransactionWPFJournal> viewModel = new List<TransactionWPFJournal>();
            List<ComptabilityWebServiceReference.Entries> entries = new List<ComptabilityWebServiceReference.Entries>(entriesList);

            entries.ForEach(delegate(ComptabilityWebServiceReference.Entries entry)
            {
                TransactionWPFJournal finalEntry = new TransactionWPFJournal(
                    string.Format("{0} {1}", entry.EntryType.ToString(),
                    entry.postingDate.ToString()),
                    entry.amount.Value,
                    entry.postingDate.Value,
                    entry.postingDate > DateTime.Now ? false : true,
                    entry.Foreign_libelle,
                    entry.Foreign_number);
                viewModel.Add(finalEntry);
            });

            return viewModel;
        }


    }
}
