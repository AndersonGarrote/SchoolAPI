using Microsoft.EntityFrameworkCore;
using School.Repository.Context;
using School.Repository.Models;
using System.Collections.Generic;
using System.Linq;


namespace School.Repository.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<Course> GetAllCourses(int id)
        {
            return _dbContext.Student
                .Where(s => s.Id == id)
                    .Include(s => s.Courses)
                .SingleOrDefault()
                .Courses
                .ToList();
        }

        public List<string> GetProfessorNames(int id)
        {
            List<string> professorNames = new List<string>();
            var coursesForStudent = _dbContext.Student
                                        .Where(s => s.Id == id)
                                            .Include(s => s.Courses)
                                        .SingleOrDefault()
                                        .Courses;

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
