using System.ComponentModel.DataAnnotations;

namespace CodeCoolAPI.Dtos.User
{
    public class LoginUserDto
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}