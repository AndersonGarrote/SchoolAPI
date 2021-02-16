using Microsoft.EntityFrameworkCore;
using School.Repository.Models;
using System.Collections.Generic;
using System.Linq;

namespace School.Repository.Repository
{
    public class ProfessorRepository : SchoolRepository<Professor>, IProfessorRepository
    {
        public IEnumerable<Course> GetAllCourses(int id)
        {
            return _dbContext
                .Course
                .Where( c => c.ProfessorId == id)
                .ToList();
        }
    }
}
