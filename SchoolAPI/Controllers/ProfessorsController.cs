using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Mapster;
using School.Repository.Repository;
using MapsterMapper;
using School.Repository.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        private IUnitOfWork _dbAcessUnitOfWork;

        public ProfessorsController(IUnitOfWork dbAcessUnitOfWork)
        {
            _dbAcessUnitOfWork = dbAcessUnitOfWork;
        }


        /// <summary>
        /// Returns all professors in the database
        /// </summary>
        /// <returns>list of professors</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProfessorViewModel>> Get()
        {
            var profs = _dbAcessUnitOfWork.Professors.GetAll();
            return Ok(profs.Adapt<IEnumerable<ProfessorViewModel>>());
        }

        /// <summary>
        /// Returns a professor by ID
        /// </summary>
        /// <param name="id">professor database identifier</param>
        /// <returns>one professor instance if it exists</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var prof = _dbAcessUnitOfWork.Professors.Get(id);
            if (prof == null) return NotFound();
            return Ok(prof.Adapt<ProfessorViewModel>());
        }

        /// <summary>
        /// Returns the courses a given professor teaches
        /// </summary>
        /// <param name="id">professor database identifier</param>
        /// <returns>list of courses</returns>
        [HttpGet("{id}/courses")]
        public ActionResult<IEnumerable<CourseViewModel>> GetCourses(int id)
        {
            var courses= _dbAcessUnitOfWork.Professors.GetAllCourses(id);
            if (courses == null) return NotFound();
            return Ok(courses.Adapt<IEnumerable<CourseViewModel>>());
        }

        // POST api/<ProfessorsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<ProfessorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProfessorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
