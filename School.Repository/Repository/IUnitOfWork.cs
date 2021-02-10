using System;
using System.Collections.Generic;
using System.Text;

namespace School.Repository.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public ICourseRepository Courses { get; set; }
        public IStudentRepository Students { get; set; }
        public IProfessorRepository Professors { get; set; }
        int Save(); 
    }
}
