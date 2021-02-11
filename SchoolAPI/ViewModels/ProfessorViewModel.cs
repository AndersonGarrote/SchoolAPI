using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace School.Repository.Models
{
    public class ProfessorViewModel
    {
        //[JsonIgnore]
        public int Id { get; set; }
        public string ProfessorName { get; set; }
        public int Age { get; set; }
        public int IngressYear { get; set; }

        //public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
