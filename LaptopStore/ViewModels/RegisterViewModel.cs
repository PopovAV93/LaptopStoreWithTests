using System.ComponentModel.DataAnnotations;

namespace LaptopStore.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter email")]
        [MaxLength(30)]
        [EmailAddress(ErrorMessage = "Email length must not exceed 30 characters.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Enter login")]
        [MaxLength(30, ErrorMessage = "Login length must not exceed 30 characters.")]
        public string loginName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter your password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter your password")]
        [Compare("password", ErrorMessage = "Passwords doesn't match")]
        public string passwordConfirm { get; set; }
        public string errorMessage { get; set; }
    }
}