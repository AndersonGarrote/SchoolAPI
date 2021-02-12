using Mapster;
using Microsoft.AspNetCore.Builder;
using School.Repository.Models;
using System;
using System.Collections.Generic;
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
                        .Map(dest => dest.Schedule, src => $"{src.Schedule.TimeOfDay.Hours}:{src.Schedule.TimeOfDay.Minutes}");

            TypeAdapterConfig<Student, StudentViewModel>.NewConfig()
                        .Map(dest => dest.Idade, src => (DateTime.Now.Year - src.DateOfBirth.Year));


            TypeAdapterConfig<Professor, ProfessorViewModel>.NewConfig()
                .Map(dest => dest.IngressYear, src=> src.IngressYear.Year)
                .Map(dest => dest.Age, src => DateTime.Now.Year - src.DateOfBirth.Year);
            return app;
        }
    }
}
