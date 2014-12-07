using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.BLL.ComptabilityBLL.BllObject;
using SupErp.Entities;

namespace SupErp.BLL.ComptabilityBLL.Assembleur
{
    public static class ComptabilityAssembleur
    {
        public static ClassOfAccount ToClassOfAccount ( this COMPTA_ClassOfAccounts _class )
        {
            ClassOfAccount result = new ClassOfAccount();

            if ( _class == null )
                return null;

            result.id = _class.id;
            result.name = _class.name;
            result.number = _class.number;

            return result;
        }

        public static IEnumerable<ClassOfAccount> ToClassOfAccount ( this IEnumerable<COMPTA_ClassOfAccounts> _listClass )
        {
            List<ClassOfAccount> result = new List<ClassOfAccount>();

            if ( _listClass == null )
                return null;

            foreach ( COMPTA_ClassOfAccounts _class in _listClass )
            {
                result.Add(_class.ToClassOfAccount());
            }

            return result;
        }

        public static ChartsOfAccount ToChartsOfAccount ( this COMPTA_ChartOfAccounts _class )
        {
            ChartsOfAccount result = new ChartsOfAccount();

            if ( _class == null )
                return null;

            result.id = _class.id;
            result.name = _class.name;
            result.class_id = _class.class_id;
            result.account_number = _class.account_number;

            return result;
        }

        public static IEnumerable<ChartsOfAccount> ToChartsOfAccount ( this IEnumerable<COMPTA_ChartOfAccounts> _listCharts )
        {
            List<ChartsOfAccount> result = new List<ChartsOfAccount>();

            if ( _listCharts == null )
                return null;

            foreach ( COMPTA_ChartOfAccounts _class in _listCharts )
            {
                result.Add(_class.ToChartsOfAccount());
            }

            return result;
        }
    }
}
