using School.Repository.Context;
using School.Repository.Models;
using School.Repository.Repository;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestSchoolRepository
{
    public class UnitTestCourses
    {
        [Fact]
        public void RollCall_InputValidId_ShouldReturnStudents()
        {
            IEnumerable<Student> students;

            using (var uof = new UnitOfWork(new SchoolDbContext()))
            {
                students = uof.Courses.RollCall(1);
            }

            Assert.NotEmpty(students);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1000001)]
        public void RollCall_InputValidId_ShouldReturnEmptyCollection(int courseId)
        {
            var uof = CreateUnitOfWork();
            IEnumerable<Student> studentsList;

            using (uof)
            {
                studentsList = uof.Courses.RollCall(courseId);
            }

            Assert.Empty(studentsList);
        }

        private static IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(new SchoolDbContext());
        }
    }
}
