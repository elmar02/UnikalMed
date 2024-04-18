using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class, IEntity
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null);
    }
}
