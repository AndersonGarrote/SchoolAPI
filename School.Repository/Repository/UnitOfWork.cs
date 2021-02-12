using School.Repository.Context;

namespace School.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext _context;
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
    }
}
