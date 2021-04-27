using System.ComponentModel.DataAnnotations;

namespace CodeCoolAPI.Dtos
{
    public class AuthorUpsertDto
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(120)]
        public string Description { get; set; }
    }
}