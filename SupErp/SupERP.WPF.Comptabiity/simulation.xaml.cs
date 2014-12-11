#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Syncfusion.Windows.SampleLayout;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SupERP.WPF.Comptabiity.ComptabilityWebServiceReference;

namespace SupERP.WPF.Comptabiity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Simulation : //SampleLayoutWindow
    {
        public Simulation()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DateTime dateDebut = dpDateDebut.SelectedDate.Value;
            //DateTime dateFin = dpDateFin.SelectedDate.Value;

            //LineChartViewModel line = new LineChartViewModel();
            //line.populateGraph(dateDebut, dateFin);
           
        }

    }


    public class LineChartViewModel
    {
        private IEnumerable<Entries> entrees;
        public LineChartViewModel()
        {

        }
        public void populateGraph(DateTime debut,DateTime fin)
        {
            this.power = new ObservableCollection<TransactionSimulation>();

            using (var ws = new ComptabilityWebServiceReference.ComptabilityServiceClient())
            {
                entrees = ws.GetEntries("", null, null, debut, fin);
            }
            foreach (Entries entree in entrees)
            {
                power.Add(new TransactionSimulation() { Date = entree.postingDate.Value, Montant =(double) entree.amount.Value });
            }
        }



        public ObservableCollection<TransactionSimulation> power
        {
            get;
            set;
        }

    }
    public class TransactionSimulation
    {
        public DateTime Date { get; set; }

        public double Montant { get; set; }
    }
}
