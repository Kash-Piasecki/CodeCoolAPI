using System.ComponentModel.DataAnnotations;

namespace CodeCoolAPI.Dtos
{
    public class ReviewUpsertDto
    {
        [Required, MaxLength(120)]
        public string TextBased { get; set; }
        [Required, Range(1,10)]
        public int DigitBased { get; set; }
        [Required]
        public int MaterialId { get; set; }
    }
}