using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.WebApp.Models
{
    public class EmployeeOrderViewModel
    {
        public int ID { get; set; }
        public int CommissionedEmployeeId { get; set; }
        public int OrderId { get; set; }
    }
}
