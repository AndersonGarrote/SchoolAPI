using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Repository.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public DateTime IngressYear { get; set; }
        //public ICollection<CourseViewModel> Courses { get; set; }

    }
}
