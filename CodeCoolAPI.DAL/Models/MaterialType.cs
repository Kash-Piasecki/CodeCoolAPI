using System.Collections.Generic;

namespace CodeCoolAPI.DAL.Models
{
    public class MaterialType : BaseEntity
    {
        public string Name { get; set; }
        public string Definition { get; set; }
        public IEnumerable<Material> Materials { get; set; }
    }
}