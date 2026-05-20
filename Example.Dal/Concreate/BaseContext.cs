#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Example.Dal.Abstract;
using Example.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Example.Dal.Concreate
{
    public class BaseContext<TEntity, TContext> : IContext<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private readonly TContext _context;

        public BaseContext()
        {
            _context = new TContext();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var result = _context.Entry(entity);
            result.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            var result = _context.Entry(entity);
            result.State = EntityState.Deleted;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            var query = _context.Set<TEntity>().AsNoTracking();
            if (filter != null)
                return await query.Where(filter).FirstOrDefaultAsync();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = _context.Set<TEntity>().AsNoTracking();
            if (filter != null)
                return await query.Where(filter).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var result = _context.Entry(entity);
            result.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}