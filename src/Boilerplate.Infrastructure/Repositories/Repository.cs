using Boilerplate.Domain.Core.Entities;
using Boilerplate.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected DbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DbContext dbContext)
        {
            Db = dbContext;
            DbSet = Db.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task Create(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChangesAsync();
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity == null) return false;
            DbSet.Remove(entity);
            await SaveChangesAsync();
            return true;

        }

        public async Task<int> SaveChangesAsync() => await Db.SaveChangesAsync();

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
