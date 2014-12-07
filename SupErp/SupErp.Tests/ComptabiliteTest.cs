using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupErp.BLL.ComptabilityBLL;
using SupErp.BLL.ComptabilityBLL.BllObject;

namespace SupErp.Tests
{
    [TestClass]
    public class ComptabiliteTest
    {
        private StringBuilder _Result = new StringBuilder();
        [TestMethod]
        public void TestMethod1 ()
        {
            PlanComptableBLL ComptaBll = new PlanComptableBLL();

            var test = ComptaBll.GetPlanComptable();

            CallWriteLineRecursive(test);

            Console.ReadLine();
        }

        private void CallWriteLineRecursive ( object item )
        {
            if ( item == null )
                return;

            List<ClassOfAccount> _class = item as List<ClassOfAccount>;

            if ( _class != null )
            {
                foreach (var _item in _class)
	            {
                    _Result.AppendLine(string.Format("Class {0} {1}", _item.number.ToString(), _item.name.ToString()));
                    CallWriteLine(_item.ChartsOfAccount);
	            }
            }
        }

        private void CallWriteLine ( IEnumerable<ChartsOfAccount> charts )
        {
            if ( charts == null )
                return;

            foreach ( var item in charts )
            {
                Console.WriteLine(string.Format("Charts ID + Name : {0} {1}",item.account_number.ToString(),item.name));
                _Result.AppendLine(string.Format("Charts {0} {1}", item.account_number.ToString(), item.name.ToString()));
                if ( item.chartsOfAccount != null )
                {
                        CallWriteLine(item.chartsOfAccount);
                }
            }
        }
    }
}
