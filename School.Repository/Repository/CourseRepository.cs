using Microsoft.EntityFrameworkCore;
using School.Repository.Models;
using System.Collections.Generic;
using System.Linq;

namespace School.Repository.Repository
{
    public class CourseRepository : SchoolRepository<Course>, ICourseRepository
    {
        /// <inheritdoc/>
        public IEnumerable<Student> RollCall(int id)
        {
            // The statement bellow can raise exception, so, it's not good enough for usage.
            // A alternative approach that does not raise exception would be:

            //var result = _dbContext.Student
            //    .Where(stu => stu.Courses.Any(c => c.Id == id))
            //    .OrderBy(stu => stu.StudentName);

            // If you start a query in table A, then include table B and only projects table B,
            // than you do not need to load the table A.
            // In this case, we do not need to load the table Course. :D

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
            // Sometimes is not the best approach to override the get of an repository to return 
            // related data. Because usually not every call will need the related data. 

            // If your linq ends with Where + Single, Where + First, Where + SingleOrDefault or Where + FirtOrDefault
            // you do not need the where, instead you can use the last operator.
            // Example: x.Where(...).First() ===>  x.First(...)
            return _dbContext
                .Set<Course>()
                .Include(c => c.Professor)
                .Where(c => c.Id == id)
                .SingleOrDefault();
        }

        /// <inheritdoc/>
        public new IEnumerable<Course> GetAll()
        {
            // The same consideration of the commentary in the above method is valid here.
            return _dbContext
                .Set<Course>()
                .Include(c => c.Professor)
                .ToList();
        }

    }
}
