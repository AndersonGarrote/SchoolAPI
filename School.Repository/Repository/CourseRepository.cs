using Microsoft.EntityFrameworkCore;
using School.Repository.Context;
using School.Repository.Models;
using System.Collections.Generic;
using System.Linq;

namespace School.Repository.Repository
{
    public class CourseRepository : SchoolRepository<Course>, ICourseRepository
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
               .ToList();
        }

    }
}
