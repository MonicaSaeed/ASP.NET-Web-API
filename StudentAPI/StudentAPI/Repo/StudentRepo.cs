using Microsoft.EntityFrameworkCore;
using StudentAPI.Context;
using StudentAPI.Models;

namespace StudentAPI.Repo
{
    public class StudentRepo: IStudentRepo
    {
        private readonly StudentContext _sdb;

        public StudentRepo(StudentContext sdb)
        {
            _sdb = sdb;
        }
        public ICollection<Student> getAll()
        {
            return _sdb.Students.Include(e => e.Department).ToList();
        }
        public Student getById(int id)
        {
            return _sdb.Students.Include(e => e.Department).FirstOrDefault(x => x.Id == id);
        }
        public Student getByName(string name)
        {
            return _sdb.Students.Include(e => e.Department).FirstOrDefault(x => x.Name == name);
        }
        public void add(Student student)
        {
            _sdb.Students.Add(student);
            _sdb.SaveChanges();
        }
        public void update(Student student)
        {
            _sdb.Students.Update(student);
            _sdb.SaveChanges();
        }
        public void delete(Student student)
        {
            _sdb.Students.Remove(student);
            _sdb.SaveChanges();
        }
    }
}
