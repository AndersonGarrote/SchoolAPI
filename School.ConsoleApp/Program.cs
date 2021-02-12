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

                
                Console.WriteLine(typeof(StudentRepository));
            }
        }
    }
}
