using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.ViewModels
{
    public class ProfessorToViewModel
    {
        public string ProfessorName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime IngressYear { get; set; }

        public ICollection<CourseToViewModel> Courses { get; set; }

    }
}
