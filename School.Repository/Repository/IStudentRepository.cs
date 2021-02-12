using School.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Repository.Repository
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<Course> GetAllCourses(int id);

        public List<string> GetProfessorNames(int id);
    }
}
