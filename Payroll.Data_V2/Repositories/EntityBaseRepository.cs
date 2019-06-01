using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Data.Infrastructure;
using Payroll.Entities;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Payroll.Data.Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
        where T : class, IEntityBase, new()
    {
        private PayrollContext dataContext;

        private readonly IDbSet<T> dbSet; //*************************

        #region Properties

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected PayrollContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }          
        }


        public EntityBaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();   //*************************
        }

        #endregion

        //public virtual IQueryable<T> GetAll()
        //{
        //     return DbContext.Set<T>();
        //}

        public virtual IEnumerable<T> GetAll()
        {
          return  dbSet.ToList();
        }

        //public virtual IQueryable<T> All
        //{
        //    get
        //    {
        //        return GetAll();
        //    }
        //}


        public virtual IEnumerable<T> All
        {
            get
            {
                return GetAll();
            }
        }

        //public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        //{
        //    IQueryable<T> query = DbContext.Set<T>();
        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }
        //    return query;
        //}

        public T GetSingle(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }



        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            //return DbContext.Set<T>().Where(predicate);
            return dbSet.Where<T>(predicate);
        }

        public virtual void Add(T entity)
        {
            //DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            //DbContext.Set<T>().Add(entity);
            dbSet.Add(entity);
        }
        public virtual void Edit(T entity)
        {
            //DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            //dbEntityEntry.State = EntityState.Modified;
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            //DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            //dbEntityEntry.State = EntityState.Deleted;
            dbSet.Remove(entity);
        }

    }
}
