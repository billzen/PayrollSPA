using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Entities;
using System.Linq.Expressions;


namespace Payroll.Data.Repositories
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        //IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        //IQueryable<T> All { get; }
        IEnumerable<T> GetAll();
        IEnumerable<T> All { get; }


        T GetSingle(int id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
}

