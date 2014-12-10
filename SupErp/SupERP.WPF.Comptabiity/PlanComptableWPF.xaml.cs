using System;
using System.Collections.Generic;
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
using SupErp.WCF;
using SupERP.WPF.Comptabiity.Model;
using SupERP.WPF.Comptabiity.Assembleur;

namespace SupERP.WPF.Comptabiity
{
    /// <summary>
    /// Logique d'interaction pour PlanComptableWPF.xaml
    /// </summary>
    public partial class PlanComptableWPF : Window
    {
        private IEnumerable<Model.ClassOfAccount> plan;
        public PlanComptableWPF()
        {
            plan = new List<Model.ClassOfAccount>();
            InitializeComponent();
            var result = getPlanComptable(); // on recupere toutes les classes

            foreach (Model.ClassOfAccount classe in result)
            {
                StringBuilder sb = new StringBuilder();
                string a = ": ";
                sb.Append(classe.id); sb.Append(a); sb.Append(classe.name);
                TreeViewItem treeItem = new TreeViewItem();
                treeItem.Items.Add(new TreeViewItem() { Header = sb.ToString() });
                sb.Clear();
                PlanComptableTreeView.Items.Add(treeItem);// ici on affiche le treeitem de la  
                                                          // classe qui vient d'etre traitee
                foreach (Model.ChartsOfAccount chart in classe.ChartsOfAccount)
	            {
                    AfficherCharts(chart,treeItem);
	            }

            }
        }

        public IEnumerable<Model.ClassOfAccount> getPlanComptable()
        {
            IEnumerable<ComptabilityWebServiceReference.ClassOfAccount> classAccount;
            using (var ws = new ComptabilityWebServiceReference.ComptabilityServiceClient())
            {
                classAccount = ws.GetPlanComptable().ToList();
            }

            IEnumerable<Model.ClassOfAccount> viewModel = classAccount.ToClassOfAccount();

            
            return viewModel;
        }

        public void AfficherCharts(Model.ChartsOfAccount chart, TreeViewItem treeItemSource)
        {
            foreach (Model.ChartsOfAccount item in chart.chartsOfAccount)
            {
                StringBuilder sb = new StringBuilder();
                string a = ": ";
                sb.Append(chart.id); sb.Append(a); sb.Append(chart.name);
                TreeViewItem treeItem = new TreeViewItem();
                treeItem.Items.Add(new TreeViewItem() { Header = sb.ToString() });
                treeItemSource.Items.Add(treeItem);//on rajoute au noeud source
                sb.Clear();
                if (item.chartsOfAccount!=null)
                {
                    AfficherCharts(item,treeItem);
                }
            }

        }
    }
}
