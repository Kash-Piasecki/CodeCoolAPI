using System.ComponentModel.DataAnnotations;

namespace CodeCoolAPI.Dtos
{
    public class IdentityLoginDto
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}