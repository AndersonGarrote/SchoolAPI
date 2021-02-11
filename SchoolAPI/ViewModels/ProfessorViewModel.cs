using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Repository.Models
{
    public class ProfessorViewModel
    {
        
        public int Id { get; set; }
        public string ProfessorName { get; set; }
        public int Age { get; set; }
        public DateTime IngressYear { get; set; }
        public ICollection<CourseViewModel> Courses { get; set; }
    }
}
