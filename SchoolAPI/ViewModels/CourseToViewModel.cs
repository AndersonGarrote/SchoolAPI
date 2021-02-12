using School.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace School.API.ViewModels
{
    public class CourseToViewModel
    {
        // public int Id { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public DateTime Schedule { get; set; }
        public int ProfessorId { get; set; }

        //public Professor Professor { get; set; }
    }
}
