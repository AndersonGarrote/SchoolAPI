using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Repository.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        public string StudentName { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public DateTime IngressYear { get; set; }

        public ICollection<Course> Courses { get; set; }

    }
}
