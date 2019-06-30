using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        PayrollContext dbContext;

        // ^^^^^No  Comment
        public PayrollContext Init()
        {
             return dbContext ?? (dbContext = new PayrollContext());
            
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

    }
}
