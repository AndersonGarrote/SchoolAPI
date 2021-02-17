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
        private List<Student> studentList;
        private Course testCourse;

        private UnitOfWork unitOfWork { get; set; }
        public SchoolDbContext SchoolDbContext { get; set; }

        [SetUp]
        public void Setup()
        {
            // Configurando o database in-memory
            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                    .UseInMemoryDatabase(databaseName: "SchoolDatabase")
                    .Options;

            SchoolDbContext = new SchoolDbContext(options);

            unitOfWork = new UnitOfWork(SchoolDbContext);

        }

        [Test]
        public void GetAll_ShouldReturnNotEmptyList()
        {
            PopulateUnitOfWork();

            var listOfCourses = unitOfWork.Courses.GetAll();

            Assert.IsNotEmpty(listOfCourses);
        }


        [Test]
        public void GetAll_ShouldReturnAllInsertedCourses()
        {
            PopulateUnitOfWork();
            var expectedList = new List<Course> { testCourse };

            var listOfCourses = unitOfWork.Courses.GetAll();

            CollectionAssert.AreEqual(expectedList, listOfCourses);
        }


        [Test]
        [TestCase(1)]
        public void Get_ValidId_ShouldReturnCourse(int validId)
        {
            PopulateUnitOfWork();

            var course = unitOfWork.Courses.Get(validId);

            Assert.AreEqual(testCourse, course);
        }


        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(10001)]
        public void Get_InvalidId_ShouldReturnNull(int invalidId)
        {
            PopulateUnitOfWork();

            var course = unitOfWork.Courses.Get(invalidId);

            Assert.IsNull(course);
        }


        [Test]
        [TestCase(1)]
        public void Exists_ValidId_ShouldReturnTrue(int validId)
        {
            PopulateUnitOfWork();

            var exist = unitOfWork.Courses.Exists(validId);

            Assert.IsTrue(exist);
        }


        [Test]
        public void Add_ValidCourse_ShouldAddInRepository()
        {
            PopulateUnitOfWork();
            var newTestCourse = new Course()
            {
                Id = 2,
                Name = "Course 2",
                Room = "A-42",
                Schedule = DateTime.Now,
                Students = studentList,
                Professor = unitOfWork.Professors.Get(1)
            };

            unitOfWork.Courses.Add(newTestCourse);
            var course = unitOfWork.Courses.Get(2);

            Assert.AreEqual(newTestCourse, course);
        }

        [Test]
        public void Add_InvalidCourse_ShouldNotAddInRepository()
        {
            PopulateUnitOfWork();
            var newTestCourse = new Course()
            {
                Id = 3,
                Name = "Course 2",
                Room = "A-42",
                Schedule = DateTime.Now,
                Students = null,
                Professor = unitOfWork.Professors.Get(1)
            };

            unitOfWork.Courses.Add(newTestCourse);
            var course = unitOfWork.Courses.Get(3);

            Assert.AreEqual(newTestCourse, course);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(10001)]
        public void Exists_InvalidId_ShouldReturnFalse(int invalidId)
        {
            PopulateUnitOfWork();

            var exist = unitOfWork.Courses.Exists(invalidId);

            Assert.IsFalse(exist);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(100000)]
        public void RollCall_InvalidId_ShouldNotThrowException(int invalidId)
        {
            PopulateUnitOfWork();
            Assert.DoesNotThrow(() => unitOfWork.Courses.RollCall(invalidId));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(100000)]
        public void RollCall_InvalidId_ShouldReturnEmptyList(int invalidId)
        {
            PopulateUnitOfWork();

            var listOfStudents = unitOfWork.Courses.RollCall(invalidId);

            Assert.IsEmpty(listOfStudents);
        }

        [Test]
        [TestCase(1)]
        public void RollCall_ValidId_ShouldNotThrowException(int validId)
        {
            PopulateUnitOfWork();
            Assert.DoesNotThrow(() => unitOfWork.Courses.RollCall(validId));
        }

        [Test]
        [TestCase(1)]
        public void RollCall_ValidId_ShouldReturnNotEmptyList(int validId)
        {
            PopulateUnitOfWork();

            var listOfStudents = unitOfWork.Courses.RollCall(validId);

            Assert.IsNotEmpty(listOfStudents);
        }


        [Test]
        [TestCase(1)]
        public void RollCall_ValidId_ReturnStudentsFromCourse(int validId)
        {
            PopulateUnitOfWork();
            var expectedList = studentList;

            var listOfStudents = unitOfWork.Courses.RollCall(validId);

            CollectionAssert.AreEqual(expectedList, listOfStudents);
        }


        void PopulateUnitOfWork()
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
            }
            );
            
            studentList = new List<Student>() { unitOfWork.Students.Get(1) };
            testCourse = new Course()
            {
                Id = 1,
                Name = "TOP",
                Room = "COV-19",
                Schedule = DateTime.Now,
                Students = studentList,
                Professor = unitOfWork.Professors.Get(1)
            };
            unitOfWork.Courses.Add(testCourse);
            unitOfWork.Save();
        }

        [TearDown]
        public void Cleanup()
        {
            unitOfWork.Professors.Remove(unitOfWork.Professors.Get(1));
            unitOfWork.Courses.Remove(unitOfWork.Courses.Get(1));
            unitOfWork.Students.Remove(unitOfWork.Students.Get(1));
            unitOfWork.Save();

        }
        [OneTimeTearDown]
        public void Cleanup()
        {
            unitOfWork.Professors.Remove(unitOfWork.Professors.Get(1));
            unitOfWork.Courses.Remove(unitOfWork.Courses.Get(1));
            unitOfWork.Students.Remove(unitOfWork.Students.Get(1));
            unitOfWork.Save();
        }
    }
}