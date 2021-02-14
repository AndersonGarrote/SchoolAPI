using School.Repository.Models;
using System.Collections.Generic;

namespace School.Repository.Repository
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<Course> GetAllCourses(int id);

        public List<string> GetProfessorNames(int id);
    }
}
