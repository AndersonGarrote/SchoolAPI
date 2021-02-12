using System;
using System.Collections.Generic;
using System.Text;
using School.Repository.Context;

namespace School.Repository.Repository
{
    class RepositoryFactory
    {
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
    }
}
