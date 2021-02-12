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

        public UnitOfWork(SchoolDbContext context)
        {
            _context = context;
            Courses = new CourseRepository(context);
            Students = new StudentRepository(context);
            Professors = new ProfessorRepository(context);
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
