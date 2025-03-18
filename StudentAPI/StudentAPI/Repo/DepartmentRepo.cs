using DepartmentAPI.Repo;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Context;
using StudentAPI.Models;

namespace StudentAPI.Repo
{
    public class DepartmentRepo: IDepartmentRepo
    {
        private readonly StudentContext _db;

        public DepartmentRepo(StudentContext db)
        {
            _db = db;
        }

        public ICollection<Department> getAll()
        {
            return _db.Departments.Include(e => e.students).ToList();
        }
        public Department getById(int id)
        {
            return _db.Departments.Include(e => e.students).FirstOrDefault(d => d.Id == id);
        }
        public Department getByName(string name)
        {
            return _db.Departments.Include(e => e.students).FirstOrDefault(d => d.Name == name);
        }
        public void add(Department department)
        {
            _db.Departments.Add(department);
            _db.SaveChanges();
        }
        public void update(Department department)
        {
            _db.Departments.Update(department);
            _db.SaveChanges();
        }
        public void delete(Department department)
        {
            _db.Departments.Remove(department);
            _db.SaveChanges();
        }

    }
}
