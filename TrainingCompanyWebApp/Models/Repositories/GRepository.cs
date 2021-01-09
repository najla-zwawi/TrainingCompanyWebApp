using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static TrainingCompanyWebApp.Models.Interfaces.IGRepository;

namespace TrainingCompanyWebApp.Models.Repositories
{
    public class GRepository<T> : IGRepository<T> where T : class
    {
        private readonly DataContext _context;
        private DbSet<T> table = null;
        public GRepository(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }


        public async Task<T> GetByIdAsync(Object id)
        {
            //var t= table.Find(id); 
            //return t; 
            return await table.FindAsync(id); // دالة في الانتتي للبحث من خلال الكي
        }

        public void Delete(object id)
        {
            //T existing = GetById(id);
            T existing = table.Find(id);
            table.Remove(existing);

        }

        public IQueryable<T> GetAll()
        {
            return table.AsNoTracking();  // عندما تكون IQueryable لا تقبل table.ToList(); لأنها لأن نتيجتها هي ليست
        }


        public void Insert(T entity)
        {
            table.Add(entity);
        }
        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return table.AsNoTracking().Where(predicate);
        }

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = table;
            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            return query; // عندما تكون IQueryable لا تقبل query.ToList(); لأنها لأن نتيجتها هي ليست
        }

        //public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        //{
        //    throw new NotImplementedException();
        //}





        //public List<T> Get(Expression<Func<T, bool>> filter = null,
        //                     Func<IQueryable<T>,
        //                     IOrderedQueryable<T>> orderBy = null,
        //                     params Expression<Func<T,
        //                     object>>[] includes)
        //{

        //    IQueryable<T> query = table;

        //    foreach (Expression<Func<T, object>> include in includes)
        //        query = query.Include(include);

        //    if (filter != null)
        //        query = query.Where(filter);

        //    if (orderBy != null)
        //        query = orderBy(query);

        //    return query.ToList();
        //}









    }
}
