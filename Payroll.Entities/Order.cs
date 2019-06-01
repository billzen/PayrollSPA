using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Entities
{
    public class Order : IEntityBase
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string Orderdescription { get; set; }
        public Nullable<decimal> OrderAmount { get; set; }
    }
}
