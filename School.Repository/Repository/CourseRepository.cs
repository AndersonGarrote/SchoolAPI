using Microsoft.EntityFrameworkCore;
using School.Repository.Context;
using School.Repository.Models;
using System.Collections.Generic;
using System.Linq;

namespace School.Repository.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolDbContext dbContext)
            : base(dbContext)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<Student> RollCall(int id)
        {
            return _dbContext.Course
               .Where(c => c.Id == id)
                   .Include(c => c.Students)
               .SingleOrDefault()
               .Students
               .OrderBy(s => s.StudentName)
               .ToList();
        }

        /// <inheritdoc/>
        public new Course Get(int id)
        {
            return _dbContext
                .Set<Course>()
                .Include(c => c.Professor)
                .Where(c => c.Id == id)
                .SingleOrDefault();
        }

        /// <inheritdoc/>
        public new IEnumerable<Course> GetAll()
        {
            return _dbContext
                .Set<Course>()
                .Include(c => c.Professor)
                .ToList();
        }

    }
}
