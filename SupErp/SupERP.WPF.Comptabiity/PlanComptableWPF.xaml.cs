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
using SupErp.WCF.ComptabilityWCF;

namespace SupERP.WPF.Comptabiity
{
    /// <summary>
    /// Logique d'interaction pour PlanComptableWPF.xaml
    /// </summary>
    public partial class PlanComptableWPF : Window
    {
        public PlanComptableWPF()
        {
            InitializeComponent();
        }

        public IEnumerable<Model.ClassOfAccount> getPlanComptable()
        {
            using (var ws = new ComptabilityService())
            {
                
            }
        }
    }
}
