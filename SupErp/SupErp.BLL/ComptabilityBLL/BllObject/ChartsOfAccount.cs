using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.BLL.ComptabilityBLL.BllObject
{
    public class ChartsOfAccount
    {    
        public long id { get; set; }
        public Nullable<long> account_number { get; set; }
        public string name { get; set; }
        public Nullable<long> class_id { get; set; }
        public IEnumerable<ChartsOfAccount> chartsOfAccount { get; set; }
    }
}
