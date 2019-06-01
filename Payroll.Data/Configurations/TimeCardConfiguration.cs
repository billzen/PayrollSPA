using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Entities;

namespace Payroll.Data.Configurations
{
    class TimeCardConfiguration: EntityBaseConfiguration<TimeCard>
    {
        public TimeCardConfiguration()
        {
           Property(tm => tm.WorkedHours);
        }
    }
}
