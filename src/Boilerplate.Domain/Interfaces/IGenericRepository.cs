using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(Guid id);

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task<bool> Delete(Guid id);

        Task<int> SaveChangesAsync();
    }
}