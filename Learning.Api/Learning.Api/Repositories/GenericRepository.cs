using Learning.Api.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Api.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
         where T : BaseEntity, new()
    {
        private LearningDbContext dbContext;

        public GenericRepository(LearningDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> GetAsync(int id)
        {
            return await dbContext.FindAsync<T>(id);
        }

        public IQueryable<T> Query()
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }

            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdatedDate = DateTime.UtcNow;

            dbContext.Set<T>().Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }

            entity.UpdatedDate = DateTime.UtcNow;

            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            T entity = new T() { Id = id };

            dbContext.Entry(entity).State = EntityState.Deleted;
            await dbContext.SaveChangesAsync();
        }

        public async Task<T> GetAsync(string id)
        {
            return await dbContext.FindAsync<T>(id);
        }
    }
}
