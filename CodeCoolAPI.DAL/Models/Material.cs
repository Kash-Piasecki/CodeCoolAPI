using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CodeCoolAPI.DAL.Models
{
    public class Material : BaseEntity
    {
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime PublishTime { get; set; }
        public MaterialType MaterialType { get; set; }
        public int MaterialTypeId { get; set; }
        public IEnumerable<Review> Reviews { get; set; }

        // public IdentityUser IdentityUser { get; set; }
        // public int IdentityUserId { get; set; }
        
    }
}