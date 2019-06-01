using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Entities
{
    public class Product : IEntityBase
    {
        public int ID { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
