using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControl_GestionClient.Models
{
    public class Company
    {
        public long id { get; set; }
        public string name { get; set; }
        public string siret { get; set; }
        public string address { get; set; }
        public int postalcode { get; set; }
        public string city { get; set; }
        public virtual ICollection<Contact> Company_Contact { get; set; }
    }
}
