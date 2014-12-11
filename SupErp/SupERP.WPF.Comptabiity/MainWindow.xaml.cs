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
        List<TransactionWPF> listeTransactionsCharges;
        List<TransactionWPF> listeTransactionsProduits;
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        double totCharges = 0;
        double totProduits = 0;

        public MainWindow()
        {
            InitializeComponent();
            listeTransactions = new List<TransactionWPF>();
            listeTransactionsCharges = new List<TransactionWPF>();
            listeTransactionsProduits = new List<TransactionWPF>();

            listeTransactions = new List<TransactionWPF>(getEntries());
            foreach (TransactionWPF item in listeTransactions)
            {
                if (item.montant > 0)
                {
                    listeTransactionsProduits.Add(item);
                    totProduits += (double)item.montant;
                }
                else
                {
                    listeTransactionsCharges.Add(item);
                    totCharges += (double)item.montant;
                }
            }

            lvListeCharges.ItemsSource = listeTransactionsCharges;
            lvListeProduits.ItemsSource = listeTransactionsProduits;

            lblTotCharge.Content=totCharges;
            lblTotProduit.Content = totProduits;

            lblDiff.Content = totProduits - totCharges;

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
