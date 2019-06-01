using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Objects;
using System.Linq.Expressions;
using Payroll.Entities;

namespace Payroll.Data.Repositories
{
    class ErrorRepository
    {

        //public readonly PayrollContext DbContext = new PayrollContext();

        //public virtual IQueryable<Error> GetAll()
        //{
        //    IQueryable<Error> Err = from aa in DbContext.Error select aa;  //orderby aa.EmpId ascending
        //    return Err;
        //}

        //public virtual IQueryable<Error> All
        //{
        //    get
        //    {
        //        return GetAll();
        //    }
        //}
        
        //public T GetSingle(int id)
        //{
        //    return GetAll().FirstOrDefault(x => x.ID == id);
        //}


        //public virtual IQueryable<Error> FindBy(Expression<Func<Error, bool>> predicate)
        //{
        //    return DbContext.Set<Error>().Where(predicate);
        //}


    }
}
