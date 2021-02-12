using Mapster;
using Microsoft.AspNetCore.Builder;
using School.API.ViewModels;
using School.Repository.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace School.API.Helppers
{
    public static class MapsterConfigurationExtension
    {
        public static IApplicationBuilder UseMapsterConfiguration(this IApplicationBuilder app)
        {
            // Configurações
            TypeAdapterConfig<Course, CourseViewModel>.NewConfig()
                        .Map(dest => dest.Schedule, src => src.Schedule.ToString("t"));
            TypeAdapterConfig<CourseToViewModel, Course>.NewConfig()
                        .Map(dest => dest.Schedule, src => DateTime.ParseExact(src.Schedule, "HH:mm", CultureInfo.InvariantCulture));

            TypeAdapterConfig<Student, StudentViewModel>.NewConfig()
                .Map(dest => dest.IngressYear, src => src.IngressYear.Year)
                .Map(dest => dest.Age, src => DateTime.Now.Year - src.DateOfBirth.Year);
            TypeAdapterConfig<StudentToViewModel, Student>.NewConfig();


            TypeAdapterConfig<Professor, ProfessorViewModel>.NewConfig()
                .Map(dest => dest.IngressYear, src=> src.IngressYear.Year)
                .Map(dest => dest.Age, src => DateTime.Now.Year - src.DateOfBirth.Year);
            TypeAdapterConfig<ProfessorToViewModel, Professor>.NewConfig();
            return app;
        }
    }
}
