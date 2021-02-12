using Microsoft.EntityFrameworkCore;
using School.Repository.Context;
using School.Repository.Models;
using System.Collections.Generic;
using System.Linq;


namespace School.Repository.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<Course> GetAllCourses(int id)
        {
            return _dbContext.Student
                .Where(s => s.Id == id)
                    .Include(s => s.Courses)
                .SingleOrDefault()
                .Courses
                .ToList();
        }

        public Student GetAllCourses2(int id)
        {
            return _dbContext.Student
                .Where(s => s.Id == id)
                    .Include(s => s.Courses)
                .SingleOrDefault();
        }
    }
}
