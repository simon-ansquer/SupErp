using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControl_GestionClient.Models
{
    class Company_Contact
    {
        public long id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int gender { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public long company_id { get; set; }
        public virtual Company Company { get; set; }
    }
}
