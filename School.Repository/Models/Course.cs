using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Repository.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Room { get; set; }

        public DateTime Schedule { get; set; }

        public ICollection<Student> Students { get; set; }

        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
    }
}
