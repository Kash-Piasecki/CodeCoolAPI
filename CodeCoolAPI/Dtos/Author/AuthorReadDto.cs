using System.Collections.Generic;

namespace CodeCoolAPI.Dtos
{
    public class AuthorReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Counter { get; set; }
        // public IEnumerable<MaterialReadDto> Materials { get; set; }
    }
}