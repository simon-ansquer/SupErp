using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupERP.WPF.Comptabiity.Model
{
    public class ClassOfAccount
    {
        public long id { get; set; }
        public Nullable<long> number { get; set; }
        public string name { get; set; }
        public IEnumerable<ChartsOfAccount> ChartsOfAccount { get; set; }
    }
}
