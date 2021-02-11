using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace School.Repository.Models
{
    public class CourseViewModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public string Schedule { get; set; }
        public ICollection<StudentDto> Students { get; set; }
        [JsonIgnore]
        public int ProfessorId { get; set; }
        public ProfessorViewModel Professor { get; set; }
    }
}
