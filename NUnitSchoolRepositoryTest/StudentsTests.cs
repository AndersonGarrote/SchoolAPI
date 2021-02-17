using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using School.Repository.Context;
using School.Repository.Models;
using School.Repository.Repository;
using System;
using System.Collections.Generic;


namespace NUnitSchoolRepositoryTest
{
    [TestFixture]
    class StudentsTests
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

            PopulateUnitOfWork(unitOfWork);
        }


        /// <summary>
        /// Checking if a list of professorNames is returned on a GetProfessorNames with valid Id
        /// </summary>
        [Test]
        public void GetProfessorNamesIdValido()
        {
            List<string> profsEsperados = new List<string>();
            profsEsperados.Add("Heleno");


            List<string> profsRetornados = unitOfWork.Students.GetProfessorNames(1);


            Assert.That(profsEsperados[0], Is.EqualTo(profsRetornados[0]));

        }

        /// <summary>
        /// Checking if an empty list is returned on a GetProfessorNames with invalid Id
        /// </summary>
        /// <param name="id"></param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(100000)]
        public void GetProfessorNamesVazioComIdInvalido(int id)
        {
            List<string> profsRetornados = unitOfWork.Students.GetProfessorNames(id);

            Assert.That(profsRetornados, Is.Empty);
        }


        /// <summary>
        /// Checking if a list of Courses is returned when using GetAllCourses with valid Id
        /// </summary>
        [Test]
        public void GetAllCoursesIdValido()
        {
            var cursoEsperadoTeste = new Course()
            {
                Id = 1,
                Name = "TOP",
                Room = "COV-19",
                Schedule = DateTime.Now,
                Students = new List<Student>() { unitOfWork.Students.Get(1) },
                Professor = unitOfWork.Professors.Get(1)
            };

            var cursosEsperados = new List<Course>() { cursoEsperadoTeste };

            var cursosRetornados = unitOfWork.Students.GetAllCourses(1) as List<Course>;

            Assert.That(cursosEsperados[0].Id, Is.EqualTo(cursosRetornados[0].Id));

        }


        /// <summary>
        /// Checking if an empty list is returned when using GetAllCourses with invalid Id
        /// </summary>
        /// <param name="id"></param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(100000)]
        public void GetAllCoursesVazioComIdInvalido(int id)
        {


            List<Course> cursosRetornados = unitOfWork.Students.GetAllCourses(id) as List<Course>;
            Assert.That(cursosRetornados, Is.Empty);


        }


        /// <summary>
        /// Checking if the correct Student entity is returned when using Get with valid Id
        /// </summary>
        [Test]
        public void GetStudentComIdValido()
        {
            Student estudanteEsperado = new Student()
            {
                Id = 1,
                StudentName = "Rogerio",
                DateOfBirth = DateTime.Now,
                IngressYear = DateTime.Now,
            };



            Student estudanteRetornado = unitOfWork.Students.Get(1);

            Assert.That(estudanteRetornado.Id, Is.EqualTo(estudanteEsperado.Id));
            Assert.That(estudanteRetornado.StudentName, Is.EqualTo(estudanteEsperado.StudentName));

        }


        /// <summary>
        /// Checking if a null reference is returned when using Get with invalid Id
        /// </summary>
        /// <param name="id"></param>
        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(100000)]
        public void GetStudentIdInvalido(int id)
        {


            Student estudanteRetornado = unitOfWork.Students.Get(id);

            Assert.IsNull(estudanteRetornado);


            ;
        }


        /// <summary>
        /// Checking the insertion of a correctly specified student through Add Method
        /// </summary>
        [Test]
        public void AddStudentComFormatoCorreto()
        {
            Student studentToInsert = new Student()
            {
                Id = 3,
                StudentName = "Luigi",
                DateOfBirth = DateTime.Now,
                IngressYear = DateTime.Now,
            };


            unitOfWork.Students.Add(studentToInsert);
            Student returnStudent = unitOfWork.Students.Get(3);

            Assert.That(studentToInsert.Id, Is.EqualTo(returnStudent.Id));
            Assert.That(studentToInsert.StudentName, Is.EqualTo(returnStudent.StudentName));
            Assert.That(studentToInsert.DateOfBirth, Is.EqualTo(returnStudent.DateOfBirth));
            Assert.That(studentToInsert.IngressYear, Is.EqualTo(returnStudent.IngressYear));

            unitOfWork.Students.Remove(unitOfWork.Students.Get(3));
            unitOfWork.Save();




        }


        // Check aberrant insertions (Get Method)


        /// <summary>
        /// Checking if a list with all students is returned when using GetAll Method
        /// </summary>
        [Test]
        public void GetAllRetornoCorreto()
        {

            List<Student> expectedStudents = new List<Student>()
            {
                new Student()
                {
                    Id = 1,
                    StudentName = "Rogerio",
                    DateOfBirth = DateTime.Now,
                    IngressYear = DateTime.Now,
                },
                new Student()
                {
                    Id = 2,
                    StudentName = "Isfairenbloden",
                    DateOfBirth = DateTime.Now,
                    IngressYear = DateTime.Now,
                }
            };


            List<Student> returnStudents = unitOfWork.Students.GetAll() as List<Student>;

            //Checking Student1
            Assert.That(returnStudents[0].Id, Is.EqualTo(expectedStudents[0].Id));
            Assert.That(returnStudents[0].StudentName, Is.EqualTo(expectedStudents[0].StudentName));

            //Checking Student2
            Assert.That(returnStudents[1].Id, Is.EqualTo(expectedStudents[1].Id));
            Assert.That(returnStudents[1].StudentName, Is.EqualTo(expectedStudents[1].StudentName));



        }


        /// <summary>
        /// Check if the Method Remove with valid Id removes specified Student
        /// </summary>
        [Test]
        public void RemoveStudentIdValido()
        {



            unitOfWork.Students.Remove(unitOfWork.Students.Get(1));
            unitOfWork.Save();
            Student expectedNullStudent = unitOfWork.Students.Get(1);

            Assert.IsNull(expectedNullStudent);

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
