using System.ComponentModel.DataAnnotations;

namespace CodeCoolAPI.Dtos.User
{
    public class RegisterUserDto
    {
        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Not a valid email address")]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$",
            ErrorMessage = "Password must contain at least 6 characters, one upper case, one lower one digit")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}