using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Repository.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds an entity to the repository
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        void Add(TEntity entity);

        /// <summary>
        /// Removes an entity from the repository
        /// </summary>
        /// <param name="entity">Entity to be removed</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Returns the Entity with the given id from the repository
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns>Entity with the given id</returns>
        TEntity Get(int id);

        /// <summary>
        /// Returns if the Entity with the given id exists in the repository
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns>True if the Entity is found, false otherwise</returns>
        bool Exists(int id);

        /// <summary>
        /// Returns all the Entities from the repository
        /// </summary>
        /// <returns>Enumerable of Entities from the repository</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Returns the Entities from the repository that match the given predicate
        /// </summary>
        /// <param name="predicate">Predicate to be tested</param>
        /// <returns>Enumerable of Entities from the repository</returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }

}
