using System.Collections.Generic;

namespace CodeCoolAPI.DAL.Models
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}