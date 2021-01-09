using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TrainingCompanyWebApp.Models.Interfaces
{
    public interface IGRepository
    {
        public interface IGRepository<T> where T : class
        {
            IQueryable<T> GetAll();
            Task<T> GetByIdAsync(Object id);
            void Insert(T entity);
            void Update(T entity);
            void Delete(object id);
            //IQueryable<T> Find(Expression<Func<T, bool>> predicate);
            IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
        }
    }
}
