using Mapster;
using StudentAPI.Models;

namespace StudentAPI.DTOs
{
    public class DepartmentDTO
    {
        public string Name { get; set; }
        public int StudentCount { get; set; }
        public List<string> StudentNames { get; set; }
        public string Status { get; set; }


        public class StudentMappingConfig : IRegister
        {
            public void Register(TypeAdapterConfig config)
            {
                config.NewConfig<Department, DepartmentDTO>()
                    .Map(dest => dest.StudentCount, src => src.students.Count)
                    .Map(dest => dest.StudentNames, src => src.students.Select(s => s.Name).ToList());
            }
        }
    }
}
