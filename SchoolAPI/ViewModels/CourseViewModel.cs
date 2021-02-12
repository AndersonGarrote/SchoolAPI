using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace School.Repository.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public string Schedule { get; set; }
        public int ProfessorId { get; set; }
    }
}
