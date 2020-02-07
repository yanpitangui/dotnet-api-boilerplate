using Boilerplate.Domain.Core.Entities;
using Boilerplate.Domain.Core.Interfaces;
using Boilerplate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected HeroDbContext Db { get; }

        protected DbSet<TEntity> DbSet { get; }

        public Repository(HeroDbContext dbContext)
        {
            Db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
        }
    }
}
