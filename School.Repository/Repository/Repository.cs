using Microsoft.EntityFrameworkCore;
using School.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly SchoolDbContext _dbContext;

        /// <inheritdoc/>
        public Repository(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        /// <inheritdoc/>
        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        /// <inheritdoc/>
        public TEntity Get(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList<TEntity>();
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList<TEntity>();
        }

        /// <inheritdoc/>
        public bool Exists(int id)
        {
            return _dbContext.Set<TEntity>().Find(id) != null;
        }
    }

}
