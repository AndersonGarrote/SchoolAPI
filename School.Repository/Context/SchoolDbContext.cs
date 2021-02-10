using Microsoft.EntityFrameworkCore;
using School.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Repository.Context
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Student { get; set; }

        //IConfiguration Configuration;

        //public SchoolDbContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SchoolDBAgoraVai;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    Id = 1,
                    DateOfBirth = DateTime.Today,
                    StudentName = "Mario Laiala",
                    IngressYear = DateTime.Today,
                },
                new Student()
                {
                    Id = 2,
                    DateOfBirth = DateTime.Today,
                    StudentName = "Cirilo",
                    IngressYear = DateTime.Today,
                },
                new Student()
                {
                    Id = 3,
                    DateOfBirth = DateTime.Today,
                    StudentName = "Maria Joaquina",
                    IngressYear = DateTime.Today,
                });

            modelBuilder.Entity<Professor>().HasData(
               new Professor()
               {
                   Id = 1,
                   ProfessorName = "Helena",
                   IngressYear = DateTime.Now,
                   DateOfBirth = DateTime.Now
               },
               new Professor()
               {
                   Id = 2,
                   ProfessorName = "Matilde",
                   IngressYear = DateTime.Now,
                   DateOfBirth = DateTime.Now
               });
            modelBuilder.Entity<Course>().HasData(
               new Course()
               {
                   Id = 1,
                   Name = "Calculo 3",
                   Schedule = DateTime.Now,
                   //Room = "A-20",
                   //Students = new List<Student> {
                   //    new Student()
                   //    {
                   //         Id = 1,
                   //         DateOfBirth = DateTime.Today,
                   //         StudentName = "Mario Laiala",
                   //         IngressYear = DateTime.Today,
                   //    },
                   //    new Student()
                   //    {
                   //         Id = 2,
                   //         DateOfBirth = DateTime.Today,
                   //         StudentName = "Cirilo",
                   //         IngressYear = DateTime.Today,
                   //    },
                   //    new Student()
                   //    {
                   //         Id = 3,
                   //         DateOfBirth = DateTime.Today,
                   //         StudentName = "Maria Joaquina",
                   //         IngressYear = DateTime.Today,
                   //     }
                   //},
                   //Professor = new Professor()
                   //{
                   //    Id = 2,
                   //    ProfessorName = "Matilde",
                   //    IngressYear = DateTime.Now,
                   //    DateOfBirth = DateTime.Now
                   //},
                   ProfessorId = 2

               });

            base.OnModelCreating(modelBuilder);
        }
    }
}
