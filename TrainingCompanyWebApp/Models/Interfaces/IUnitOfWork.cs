using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TrainingCompanyWebApp.Models.Interfaces.IGRepository;

namespace TrainingCompanyWebApp.Models.Interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        IGRepository<T> Entity { get; }
        Task SaveAsync();
    }
}
