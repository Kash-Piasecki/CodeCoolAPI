using System;
using System.ComponentModel.DataAnnotations;

namespace CodeCoolAPI.Dtos
{
    public class MaterialUpsertDto
    {
        [Required]
        public int AuthorId { get; set; }
        [Required, MaxLength(60)]
        public string Description { get; set; }
        [MaxLength(60)]
        public string Location { get; set; }
        [Required]
        public DateTime PublishTime { get; set; }
        [Required]
        public int MaterialTypeId { get; set; }
    }
}