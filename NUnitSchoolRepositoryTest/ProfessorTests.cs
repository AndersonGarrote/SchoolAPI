using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using School.Repository.Context;
using School.Repository.Models;
using School.Repository.Repository;

namespace NUnitSchoolRepositoryTest
{
    [TestFixture]
    class ProfessorTests
    {
        private UnitOfWork unitOfWork { get; set; }

        private DbContextOptions<SchoolDbContext> options = new DbContextOptionsBuilder<SchoolDbContext>()
            .UseInMemoryDatabase(databaseName: "SchoolDatabase")
            .Options;


        private SchoolDbContext context { get; set; }

        [SetUp]
        public void Setup()
        {
           
            context = new SchoolDbContext(options);
            unitOfWork = new UnitOfWork(context);

            PopulateUnitOfWork();


        }

        [TearDown]
        public void Cleanup()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
            unitOfWork.Dispose();
            unitOfWork = null;

        }

        void PopulateUnitOfWork()
        {
            unitOfWork.Professors.Add(new Professor()
                {
                    Id = 1,
                    ProfessorName = "Helena",
                    DateOfBirth = DateTime.Now,
                    IngressYear = DateTime.Now,
                }
            );
            unitOfWork.Professors.Add(new Professor()
                {
                    Id = 2,
                    ProfessorName = "René",
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
                }
            );
            unitOfWork.Courses.Add(new Course()
                {
                    Id = 1,
                    Name = "Curso1",
                    Room = "1",
                    ProfessorId = 1,
                    Schedule = DateTime.Now
                }
            );
            unitOfWork.Courses.Add(new Course()
                {
                    Id = 2,
                    Name = "Curso2",
                    Room = "2",
                    ProfessorId = 1,
                    Schedule = DateTime.Now
                }
            );
            unitOfWork.Save();
        }

        [Test]
        [TestCase(1)]
        public void GetAllCoursesProfessorReturnsCorrectCourseTest(int id)
        {
            var courses = unitOfWork.Professors.GetAllCourses(id).ToList();

            Assert.AreEqual(1, courses[0].Id);
            Assert.AreEqual(2, courses[1].Id);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(10000)]
        public void GetAllCoursesReturnsEmptyToInvalidId(int invalidId)
        {

            Assert.IsEmpty(unitOfWork.Professors.GetAllCourses(invalidId));
        }

        [Test]
        [TestCase(2)]
        public void GetAllCoursesReturnsEmptyToProfessorWithoutCourses(int id)
        {
            var courses = unitOfWork.Professors.GetAllCourses(id);

            Assert.IsEmpty(courses);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetProfessorByIdReturnRequestedProfessor(int id)
        {
            var professor = unitOfWork.Professors.Get(id);
            Assert.AreEqual(id, professor.Id);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(10000)]
        public void GetProfessorByIdReturnNullForInvalidId(int invalidId)
        {
            Assert.IsNull(unitOfWork.Professors.Get(invalidId));
        }

        [Test]
        public void GetAllProfessorsReturnsAllProfessors()
        {
            var professors = unitOfWork.Professors.GetAll().ToList();
            Assert.AreEqual(1, professors[0].Id);
            Assert.AreEqual(2, professors[1].Id);
        }

        [Test]
        public void AddProfessorActuallyAddsAProfessor()
        {
            var professor = new Professor()
            {
                Id = 3,
                DateOfBirth = DateTime.MinValue,
                IngressYear = DateTime.Today,
                ProfessorName = "Jorge"
            };

            //arrange
            unitOfWork.Professors.Add(professor);
            unitOfWork.Save();

            //act
            var professorGet = unitOfWork.Professors.Get(3);

            //assert
            Assert.IsNotNull(professorGet);
            Assert.AreEqual(3, professorGet.Id);

            unitOfWork.Professors.Remove(professor);
        }

        [Test]
        public void RemoveProfessorActuallyRemovesAProfessor()
        {
            var professor = new Professor()
            {
                Id = 3,
                DateOfBirth = DateTime.MinValue,
                IngressYear = DateTime.Today,
                ProfessorName = "Jorge"
            };

            //arrange
            unitOfWork.Professors.Add(professor);
            //arrange
            var professorGet = unitOfWork.Professors.Get(3);

            //act
            unitOfWork.Professors.Remove(professorGet);
            unitOfWork.Save();
            var professorAfterRemove = unitOfWork.Professors.Get(3);

            //assert
            Assert.IsNull(professorAfterRemove);

        }

        [Test]
        public void FindProfessorByName()
        {
            var professor = unitOfWork.Professors.Find(p => p.ProfessorName == "Helena").ToList();
            Assert.AreEqual(1, professor[0].Id);
        }

        [Test]
        public void FindProfessorByCourse()
        {
            var professor = unitOfWork.Professors.Find(p => p.Courses.Any(d => d.Id == 1)).ToList();
            Assert.AreEqual(1, professor[0].Id);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void ExistsProfessorValidId(int id)
        {
            Assert.IsTrue(unitOfWork.Professors.Exists(id));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(10000)]
        public void ExistsProfessorInvalidId(int id)
        {
            Assert.IsFalse(unitOfWork.Professors.Exists(id));
        }

    }
}

