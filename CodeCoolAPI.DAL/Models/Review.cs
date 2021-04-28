using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CodeCoolAPI.DAL.Models
{
    public class Review : BaseEntity
    {
        public string TextBased { get; set; }
        public int DigitBased { get; set; }
        public Material Material { get; set; }
        public int MaterialId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        
    }
}