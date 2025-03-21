using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Context;
using StudentAPI.DTOs;
using StudentAPI.Filters;
using StudentAPI.Models;
using StudentAPI.Repo;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentRepo studentRepo;
        public StudentController(IStudentRepo sd)
        {
            studentRepo = sd;
        }

        [HttpGet]
        [MyResultFilterAttribute]
        [Authorize(Roles = "Admin,User")] 
        public IActionResult GetAll()
        {
            //var studs = _sdb.Students.Include(e => e.Department).ToList();
            var studs = studentRepo.getAll();
            if (studs == null) 
                return NotFound();

            //return Ok(studs);
            var studentDTOs = studs.Adapt<List<StudentDTO>>(); 
            return Ok(studentDTOs);
        }
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(int id)
        {
            //var stud = _sdb.Students.Include(e => e.Department).FirstOrDefault(x => x.Id == id);
            var stud = studentRepo.getById(id);
            if (stud == null)
                return NotFound();
            //return Ok(stud);
            var studentDTO = stud.Adapt<StudentDTO>();
            return Ok(studentDTO);
        }
        [HttpGet("{name:alpha}")]
        [Authorize(Roles = "Admin,User")] 
        public IActionResult GetByName(string name) 
        {
            //var stud = _sdb.Students.Include(e => e.Department).FirstOrDefault(x => x.Name == name);
            var stud = studentRepo.getByName(name);
            if (stud == null)
                return NotFound();
            //return Ok(stud);
            var studentDTO = stud.Adapt<StudentDTO>();
            return Ok(studentDTO);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(Student st)
        {
            if (st.Name == null || st.Age == null || st.Address == null || st.Grade == null)
                return BadRequest();
            //_sdb.Students.Add(st);
            //_sdb.SaveChanges();
            studentRepo.add(st);
            return CreatedAtAction(nameof(GetById), new { id = st.Id }, new { message = "Added Successfully" });
        }
        //[HttpPost("v2")]
        //[ValidateAddressAttribute]
        //public IActionResult AddV2([FromBody] Student st)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var department = _sdb.Departments.FirstOrDefault(d => d.Id == st.DeptId);
        //    if (department == null)
        //        return BadRequest(new { message = "Invalid Department ID" });

        //    _sdb.Students.Add(st);
        //    _sdb.SaveChanges();

        //    return CreatedAtAction(nameof(GetById), new { id = st.Id }, new { message = "Added Successfully" });
        //}

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Student st)
        {
            if (st.Name == null)
                return BadRequest();
            //_sdb.Students.Update(st);
            //_sdb.SaveChanges();
            studentRepo.update(st);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            //var stud = _sdb.Students.Include(s => s.Department).FirstOrDefault(x => x.Id == id);
            var stud = studentRepo.getById(id);
            if (stud == null)
                return NotFound();
            //_sdb.Students.Remove(stud);
            //_sdb.SaveChanges();
            studentRepo.delete(stud);
            return Ok(stud);
        }
        [HttpGet("throw")]
        [Authorize(Roles = "Admin")] 
        public IActionResult ThrowException()
        {
            throw new Exception("This is a test exception!");
        }
    }
}
