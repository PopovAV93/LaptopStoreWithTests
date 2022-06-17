using System.ComponentModel.DataAnnotations;

namespace LaptopStore.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter email")]
        [StringLength(30)]
        [EmailAddress(ErrorMessage = "Email length must not exceed 30 characters.")]
        public string email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter your password")]
        [MinLength(6, ErrorMessage = "Password must be longer than 6 characters")]
        public string password { get; set; }
        public string errorMessage { get; set; }
    }
}