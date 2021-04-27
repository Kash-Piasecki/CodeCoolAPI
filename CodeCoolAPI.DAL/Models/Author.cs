using System.Collections.Generic;

namespace CodeCoolAPI.DAL.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Counter { get; set; }
        public IEnumerable<Material> Materials { get; set; }
    }
}