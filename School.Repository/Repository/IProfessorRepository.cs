using School.Repository.Models;
using System.Collections.Generic;

namespace School.Repository.Repository
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        IEnumerable<Course> GetAllCourses(int id);
    }
}
