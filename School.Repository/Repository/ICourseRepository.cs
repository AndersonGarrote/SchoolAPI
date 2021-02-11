using School.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
