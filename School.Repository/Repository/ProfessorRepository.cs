using Microsoft.EntityFrameworkCore;
using School.Repository.Context;
using School.Repository.Models;
using System.Collections.Generic;
using System.Linq;

namespace School.Repository.Repository
{
    public class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(SchoolDbContext dbContext)
            : base(dbContext)
        {
        }
        public IEnumerable<Course> GetAllCourses(int id)
        {
            return _dbContext
                .Course
                .Where( c => c.ProfessorId == id)
                    .Include(c => c.Professor)
                .ToList();
        }
    }
}
