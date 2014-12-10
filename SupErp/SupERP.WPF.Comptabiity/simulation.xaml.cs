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

namespace SupERP.WPF.Comptabiity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Simulation : SampleLayoutWindow
    {
        public Simulation()
        {
            InitializeComponent();
        }

    }
    public class LineChartViewModel
    {
        public LineChartViewModel()
        {
            this.power = new ObservableCollection<TransactionSimulation>();

            using (var ws = new ComptabilityWebServiceReference.ComptabilityServiceClient())
            {
            }

            DateTime yr = new DateTime(2002, 5, 1);
            power.Add(new TransactionSimulation() { Date = yr.AddYears(1), Montant = 3900 });
            power.Add(new TransactionSimulation() { Date = yr.AddYears(2), Montant = 3600});
            power.Add(new TransactionSimulation() { Date = yr.AddYears(3), Montant = 9400 });
            power.Add(new TransactionSimulation() { Date = yr.AddYears(4), Montant = 44 });
            power.Add(new TransactionSimulation() { Date = yr.AddYears(5), Montant = 45 });
            power.Add(new TransactionSimulation() { Date = yr.AddYears(6), Montant = 48 });
            power.Add(new TransactionSimulation() { Date = yr.AddYears(7), Montant = 46 });

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
