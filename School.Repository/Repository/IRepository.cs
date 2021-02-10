using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Repository.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }

}
