using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Entities
{
    public class EmployeeOrder: IEntityBase
    {
        public int ID { get; set; }
        public int CommissionedEmployeeId { get; set; }
        public int OrderId { get; set; }
    }
}
