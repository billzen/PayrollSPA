using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Objects;
using System.Linq.Expressions;
using Payroll.Entities;
using Payroll.Data.Infrastructure;

namespace Payroll.Data.Repositories
{
    class EmployeeTypeRepository : EntityBaseRepository<EmployeeType>, IEmployeeTypeRepository
    {

        public EmployeeTypeRepository(IDbFactory dbFactory) : base(dbFactory) { }
    
    }
        
}
