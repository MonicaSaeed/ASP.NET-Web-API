using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Context;
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
            var studs = _sdb.Students.ToList();
            if (studs == null) 
                return NotFound();

            return Ok(studs);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var stud = _sdb.Students.FirstOrDefault(x => x.Id == id);
            if (stud == null)
                return NotFound();
            return Ok(stud);
        }
        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name) 
        {
            var stud = _sdb.Students.FirstOrDefault(x => x.Name == name);
            if (stud == null)
                return NotFound();
            return Ok(stud);
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
        public IActionResult AddV2(Student st)
        {
            if (st.Name == null || st.Age == null || st.Address == null || st.Grade == null)
                return BadRequest();
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
            var stud = _sdb.Students.FirstOrDefault(x => x.Id == id);
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
