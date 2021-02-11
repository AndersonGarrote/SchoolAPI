using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.ViewModels
{
    public class StudentToViewModel
    {

        public string StudentName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime IngressYear { get; set; }

        public IEnumerable<int> Courses { get; set; }
    }
}
