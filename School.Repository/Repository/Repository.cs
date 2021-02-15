using Microsoft.EntityFrameworkCore;
using School.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace School.Repository.Repository
{
    public abstract class Repository<TContext> where TContext : DbContext
    {
        internal TContext _dbContext { get; set; }
    }

    public abstract class Repository<TEntity, TContext> : Repository<TContext>, IRepository<TEntity> where TEntity : class where TContext: DbContext
    {
        public Repository() { }
        public Repository(TContext dbContext)
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


    // TODO: Move to a exclusive file.
    public class SchoolRepository<TEntity> : Repository<TEntity, SchoolDbContext> where TEntity : class
    {
        public SchoolRepository() { }
        public SchoolRepository(SchoolDbContext dbContext) : base(dbContext) { }
    }

}
