using Microsoft.EntityFrameworkCore;

namespace School.Repository.Repository
{
    public class RepositoryFactory<T> where T : DbContext
    {
        // TODO: remove if the code runs well, otherwise we will need in case of some nullException we have to fix the constructors.
        private T _context;
        public RepositoryFactory(T context)
        {
            _context = context;
        }


        /// <summary>
        /// Factory method to create specific repository.
        /// </summary>
        /// <typeparam name="TRepository">The type o repository to create</typeparam>
        /// <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-constraint"/>
        /// <returns>A brand new Repository.</returns>
        protected TRepository CreateRepository<TRepository>() where TRepository : Repository<T>, new()
        {
            return new TRepository{ _dbContext = _context };
        }
    }
}
