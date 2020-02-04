using Boilerplate.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(Guid id);

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task<bool> Delete(Guid id);

        Task<int> SaveChangesAsync();
    }
}