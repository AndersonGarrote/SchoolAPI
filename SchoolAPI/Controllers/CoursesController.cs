using Mapster;
using Microsoft.AspNetCore.Mvc;
using School.API.ViewModels;
using School.Repository.Models;
using School.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        IUnitOfWork DbAccessUnit;

        public CoursesController(IUnitOfWork dbAccessUnit)
        {
            DbAccessUnit = dbAccessUnit;
        }


        /// <summary>
        /// Retrieves all the Courses from the database
        /// </summary>
        /// <returns>A list of Courses</returns>
        [HttpGet]
        public IEnumerable<CourseViewModel> Get()
        {
            return DbAccessUnit.Courses.GetAll().Adapt<IEnumerable<CourseViewModel>>();
        }

        /// <summary>
        /// Retrieves the Course with the given identifier from the database
        /// </summary>
        /// <param name="id">Course identifier</param>
        /// <returns>The Course</returns>
        [HttpGet("{id}", Name = "GetCourse")]
        public ActionResult<CourseViewModel> Get(int id)
        {
            if (!DbAccessUnit.Courses.Exists(id))
            {
                return NotFound();
            }

            return DbAccessUnit.Courses.Get(id).Adapt<CourseViewModel>();
        }

        /// <summary>
        /// Creates the given Course in the database
        /// </summary>
        /// <param name="course">Course to be created</param>
        /// <returns>Created course</returns>
        [HttpPost]
        public ActionResult<CourseViewModel> Post(CourseToViewModel course)
        {

            if (!DbAccessUnit.Professors.Exists(course.ProfessorId)) return NotFound("Invalid Professor ID");
            var courseEntity = course.Adapt<Course>();
            DbAccessUnit.Courses.Add(courseEntity);
            DbAccessUnit.Save();

            var courseToReturn = courseEntity.Adapt<CourseViewModel>();
            return CreatedAtRoute("GetCourse",
                new { id = courseEntity.Id },
                courseToReturn);
        }

        /// <summary>
        /// Updates the given Course in the database
        /// </summary>
        /// <param name="id">Course identifier</param>
        /// <param name="course">Course to be updated</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CourseToViewModel course)
        {
            if (!DbAccessUnit.Professors.Exists(course.ProfessorId)) return NotFound("Invalid Professor ID");
            
            var courseFromDb = DbAccessUnit.Courses.Get(id);
            if (courseFromDb == null)
            {
                var courseToAdd = course.Adapt<Course>();
                DbAccessUnit.Courses.Add(courseToAdd);
                DbAccessUnit.Save();
                var courseToReturn = courseToAdd.Adapt<CourseViewModel>();
                return CreatedAtRoute("GetCourse",
                    new { id = courseToAdd.Id },
                    courseToReturn);
            }

            course.Adapt(courseFromDb);
            DbAccessUnit.Save();
        
            return NoContent();
        }

        /// <summary>
        /// Deletes the Course with the given identifier from the database
        /// </summary>
        /// <param name="id">Course identifier</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!DbAccessUnit.Courses.Exists(id))
            {
                return NotFound();
            }

            var course = DbAccessUnit.Courses.Get(id);
            DbAccessUnit.Courses.Remove(course);

            DbAccessUnit.Save();

            return Ok();
        }

        /// <summary>
        /// Lists the Students from the Course with the given identifier, ordered by name
        /// </summary>
        /// <param name="id">Course identifier</param>
        /// <returns>List of Students</returns>
        [HttpGet("{id}/Students", Name = "GetCourseStudents")]
        public ActionResult<IEnumerable<StudentViewModel>> GetCourseStudents(int id)
        {
            if (!DbAccessUnit.Courses.Exists(id))
            {
                return NotFound();
            }

            return Ok(DbAccessUnit.Courses.RollCall(id).Adapt<IEnumerable<StudentViewModel>>());
        }

        /// <summary>
        /// Retrieves the Professor from the Course with the given identifier
        /// </summary>
        /// <param name="id">Course identifier</param>
        /// <returns>Professor from the Course</returns>
        [HttpGet("{id}/Professor", Name = "GetCourseProfessor")]
        public ActionResult<ProfessorViewModel> GetCourseProfessor(int id)
        {
            if (!DbAccessUnit.Courses.Exists(id)) return NotFound();

            var courseFromDb = DbAccessUnit.Courses.Get(id);

            if (!DbAccessUnit.Professors.Exists(courseFromDb.ProfessorId)) return NotFound();

            return Ok(DbAccessUnit.Professors.Get(courseFromDb.ProfessorId).Adapt<ProfessorViewModel>());
        }
    }
}
