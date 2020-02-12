using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Core.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(Guid id);

        Task<TEntity> Create(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<bool> Delete(Guid id);

        Task<int> SaveChangesAsync();
    }
}