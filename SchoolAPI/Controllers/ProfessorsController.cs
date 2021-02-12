using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Mapster;
using School.Repository.Repository;
using MapsterMapper;
using School.API.ViewModels;
using School.Repository.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        private IUnitOfWork _dbAcessUnitOfWork;
        private IMapper _mapper;

        public ProfessorsController(IUnitOfWork dbAcessUnitOfWork, IMapper mapper)
        {
            _dbAcessUnitOfWork = dbAcessUnitOfWork;
            _mapper = mapper;
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
        [HttpGet("{id}", Name = "GetProfessor")]
        public IActionResult GetById(int id)
        {
            var prof = _dbAcessUnitOfWork.Professors.Get(id);
            if (prof == null) return NotFound();
            return Ok(prof.Adapt<ProfessorViewModel>());
        }

        /// <summary>
        /// Retrieves the courses a given professor teaches
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

        /// <summary>
        /// Counts the number of courses a professor teaches
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/courses/count")]
        public ActionResult GetCoursesCount(int id)
        {
            var courses = _dbAcessUnitOfWork.Professors.GetAllCourses(id);
            if (courses == null) return NotFound();
            return Ok(courses.Count());
        }

       /// <summary>
       /// Post a professor
       /// </summary>
       /// <param name="professor">Professor instance</param>
       /// <returns></returns>
        [HttpPost]
        public ActionResult<ProfessorViewModel> Post(ProfessorToViewModel professor)
        {
            var professorEntity = professor.Adapt<Professor>();
            _dbAcessUnitOfWork.Professors.Add(professorEntity);
            _dbAcessUnitOfWork.Save();

            var profToReturn = professorEntity.Adapt<ProfessorViewModel>();
            return CreatedAtRoute("GetProfessor", 
                new {id = professorEntity.Id}, 
                profToReturn);
        }

        /// <summary>
        /// Update or insert a professor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="professor"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorToViewModel professor)
        {
            var profFromDb = _dbAcessUnitOfWork.Professors.Get(id);

            if (profFromDb == null)
            {
                var profToAdd = professor.Adapt<Professor>();
                _dbAcessUnitOfWork.Professors.Add(profToAdd);
                _dbAcessUnitOfWork.Save();
                var profToReturn = profToAdd.Adapt<ProfessorViewModel>();
                return CreatedAtRoute("GetProfessor",
                    new { id = profToAdd.Id },
                    profToReturn);
            }

            _mapper.Map(professor, profFromDb);

            //profFromDb.DateOfBirth = professor.DateOfBirth;
            //profFromDb.IngressYear = professor.IngressYear;
            //profFromDb.ProfessorName = professor.ProfessorName;
            _dbAcessUnitOfWork.Save();
            return NoContent();
        }

        /// <summary>
        /// Delete a professor
        /// </summary>
        /// <param name="id">database identifier</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var prof = _dbAcessUnitOfWork.Professors.Get(id);
            if (prof == null) return NotFound();
            _dbAcessUnitOfWork.Professors.Remove(prof);
            _dbAcessUnitOfWork.Save();
            return NoContent();
        }
    }
}
