﻿using School.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace School.API.ViewModels
{
    public class StudentToViewModel
    {

        public string StudentName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime IngressYear { get; set; }

        public IEnumerable<int> CoursesIds { get; set; }

        
    }
}
