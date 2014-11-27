using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupErp.Entities
{
    public partial class User
    {

        public Salary GetCurrentSalary()
        {
            return this.Salaries.OrderByDescending(s => s.Date).First();
        }

    }
}
