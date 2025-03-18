using StudentAPI.Models;

namespace DepartmentAPI.Repo
{
    public interface IDepartmentRepo
    {
        public ICollection<Department> getAll();
        public Department getById(int id);
        public Department getByName(string name);
        public void add(Department Department);

        public void update(Department Department);

        public void delete(Department Department);
    }
}
