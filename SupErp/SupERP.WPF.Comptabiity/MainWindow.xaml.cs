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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupERP.WPF.Comptabiity
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<TransactionWPF> listeTransactions;
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        public MainWindow()
        {
            InitializeComponent();
            listeTransactions = new List<TransactionWPF>();
            lvListeTransactions.ItemsSource = listeTransactions;

            listeTransactions = new List<TransactionWPF>(getEntries());

            listeTransactions.ForEach(addTransactionToListView);

            //// donnees en dur en attendant de recuperer les vraies donnees
            //TransactionWPF transction1 = new TransactionWPF("premier", 140, DateTime.Now, true, "Client1");
            //TransactionWPF transction2 = new TransactionWPF("deuxieme", 14, DateTime.Now, false, "Client2");
            //TransactionWPF transction3 = new TransactionWPF("trois", 1, DateTime.Now, true, "Client3");
            //TransactionWPF transction4 = new TransactionWPF("quatre", 4, DateTime.Now, false, "Client1");
            //TransactionWPF transction5 = new TransactionWPF("cinq", 41, DateTime.Now, false, "Client4");
            //TransactionWPF transction6 = new TransactionWPF("six", 141, DateTime.Now, false, "Client2");
            //TransactionWPF transction7 = new TransactionWPF("sept", 1400, DateTime.Now, true, "Client6");

            //addTransactionToListView(transction1);
            //addTransactionToListView(transction2);
            //addTransactionToListView(transction3);
            //addTransactionToListView(transction4);
            //addTransactionToListView(transction5);
            //addTransactionToListView(transction6);
            //addTransactionToListView(transction7);


        }

        public IEnumerable<Model.TransactionWPF> getEntries ()
        {
            IEnumerable<ComptabilityWebServiceReference.Entries> entriesList;
            using ( var ws = new ComptabilityWebServiceReference.ComptabilityServiceClient() )
            {
                entriesList = ws.GetEntries(string.Empty,null,null,DateTime.Now,null).ToList();
            }

            List<Model.TransactionWPF> viewModel = new List<TransactionWPF>();
            List<ComptabilityWebServiceReference.Entries> entries = new List<ComptabilityWebServiceReference.Entries>(entriesList);

            entries.ForEach(delegate( ComptabilityWebServiceReference.Entries entry )
            {
                TransactionWPF finalEntry = new TransactionWPF(
                    string.Format("{0} {1}", entry.EntryType.ToString(), 
                    entry.postingDate.ToString()), 
                    entry.amount.Value,
                    entry.postingDate.Value, 
                    entry.postingDate > DateTime.Now ? false : true, 
                    entry.Foreign_libelle);
                viewModel.Add(finalEntry);
            });

            return viewModel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void addTransactionToListView(TransactionWPF transaction)
        {
            listeTransactions.Add(new TransactionWPF(transaction.libelle, transaction.montant, transaction.date, transaction.paye, transaction.client));
        }

        void GridViewColumnHeaderClickedHandler(object sender,
                                            RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked =
                  e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    string header = headerClicked.Column.Header as string;
                    Sort(header, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }


                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }


        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(lvListeTransactions.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void cbTypeTransaction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem typeItem = (ComboBoxItem)cbTypeTransaction.SelectedItem;
            if (typeItem != null && typeItem.Content != null)
            {
                string value = typeItem.Content.ToString();
                switch (value)
                {
                    case "Entrees et sorties":

                        break;
                    case "Entrees":

                        break;
                    case "Sorties":
                        break;
                    case "Payés":
                        break;
                    case "Impayés":
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
