using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingCompanyWebApp.Models.Interfaces;
using TrainingCompanyWebApp.Models.Repositories;
using static TrainingCompanyWebApp.Models.Interfaces.IGRepository;

namespace TrainingCompanyWebApp.Models.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _context;
        private IGRepository<T> _entity;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IGRepository<T> Entity
        {
            get
            {
                return _entity ?? (_entity = new GRepository<T>(_context));
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
