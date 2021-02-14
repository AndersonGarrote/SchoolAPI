using School.Repository.Models;
using System.Collections.Generic;

namespace School.Repository.Repository
{
    public interface ICourseRepository : IRepository<Course>
    {
        /// <summary>
        /// Lists Students from Course
        /// </summary>
        /// <param name="id">Course Identifier</param>
        /// <returns>Enumerable of Students from Course</returns>
        IEnumerable<Student> RollCall(int id);
    }
}
