using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Models
{
    public class Department
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [MinLength(3, ErrorMessage = "Manager Name must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "Manager Name cannot exceed 50 characters.")]
        public string MgrName { get; set; }
    }
}
