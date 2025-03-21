using Mapster;
using StudentAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace Mobile45API.DTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password not match")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }

    }
}
