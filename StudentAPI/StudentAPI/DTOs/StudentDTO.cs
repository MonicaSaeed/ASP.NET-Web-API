using Mapster;
using StudentAPI.Models;

namespace StudentAPI.DTOs
{
    public class StudentDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string DeptName { get; set; }
    }

    public class StudentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Student, StudentDTO>()
                  .Map(dest => dest.DeptName, src => src.Department != null ? src.Department.Name : "No Department");
        }
    }
}
