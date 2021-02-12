using Mapster;
using MapsterMapper;
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
    [Route("api/Students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private IUnitOfWork _dbAccessUnitOfWork;
        private IMapper _mapper;

        public StudentsController(IUnitOfWork dbAccessUnitOfWork, IMapper mapper)
        {
            _dbAccessUnitOfWork = dbAccessUnitOfWork;
            _mapper = mapper;
        }



        /// <summary>
        /// Returns All Students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var allStudents = _dbAccessUnitOfWork.Students.GetAll();
            if (allStudents == null)
            {
                return NotFound();
            }
            return Ok(allStudents.Adapt<IEnumerable<StudentViewModel>>());

        }

        /// <summary>
        /// Returns a Single Student by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetStudent")]
        public IActionResult Get(int id)
        {
            var singleStudent = _dbAccessUnitOfWork.Students.Get(id);
            if (singleStudent == null)
            {
                return NotFound();
            }
            return Ok(singleStudent.Adapt<StudentViewModel>());
        }

        /// <summary>
        /// Insert a Student into Database
        /// </summary>
        /// <param name="studentFromClient"></param>
        [HttpPost]
        public IActionResult Post([FromBody] StudentToViewModel studentFromClient)
        {
            ICollection<Course> courses = new List<Course>();

            foreach (int course in studentFromClient.CoursesIds)
            {
                var courseFromDb = _dbAccessUnitOfWork.Courses.Get(course);
                if (courseFromDb == null)
                {
                    return NotFound($"Course {course} not found");
                }
                else
                {
                    courses.Add(courseFromDb);
                }

            }


            if (!ModelState.IsValid) //por validação de annotation
            {
                return BadRequest();
            }

            var studentEntity = studentFromClient.Adapt<Student>();
            studentEntity.Courses = courses;
            _dbAccessUnitOfWork.Students.Add(studentEntity);
            _dbAccessUnitOfWork.Save();

            var studentToReturn = studentEntity.Adapt<StudentViewModel>();
            return CreatedAtRoute("GetStudent",
                new { id = studentEntity.Id },
                studentToReturn);
        }

        /// <summary>
        /// Update a single student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentFromClient"></param>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StudentToViewModel studentFromClient)
        {
            var student = _dbAccessUnitOfWork.Students.Get(id);

            foreach (int course in studentFromClient.CoursesIds)
            {
                var courseFromDb = _dbAccessUnitOfWork.Courses.Get(course);

                if (courseFromDb == null)
                {
                    return NotFound($"Course {course} not found");
                }
                else
                {
                    if (student.Courses == null) student.Courses = new List<Course>();
                    if (courseFromDb.Students == null) courseFromDb.Students = new List<Student>();
                    student.Courses.Add(courseFromDb);
                    courseFromDb.Students.Add(student);
                }

            }

            _mapper.Map(studentFromClient, student);

            _dbAccessUnitOfWork.Save();


            return NoContent();
        }


        /// <summary>
        /// Remove a student from the database
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_dbAccessUnitOfWork.Students.Get(id) == null)
            {
                return BadRequest();
            }
            var removedStudent = _dbAccessUnitOfWork.Students.Get(id);
            _dbAccessUnitOfWork.Students.Remove(removedStudent);
            _dbAccessUnitOfWork.Save();
            return NoContent();
        }

        /// <summary>
        /// Returns the courses for a single student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/courses", Name = "GetStudentCourses")]
        public ActionResult<IEnumerable<CourseViewModel>> GetCourses(int id)
        {
            var singleStudent = _dbAccessUnitOfWork.Students.Get(id);
            if (singleStudent == null)
            {
                return NotFound();
            }

            var courses = _dbAccessUnitOfWork.Students.GetAllCourses(id).Adapt<IEnumerable<CourseViewModel>>();

            return Ok(courses.OrderBy(c => c.Schedule));
        }

        /// <summary>
        /// Returns the names of the professors of a single student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/professors", Name = "GetStudentProfessors")]
        public ActionResult<List<String>> GetStudentProfessors(int id)
        {

            return Ok(_dbAccessUnitOfWork.Students.GetProfessorNames(id));
        }
    }
}
