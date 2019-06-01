using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Entities
{
    public class PartTimeEmployee: IEntityBase
    {
        public int ID { get; set; }

        public int EmpId { get; set; }

        public decimal PartTimeEmployeeRate { get; set; }

    }
}
