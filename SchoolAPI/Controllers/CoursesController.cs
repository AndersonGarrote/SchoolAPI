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


        // GET: api/<CoursesController>
        /// <summary>
        /// Retrieves all the Courses from the database
        /// </summary>
        /// <returns>A list of Courses</returns>
        [HttpGet]
        public IEnumerable<CourseViewModel> Get()
        {
            return DbAccessUnit.Courses.GetAll().Adapt<IEnumerable<CourseViewModel>>();
        }

        // GET api/<CoursesController>/5
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

        // POST api/<CoursesController>
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

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CourseToViewModel course)
        {
            var courseFromDb = DbAccessUnit.Courses.Get(id);

            if (courseFromDb == null)
            {
                if (!DbAccessUnit.Professors.Exists(course.ProfessorId)) return NotFound("Invalid Professor ID");
                var courseToAdd = course.Adapt<Course>();
                DbAccessUnit.Courses.Add(courseToAdd);
                DbAccessUnit.Save();
                var courseToReturn = courseToAdd.Adapt<CourseViewModel>();
                return CreatedAtRoute("GetCourse",
                    new { id = courseToAdd.Id },
                    courseToReturn);
            }

            courseFromDb.Name = course.Name;
            courseFromDb.ProfessorId = course.ProfessorId;

            //courseFromDb.IngressYear = course.IngressYear;
            //courseFromDb.ProfessorName = course.ProfessorName;
            DbAccessUnit.Save();
            return NoContent();
        }

        // DELETE api/<CoursesController>/5
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
    }
}
