using DepartmentAPI.Repo;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Context;
using StudentAPI.DTOs;
using StudentAPI.Migrations;
using StudentAPI.Models;
using StudentAPI.Repo;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController: ControllerBase
    {
        IDepartmentRepo departmentRepo;
        public DepartmentController(IDepartmentRepo Idr)
        {
            departmentRepo = Idr;  
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //var departments = _db.Departments.Include(e => e.students).ToList();
            var departments = departmentRepo.getAll();
            if (departments == null || !departments.Any())
                return NotFound();

            var departmentDTOs = departments.Adapt<List<DepartmentDTO>>();

            foreach (var dept in departmentDTOs)
            {
                dept.Status = dept.StudentCount > 15 ? "overload" : "success";
            }

            return Ok(departmentDTOs);
        }


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            //var department = _db.Departments.Include(e=>e.students).FirstOrDefault(d => d.Id == id);
            var department = departmentRepo.getById(id);
            if (department == null)
                return NotFound();

            var departmentDTOs = department.Adapt<DepartmentDTO>();
            departmentDTOs.Status = departmentDTOs.StudentCount > 15 ? "overload" : "success";

            return Ok(departmentDTOs);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            //var department = _db.Departments.Include(e => e.students).FirstOrDefault(d => d.Name == name);
            var department = departmentRepo.getByName(name);
            if (department == null)
                return NotFound();

            var departmentDTOs = department.Adapt<DepartmentDTO>();
            departmentDTOs.Status = departmentDTOs.StudentCount > 15 ? "overload" : "success";

            return Ok(departmentDTOs);
        }

        [HttpPost]
        public IActionResult Add(Department department)
        {
            if (department.Name == null || department.Location == null || department.MgrName == null)
                return BadRequest();
            //_db.Departments.Add(department);
            //_db.SaveChanges();
            departmentRepo.add(department);
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, new { message = "Added Successfully" });
        }

        [HttpPut]
        public IActionResult Update(Department department)
        {
            if (department.Name == null || department.Location == null || department.MgrName == null)
                return BadRequest();
            //_db.Departments.Update(department);
            //_db.SaveChanges();
            departmentRepo.update(department);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            //var department = _db.Departments.FirstOrDefault(d => d.Id == id);
            var department = departmentRepo.getById(id);
            if (department == null)
                return NotFound();
            //_db.Departments.Remove(department);
            //_db.SaveChanges();
            departmentRepo.delete(department);
            return Ok(department);
        }
    }
}