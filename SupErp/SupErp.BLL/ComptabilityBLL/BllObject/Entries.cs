using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.BLL.ComptabilityBLL.BllObject
{
    public class Entries
    {
        public EntriesTypeEnum EntryType { get; set; }

        public SourceEntriesEnum SourceType { get; set; }

        public long id { get; set; }

        public Nullable<decimal> amount { get; set; }

        public Nullable<System.DateTime> postingDate { get; set; }

        public Nullable<long> Foreign_id { get; set; }
        public string Foreign_libelle { get; set; }

        public Periodicity periode { get; set; }
    }
}
