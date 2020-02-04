using Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        protected DbContext dbContext;

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await dbContext.Set<TEntity>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(TEntity entity)
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
            await SaveChangesAsync(); 
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity == null) return false;
            dbContext.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
            return true;

        }

        public async Task<int> SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
