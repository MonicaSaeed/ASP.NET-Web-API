using StudentAPI.Validators;
using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage ="min length is 3")]
        [MaxLength(10, ErrorMessage ="max length is 10")]
        public string Name { get; set; }
        [RegularExpression("[^\\s]+(.*?)\\.(jpg|png)$", ErrorMessage = "img extinction must be jpg or png only")]
        public string? Img { get; set; }
        public string Address { get; set; }
        [Range(18,22)]
        public int Age { get; set; }
        [DateInPastAttribute]
        public DateTime? BirthDate { get; set; }
        public string? PhoneNum { get; set; }
        public float Grade { get; set; }

    }
}
