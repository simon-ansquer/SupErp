using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupErp.Entities;
using System.Runtime.Serialization;

namespace SupErp.DAL.FacturationModele
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(BILL_LineBillQuotation))]
    [KnownType(typeof(LineCompleted))]
    public class LineExtended : LineCompleted
    {
        [DataMember]
        public bool Included { get; private set; }

        public LineExtended(BILL_LineBillQuotation line, bool included): base(line)
        {
            Included = included;
        }

    }
}
