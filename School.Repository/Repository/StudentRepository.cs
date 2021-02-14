using Microsoft.EntityFrameworkCore;
using School.Repository.Models;
using System.Collections.Generic;
using System.Linq;


namespace School.Repository.Repository
{
    public class StudentRepository : SchoolRepository<Student>, IStudentRepository
    {
        public IEnumerable<Course> GetAllCourses(int id)
        {
            // TODO Refactor this query, because the one below can throw exceptions rather then returning a empty set
            /*
             * return _dbContext.Course.Where(c => c.Students.Any(student => student.Id == id));
             */

            return _dbContext.Student
                .Where(s => s.Id == id)
                    .Include(s => s.Courses)
                        .ThenInclude(c => c.Professor)
                .SingleOrDefault()
                .Courses
                .ToList();
        }

        public List<string> GetProfessorNames(int id)
        {
            
            List<string> professorNames = new List<string>();

            // The logic bellow is duplicated, instead of that, should call GetAllCourses.
            // Always avoid duplicating logic.
            var coursesForStudent = _dbContext.Student
                                        .Where(s => s.Id == id)
                                            .Include(s => s.Courses)
                                        .SingleOrDefault()
                                        .Courses;
            /*
             The foreach bellow can be replaced with something like:
            var professorsNames = _dbContext.Professor.Where(p => coursesForStudent.Any(c => c.Professor == p))
                .Select(p => p.ProfessorName);
            */
            
           foreach (var course in coursesForStudent)
            {
                string profname = _dbContext.Professor
                                    .Where(p => p.Id == course.ProfessorId)
                                    .SingleOrDefault()
                                    .ProfessorName;
                professorNames.Add(profname);
            }

            return professorNames;
        }
    }
}
