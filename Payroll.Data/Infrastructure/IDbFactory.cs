﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        PayrollContext Init();
    }
}
