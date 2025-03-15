using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;

namespace StudentAPI.Context
{
    public class StudentContext: DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
