using Example.Dal.Abstract;
using Example.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Example.Dal.Concreate
{
    public class BaseContext<TEntity, TContext> : IContext<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<TEntity> Add(TEntity entity)
        {
            await using var context = new TContext();
            var result = context.Entry(entity);
            result.State = EntityState.Added;
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            await using var context = new TContext();
            var result = context.Entry(entity);
            result.State = EntityState.Deleted;
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            await using var context = new TContext();
            if (filter != null)
                return await context.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
            else
                return await context.Set<TEntity>().FirstOrDefaultAsync();
        }

        public async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();
            if (filter != null)
                return await context.Set<TEntity>().Where(filter).ToListAsync();
            else
                return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            await using var context = new TContext();
            var result = context.Entry(entity);
            result.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
