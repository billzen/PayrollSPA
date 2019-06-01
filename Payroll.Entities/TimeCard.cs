using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Entities
{
    public class TimeCard: IEntityBase
    {
        public int ID { get; set; }

        public decimal WorkedHours { get; set; }
    }
}
