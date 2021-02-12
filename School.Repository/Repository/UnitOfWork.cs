using Microsoft.EntityFrameworkCore.Storage;
using School.Repository.Context;

namespace School.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext _context;
        private IDbContextTransaction _transaction;
        public ICourseRepository Courses { get; set; }
        public IStudentRepository Students { get; set; }
        public IProfessorRepository Professors { get; set; }

        private RepositoryFactory factory = new RepositoryFactory();

        public UnitOfWork(SchoolDbContext context)
        {
            _context = context;

            Courses = factory.CreateCourseRepository(context);
            Students = factory.CreateStudentRepository(context);
            Professors = factory.CreateProfessorRepository(context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void BeginTransaction()
        {
             _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
