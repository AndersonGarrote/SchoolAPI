using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using School.Repository.Context;

namespace School.Repository.Repository
{
    public class RepositoryFactory<T> where T : DbContext
    {
        private T _context;
        public RepositoryFactory(T context)
        {
            _context = context;
        }
        public StudentRepository CreateStudentRepository(SchoolDbContext context)
        {
            return new StudentRepository(context);
        }

        public CourseRepository CreateCourseRepository(SchoolDbContext context)
        {
            return new CourseRepository(context);
        }

        public ProfessorRepository CreateProfessorRepository(SchoolDbContext context)
        {
            return new ProfessorRepository(context);
        }

        protected TRepository CreateRepository<TRepository>() where TRepository : Repository<T>, new()
        {​​
            return new TRepository {​​ Context = _context }​​;
        }​​
    }
}
