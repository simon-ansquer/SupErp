using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupERP.WPF.Comptabiity.Model;

namespace SupERP.WPF.Comptabiity.Assembleur
{
    public static class ViewModelAssembleur
    {
        public static IEnumerable<ChartsOfAccount> ToChartsOfAccount ( this IEnumerable<ComptabilityWebServiceReference.ChartsOfAccount> charts )
        {
            if ( charts == null )
                return new List<ChartsOfAccount>();

            List<ChartsOfAccount> result = new List<ChartsOfAccount>();

            foreach ( ComptabilityWebServiceReference.ChartsOfAccount chart in charts )
            {
                result.Add(chart.ToChartsOfAccount());
            }

            return result;
        }

        public static ChartsOfAccount ToChartsOfAccount ( this ComptabilityWebServiceReference.ChartsOfAccount charts )
        {
            if ( charts == null )
                return new ChartsOfAccount();

            return new ChartsOfAccount()
            {
                account_number = charts.account_number,
                chartsOfAccount = charts.chartsOfAccount.ToChartsOfAccount(),
                class_id = charts.class_id,
                id = charts.id,
                name = charts.name
            };

        }




        public static IEnumerable<ClassOfAccount> ToClassOfAccount ( this IEnumerable<ComptabilityWebServiceReference.ClassOfAccount> classAccount )
        {
            if ( classAccount == null )
                return new List<ClassOfAccount>();

            List<ClassOfAccount> result = new List<ClassOfAccount>();

            foreach ( ComptabilityWebServiceReference.ClassOfAccount chart in classAccount )
            {
                result.Add(chart.ToClassOfAccount());
            }

            return result;
        }

        public static ClassOfAccount ToClassOfAccount ( this ComptabilityWebServiceReference.ClassOfAccount _class )
        {
            if ( _class == null )
                return new ClassOfAccount();

            return new ClassOfAccount()
            {
                ChartsOfAccount = _class.ChartsOfAccount.ToChartsOfAccount(),
                id = _class.id,
                name = _class.name,
                number = _class.number
            };

        }
    }
}
