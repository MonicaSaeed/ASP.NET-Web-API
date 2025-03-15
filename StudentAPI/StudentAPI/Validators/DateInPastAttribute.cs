using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Validators
{
    public class DateInPastAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime? date = (DateTime)value;
            
            if (date == null) return new ValidationResult("Date id required");
            
            if (date > DateTime.Now) return new ValidationResult("Date must be in past");
            
            var today = DateTime.Today;
            var age = today.Year - date.Value.Year;
            if (age < 18 || age > 22) return new ValidationResult("Age must be between 18-22");
            
            return ValidationResult.Success;
        }
    }
}
