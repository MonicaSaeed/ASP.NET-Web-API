using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Context;
using StudentAPI.DTOs;
using StudentAPI.Filters;
using StudentAPI.Models;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentContext _sdb;

        public StudentController(StudentContext sdb)
        {
            _sdb = sdb;
        }

        [HttpGet]
        [MyResultFilterAttribute]
        public IActionResult GetAll()
        {
            var studs = _sdb.Students.Include(e => e.Department).ToList();
            if (studs == null) 
                return NotFound();

            //return Ok(studs);
            var studentDTOs = studs.Adapt<List<StudentDTO>>(); 
            return Ok(studentDTOs);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var stud = _sdb.Students.Include(e => e.Department).FirstOrDefault(x => x.Id == id);
            if (stud == null)
                return NotFound();
            //return Ok(stud);
            var studentDTO = stud.Adapt<StudentDTO>();
            return Ok(studentDTO);
        }
        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name) 
        {
            var stud = _sdb.Students.FirstOrDefault(x => x.Name == name);
            if (stud == null)
                return NotFound();
            //return Ok(stud);
            var studentDTO = stud.Adapt<StudentDTO>();
            return Ok(studentDTO);
        }
        [HttpPost]
        public IActionResult Add(Student st)
        {
            if (st.Name == null || st.Age == null || st.Address == null || st.Grade == null)
                return BadRequest();
            _sdb.Students.Add(st);
            _sdb.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = st.Id }, new { message = "Added Successfully" });
        }
        [HttpPost("v2")]
        [ValidateAddressAttribute]
        public IActionResult AddV2([FromBody] Student st)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = _sdb.Departments.FirstOrDefault(d => d.Id == st.DeptId);
            if (department == null)
                return BadRequest(new { message = "Invalid Department ID" });

            _sdb.Students.Add(st);
            _sdb.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = st.Id }, new { message = "Added Successfully" });
        }

        [HttpPut]
        public IActionResult Update(Student st)
        {
            if (st.Name == null)
                return BadRequest();
            _sdb.Students.Update(st);
            _sdb.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var stud = _sdb.Students.Include(s => s.Department).FirstOrDefault(x => x.Id == id);
            if (stud == null)
                return NotFound();
            _sdb.Students.Remove(stud);
            _sdb.SaveChanges();
            return Ok(stud);
        }
        [HttpGet("throw")]
        public IActionResult ThrowException()
        {
            throw new Exception("This is a test exception!");
        }
    }
}
