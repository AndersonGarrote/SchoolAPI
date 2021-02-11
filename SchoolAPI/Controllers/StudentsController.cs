using Mapster;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{id}")]
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
        /// <param name="value"></param>
        [HttpPost]
        public IActionResult Post([FromBody] Student studentFromClient) //aqui será StudentToViewModel
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Student insertedStudent = studentFromClient.Adapt<Student>();

            _dbAccessUnitOfWork.Students.Add(insertedStudent);
            _dbAccessUnitOfWork.Save();                                             // Possível troca por SaveAsync ?

            //_dbAccessUnitOfWork.Students.Find(s => (s.));

            //return Created();

            return Ok(); //placeholder


        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
