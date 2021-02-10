using School.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Repository.Repository
{
    public interface ICourseRepository
    {
        IEnumerable<Student> RollCall(int id);
    }
}
