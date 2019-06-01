using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Entities
{
    public class ProductImage : IEntityBase
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public Nullable<int> Filesize { get; set; }
        public string LogFilename { get; set; }
        public byte[] Filebytes { get; set; }
    }
}
