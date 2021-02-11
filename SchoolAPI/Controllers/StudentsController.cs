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
    [Route("api/Students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private IUnitOfWork _dbAccessUnitOfWork;

        public StudentsController(IUnitOfWork dbAccessUnitOfWork)
        {
            _dbAccessUnitOfWork = dbAccessUnitOfWork;
        }
        
        
        
        /// <summary>
        /// Returns All Students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var allStudents = _dbAccessUnitOfWork.Students.GetAll();
            if(allStudents == null)
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
            if(singleStudent == null)
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
        public IActionResult Post([FromBody] StudentToViewModel studentFromClient) //aqui será StudentToViewModel
        {
            ICollection<Course> courses = new List<Course>();

            foreach (int course in studentFromClient.Courses)
            {
                var courseFromDb = _dbAccessUnitOfWork.Courses.Get(course);
                if (courseFromDb == null)
                {
                    return NotFound($"Course {course} not found");
                }
                else
                {
                    courses.Append(courseFromDb);
                }
                
            }

            if(!ModelState.IsValid) //por validação de annotation
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentsController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
