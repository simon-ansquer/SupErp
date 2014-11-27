using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.Entities
{
    public partial class SUPERPEntities
    {
        public SUPERPEntities(bool enableProxy)
            : base("name=SUPERPEntities")
        {
            Configuration.LazyLoadingEnabled = enableProxy;
            Configuration.ProxyCreationEnabled = enableProxy;
        }
    }
}
