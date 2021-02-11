using Microsoft.EntityFrameworkCore;
using School.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Repository.Repository
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        IEnumerable<Course> GetAllCourses(int id);
    }
}
