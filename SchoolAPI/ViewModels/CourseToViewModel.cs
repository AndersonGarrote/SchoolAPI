using School.Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace School.API.ViewModels
{
    public class CourseToViewModel
    {
        public string Name { get; set; }
        public string Room { get; set; }
        public string Schedule { get; set; }
        [Required]
        public int ProfessorId { get; set; }

    }
}
