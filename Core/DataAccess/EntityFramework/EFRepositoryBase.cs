using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EFRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext, new()

    {
        public void Add(TEntity entity)
        {
            using var context = new TContext();
            var addEntity = context.Entry(entity);
            addEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public async Task AddAsync(TEntity entity)
        {
            using var context = new TContext();
            var addEntity = context.Entry(entity);
            addEntity.State = EntityState.Added;
            await context.SaveChangesAsync();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            using var context = new TContext();
            return context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            using var context = new TContext();
            return expression == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(expression).ToList();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            using var context = new TContext();
            return expression == null ? await context.Set<TEntity>().ToListAsync() : await context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using var context = new TContext();
            return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public void Remove(TEntity entity)
        {
            using var context = new TContext();
            var removeEntity = context.Entry(entity);
            removeEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            using var context = new TContext();
            var updateEntity = context.Entry(entity);
            updateEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
