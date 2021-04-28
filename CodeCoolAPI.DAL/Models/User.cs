using System.Collections.Generic;

namespace CodeCoolAPI.DAL.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole UserRole { get; set; }
        public int UserRoleId { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}