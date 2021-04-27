using System.Collections.Generic;

namespace CodeCoolAPI.DAL.Models
{
    public class Author
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Counter { get; set; }
        public IEnumerable<Material> Materials { get; set; }
    }
}