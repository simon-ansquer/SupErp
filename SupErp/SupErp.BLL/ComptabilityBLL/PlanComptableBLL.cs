using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.BLL.ComptabilityBLL.BllObject;
using SupErp.DAL.GestionComptabilityDAL;
using SupErp.BLL.ComptabilityBLL.Assembleur;


namespace SupErp.BLL.ComptabilityBLL
{
    public class PlanComptableBLL
    {
        private static readonly Lazy<ComptabilityDAL> lazyComptabilityDal = new Lazy<ComptabilityDAL>(() => new ComptabilityDAL());
        private static ComptabilityDAL comptabilityDal { get { return lazyComptabilityDal.Value; } }


        public IEnumerable<ClassOfAccount> GetPlanComptable()
        {
            List<ClassOfAccount> classAccount = new List<ClassOfAccount>(comptabilityDal.GetClassOfAccounts().ToClassOfAccount());

            List<ChartsOfAccount> chartsAccount = new List<ChartsOfAccount>(comptabilityDal.GetChartOfAccounts().ToChartsOfAccount());

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
    }
}
