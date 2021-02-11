using School.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Repository.Repository
{
    public interface ICourseRepository
    {
        /// <summary>
        /// Lista os alunos do curso
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<Student> RollCall(int id);
    }
}
