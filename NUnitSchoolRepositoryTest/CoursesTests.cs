using NUnit.Framework;
using School.Repository.Context;
using School.Repository.Repository;
using System;
using Moq;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School.Repository.Models;
using System.Collections.Generic;

namespace NUnitSchoolRepositoryTest
{
    public class CoursesTests
    {
        private UnitOfWork unitOfWork { get; set; }
        private DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
            .UseInMemoryDatabase(databaseName: "SchoolDatabase")
            .Options;


        private SchoolDbContext context { get; set; }

        [SetUp]
        public void Setup()
        {
            //var options = new DbContextOptionsBuilder<SchoolDbContext>()
            //        .UseInMemoryDatabase(databaseName: "SchoolDatabase")
            //        .Options;
            //var schoolDbContext = new SchoolDbContext(options)

            context = new SchoolDbContext(options);
            unitOfWork = new UnitOfWork(context);

            PopulateUnitOfWork(unitOfWork);


        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(100000)]
        public void RollCall_InvalidId_ShouldThrowException(int invalidId)
        {
            Assert.Throws<NullReferenceException>(
              () => unitOfWork.Courses.RollCall(invalidId)
            );
        }

        [Test]
        [TestCase(1)]
        public void RollCall_ValidId_ShouldNotThrowException(int validId)
        {
            Assert.DoesNotThrow(() => unitOfWork.Courses.RollCall(validId));
        }

        [Test]
        [TestCase(1)]
        public void RollCall_ValidId_ReturnStudentsFromCourse(int validId)
        {
            Assert.DoesNotThrow(() => unitOfWork.Courses.RollCall(validId));
        }

        void PopulateUnitOfWork(UnitOfWork unitOfWork)
        {
            unitOfWork.Professors.Add(new Professor()
                {
                    Id = 1,
                    ProfessorName = "Heleno",
                    DateOfBirth = DateTime.Now,
                    IngressYear = DateTime.Now,
                }
            );
            unitOfWork.Students.Add(new Student()
            {
                Id = 1,
                StudentName = "Rogerio",
                DateOfBirth = DateTime.Now,
                IngressYear = DateTime.Now,
            });

            unitOfWork.Students.Add(new Student()
            {
                Id = 2,
                StudentName = "Isfairenbloden",
                DateOfBirth = DateTime.Now,
                IngressYear = DateTime.Now,
            });

            unitOfWork.Courses.Add(new Course()
                {
                    Id = 1,
                    Name = "TOP",
                    Room = "COV-19",
                    Schedule = DateTime.Now,
                    Students = new List<Student>() { unitOfWork.Students.Get(1) },
                    Professor = unitOfWork.Professors.Get(1)
                }
            );
            unitOfWork.Save();
        }

        [TearDown]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
            unitOfWork.Dispose();
            unitOfWork = null;

        }
    }
}