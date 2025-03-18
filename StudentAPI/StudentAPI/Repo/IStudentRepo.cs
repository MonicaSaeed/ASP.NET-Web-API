using StudentAPI.Models;

namespace StudentAPI.Repo
{
    public interface IStudentRepo
    {
        public ICollection<Student> getAll();
        public Student getById(int id);
        public Student getByName(string name);
        public void add(Student student);

        public void update(Student student);

        public void delete(Student student);

    }
}
