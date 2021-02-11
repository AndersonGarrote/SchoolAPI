using School.Repository.Context;
using School.Repository.Models;
using School.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace School.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var unitOfWork = new UnitOfWork(new SchoolDbContext()))
            {

                var profs = unitOfWork.Professors.GetAllCourses(2);
                Console.WriteLine(JsonSerializer.Serialize(profs, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}
